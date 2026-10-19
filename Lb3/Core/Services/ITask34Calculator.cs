using Lpnu.Lb3.Core.Models;

namespace Lpnu.Lb3.Core.Services;

/// <summary>
/// Service contract for evaluating Task 3.4: determining whether a point (x, y)
/// falls within the shaded target region for radius parameter R.
/// </summary>
public interface ITask34Calculator
{
    /// <summary>
    /// Evaluates whether point (x, y) falls within the shaded area defined by radius parameter R.
    /// The shaded area is composed of two point-symmetric circular sectors:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// Region 1 (Quadrant II): Bounded by the vertical line segment x = -R (for 0 &lt;= y &lt;= R),
    /// the horizontal axis segment y = 0 (for -R &lt;= x &lt;= 0), and the circular arc
    /// of radius R centered at (-R, 0) given by (x + R)^2 + y^2 &lt;= R^2.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Region 2 (Quadrant IV): Bounded by the horizontal axis segment y = 0 (for 0 &lt;= x &lt;= R),
    /// the vertical line segment x = R (for -R &lt;= y &lt;= 0), and the circular arc
    /// of radius R centered at (R, 0) given by (x - R)^2 + y^2 &lt;= R^2.
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <param name="y">The y-coordinate of the point.</param>
    /// <param name="r">The radius/scale parameter R (must be strictly positive and finite).</param>
    /// <returns>A <see cref="Task34Result"/> containing the hit status, identified zone, and descriptive textual message.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="r"/> &lt;= 0 or any parameter is non-finite (NaN or Infinity).
    /// </exception>
    Task34Result Calculate(double x, double y, double r);

    /// <summary>
    /// Determines whether point (x, y) falls within or on the boundary of Region 1 (Quadrant II).
    /// </summary>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <param name="y">The y-coordinate of the point.</param>
    /// <param name="r">The radius parameter R.</param>
    /// <returns><c>true</c> if the point is inside or on the boundary of Region 1; otherwise, <c>false</c>.</returns>
    bool IsInRegion1(double x, double y, double r);

    /// <summary>
    /// Determines whether point (x, y) falls within or on the boundary of Region 2 (Quadrant IV).
    /// </summary>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <param name="y">The y-coordinate of the point.</param>
    /// <param name="r">The radius parameter R.</param>
    /// <returns><c>true</c> if the point is inside or on the boundary of Region 2; otherwise, <c>false</c>.</returns>
    bool IsInRegion2(double x, double y, double r);
}
