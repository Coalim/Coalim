using Coalim.Database.Accessor.Exceptions;
using Coalim.Database.Schema;
using Coalim.Database.Schema.Data.Chat;
using Coalim.Database.Schema.Data.User;

namespace Coalim.Database.Accessor;

public class CoalimDatabaseContext : IDisposable
{
    private readonly CoalimDatabaseSchemaContext _context;
    
    public CoalimDatabaseContext(CoalimDatabaseSchemaContext context)
    {
        this._context = context;
    }

    public void SaveChanges()
    {
        this._context.SaveChanges();
    }

    public CoalimUser CreateUser(string username)
    {
        CoalimUser user = new CoalimUser
        {
            Username = username,
        };

        this._context.Users.Add(user);
        return user;
    }
    
    public CoalimUser? GetUserByGuid(Guid guid)
        => this._context.Users.FirstOrDefault(u => u.UserId == guid);

    public CoalimServer CreateServer(CoalimUser creator, string name)
    {
        CoalimServer server = new CoalimServer
        {
            Name = name,
            Creator = creator,
            Channels = [
                new CoalimChannel
                {
                    Name = "general",
                },
            ],
        };
        
        this._context.Servers.Add(server);
        return server;
    }

    public CoalimServer? GetServerByGuid(Guid guid)
        => this._context.Servers.FirstOrDefault(s => s.ServerId == guid);

    public CoalimChannel CreateChannel(CoalimServer server, string name)
    {
        CoalimChannel channel = new CoalimChannel
        {
            Name = name,
            Server = server,
        };
        
        server.Channels.Add(channel);
        return channel;
    }

    public CoalimChannel? GetChannelByGuid(Guid guid)
        => this._context.Channels.FirstOrDefault(s => s.ChannelId == guid);

    public CoalimMessage CreateMessage(CoalimUser author, CoalimChannel channel, string content)
    {
        CoalimMessage message = new()
        {
            Author = author,
            Channel = channel,
            Content = content,
        };

        this._context.Messages.Add(message);
        return message;
    }
    
    public CoalimMessage? GetMessageByGuid(Guid guid)
        => this._context.Messages.FirstOrDefault(s => s.MessageId == guid);

    public CoalimToken? GetTokenByTokenData(string tokenData)
        => this._context.Tokens.FirstOrDefault(t => t.TokenData == tokenData);

    public void Dispose()
    {
        if (this._context.ChangeTracker.HasChanges())
        {
            throw new UnsavedChangesException();
        }
        
        this._context.Dispose();
        GC.SuppressFinalize(this);
    }
}