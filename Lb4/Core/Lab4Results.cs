namespace Lpnu.Lb4.Core;

/// <summary>One sampled argument and function value.</summary>
public sealed record TableRow(double X, double Value);

/// <summary>Partial sums and convergence metadata for the logarithm series.</summary>
public sealed record SeriesResult(double Sum, int Terms);

/// <summary>Results of the four implementations of the nested sum.</summary>
public sealed record NestedSums(double While, double DoWhile, double ForIncreasing, double ForDecreasing);
