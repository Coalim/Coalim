using Coalim.Api.Serialization.Data.User;
using Coalim.Database.Schema.Data.User;

namespace Coalim.Api.Serialization;

/// <summary>
/// Represents a destination class from an internal database object.
/// For example, the relationship between <see cref="CoalimUser"/> to <see cref="CoalimApiUser"/>.
/// </summary>
/// <typeparam name="TTarget">The target type to convert to.</typeparam>
/// <typeparam name="TSource">The database type to convert from.</typeparam>
public interface IMappableObject<out TTarget, in TSource> where TTarget : class where TSource : class
{
    /// <summary>
    /// Returns a mapped object from the database result.
    /// </summary>
    /// <param name="source">The original database object to map from.</param>
    /// <returns>The mapped object.</returns>
    public static abstract TTarget MapFrom(TSource source);
}