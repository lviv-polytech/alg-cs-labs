namespace Lpnu.Lb3.Core.Models;

/// <summary>
/// Specifies the geometric classification of a point relative to the shaded target regions.
/// </summary>
public enum HitZone
{
    /// <summary>
    /// Point lies outside all target regions.
    /// </summary>
    Outside = 0,

    /// <summary>
    /// Point lies inside or on the boundary of Region 1 (Quadrant II, upper-left circular sector).
    /// Bounded by -R &lt;= x &lt;= 0, y &gt;= 0, and (x + R)^2 + y^2 &lt;= R^2.
    /// </summary>
    Region1UpperLeft = 1,

    /// <summary>
    /// Point lies inside or on the boundary of Region 2 (Quadrant IV, lower-right circular sector).
    /// Bounded by 0 &lt;= x &lt;= R, y &lt;= 0, and (x - R)^2 + y^2 &lt;= R^2.
    /// </summary>
    Region2LowerRight = 2,

    /// <summary>
    /// Point is precisely at the origin (0, 0), which is the intersection point on the boundary of both regions.
    /// </summary>
    Origin = 3
}
