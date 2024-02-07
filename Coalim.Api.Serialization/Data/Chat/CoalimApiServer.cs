using Coalim.Database.Schema.Data.Chat;

namespace Coalim.Api.Serialization.Data.Chat;

public class CoalimApiServer : IMappableObject<CoalimApiServer, CoalimServer>
{
    public required Guid ServerId { get; init; }
    public required string Name { get; set; }
    
    public required Guid CreatorId { get; set; }
    
    public required IEnumerable<CoalimApiChannel> Channels { get; set; }
    
    public static CoalimApiServer Map(CoalimServer source)
    {
        return new CoalimApiServer()
        {
            ServerId = source.ServerId,
            Name = source.Name,
            CreatorId = source.CreatorId,
            Channels = source.Channels.Select(CoalimApiChannel.Map),
        };
    }
}