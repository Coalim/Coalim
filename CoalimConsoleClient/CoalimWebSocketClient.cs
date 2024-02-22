using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace CoalimConsoleClient;

public class CoalimWebSocketClient : IDisposable
{
    private readonly ClientWebSocket _ws;
    private readonly ThreadLocal<byte[]> _receiveBuffer;

    private const ushort ReceiveBufferLength = 16384;

    public CoalimWebSocketClient(Uri uri)
    {
        this._ws = new ClientWebSocket();
        this._ws.ConnectAsync(uri, default).Wait();

        this._receiveBuffer = new ThreadLocal<byte[]>(() => new byte[ReceiveBufferLength], false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Task SendAsync(ArraySegment<byte> data, WebSocketMessageType type)
    {
        return this._ws.SendAsync(data, type, true, default);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task SendTextAsync(string text)
    {
        return this.SendAsync(Encoding.UTF8.GetBytes(text), WebSocketMessageType.Text);
    }

    public async Task<string> ReceiveTextAsync()
    {
        byte[] buffer = this._receiveBuffer.Value!;
        WebSocketReceiveResult result = await this._ws.ReceiveAsync(buffer, default);
        
        ArraySegment<byte> bufferSegment = new ArraySegment<byte>(buffer, 0, result.Count);
        
        return Encoding.UTF8.GetString(bufferSegment);
    }

    public void Dispose()
    {
        this._ws.Dispose();
        GC.SuppressFinalize(this);
    }
}