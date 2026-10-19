namespace Lpnu.Lb4.Core;

/// <summary>Contracts for the target-area hit detection (Lab 4, Task 4.5).</summary>
public interface ITargetCalculator
{
    /// <summary>
    /// Determines whether a point (x, y) falls within the target region defined by parameter R > 0.
    /// The target consists of two quarter-circle regions:
    /// - Part A (upper-left): quarter circle centered at (-R, 0) with radius R, bounds: -R ≤ x ≤ 0, y ≥ 0.
    /// - Part B (lower-right): quarter circle centered at (R, 0) with radius R, bounds: 0 ≤ x ≤ R, y ≤ 0.
    /// </summary>
    bool InTarget(double x, double y, double radius);

    /// <summary>Generates random coordinates uniformly in [-R, R] × [-R, R].</summary>
    (double X, double Y) GenerateRandomShot(double radius);
}
