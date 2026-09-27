using Lpnu.Lb3.Core.Models;

namespace Lpnu.Lb3.Core.Services;

/// <summary>
/// Service contract for evaluating Task 3.1: piecewise function of a single variable.
/// </summary>
public interface ITask31Calculator
{
    /// <summary>
    /// Computes Y = m^2 + 1 + Phi(m) for the given parameters m and n.
    /// </summary>
    /// <param name="m">The primary real argument m.</param>
    /// <param name="n">The auxiliary real argument n.</param>
    /// <returns>A <see cref="Task31Result"/> with evaluated sub-functions and result.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when <paramref name="m"/> or <paramref name="n"/> is non-finite.
    /// </exception>
    Task31Result Calculate(double m, double n);
}
