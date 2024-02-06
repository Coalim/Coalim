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
        Assert.That(server.Channels, Has.Count.EqualTo(1));
        Assert.That(server.Channels[0].Name, Is.EqualTo("general"));
        Assert.That(server.Channels[0].Server, Is.EqualTo(server));
    }

    [Test]
    public void CreatesChannel()
    {
        // Arrange
        using CoalimDatabaseContext db = TestHelper.CreateDb();

        // Act
        CoalimUser user = db.CreateUser("user");
        CoalimServer server = db.CreateServer(user, "server");
        CoalimChannel channel = db.CreateChannel(server, "general2");
        db.SaveChanges();

        // Assert
        Assert.That(server.Channels, Has.Count.EqualTo(2));
        Assert.That(server.Channels[0].Name, Is.EqualTo("general"));
        Assert.That(server.Channels[0].Server, Is.EqualTo(server));
        
        Assert.That(server.Channels[1].ChannelId, Is.EqualTo(channel.ChannelId));
    }

    [Test]
    public void GetsChannelByGuid()
    {
        // Arrange
        using CoalimDatabaseContext db = TestHelper.CreateDb();
        
        // Act
        Guid guid = db.CreateServer(db.CreateUser("user"), "server").Channels[0].ChannelId;
        db.SaveChanges();

        CoalimChannel? channel = db.GetChannelByGuid(guid);

        // Assert
        Assert.That(channel, Is.Not.Null);
        Assert.That(channel!.Server.Name, Is.EqualTo("server"));
    }
}