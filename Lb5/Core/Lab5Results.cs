namespace Lpnu.Lb5.Core;

/// <summary>Computed Taylor sum and the number of included terms.</summary>
public sealed record SeriesResult(double Sum, int Terms);

/// <summary>A tabulation row for the ln(x) Taylor series table (null when undefined or outside convergence domain).</summary>
public sealed record LogTableRow(double Argument, double? ExactValue, double? SeriesSum, int Terms);

/// <summary>A tabulation row with a nullable result for mathematically undefined values.</summary>
public sealed record TableRow(double Argument, double? Value);

/// <summary>Geometric progression results and maximum recursive call-chain lengths.</summary>
public sealed record ProgressionResult(double NthTerm, double Sum, int TermDepth, int SumDepth);

