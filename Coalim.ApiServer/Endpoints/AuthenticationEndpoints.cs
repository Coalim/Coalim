using Bunkum.Core.Responses;

namespace Coalim.ApiServer.Endpoints;

public class AuthenticationEndpoints : EndpointGroup
{
    [ApiEndpoint("/login"), Authentication(false)]
    public Response Login(RequestContext context)
    {
        return OK;
    }
}