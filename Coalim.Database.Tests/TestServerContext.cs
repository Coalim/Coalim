using Coalim.Database.Accessor;
using Coalim.Database.Schema.Data.Chat;
using Coalim.Database.Schema.Data.User;

namespace Coalim.Database.Tests;

#nullable disable

public class TestServerContext
{
    public TestServerContext(CoalimDatabaseContext context)
    {
        this.User1 = context.CreateUser("User1");
        this.User2 = context.CreateUser("User2");
        this.User3 = context.CreateUser("User3");
        
        this.Server = context.CreateServer(this.User1, "Test Server");
        this.Channel = this.Server.Channels[0];
    }
    
    public CoalimServer Server { get; }
    public CoalimChannel Channel { get; }
    
    public CoalimUser User1 { get; }
    public CoalimUser User2 { get; }
    public CoalimUser User3 { get; }
}