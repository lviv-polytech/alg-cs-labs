namespace Lpnu.Lb4.Infrastructure;

using Core;

/// <summary>Factory for creating target calculator instances.</summary>
public static class TargetCalculatorFactory
{
    /// <summary>Creates a new instance of the target calculator.</summary>
    public static ITargetCalculator CreateTargetCalculator()
    {
        return new TargetCalculator();
    }
}
