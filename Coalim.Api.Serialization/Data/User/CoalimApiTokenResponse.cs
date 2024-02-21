using Coalim.Database.Schema.Data.User;

namespace Coalim.Api.Serialization.Data.User;

public class CoalimApiTokenResponse : IMappableObject<CoalimApiTokenResponse, CoalimToken>
{
    public required string TokenData { get; set; }
    public required CoalimApiUser AssociatedUser { get; set; }
    
    public static CoalimApiTokenResponse Map(CoalimToken source)
    {
        return new CoalimApiTokenResponse
        {
            TokenData = source.TokenData,
            AssociatedUser = CoalimApiUser.Map(source.User),
        };
    }
}