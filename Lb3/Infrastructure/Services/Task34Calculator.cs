using System.Globalization;
using Lpnu.Lb3.Core.Models;
using Lpnu.Lb3.Core.Services;

namespace Lpnu.Lb3.Infrastructure.Services;

/// <summary>
/// Internal service evaluating Task 3.4: determining whether a point with coordinates (x, y)
/// falls within the shaded gray geometric target region on the Cartesian plane for radius parameter R.
/// </summary>
/// <remarks>
/// <para>
/// <b>Mathematical Problem Statement:</b>
/// Given user-entered coordinates (x, y) and parameter R &gt; 0, check whether (x, y) belongs to the
/// shaded target region S = S_1 U S_2.
/// </para>
/// <para>
/// <b>Geometric Analysis:</b>
/// <list type="bullet">
/// <item>
/// <description>
/// <b>Region 1 (S_1, Quadrant II):</b> Upper-left circular sector.
/// Bounded on the left by the vertical line segment x = -R (for 0 &lt;= y &lt;= R),
/// on the bottom by the X-axis segment y = 0 (for -R &lt;= x &lt;= 0), and on the right/top by the
/// circular arc centered at (-R, 0) with radius R, defined by (x + R)^2 + y^2 &lt;= R^2.
/// Transition points: (-R, 0) [sector center on X-axis], (-R, R) [top boundary], (0, 0) [origin contact].
/// Compound predicate:
/// <code>-R &lt;= x &amp;&amp; x &lt;= 0 &amp;&amp; y &gt;= 0 &amp;&amp; (x + R)^2 + y^2 &lt;= R^2</code>
/// </description>
/// </item>
/// <item>
/// <description>
/// <b>Region 2 (S_2, Quadrant IV):</b> Lower-right circular sector, point-symmetric to S_1 about (0, 0).
/// Bounded on the top by the X-axis segment y = 0 (for 0 &lt;= x &lt;= R),
/// on the right by the vertical line segment x = R (for -R &lt;= y &lt;= 0), and on the left/bottom by the
/// circular arc centered at (R, 0) with radius R, defined by (x - R)^2 + y^2 &lt;= R^2.
/// Transition points: (R, 0) [sector center on X-axis], (R, -R) [bottom boundary], (0, 0) [origin contact].
/// Compound predicate:
/// <code>0 &lt;= x &amp;&amp; x &lt;= R &amp;&amp; y &lt;= 0 &amp;&amp; (x - R)^2 + y^2 &lt;= R^2</code>
/// </description>
/// </item>
/// </list>
/// </para>
/// <para>
/// Boundary points are included in the target region as per standard laboratory specification.
/// </para>
/// </remarks>
internal class Task34Calculator : ITask34Calculator
{
    private const double Epsilon = 1e-12;

    /// <inheritdoc />
    public Task34Result Calculate(double x, double y, double r)
    {
        if (!double.IsFinite(x))
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Argument x must be a finite real number.");
        }

        if (!double.IsFinite(y))
        {
            throw new ArgumentOutOfRangeException(nameof(y), "Argument y must be a finite real number.");
        }

        if (!double.IsFinite(r) || r <= 0.0)
        {
            throw new ArgumentOutOfRangeException(nameof(r), "Radius parameter R must be a strictly positive finite number (R > 0).");
        }

        bool inRegion1 = IsInRegion1(x, y, r);
        bool inRegion2 = IsInRegion2(x, y, r);
        bool isHit = inRegion1 || inRegion2;

        HitZone zone;
        string message;

        if (Math.Abs(x) < Epsilon && Math.Abs(y) < Epsilon)
        {
            zone = HitZone.Origin;
            message = string.Format(
                CultureInfo.InvariantCulture,
                "Point ({0:G6}, {1:G6}) hits the shaded region at the coordinate origin (0, 0) on the boundary.",
                x, y);
        }
        else if (inRegion1)
        {
            zone = HitZone.Region1UpperLeft;
            message = string.Format(
                CultureInfo.InvariantCulture,
                "Point ({0:G6}, {1:G6}) hits the shaded region in Quadrant II (upper-left arc sector, R = {2:G6}).",
                x, y, r);
        }
        else if (inRegion2)
        {
            zone = HitZone.Region2LowerRight;
            message = string.Format(
                CultureInfo.InvariantCulture,
                "Point ({0:G6}, {1:G6}) hits the shaded region in Quadrant IV (lower-right arc sector, R = {2:G6}).",
                x, y, r);
        }
        else
        {
            zone = HitZone.Outside;
            message = string.Format(
                CultureInfo.InvariantCulture,
                "Point ({0:G6}, {1:G6}) does NOT fall into the shaded region for R = {2:G6}.",
                x, y, r);
        }

        return new Task34Result(x, y, r, isHit, zone, message);
    }

    /// <inheritdoc />
    public bool IsInRegion1(double x, double y, double r)
    {
        // Region 1: -R <= x <= 0 AND y >= 0 AND (x + R)^2 + y^2 <= R^2
        if (x < -r - Epsilon || x > Epsilon)
        {
            return false;
        }

        if (y < -Epsilon)
        {
            return false;
        }

        double dx = x + r;
        double distanceSquared = dx * dx + y * y;
        double radiusSquared = r * r;

        return distanceSquared <= radiusSquared + Epsilon;
    }

    /// <inheritdoc />
    public bool IsInRegion2(double x, double y, double r)
    {
        // Region 2: 0 <= x <= R AND y <= 0 AND (x - R)^2 + y^2 <= R^2
        if (x < -Epsilon || x > r + Epsilon)
        {
            return false;
        }

        if (y > Epsilon)
        {
            return false;
        }

        double dx = x - r;
        double distanceSquared = dx * dx + y * y;
        double radiusSquared = r * r;

        return distanceSquared <= radiusSquared + Epsilon;
    }
}
