using Lpnu.Lb4.Core;

namespace Lpnu.Lb4.Infrastructure;

/// <summary>Implements loop-based sums and the Lab 4 function tables.</summary>
internal sealed class Lab4Calculator : ILab4Calculator
{
    private const int MaxSeriesTerms = 1_000_000;

    /// <summary>
    /// Computes sum(sin(i)/(1+cos(i))) over i=1..n using a precondition while loop.
    /// The arguments are integer radians; the finite sum is accumulated in double precision.
    /// </summary>
    public double SumWithWhile(int n)
    {
        ValidateN(n);
        var sum = 0.0;
        var i = 1;
        while (i <= n) sum += Term(i++);
        return sum;
    }

    /// <summary>Computes the same integer-indexed trigonometric sum using do-while.</summary>
    public double SumWithDoWhile(int n)
    {
        ValidateN(n);
        var sum = 0.0;
        var i = 1;
        do { sum += Term(i++); } while (i <= n);
        return sum;
    }

    /// <summary>Computes the same integer-indexed trigonometric sum using an increasing for loop.</summary>
    public double SumWithForIncreasing(int n)
    {
        ValidateN(n);
        var sum = 0.0;
        for (var i = 1; i <= n; i++) sum += Term(i);
        return sum;
    }

    /// <summary>Computes the same integer-indexed trigonometric sum in reverse index order.</summary>
    public double SumWithForDecreasing(int n)
    {
        ValidateN(n);
        var sum = 0.0;
        for (var i = n; i >= 1; i--) sum += Term(i);
        return sum;
    }

    /// <summary>
    /// Evaluates the nested sum from k=1 to 20, with an inner sum of cos(i+k), i=1..k.
    /// Each implementation resets the inner accumulator for every k and applies absolute
    /// value before adding sin(k)^2 and taking the square root.
    /// </summary>
    public NestedSums CalculateNestedSums() => new(
        NestedSumWhile(), NestedSumDoWhile(), NestedSumForIncreasing(), NestedSumForDecreasing());

    /// <summary>
    /// Tabulates y=x^2+1+B(x), using the assignment's three consecutive branches:
    /// polynomial for x&lt;4, atan((|x|+1)/2)+8.3x for 4&lt;=x&lt;7, and
    /// ln|2x+exp(4x+1)| for x&gt;=7.
    /// </summary>
    public IReadOnlyList<TableRow> TabulateFormula(double start, double end, double step) =>
        BuildTable(start, end, step, x =>
        {
            double b;
            if (x < 4) b = 4 * Math.Pow(x, 7) - Math.Pow(x, 5) + Math.Pow(x, 3) - 2;
            else if (x < 7) b = Math.Atan((Math.Abs(x) + 1) / 2) + 8.3 * x;
            else b = Math.Log(Math.Abs(2 * x + Math.Exp(4 * x + 1)));
            return x * x + 1 + b;
        });

    /// <summary>
    /// Tabulates the parameterized piecewise function. Branch precedence is retained;
    /// singular denominators are represented by NaN so the UI can print "undefined".
    /// </summary>
    public IReadOnlyList<TableRow> TabulateParameterized(double a, double b, double c, double start, double end, double step) =>
        BuildTable(start, end, step, x =>
        {
            if (x + 10 < 0 && b != 0) return a * x * x - c * x + b;
            if (x + 10 > 0 && b == 0) return x == c ? double.NaN : (x - a) / (x - c);
            return a == c ? double.NaN : -x / (a - c);
        });

    /// <summary>
    /// Tabulates the graph-defined function. At x=-1-R the circular-arc branch is used;
    /// max(0, radicand) prevents a tiny negative round-off value at the arc endpoint.
    /// </summary>
    public IReadOnlyList<TableRow> TabulateGraph(double radius, double start, double end, double step)
    {
        if (!double.IsFinite(radius) || radius <= 0) throw new ArgumentOutOfRangeException(nameof(radius));
        return BuildTable(start, end, step, x =>
        {
            if (x < -1 - radius) return 1;
            if (x <= -1) return -Math.Sqrt(Math.Max(0, radius * radius - (x + 1) * (x + 1)));
            if (x <= 2) return -radius;
            return radius * (x - 4) / 2;
        });
    }

    /// <summary>
    /// Tabulates ln(x) and its series sum. The recurrence a[n]=a[n-1]*q*n/(n+1)
    /// uses q=(x-1)/x and adds the first term whose magnitude is below epsilon.
    /// Points x&lt;=0.5 are outside convergence and return a null sum.
    /// </summary>
    public IReadOnlyList<(double X, double? Sum, int Terms)> TabulateLogSeries(
        double start, double end, double step, double epsilon)
    {
        ValidateInterval(start, end, step);
        if (!double.IsFinite(epsilon) || epsilon <= 0) throw new ArgumentOutOfRangeException(nameof(epsilon));
        var rows = new List<(double, double?, int)>();
        for (var i = 0; ; i++)
        {
            var x = start + i * step;
            if (x > end + 1e-9) break;
            if (x <= 0.5) rows.Add((x, null, 0));
            else
            {
                var q = (x - 1) / x;
                var term = q;
                var sum = 0.0;
                var terms = 0;
                do
                {
                    sum += term;
                    terms++;
                    if (Math.Abs(term) < epsilon) break;
                    term *= q * terms / (terms + 1.0);
                } while (terms < MaxSeriesTerms);
                if (terms == MaxSeriesTerms && Math.Abs(term) >= epsilon)
                    throw new InvalidOperationException("The series did not converge within the iteration limit.");
                rows.Add((x, sum, terms));
            }
        }
        return rows;
    }

    private static double Term(int i) => Math.Sin(i) / (1 + Math.Cos(i));

    private static void ValidateN(int n)
    {
        if (n < 1) throw new ArgumentOutOfRangeException(nameof(n), "N must be at least 1.");
    }

    private static IReadOnlyList<TableRow> BuildTable(double start, double end, double step, Func<double, double> calculate)
    {
        ValidateInterval(start, end, step);
        var rows = new List<TableRow>();
        for (var i = 0; ; i++)
        {
            var x = start + i * step;
            if (x > end + 1e-9) break;
            rows.Add(new TableRow(x, calculate(x)));
        }
        return rows;
    }

    private static void ValidateInterval(double start, double end, double step)
    {
        if (!double.IsFinite(start) || !double.IsFinite(end) || !double.IsFinite(step) ||
            step <= 0 || start > end)
            throw new ArgumentOutOfRangeException(nameof(step), "Require finite bounds, start <= end, and step > 0.");
    }

    private static double NestedSumWhile()
    {
        var total = 0.0; var k = 1;
        while (k <= 20) { var inner = 0.0; var i = 1; while (i <= k) inner += Math.Cos(i++ + k); total += Math.Sqrt(Math.Pow(Math.Sin(k), 2) + Math.Abs(inner)); k++; }
        return total;
    }

    private static double NestedSumDoWhile()
    {
        var total = 0.0; var k = 1;
        do { var inner = 0.0; var i = 1; do { inner += Math.Cos(i++ + k); } while (i <= k); total += Math.Sqrt(Math.Pow(Math.Sin(k), 2) + Math.Abs(inner)); k++; } while (k <= 20);
        return total;
    }

    private static double NestedSumForIncreasing()
    {
        var total = 0.0;
        for (var k = 1; k <= 20; k++) { var inner = 0.0; for (var i = 1; i <= k; i++) inner += Math.Cos(i + k); total += Math.Sqrt(Math.Pow(Math.Sin(k), 2) + Math.Abs(inner)); }
        return total;
    }

    private static double NestedSumForDecreasing()
    {
        var total = 0.0;
        for (var k = 20; k >= 1; k--) { var inner = 0.0; for (var i = k; i >= 1; i--) inner += Math.Cos(i + k); total += Math.Sqrt(Math.Pow(Math.Sin(k), 2) + Math.Abs(inner)); }
        return total;
    }
}
