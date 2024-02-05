namespace Coalim.Database.Schema.Data;

#nullable disable

public class CoalimUser
{
    [Key]
    public Guid UserId { get; set; }
    
    [Required, DataType(DataType.Text)]
    public string Username { get; set; }
}