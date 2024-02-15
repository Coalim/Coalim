using Coalim.Api.Serialization.Data.User;
using Coalim.Database.Schema.Data.User;

namespace Coalim.Api.Server.Endpoints;

public class UserEndpoints : EndpointGroup
{
    [ApiEndpoint("/users/me")]
    public CoalimApiUser GetOwnUser(RequestContext context, CoalimUser user)
    {
        return CoalimApiUser.Map(user);
    }
}