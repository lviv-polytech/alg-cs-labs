using Lpnu.Lb1.Core.Services;
using Lpnu.Lb1.Infrastructure.Services;

namespace Lpnu.Lb1.Infrastructure;

/// <summary>
/// Factory provider for obtaining instances of <see cref="IFactorialCalculator"/> without exposing the internal concrete implementation.
/// </summary>
public static class FactorialServiceFactory
{
    /// <summary>
    /// Creates and returns a new default instance of <see cref="IFactorialCalculator"/>.
    /// </summary>
    /// <returns>A concrete instance implementing <see cref="IFactorialCalculator"/>.</returns>
    public static IFactorialCalculator CreateCalculator() => new FactorialCalculator();
}
