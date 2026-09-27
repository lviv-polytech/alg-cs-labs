namespace Lpnu.Lb2.Core.Models;

/// <summary>
/// Encapsulates the results of calculating Variant 17 expressions z1 and z2.
/// </summary>
/// <param name="Z1">The computed value of the first expression z1.</param>
/// <param name="Z2">The computed value of the second expression z2.</param>
public readonly record struct CalculationResult(double Z1, double Z2);
