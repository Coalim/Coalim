using Coalim.Database.Accessor;
using Coalim.Database.Accessor.Exceptions;
using Coalim.Database.Schema.Data;

namespace Coalim.Database.Tests.Tests;

public class DatabaseTests
{
    [Test]
    public void CatchesUnsavedChanges()
    {
        // Arrange
        CoalimDatabaseContext db = TestHelper.CreateDb();

        // Act
        db.CreateUser("user");

        // Assert
        Assert.That(db.Dispose, Throws.Exception.TypeOf(typeof(UnsavedChangesException)));
    }
}