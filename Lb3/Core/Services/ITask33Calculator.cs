using Lpnu.Lb3.Core.Models;

namespace Lpnu.Lb3.Core.Services;

/// <summary>
/// Service contract for evaluating Task 3.3: graph-based piecewise function y(x, R).
/// </summary>
public interface ITask33Calculator
{
    /// <summary>
    /// Computes y(x, R) across the four geometric intervals defined by the function graph.
    /// </summary>
    /// <param name="x">The real argument x.</param>
    /// <param name="r">The radius/scale parameter R (must be strictly positive).</param>
    /// <returns>A <see cref="Task33Result"/> with calculated value and interval description.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="r"/> &lt;= 0 or arguments are non-finite.
    /// </exception>
    Task33Result Calculate(double x, double r);
}
