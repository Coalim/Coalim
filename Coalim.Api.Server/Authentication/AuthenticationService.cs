using System.Reflection;
using Bunkum.Core.Database;
using Bunkum.Core.Services;
using Bunkum.Listener.Protocol;
using Bunkum.Listener.Request;
using Coalim.Database.Accessor;
using Coalim.Database.Schema.Data.User;
using NotEnoughLogs;

namespace Coalim.Api.Server.Authentication;

/// <summary>
/// Thinned implementation of Bunkum's own authentication service.
/// </summary>
public class AuthenticationService : Service
{
    internal AuthenticationService(Logger logger) : base(logger)
    {}
    
    // Cache the token to avoid double lookups.
    // We don't use a list here as you can't have multiple tokens per request (and thus one token per thread)
    private readonly ThreadLocal<CoalimToken?> _tokenCache = new(() => null);
    
    public override Response? OnRequestHandled(ListenerContext context, MethodInfo method, Lazy<IDatabaseContext> database)
    {
        if (!(method.GetCustomAttribute<AuthenticationAttribute>()?.Required ?? true)) return null;
        
        CoalimToken? token = this.AuthenticateToken(context, database);
        this._tokenCache.Value = token;
            
        if (token == null)
            return new Response(Array.Empty<byte>(), ContentType.Plaintext, Forbidden);

        return null;
    }
    
    /// <inheritdoc />
    public override void AfterRequestHandled(ListenerContext context, Response response, MethodInfo method, Lazy<IDatabaseContext> database)
    {
        this._tokenCache.Value = null;
    }
    
    /// <inheritdoc />
    public override object? AddParameterToEndpoint(ListenerContext context, ParameterInfo parameter, Lazy<IDatabaseContext> database)
    {
        if(ParameterEqualTo<CoalimToken>(parameter))
            return this.AuthenticateToken(context, database);

        if (ParameterEqualTo<CoalimUser>(parameter))
            return this.AuthenticateToken(context, database)?.User;
        
        return null;
    }

    private CoalimToken? AuthenticateToken(ListenerContext context, Lazy<IDatabaseContext> databaseLazy, bool remove = false)
    {
        // Look for the user in the cache.
        // ReSharper disable once InvertIf
        if (this._tokenCache.Value != null)
        {
            if(remove) this._tokenCache.Value = null;
            return this._tokenCache.Value;
        }

        // string? header = context.RequestHeaders["Authorization"];
        string? header = context.Query["token"]; // TODO: don't do this
        if (header == null) return null;
        
        CoalimDatabaseContext database = (CoalimDatabaseContext)databaseLazy.Value;
        return database.GetTokenByTokenData(header);
    }
}