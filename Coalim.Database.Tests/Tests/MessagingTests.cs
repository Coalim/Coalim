using Coalim.Database.Accessor;

namespace Coalim.Database.Tests.Tests;

public class MessagingTests
{
    [Test]
    public void SendsMessage()
    {
        using CoalimDatabaseContext db = TestHelper.CreateDb();
        TestDatabaseContext context = new TestDatabaseContext(db);

        db.CreateMessage(context.User1, context.Channel, "yo man");
        db.CreateMessage(context.User2, context.Channel, "whats good");
        db.SaveChanges();
    }
}