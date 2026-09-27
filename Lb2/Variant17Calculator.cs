using Lpnu.Lb2.Core.Models;
using Lpnu.Lb2.Infrastructure;

namespace alg_cs_labs.Lb2;

/// <summary>
/// Legacy wrapper for calculation result record struct.
/// </summary>
/// <param name="Z1">Value of expression z1.</param>
/// <param name="Z2">Value of expression z2.</param>
public readonly record struct CalculationResult(double Z1, double Z2)
{
    /// <summary>
    /// Implicit conversion from the new Core model to this legacy model.
    /// </summary>
    /// <param name="core">Core calculation result.</param>
    public static implicit operator CalculationResult(Lpnu.Lb2.Core.Models.CalculationResult core) =>
        new(core.Z1, core.Z2);
}

/// <summary>
/// Legacy static calculator adapter delegating to the modular service implementation.
/// </summary>
public static class Variant17Calculator
{
    private static readonly Lpnu.Lb2.Core.Services.IVariant17Calculator Calculator = Variant17ServiceFactory.CreateCalculator();

    /// <summary>
    /// Evaluates expressions z1 and z2 for a given parameter m.
    /// </summary>
    /// <param name="m">Positive finite real number.</param>
    /// <returns>A calculation result containing z1 and z2.</returns>
    public static CalculationResult Calculate(double m)
    {
        var result = Calculator.Calculate(m);
        return new CalculationResult(result.Z1, result.Z2);
    }
}
