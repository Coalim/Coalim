using Coalim.Database.Schema.Data.User;

namespace Coalim.Api.Serialization.Data.User;

public class CoalimApiUser : IMappableObject<CoalimApiUser, CoalimUser>
{
    public required Guid UserId { get; init; }
    public required string Username { get; init; }
    
    public static CoalimApiUser Map(CoalimUser source)
    {
        return new CoalimApiUser
        {
            Username = source.Username,
            UserId = source.UserId,
        };
    }
}