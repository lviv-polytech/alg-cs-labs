using Lpnu.Lb2.Core.Models;

namespace Lpnu.Lb2.Core.Services;

/// <summary>
/// Contract for the mathematical calculation service of Lab 2 (Variant 17).
/// </summary>
public interface IVariant17Calculator
{
    /// <summary>
    /// Evaluates the algebraic expressions <c>z1</c> and <c>z2</c> for a given parameter <paramref name="m"/>.
    /// </summary>
    /// <param name="m">A strictly positive, finite real number (<c>m &gt; 0</c>).</param>
    /// <returns>A <see cref="CalculationResult"/> containing evaluated values for <c>z1</c> and <c>z2</c>.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="m"/> is not a finite number or is less than or equal to 0.
    /// </exception>
    /// <exception cref="System.ArgumentException">
    /// Thrown when <paramref name="m"/> equals 2/3 (or causes division by zero in the denominator).
    /// </exception>
    CalculationResult Calculate(double m);
}
