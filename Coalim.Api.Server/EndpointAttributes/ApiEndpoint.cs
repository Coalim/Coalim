namespace Coalim.Api.Server.EndpointAttributes;

public class ApiEndpoint : HttpEndpointAttribute
{
    public const string BaseUrl = "/api/v1";

    public ApiEndpoint(string route, HttpMethods method = HttpMethods.Get, string contentType = "application/json")
        : base(BaseUrl + route, method, contentType)
    {}

    public ApiEndpoint(string route, string contentType)
        : base(BaseUrl + route, contentType)
    {
    }

    public ApiEndpoint(string route, string contentType, HttpMethods method)
        : base(BaseUrl + route, contentType, method)
    {
    }
}