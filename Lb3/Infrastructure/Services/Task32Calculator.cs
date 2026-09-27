using CommonUtils.Math;
using Lpnu.Lb3.Core.Models;
using Lpnu.Lb3.Core.Services;

namespace Lpnu.Lb3.Infrastructure.Services;

/// <summary>
/// Internal service evaluating Task 3.2: parameterized branching function F(x).
/// Implements both short-form and full-form branching algorithms as strictly required by assignment specifications.
/// </summary>
internal class Task32Calculator : ITask32Calculator
{
    /// <inheritdoc />
    public Task32Result Calculate(double a, double b, double c, double x)
    {
        ValidateInputs(a, b, c, x);

        var shortFormResult = CalculateShortForm(a, b, c, x);
        var fullFormResult = CalculateFullForm(a, b, c, x);

        var matches = MathUtils.AreApproximatelyEqual(shortFormResult, fullFormResult, 1e-7);

        return new Task32Result(a, b, c, x, shortFormResult, fullFormResult, matches);
    }

    /// <summary>
    /// Computes F(x) strictly utilizing short-form branching commands (if without else).
    /// </summary>
    public double CalculateShortForm(double a, double b, double c, double x)
    {
        ValidateInputs(a, b, c, x);

        double result = 0.0;
        var cond1 = (x + 10.0 < 0.0) && !MathUtils.IsZero(b);
        var cond2 = (x + 10.0 > 0.0) && MathUtils.IsZero(b);

        if (cond1)
        {
            result = a * x * x - c * x + b;
        }

        if (cond2)
        {
            if (MathUtils.IsZero(x - c))
            {
                throw new ArgumentException("The expression is undefined: division by zero in (x - c).", nameof(x));
            }

            result = (x - a) / (x - c);
        }

        if (!cond1 && !cond2)
        {
            if (MathUtils.IsZero(a - c))
            {
                throw new ArgumentException("The expression is undefined: division by zero in (a - c).", nameof(a));
            }

            result = -x / (a - c);
        }

        return result;
    }

    /// <summary>
    /// Computes F(x) strictly utilizing full-form branching commands (if-else).
    /// </summary>
    public double CalculateFullForm(double a, double b, double c, double x)
    {
        ValidateInputs(a, b, c, x);

        if ((x + 10.0 < 0.0) && !MathUtils.IsZero(b))
        {
            return a * x * x - c * x + b;
        }
        else if ((x + 10.0 > 0.0) && MathUtils.IsZero(b))
        {
            if (MathUtils.IsZero(x - c))
            {
                throw new ArgumentException("The expression is undefined: division by zero in (x - c).", nameof(x));
            }

            return (x - a) / (x - c);
        }
        else
        {
            if (MathUtils.IsZero(a - c))
            {
                throw new ArgumentException("The expression is undefined: division by zero in (a - c).", nameof(a));
            }

            return -x / (a - c);
        }
    }

    private static void ValidateInputs(double a, double b, double c, double x)
    {
        if (!double.IsFinite(a)) throw new ArgumentOutOfRangeException(nameof(a), "Parameter a must be a finite number.");
        if (!double.IsFinite(b)) throw new ArgumentOutOfRangeException(nameof(b), "Parameter b must be a finite number.");
        if (!double.IsFinite(c)) throw new ArgumentOutOfRangeException(nameof(c), "Parameter c must be a finite number.");
        if (!double.IsFinite(x)) throw new ArgumentOutOfRangeException(nameof(x), "Argument x must be a finite number.");
    }
}
