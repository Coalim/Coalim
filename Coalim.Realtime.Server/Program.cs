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
        
        WebSocketServer server = new WebSocketServer(10060);
        server.AddWebSocketService("/ws", InitializeServer);

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
            };
            
            _logger.Log(level, "WebSocketServer", data.Message);
        };

        server.Log.Level = WebSocketSharp.LogLevel.Trace;
        
        server.Start();
        await Task.Delay(-1);
    }

    private static CoalimRealtimeServer InitializeServer()
    {
        CoalimDatabaseContext databaseContext = new CoalimDatabaseContext(new PostgresSchemaContext());
        
        CoalimRealtimeServer server = new(_logger, databaseContext);
        return server;
    }
}