using System.Diagnostics;
using Coalim.Api.Serialization.Data;
using Coalim.Api.Serialization.Data.Chat;
using Coalim.Api.Serialization.Data.User;
using Coalim.Database.Accessor;
using Coalim.Database.Schema.Data.Chat;
using Coalim.Database.Schema.Data.User;
using Coalim.Realtime.Server.Transmission;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using WebSocketSharp;
using WebSocketSharp.Server;
using static Coalim.Realtime.Server.Transmission.RealtimeMessageOpcode;
using Logger = NotEnoughLogs.Logger;

namespace Coalim.Realtime.Server;

public class CoalimRealtimeServer : WebSocketBehavior, IDisposable
{
    private readonly Logger _logger;
    private readonly CoalimDatabaseContext _database;
    private CoalimUser? _user = null;
    
    private WebSocket Socket => this.Context.WebSocket;

    private const int ProtocolVersion = 1;
    
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

    protected override void OnOpen()
    {
        this.SendMessage(ServerInformation, new CoalimServerInformation
        {
            ServerName = "Coalim Realtime Server",
            ProtocolVersion = ProtocolVersion,
        });
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
        this._logger.LogDebug("Client", "Received message: {0}", data);

        try
        {
            RealtimeMessage message = JsonConvert.DeserializeObject<RealtimeMessage>(data);

            if (!Enum.IsDefined(message.Opcode))
            {
                this.Socket.Close(1002, $"Unknown opcode '{message.Opcode}'");
                return;
            }
            
            HandleMessage(message);
        }
        catch(Exception e)
        {
            this._logger.LogWarning("Client", e.ToString());
        }
    }

    private void OnBinaryMessage(byte[] data)
    {
        // Close immediately if the client sends binary text as this does not comply with our server
        // https://www.rfc-editor.org/rfc/rfc6455.html#section-7.4.1
        this.Socket.Close(1003, "This server only accepts TEXT.");
    }

    private void HandleMessage(RealtimeMessage message)
    {
        if (this._user == null)
        {
            HandleHandshakeMessage(message);
            return;
        }
        
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (message.Opcode)
        {
            case ClientIdentifyPacketOpcode:
                this.SendMessage(ServerPacketOpcode, ((RealtimeMessageOpcode)message.Data!).ToString());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleHandshakeMessage(RealtimeMessage message)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (message.Opcode)
        {
            case ClientLogin:
            {
                CoalimLoginRequest? request = ((JObject)message.Data!).ToObject<CoalimLoginRequest>();
                if (request == null) return;
                CoalimUser? user = this._database.GetUserByUsername(request.Username);
                if (user == null) return;

                CoalimToken token = this._database.CreateTokenForUser(user);

                this._user = user;
                this.SendMessage(ServerLoginResponse, CoalimApiTokenResponse.Map(token));
                this.SendCurrentState();
                break;
            }
            default:
                this.Socket.Close(1002, "Must authenticate to use this packet");
                break;
        }
    }

    private void SendCurrentState()
    {
        Debug.Assert(this._user != null);
        IEnumerable<CoalimServer> servers = this._database.GetServersContainingUser(this._user);
        foreach (CoalimServer server in servers)
        {
            this.SendMessage(ServerJoinServer, CoalimApiServer.Map(server));
        }
    }

    private void SendMessage(RealtimeMessageOpcode opcode, object? data = null)
    {
        RealtimeMessage message = new()
        {
            Opcode = opcode,
            Data = data,
        };

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            #if DEBUG
            Formatting = Formatting.Indented,
            #else
            Formatting = Formatting.None,
            #endif
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        string messageData = JsonConvert.SerializeObject(message, settings);
        
        this.Send(messageData);
    }
}