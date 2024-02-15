using Coalim.Database.Schema.Data.User;

namespace Coalim.Api.Server.Endpoints;

public class AuthenticationEndpoints : EndpointGroup
{
    [ApiEndpoint("/login"), Authentication(false)]
    public Response Login(RequestContext context, CoalimBunkumDatabaseContext database)
    {
        string? username = context.QueryString["username"];
        if (username == null)
            return BadRequest;
        
        CoalimUser user = database.GetUserByUsername(username) ?? database.CreateUser(username);
        CoalimToken token = database.CreateTokenForUser(user);
        database.SaveChanges();
        
        return new Response(token.TokenData);
    }
}