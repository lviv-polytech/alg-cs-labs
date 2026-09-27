namespace alg_cs_labs.Lb2;

public readonly record struct CalculationResult(double Z1, double Z2);

public static class Variant17Calculator
{
    public static CalculationResult Calculate(double m)
    {
        if (!double.IsFinite(m) || m <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(m), "m must be a positive, finite number.");
        }

        var numerator = Math.Sqrt(Math.Pow(3 * m + 2, 2) - 24 * m);
        var denominator = 3 * Math.Sqrt(m) - 2 / Math.Sqrt(m);
        if (denominator == 0)
        {
            throw new ArgumentException("The expression is undefined for m = 2/3.", nameof(m));
        }

        return new CalculationResult(numerator / denominator, Math.Sqrt(m));
    }
}
