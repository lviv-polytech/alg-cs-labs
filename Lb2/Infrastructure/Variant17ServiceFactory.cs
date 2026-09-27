using Lpnu.Lb2.Core.Services;
using Lpnu.Lb2.Infrastructure.Services;

namespace Lpnu.Lb2.Infrastructure;

/// <summary>
/// Factory providing instances of <see cref="IVariant17Calculator"/> without exposing the internal concrete implementation.
/// </summary>
public static class Variant17ServiceFactory
{
    /// <summary>
    /// Creates and returns a new default instance of <see cref="IVariant17Calculator"/>.
    /// </summary>
    /// <returns>A concrete instance implementing <see cref="IVariant17Calculator"/>.</returns>
    public static IVariant17Calculator CreateCalculator() => new Variant17Calculator();
}
