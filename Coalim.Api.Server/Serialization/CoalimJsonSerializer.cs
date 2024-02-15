using Bunkum.Listener.Protocol;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Coalim.Api.Server.Serialization;

public class CoalimJsonSerializer : IBunkumSerializer
{
    private static readonly JsonSerializer JsonSerializer = new();

    static CoalimJsonSerializer()
    {
        JsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
        
#if DEBUG
        JsonSerializer.Formatting = Formatting.Indented;
#endif
    }

    /// <inherit-doc/>
    public string[] ContentTypes { get; } = [ ContentType.Json ];
    
    /// <inherit-doc/>
    public byte[] Serialize(object data)
    {
        using MemoryStream stream = new();
        using StreamWriter sw = new(stream);
        using JsonWriter writer = new JsonTextWriter(sw);

        JsonSerializer.Serialize(writer, data);
        writer.Flush();
        return stream.ToArray();
    }
}