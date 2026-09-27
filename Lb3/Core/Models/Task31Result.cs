namespace Lpnu.Lb3.Core.Models;

/// <summary>
/// Encapsulates the evaluation output of single-variable piecewise function Task 3.1.
/// </summary>
/// <param name="M">The primary input argument m.</param>
/// <param name="N">The auxiliary input argument n.</param>
/// <param name="Phi">The evaluated piecewise sub-function Phi(m).</param>
/// <param name="Y">The final computed function value Y = m^2 + 1 + Phi(m).</param>
public readonly record struct Task31Result(double M, double N, double Phi, double Y);
