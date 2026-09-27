using Lpnu.Lb3.Core.Models;

namespace Lpnu.Lb3.Core.Services;

/// <summary>
/// Service contract for evaluating Task 3.2: parameterized branching function F(x).
/// </summary>
public interface ITask32Calculator
{
    /// <summary>
    /// Computes F(x) using both short-form branching (if only) and full-form branching (if-else).
    /// </summary>
    /// <param name="a">Parameter a.</param>
    /// <param name="b">Parameter b.</param>
    /// <param name="c">Parameter c.</param>
    /// <param name="x">Argument x.</param>
    /// <returns>A <see cref="Task32Result"/> containing outputs from both algorithms and verification of equality.</returns>
    /// <exception cref="System.ArgumentException">
    /// Thrown when parameters lead to division by zero (such as x = c in branch 2 or a = c in branch 3).
    /// </exception>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when any argument is non-finite.
    /// </exception>
    Task32Result Calculate(double a, double b, double c, double x);

    /// <summary>
    /// Evaluates F(x) strictly using short-form branching statements (if only).
    /// </summary>
    double CalculateShortForm(double a, double b, double c, double x);

    /// <summary>
    /// Evaluates F(x) strictly using full-form branching statements (if-else).
    /// </summary>
    double CalculateFullForm(double a, double b, double c, double x);
}
