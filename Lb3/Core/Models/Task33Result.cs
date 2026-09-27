namespace Lpnu.Lb3.Core.Models;

/// <summary>
/// Encapsulates the results of evaluating the graph-based piecewise function in Task 3.3.
/// </summary>
/// <param name="X">Input argument x.</param>
/// <param name="R">Radius/scale parameter R.</param>
/// <param name="Y">Calculated function value y(x, R).</param>
/// <param name="IntervalDescription">Descriptive label identifying which geometric interval matched the input.</param>
public readonly record struct Task33Result(double X, double R, double Y, string IntervalDescription);
