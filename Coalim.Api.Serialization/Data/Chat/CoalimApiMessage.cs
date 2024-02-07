using Coalim.Database.Schema.Data.Chat;

namespace Coalim.Api.Serialization.Data.Chat;

public class CoalimApiMessage : IMappableObject<CoalimApiMessage, CoalimMessage>
{
    public required Guid MessageId { get; init; }
    public required string Content { get; set; }
    public required Guid AuthorId { get; set; }
    
    public static CoalimApiMessage Map(CoalimMessage source)
    {
        return new CoalimApiMessage
        {
            MessageId = source.MessageId,
            Content = source.Content,
            AuthorId = source.AuthorId,
        };
    }
}