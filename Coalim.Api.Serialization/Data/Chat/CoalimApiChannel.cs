using Coalim.Database.Schema.Data.Chat;

namespace Coalim.Api.Serialization.Data.Chat;

public class CoalimApiChannel : IMappableObject<CoalimApiChannel, CoalimChannel>
{
    public required Guid ChannelId { get; init; }
    public required string Name { get; init; }
    public required Guid ServerId { get; init; }
    
    public static CoalimApiChannel Map(CoalimChannel source)
    {
        return new CoalimApiChannel
        {
            Name = source.Name,
            ChannelId = source.ChannelId,
            ServerId = source.ServerId,
        };
    }
}