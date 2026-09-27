using Lpnu.Lb1.Core.Models;

namespace Lpnu.Lb1.Core.Services;

/// <summary>
/// Contract defining the factorial calculation service interface.
/// </summary>
public interface IFactorialCalculator
{
    /// <summary>
    /// Computes the factorial of a given non-negative integer <paramref name="n"/> using iterative multiplication.
    /// </summary>
    /// <param name="n">The non-negative integer whose factorial is to be determined (0 &lt;= n &lt;= 20).</param>
    /// <returns>A <see cref="FactorialResult"/> containing the product and terminal loop counter.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="n"/> is negative or greater than 20 (causing 64-bit integer overflow).
    /// </exception>
    FactorialResult Calculate(int n);
}
