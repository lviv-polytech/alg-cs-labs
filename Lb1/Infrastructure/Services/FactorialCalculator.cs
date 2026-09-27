using Lpnu.Lb1.Core.Models;
using Lpnu.Lb1.Core.Services;

namespace Lpnu.Lb1.Infrastructure.Services;

/// <summary>
/// Internal implementation of <see cref="IFactorialCalculator"/> implementing iterative factorial calculation
/// adhering to the original FirstSetup C++ lab assignment specification.
/// </summary>
internal class FactorialCalculator : IFactorialCalculator
{
    /// <summary>
    /// Computes the factorial of a non-negative integer <c>n</c> iteratively.
    /// </summary>
    /// <remarks>
    /// Mathematical Context and Formula:
    /// In mathematical analysis, the factorial of a non-negative integer <c>n</c>, denoted as <c>n!</c>,
    /// is defined as the product of all positive integers less than or equal to <c>n</c>:
    /// <code>
    /// n! = 1 * 2 * 3 * ... * n = \prod_{k=1}^n k
    /// </code>
    /// By mathematical convention and definition:
    /// <code>
    /// 0! = 1
    /// </code>
    /// For the canonical lab assignment (originally FirstSetupCpp), the task specifically computes the factorial
    /// of 10 (<c>10!</c>).
    /// The algorithm uses a while loop with a counter <c>i</c> initialized to 1 and continuing while <c>i &lt;= n</c>:
    /// <list type="bullet">
    ///   <item><description>At iteration 1: product = 1, i = 2</description></item>
    ///   <item><description>At iteration 10: product = 3628800, i = 11</description></item>
    ///   <item><description>Loop terminates with product = 3,628,800 and final index i = 11</description></item>
    /// </list>
    /// Assignment Constraints:
    /// <list type="bullet">
    ///   <item><description><paramref name="n"/> must be non-negative (<c>n &gt;= 0</c>).</description></item>
    ///   <item><description><paramref name="n"/> must not exceed 20, as <c>21! &gt; long.MaxValue</c> (causing 64-bit integer overflow).</description></item>
    /// </list>
    /// </remarks>
    /// <param name="n">The non-negative integer to compute the factorial for. Must satisfy <c>0 &lt;= n &lt;= 20</c>.</param>
    /// <returns>
    /// A <see cref="FactorialResult"/> containing the input <c>n</c>, the computed product <c>P = n!</c>,
    /// and the post-loop index <c>i = n + 1</c>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="n"/> is negative or greater than 20.
    /// </exception>
    public FactorialResult Calculate(int n)
    {
        if (n < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n), n, "Factorial is not defined for negative integers.");
        }

        if (n > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(n), n, "Factorial computation for n > 20 exceeds 64-bit signed integer limits.");
        }

        long product = 1;
        var i = 1;

        while (i <= n)
        {
            product *= i;
            i++;
        }

        return new FactorialResult(n, product, i);
    }
}
