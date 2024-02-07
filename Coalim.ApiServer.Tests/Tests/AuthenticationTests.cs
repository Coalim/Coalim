namespace Coalim.ApiServer.Tests.Tests;

public class AuthenticationTests
{
    [Test]
    public async Task CanAuthenticate()
    {
        using TestServerContext context = new();
        HttpResponseMessage request = await context.Http.GetAsync("/api/v1/login?username=user");
        Assert.That(request.StatusCode, Is.EqualTo(OK));
    }
}