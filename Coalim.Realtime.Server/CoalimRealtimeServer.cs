using System.Diagnostics;
using Coalim.Database.Accessor;
using WebSocketSharp;
using WebSocketSharp.Server;
using Logger = NotEnoughLogs.Logger;

namespace Coalim.Realtime.Server;

public class CoalimRealtimeServer : WebSocketBehavior, IDisposable
{
    private readonly Logger _logger;
    private readonly CoalimDatabaseContext _database;
    
    private WebSocket Socket => this.Context.WebSocket;
    
    public CoalimRealtimeServer(Logger logger, CoalimDatabaseContext database)
    {
        this._logger = logger;
        this._database = database;
    }

    public void Dispose()
    {
        this._database.Dispose();
        GC.SuppressFinalize(this);
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        if (e.IsPing) return;

        if (e.IsText)
            this.OnTextMessage(e.Data);
        else if (e.IsBinary)
            this.OnBinaryMessage(e.RawData);
        else
            throw new UnreachableException();
    }

    private void OnTextMessage(string data)
    {
        this._logger.LogTrace("Client", "Received message: {0}", data);
        this.Send(data);
    }

    private void OnBinaryMessage(byte[] data)
    {
        // Close immediately if the client sends binary text as this does not comply with our server
        // https://www.rfc-editor.org/rfc/rfc6455.html#section-7.4.1
        this.Socket.Close(1003, "This server only accepts TEXT.");
    }
}