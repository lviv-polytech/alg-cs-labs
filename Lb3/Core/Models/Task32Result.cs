namespace Lpnu.Lb3.Core.Models;

/// <summary>
/// Encapsulates the results of evaluating parameterized function Task 3.2 using both branching methods.
/// </summary>
/// <param name="A">Parameter a.</param>
/// <param name="B">Parameter b.</param>
/// <param name="C">Parameter c.</param>
/// <param name="X">Argument x.</param>
/// <param name="ResultShortForm">Computed value of F(x) using short-form branching (if only).</param>
/// <param name="ResultFullForm">Computed value of F(x) using full-form branching (if-else).</param>
/// <param name="ResultsMatch">Indicates whether both branching approaches yielded identical results within precision tolerance.</param>
public readonly record struct Task32Result(
    double A,
    double B,
    double C,
    double X,
    double ResultShortForm,
    double ResultFullForm,
    bool ResultsMatch);
