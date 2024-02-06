using Coalim.Database.Accessor;
using Coalim.Database.Schema.Data.User;

namespace Coalim.Database.Tests.Tests;

public class UserTests
{
    [Test]
    public void CreatesUser()
    {
        // Arrange
        using CoalimDatabaseContext db = TestHelper.CreateDb();

        // Act
        CoalimUser user = db.CreateUser("user");
        db.SaveChanges();

        // Assert
        Assert.That(user.Username, Is.EqualTo("user"));
    }

    [Test]
    public void FindsExistingUser()
    {
        // Arrange
        using CoalimDatabaseContext db = TestHelper.CreateDb();

        // Act
        Guid guid = db.CreateUser("user").UserId;
        db.SaveChanges();

        CoalimUser? user = db.GetUserByGuid(guid);

        // Assert
        Assert.That(user, Is.Not.Null);
        Assert.That(user!.Username, Is.EqualTo("user"));
    }
    
    [Test]
    public void DoesntFindNonExistingUser()
    {
        // Arrange
        using CoalimDatabaseContext db = TestHelper.CreateDb();

        // Act
        db.CreateUser("user");
        db.SaveChanges();

        CoalimUser? user = db.GetUserByGuid(new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));

        // Assert
        Assert.That(user, Is.Null);
    }
}