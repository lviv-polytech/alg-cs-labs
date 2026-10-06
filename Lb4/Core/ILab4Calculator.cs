namespace Lpnu.Lb4.Core;

/// <summary>Contracts for the loop and tabulation exercises in Lab 4.</summary>
public interface ILab4Calculator
{
    double SumWithWhile(int n);
    double SumWithDoWhile(int n);
    double SumWithForIncreasing(int n);
    double SumWithForDecreasing(int n);
    NestedSums CalculateNestedSums();
    IReadOnlyList<TableRow> TabulateFormula(double start, double end, double step);
    IReadOnlyList<TableRow> TabulateParameterized(double a, double b, double c, double start, double end, double step);
    IReadOnlyList<TableRow> TabulateGraph(double radius, double start, double end, double step);
    IReadOnlyList<(double X, double? Sum, int Terms)> TabulateLogSeries(double start, double end, double step, double epsilon);
}
