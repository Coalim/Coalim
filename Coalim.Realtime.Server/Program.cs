using Coalim.Api.Serialization;
using Coalim.Database.Accessor;
using NotEnoughLogs;
using NotEnoughLogs.Behaviour;
using WebSocketSharp.Server;

namespace Coalim.Realtime.Server;

public static class Program
{
    private static Logger _logger = null!;
    
    public static async Task Main()
    {
        _logger = new Logger(new LoggerConfiguration
        {
            Behaviour = new QueueLoggingBehaviour(),
#if DEBUG
            MaxLevel = LogLevel.Trace,
#else
            MaxLevel = LogLevel.Info,
#endif
        });

        const string endpoint = "/ws";
        const ushort port = 10060;
        
        WebSocketServer server = new WebSocketServer(port);
        server.AddWebSocketService(endpoint, InitializeServer);

        server.Log.Output = (data, s) =>
        {
            LogLevel level = data.Level switch
            {
                WebSocketSharp.LogLevel.Trace => LogLevel.Trace,
                WebSocketSharp.LogLevel.Debug => LogLevel.Debug,
                WebSocketSharp.LogLevel.Info => LogLevel.Info,
                WebSocketSharp.LogLevel.Warn => LogLevel.Warning,
                WebSocketSharp.LogLevel.Error => LogLevel.Error,
                WebSocketSharp.LogLevel.Fatal => LogLevel.Critical,
                _ => LogLevel.Info,
            };
            
            _logger.Log(level, CoalimLog.RealtimeServer, data.Message);
        };

        server.Log.Level = WebSocketSharp.LogLevel.Trace;
        
        server.Start();
        _logger.LogInfo(CoalimLog.RealtimeServer, "Now listening for realtime connections at '{0}' at port {1}", endpoint, port);
        
        await Task.Delay(-1);
    }

    private static CoalimRealtimeServer InitializeServer()
    {
        CoalimDatabaseContext databaseContext = new CoalimDatabaseContext(new PostgresSchemaContext());
        
        CoalimRealtimeServer server = new(_logger, databaseContext);
        return server;
    }
}