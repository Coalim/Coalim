using Newtonsoft.Json;

namespace Coalim.Realtime.Server.Transmission;

[JsonObject]
public readonly struct RealtimeMessage
{
    [JsonProperty("o")]
    public RealtimeMessageOpcode Opcode { get; init; }
    
    [JsonProperty("d", NullValueHandling = NullValueHandling.Ignore)]
    public dynamic? Data { get; init; }
}