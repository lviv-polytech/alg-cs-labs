namespace Lpnu.Lb5.Core;

/// <summary>Computed Taylor sum and the number of included terms.</summary>
public sealed record SeriesResult(double Sum, int Terms);

/// <summary>A tabulation row with a nullable result for mathematically undefined values.</summary>
public sealed record TableRow(double Argument, double? Value);

/// <summary>Geometric progression results and maximum recursive call-chain lengths.</summary>
public sealed record ProgressionResult(double NthTerm, double Sum, int TermDepth, int SumDepth);
