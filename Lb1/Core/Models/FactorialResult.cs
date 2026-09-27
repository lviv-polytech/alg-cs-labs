namespace Lpnu.Lb1.Core.Models;

/// <summary>
/// Encapsulates the mathematical result of an iterative factorial computation.
/// </summary>
/// <param name="N">The target non-negative integer for which the factorial was calculated.</param>
/// <param name="Product">The computed factorial value (N!).</param>
/// <param name="LastIterationIndex">The final counter value when the loop condition terminates (typically N + 1).</param>
public readonly record struct FactorialResult(int N, long Product, int LastIterationIndex);
