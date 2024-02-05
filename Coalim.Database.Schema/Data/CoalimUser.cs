namespace Coalim.Database.Schema.Data;

#nullable disable

public class CoalimUser
{
    [Key, ExcludeFromCodeCoverage]
    public Guid UserId { get; init; }
    
    [Required, DataType(DataType.Text), MaxLength(24)]
    public string Username { get; set; }
}