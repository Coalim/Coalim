namespace Coalim.Database.Schema.Data;

#nullable disable

public class CoalimChannel
{
    [Key, ExcludeFromCodeCoverage]
    public Guid ChannelId { get; init; }
    
    [Required, DataType(DataType.Text), MaxLength(Limits.NameLimit)]
    public string Name { get; set; }
    
    public Guid ServerId { get; set; }
    public CoalimServer Server { get; set; }
}