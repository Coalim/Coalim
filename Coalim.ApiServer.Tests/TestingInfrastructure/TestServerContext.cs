using Bunkum.Protocols.Http;
using Bunkum.Protocols.Http.Direct;
using Coalim.Api.Server;
using Coalim.Database.Accessor;
using Coalim.Database.BunkumSupport;
using Coalim.Database.Schema.Tests;
using NotEnoughLogs;
using NotEnoughLogs.Behaviour;

namespace Coalim.ApiServer.Tests.TestingInfrastructure;

public class TestServerContext : IDisposable
{
    private readonly Logger _logger = new(new []
    {
        new NUnitSink(),
    });
    
    public readonly BunkumHttpServer Server;
    public readonly DirectHttpListener Listener;
    public readonly CoalimDatabaseContext Database;
    public readonly HttpClient Http;

    public TestServerContext()
    {
        this.Server = new BunkumHttpServer(new LoggerConfiguration
        {
            Behaviour = new DirectLoggingBehaviour(),
            MaxLevel = LogLevel.Trace,
        });

        this.Listener = new DirectHttpListener(_logger);
        this.Http = this.Listener.GetClient();

        TestDatabaseProvider databaseProvider = new TestDatabaseProvider();
        
        this.Server.UseListener(this.Listener);
        this.Server.UseDatabaseProvider(databaseProvider);
        this.Server.DiscoverEndpointsFromAssembly(typeof(Program).Assembly);
        
        this.Server.Start();
        this.Database = databaseProvider.GetContext();
    }

    public void Dispose()
    {
        this.Server.Stop();
        
        this.Listener.Dispose();
        this.Http.Dispose();
        
        this._logger.Dispose();
        
        GC.SuppressFinalize(this);
    }
}