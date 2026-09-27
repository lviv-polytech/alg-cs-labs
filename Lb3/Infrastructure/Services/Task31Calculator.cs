using Lpnu.Lb3.Core.Models;
using Lpnu.Lb3.Core.Services;

namespace Lpnu.Lb3.Infrastructure.Services;

/// <summary>
/// Internal service evaluating Task 3.1: piecewise function of a single variable.
/// </summary>
internal class Task31Calculator : ITask31Calculator
{
    /// <summary>
    /// Computes the function value Y = m^2 + 1 + Phi(m) based on interval conditions for m.
    /// </summary>
    /// <remarks>
    /// Mathematical Context and Formulas:
    /// In Lab 3 Task 3.1 (Variant 17), the overall function is given by:
    /// <code>
    /// Y = m^2 + 1 + \Phi(m)
    /// </code>
    /// where the auxiliary function \Phi(m) is piecewise-defined across three real intervals:
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       <b>Interval 1 (m &lt; 4):</b> Polynomial expression:
    ///       <code>\Phi(m) = 4m^7 - m^5 + m^3 - 2</code>
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <b>Interval 2 (4 &lt;= m &lt; 7):</b> Trigonometric and linear expression:
    ///       <code>\Phi(m) = \arctan\left(\frac{|m| + 1}{2}\right) + 8.3m</code>
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <b>Interval 3 (m &gt;= 7):</b> Logarithmic-exponential expression:
    ///       <code>\Phi(m) = \ln\left(|2m + e^{4m + 1}|\right)</code>
    ///     </description>
    ///   </item>
    /// </list>
    /// Constraints:
    /// <list type="bullet">
    ///   <item><description>Both <c>m</c> and <c>n</c> must be finite real numbers.</description></item>
    ///   <item><description>For very large values of <c>m</c> (m &gt; 177), direct calculation of <c>e^{4m+1}</c> would exceed IEEE 754 double range, handled via asymptotic expansion <c>4m + 1 + \ln(1 + 2m e^{-(4m+1)})</c>.</description></item>
    /// </list>
    /// </remarks>
    /// <param name="m">The primary real argument.</param>
    /// <param name="n">The secondary real argument.</param>
    /// <returns>A <see cref="Task31Result"/> with evaluated values.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if m or n is non-finite.</exception>
    public Task31Result Calculate(double m, double n)
    {
        if (!double.IsFinite(m))
        {
            throw new ArgumentOutOfRangeException(nameof(m), "Argument m must be a finite real number.");
        }

        if (!double.IsFinite(n))
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Argument n must be a finite real number.");
        }

        double phi;

        if (m < 4.0)
        {
            var m3 = Math.Pow(m, 3);
            var m5 = m3 * m * m;
            var m7 = m5 * m * m;
            phi = 4.0 * m7 - m5 + m3 - 2.0;
        }
        else if (m < 7.0)
        {
            var numerator = Math.Abs(m) + 1.0;
            phi = Math.Atan(numerator / 2.0) + 8.3 * m;
        }
        else
        {
            var expArg = 4.0 * m + 1.0;
            if (expArg > 700.0)
            {
                // Asymptotic expansion: ln(2m + e^(4m+1)) = 4m + 1 + ln(1 + 2m * e^-(4m+1)) ~ 4m + 1
                phi = expArg;
            }
            else
            {
                var inner = 2.0 * m + Math.Exp(expArg);
                phi = Math.Log(Math.Abs(inner));
            }
        }

        var y = m * m + 1.0 + phi;
        return new Task31Result(m, n, phi, y);
    }
}
