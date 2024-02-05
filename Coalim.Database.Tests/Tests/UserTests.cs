using Coalim.Database.Accessor;
using Coalim.Database.Schema.Data;

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

        // Assert
        Assert.That(user.Username, Is.EqualTo("user"));
    }
}