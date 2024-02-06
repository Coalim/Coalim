using Coalim.Database.Accessor;
using Coalim.Database.Schema.Data;

namespace Coalim.Database.Tests.Tests;

public class ServerTests
{
    [Test]
    public void CreatesServer()
    {
        // Arrange
        using CoalimDatabaseContext db = TestHelper.CreateDb();

        // Act
        CoalimUser user = db.CreateUser("user");
        Guid guid = db.CreateServer(user, "server").ServerId;
        db.SaveChanges();

        CoalimServer server = db.GetServerByGuid(guid)!;

        // Assert
        Assert.That(server.Channels, Is.Not.Empty);
        Assert.That(server.Channels[0].Name, Is.EqualTo("general"));
        Assert.That(server.Channels[0].Server, Is.EqualTo(server));
    }
}