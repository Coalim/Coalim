namespace Coalim.Database.Schema.Data;

#nullable disable

public class CoalimServer
{
    [Key, ExcludeFromCodeCoverage]
    public Guid ServerId { get; init; }
    
    [Required, DataType(DataType.Text), MaxLength(Limits.NameLimit)]
    public string Name { get; set; }
    
    public Guid CreatorId { get; set; }
    public CoalimUser Creator { get; set; }
    
    public List<CoalimChannel> Channels { get; set; }
}