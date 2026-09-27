using Lpnu.Lb3.Core.Models;
using Lpnu.Lb3.Core.Services;

namespace Lpnu.Lb3.Infrastructure.Services;

/// <summary>
/// Internal service evaluating Task 3.3: graph-based piecewise function y(x, R).
/// </summary>
internal class Task33Calculator : ITask33Calculator
{
    /// <inheritdoc />
    public Task33Result Calculate(double x, double r)
    {
        if (!double.IsFinite(x))
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Argument x must be a finite number.");
        }

        if (!double.IsFinite(r) || r <= 0.0)
        {
            throw new ArgumentOutOfRangeException(nameof(r), "Scale parameter R must be a strictly positive finite number (R > 0).");
        }

        double y;
        string intervalDescription;

        if (x <= -1.0 - r)
        {
            y = 1.0;
            intervalDescription = "Interval 1: Horizontal line segment (x <= -1 - R)";
        }
        else if (x <= -1.0)
        {
            var dx = x + 1.0;
            var radicand = r * r - dx * dx;
            // Guard against tiny precision underflow below zero
            if (radicand < 0.0) radicand = 0.0;
            y = -Math.Sqrt(radicand);
            intervalDescription = "Interval 2: Circular arc segment (-1 - R < x <= -1)";
        }
        else if (x <= 2.0)
        {
            y = -r;
            intervalDescription = "Interval 3: Horizontal line segment (-1 < x <= 2)";
        }
        else
        {
            y = r * (x - 4.0) / 2.0;
            intervalDescription = "Interval 4: Inclined linear ray (x > 2)";
        }

        return new Task33Result(x, r, y, intervalDescription);
    }
}
