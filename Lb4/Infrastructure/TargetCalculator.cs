namespace Lpnu.Lb4.Infrastructure;

using Core;

/// <summary>Implements target-area hit detection for Lab 4, Task 4.5.</summary>
public class TargetCalculator : ITargetCalculator
{
    private readonly Random _random;

    public TargetCalculator()
    {
        _random = new Random();
    }

    /// <summary>
    /// Determines whether a point (x, y) falls within the target region.
    /// Part A: -R ≤ x ≤ 0 ∧ y ≥ 0 ∧ (x+R)² + y² ≤ R²
    /// Part B: 0 ≤ x ≤ R ∧ y ≤ 0 ∧ (x-R)² + y² ≤ R²
    /// </summary>
    public bool InTarget(double x, double y, double radius)
    {
        // Part A: upper-left quarter circle centered at (-R, 0)
        bool partA = x >= -radius && x <= 0 
                     && y >= 0 
                     && Math.Pow(x + radius, 2) + Math.Pow(y, 2) <= Math.Pow(radius, 2);

        // Part B: lower-right quarter circle centered at (R, 0)
        bool partB = x >= 0 && x <= radius 
                     && y <= 0 
                     && Math.Pow(x - radius, 2) + Math.Pow(y, 2) <= Math.Pow(radius, 2);

        return partA || partB;
    }

    /// <summary>
    /// Generates a random shot uniformly in [-R, R] × [-R, R].
    /// Formula: x = 2R * (rand / RAND_MAX) - R, y = 2R * (rand / RAND_MAX) - R
    /// </summary>
    public (double X, double Y) GenerateRandomShot(double radius)
    {
        // Each coordinate is generated independently using the formula
        double x = 2 * radius * _random.NextDouble() - radius;
        double y = 2 * radius * _random.NextDouble() - radius;

        return (x, y);
    }
}
