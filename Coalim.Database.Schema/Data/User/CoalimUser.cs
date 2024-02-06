namespace Coalim.Database.Schema.Data.User;

#nullable disable

public class CoalimUser
{
    [Key, ExcludeFromCodeCoverage]
    public Guid UserId { get; init; }
    
    [Required, DataType(DataType.Text), MaxLength(Limits.UsernameLimit)]
    public string Username { get; set; }
}