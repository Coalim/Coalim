namespace Coalim.Database.Schema.Data.User;

#nullable disable

public class CoalimToken
{
    [Key, ExcludeFromCodeCoverage]
    public Guid TokenId { get; init; }
    
    [Required, DataType(DataType.Password), MaxLength(Limits.TokenLength), MinLength(Limits.TokenLength)]
    public string TokenData { get; set; }
    
    public Guid UserId { get; set; }
    public CoalimUser User { get; set; }
}