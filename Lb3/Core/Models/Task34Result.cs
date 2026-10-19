namespace Lpnu.Lb3.Core.Models;

/// <summary>
/// Encapsulates the results of determining whether a point (x, y) falls within the shaded target region.
/// </summary>
/// <param name="X">The x-coordinate of the tested point.</param>
/// <param name="Y">The y-coordinate of the tested point.</param>
/// <param name="R">The radius/scale parameter R.</param>
/// <param name="IsHit">Indicates whether the point is inside or on the boundary of the shaded area.</param>
/// <param name="Zone">The identified zone of the point relative to the target area.</param>
/// <param name="Message">Descriptive textual message detailing whether and where the point landed.</param>
public readonly record struct Task34Result(
    double X,
    double Y,
    double R,
    bool IsHit,
    HitZone Zone,
    string Message);
