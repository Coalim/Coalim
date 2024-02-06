using System.Reflection;
using Coalim.Api.Server.Authentication;
using Coalim.Database.BunkumSupport;
using NotEnoughLogs;
using NotEnoughLogs.Behaviour;

namespace Coalim.Api.Server;

[ExcludeFromCodeCoverage]
public class Program
{
    public static async Task Main()
    {
        LoggerConfiguration logConfig = new LoggerConfiguration
        {
            Behaviour = new QueueLoggingBehaviour(),
            MaxLevel = LogLevel.Trace,
        };
        
        BunkumServer server = new BunkumHttpServer(logConfig);

        server.Initialize = s =>
        {
            CoalimDatabaseProvider<PostgresSchemaContext> databaseProvider = new();
            databaseProvider.GetContext().SaveChanges();
            
            s.DiscoverEndpointsFromAssembly(Assembly.GetExecutingAssembly());
            s.UseDatabaseProvider(databaseProvider);
            s.AddService<AuthenticationService>();
        };
        
        server.Start();
        await Task.Delay(-1);
    }
}