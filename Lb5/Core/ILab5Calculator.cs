namespace Lpnu.Lb5.Core;

/// <summary>Contracts for the function, Taylor-series, and recursion tasks in Lab 5.</summary>
public interface ILab5Calculator
{
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

