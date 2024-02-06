using Coalim.Database.Schema.Data.User;

namespace Coalim.Api.Serialization.Data.User;

public class CoalimApiUser : IMappableObject<CoalimApiUser, CoalimUser>
{
    public Guid UserId { get; init; }
    public string Username { get; init; }
    
    public static CoalimApiUser MapFrom(CoalimUser source)
    {
        return new CoalimApiUser
        {
            Username = source.Username,
            UserId = source.UserId,
        };
    }
}