namespace Coalim.Database.Schema.Data;

#nullable disable

public class CoalimMessage
{
    [Key, ExcludeFromCodeCoverage]
    public Guid MessageId { get; init; }
    
    [Required, DataType(DataType.Text), MaxLength(Limits.MessageLimit)]
    public string Content { get; set; }
    
    public Guid AuthorId { get; set; }
    public CoalimUser Author { get; set; }
    
    
    public Guid ChannelId { get; set; }
    public CoalimChannel Channel { get; set; }
}