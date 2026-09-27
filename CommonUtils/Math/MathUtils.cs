namespace CommonUtils.Math;

/// <summary>
/// General-purpose mathematical helpers and tolerance-based comparison utilities.
/// </summary>
public static class MathUtils
{
    /// <summary>
    /// Default floating-point comparison precision tolerance.
    /// </summary>
    public const double DefaultEpsilon = 1e-9;

    /// <summary>
    /// Checks whether two double-precision floating-point numbers are equal within a specified tolerance.
    /// </summary>
    /// <param name="a">The first value.</param>
    /// <param name="b">The second value.</param>
    /// <param name="tolerance">The absolute maximum allowable difference (must be non-negative).</param>
    /// <returns><c>true</c> if the numbers differ by less than or equal to <paramref name="tolerance"/>; otherwise <c>false</c>.</returns>
    public static bool AreApproximatelyEqual(double a, double b, double tolerance = DefaultEpsilon)
    {
        return System.Math.Abs(a - b) <= tolerance;
    }

    /// <summary>
    /// Checks whether a floating-point number is approximately zero within a specified tolerance.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="tolerance">The absolute tolerance around zero.</param>
    /// <returns><c>true</c> if the value is within <paramref name="tolerance"/> of zero; otherwise <c>false</c>.</returns>
    public static bool IsZero(double value, double tolerance = DefaultEpsilon)
    {
        return System.Math.Abs(value) <= tolerance;
    }
}
