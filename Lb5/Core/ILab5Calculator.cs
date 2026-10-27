namespace Lpnu.Lb5.Core;

/// <summary>Contracts for the function, Taylor-series, and recursion tasks in Lab 5.</summary>
public interface ILab5Calculator
{
    /// <summary>
    /// Introductory calculation for Task 5.0:
    /// calculate(x, y) = (x^2 + y) / 2.0.
    /// Pure computation isolated from console I/O for standalone unit testing.
    /// </summary>
    /// <param name="x">First argument x.</param>
    /// <param name="y">Second argument y.</param>
    /// <returns>Computed value (x * x + y) / 2.0.</returns>
    double Calculate(double x, double y);

    double CalculateExpression(double a, double b);
    double CalculateNextLogTerm(double x, int n, double previousTerm);
    SeriesResult CalculateLogSeries(double x, double epsilon);
    IReadOnlyList<LogTableRow> TabulateLogSeries(double start, double end, double step, double epsilon);
    double? CalculateHelper(double x);
    IReadOnlyList<TableRow> TabulateHelperExpression(double start, double end, int intervals);
    double RecursiveSumUpIncreasing(int n);
    double RecursiveSumUpDecreasing(int n);
    double RecursiveSumDownIncreasing(int n);
    double RecursiveSumDownDecreasing(int n);
    double IterativeSum(int n);
    ProgressionResult CalculateProgression(double first, double ratio, int n);
}

