namespace Coalim.Api.Serialization.Data;

public class CoalimServerInformation
{
    public required string ServerName { get; init; }
    public required int ProtocolVersion { get; init; }
}