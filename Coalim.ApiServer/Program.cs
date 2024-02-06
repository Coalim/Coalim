

using System.Reflection;
using Coalim.ApiServer.Authentication;
using Coalim.Database.BunkumSupport;

namespace Coalim.ApiServer;

[ExcludeFromCodeCoverage]
public class Program
{
    public static async Task Main()
    {
        BunkumServer server = new BunkumHttpServer();

        server.Initialize = s =>
        {
            CoalimDatabaseProvider databaseProvider = new CoalimDatabaseProvider();
            databaseProvider.GetContext().SaveChanges();
            
            s.DiscoverEndpointsFromAssembly(Assembly.GetExecutingAssembly());
            s.UseDatabaseProvider(databaseProvider);
            s.AddService<AuthenticationService>();
        };
        
        server.Start();
        await Task.Delay(-1);
    }
}