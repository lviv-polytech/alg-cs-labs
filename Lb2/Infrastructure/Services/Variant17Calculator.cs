using CommonUtils.Math;
using Lpnu.Lb2.Core.Models;
using Lpnu.Lb2.Core.Services;

namespace Lpnu.Lb2.Infrastructure.Services;

/// <summary>
/// Internal concrete implementation of <see cref="IVariant17Calculator"/> evaluating Variant 17 mathematical formulas.
/// </summary>
internal class Variant17Calculator : IVariant17Calculator
{
    /// <summary>
    /// Computes the values of expressions <c>z1</c> and <c>z2</c> according to the Variant 17 specification.
    /// </summary>
    /// <remarks>
    /// Mathematical Context and Formulas:
    /// In Lab 2, Variant 17 requires evaluating two algebraic expressions for a given real parameter <c>m</c>:
    /// <code>
    /// z_1 = \frac{\sqrt{(3m + 2)^2 - 24m}}{3\sqrt{m} - \frac{2}{\sqrt{m}}}
    /// </code>
    /// <code>
    /// z_2 = \sqrt{m}
    /// </code>
    /// 
    /// Analytical Simplification:
    /// Let us analyze the radicand of the numerator:
    /// <code>
    /// (3m + 2)^2 - 24m = 9m^2 + 12m + 4 - 24m = 9m^2 - 12m + 4 = (3m - 2)^2
    /// </code>
    /// Therefore, the numerator simplifies to:
    /// <code>
    /// \sqrt{(3m - 2)^2} = |3m - 2|
    /// </code>
    /// 
    /// The denominator is:
    /// <code>
    /// 3\sqrt{m} - \frac{2}{\sqrt{m}} = \frac{3m - 2}{\sqrt{m}}
    /// </code>
    /// 
    /// Consequently:
    /// <code>
    /// z_1 = \frac{|3m - 2|}{\frac{3m - 2}{\sqrt{m}}} = \frac{|3m - 2|}{3m - 2} \cdot \sqrt{m} = \text{sgn}(3m - 2) \cdot \sqrt{m}
    /// </code>
    /// 
    /// Assignment Constraints &amp; Singularity Analysis:
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       <b>Domain:</b> For real results, <c>\sqrt{m}</c> requires <c>m &gt; 0</c>. If <c>m &lt;= 0</c> or <c>m</c> is non-finite,
    ///       an <see cref="ArgumentOutOfRangeException"/> is thrown.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <b>Singularity at <c>m = 2/3</c>:</b> When <c>3m - 2 = 0 \implies m = 2/3</c>, the denominator vanishes (<c>0</c>).
    ///       The expression is undefined, so an <see cref="ArgumentException"/> is thrown.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <b>Branching behavior:</b>
    ///       When <c>m &gt; 2/3</c>, <c>3m - 2 &gt; 0 \implies z_1 = \sqrt{m} = z_2</c>.
    ///       When <c>0 &lt; m &lt; 2/3</c>, <c>3m - 2 &lt; 0 \implies z_1 = -\sqrt{m} = -z_2</c>.
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    /// <param name="m">The input parameter <c>m</c>. Must be a finite, strictly positive real number (<c>m &gt; 0</c>).</param>
    /// <returns>A <see cref="CalculationResult"/> containing evaluated <c>z1</c> and <c>z2</c>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="m"/> is not finite (<see cref="double.IsFinite"/> is <c>false</c>) or <c>m &lt;= 0</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="m"/> equals 2/3 (or causes division by zero within precision tolerances).
    /// </exception>
    public CalculationResult Calculate(double m)
    {
        if (!double.IsFinite(m) || m <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(m), "m must be a positive, finite number.");
        }

        var sqrtM = Math.Sqrt(m);
        var numerator = Math.Sqrt(Math.Pow(3 * m + 2, 2) - 24 * m);
        var denominator = 3 * sqrtM - 2 / sqrtM;

        if (MathUtils.IsZero(denominator) || MathUtils.IsZero(3 * m - 2))
        {
            throw new ArgumentException("The expression is undefined for m = 2/3.", nameof(m));
        }

        var z1 = numerator / denominator;
        var z2 = sqrtM;

        return new CalculationResult(z1, z2);
    }
}
