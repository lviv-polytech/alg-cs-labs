using Lpnu.Lb5.Core;

namespace Lpnu.Lb5.Infrastructure;

/// <summary>Implements the mathematical services for Lab 5.</summary>
internal sealed class Lab5Calculator : ILab5Calculator
{
    private const int MaximumSeriesTerms = 1_000_000;

    /// <summary>
    /// Introductory calculation for Task 5.0: (x^2 + y) / 2.0.
    /// Pure computation isolated from console I/O for standalone unit testing.
    /// </summary>
    /// <param name="x">First argument x.</param>
    /// <param name="y">Second argument y.</param>
    /// <returns>Computed value (x * x + y) / 2.0.</returns>
    public double Calculate(double x, double y)
    {
        EnsureFinite(x, nameof(x));
        EnsureFinite(y, nameof(y));
        return (x * x + y) / 2.0;
    }

    /// <summary>
    /// Evaluates (h(a,b,1)+h(1,a,b))/(1+h(a^2+b^2,1,0)),
    /// where h(x,y,z)=(x+y+z)/(x^2+y^2+z^2). Each h denominator is positive
    /// because every invocation has at least one argument equal to one.
    /// </summary>
    public double CalculateExpression(double a, double b)
    {
        EnsureFinite(a, nameof(a)); EnsureFinite(b, nameof(b));
        return (H(a, b, 1) + H(1, a, b)) / (1 + H(a * a + b * b, 1, 0));
    }

    /// <summary>
    /// Computes the next Taylor series term a[n] from a[n-1] using the recurrence relation:
    /// a[n] = a[n-1] * ((x - 1) / x) * (n / (n + 1.0)) for n &gt;= 1, x != 0.
    /// </summary>
    public double CalculateNextLogTerm(double x, int n, double previousTerm)
    {
        EnsureFinite(x, nameof(x)); EnsureFinite(previousTerm, nameof(previousTerm));
        if (Math.Abs(x) < 1e-15) throw new ArgumentOutOfRangeException(nameof(x), "x must not be zero.");
        if (n < 1) throw new ArgumentOutOfRangeException(nameof(n), "Recurrent index n must be >= 1.");
        var q = (x - 1.0) / x;
        return previousTerm * q * (n / (n + 1.0));
    }

    /// <summary>
    /// Sums ln(x)=q+q^2/2+... using CalculateNextLogTerm(x, n, term), q=(x-1)/x.
    /// The initial term is always included; the first term below epsilon is included too.
    /// The specified convergence domain is x&gt;0.5.
    /// </summary>
    public SeriesResult CalculateLogSeries(double x, double epsilon)
    {
        EnsureFinite(x, nameof(x));
        if (x <= 0.5) throw new ArgumentOutOfRangeException(nameof(x), "The series converges only for x > 0.5.");
        if (!double.IsFinite(epsilon) || epsilon <= 0) throw new ArgumentOutOfRangeException(nameof(epsilon));
        var term = (x - 1.0) / x;
        var sum = term;
        var terms = 1;
        var n = 0;
        while (Math.Abs(term) >= epsilon)
        {
            if (terms >= MaximumSeriesTerms)
                throw new InvalidOperationException("The series did not converge within the iteration limit.");
            n++;
            term = CalculateNextLogTerm(x, n, term);
            sum += term;
            terms++;
        }
        return new SeriesResult(sum, terms);
    }

    /// <summary>
    /// Tabulates ln(x) and its Taylor series approximation over [start, end] with step dx.
    /// Points with x &lt;= 0.5 have null SeriesSum and 0 Terms; points with x &lt;= 0 also have null ExactValue.
    /// </summary>
    public IReadOnlyList<LogTableRow> TabulateLogSeries(double start, double end, double step, double epsilon)
    {
        EnsureFinite(start, nameof(start)); EnsureFinite(end, nameof(end));
        EnsureFinite(step, nameof(step)); EnsureFinite(epsilon, nameof(epsilon));
        if (start > end) throw new ArgumentOutOfRangeException(nameof(start), "Require start <= end.");
        if (step <= 0) throw new ArgumentOutOfRangeException(nameof(step), "Require step > 0.");
        if (epsilon <= 0) throw new ArgumentOutOfRangeException(nameof(epsilon), "Require epsilon > 0.");

        var rows = new List<LogTableRow>();
        for (var i = 0; ; i++)
        {
            var x = start + i * step;
            if (x > end + 1e-9) break;
            if (Math.Abs(x) < 1e-12) x = 0.0;

            double? exact = x > 0 ? Math.Log(x) : null;
            if (x <= 0.5)
            {
                rows.Add(new LogTableRow(x, exact, null, 0));
            }
            else
            {
                var series = CalculateLogSeries(x, epsilon);
                rows.Add(new LogTableRow(x, exact, series.Sum, series.Terms));
            }
        }
        return rows;
    }

    /// <summary>
    /// Evaluates h(x)=(sin(x)+1)/(1+cos(x)) for |x|&gt;=1, unless the denominator
    /// is within 1e-12 of zero; for |x|&lt;1, uses the prescribed six-term sum
    /// sum(x^i/(2i)!), i=0..5, updating each term by x/((2i-1)(2i)).
    /// </summary>
    public double? CalculateHelper(double x)
    {
        EnsureFinite(x, nameof(x));
        if (Math.Abs(x) >= 1)
        {
            var denominator = 1 + Math.Cos(x);
            if (Math.Abs(denominator) < 1e-12) return null;
            return (Math.Sin(x) + 1) / denominator;
        }

        var sum = 1.0;
        var term = 1.0;
        for (var i = 1; i <= 5; i++)
        {
            term *= x / ((2.0 * i - 1) * (2.0 * i));
            sum += term;
        }
        return sum;
    }

    /// <summary>
    /// Tabulates E(g)=h(g^2+1)+h(g+h(1))+1 over n equal intervals.
    /// A singular h invocation propagates as an undefined table value; the fixed
    /// six-term small-argument expansion is used exactly as specified.
    /// </summary>
    public IReadOnlyList<TableRow> TabulateHelperExpression(double start, double end, int intervals)
    {
        EnsureFinite(start, nameof(start)); EnsureFinite(end, nameof(end));
        if (intervals < 1 || start > end) throw new ArgumentOutOfRangeException(nameof(intervals));
        var h1 = CalculateHelper(1)!.Value;
        var rows = new List<TableRow>(intervals + 1);
        for (var j = 0; j <= intervals; j++)
        {
            var g = start + j * ((end - start) / intervals);
            var first = CalculateHelper(g * g + 1);
            var second = CalculateHelper(g + h1);
            rows.Add(new TableRow(g, first.HasValue && second.HasValue ? first.Value + second.Value + 1 : null));
        }
        return rows;
    }

    /// <summary>Control sum of sin(i)/(1+cos(i)) for i=1..n.</summary>
    public double IterativeSum(int n)
    {
        ValidateRecursiveN(n, 5000);
        var sum = 0.0;
        for (var i = 1; i <= n; i++) sum += Term(i);
        return sum;
    }

    /// <summary>Recursion-unwind sum with increasing indices, adding each term after return.</summary>
    public double RecursiveSumUpIncreasing(int n)
    {
        ValidateRecursiveN(n, 5000);
        return UpIncreasing(1, n);
    }

    /// <summary>Recursion-unwind sum with decreasing indices, adding each term after return.</summary>
    public double RecursiveSumUpDecreasing(int n)
    {
        ValidateRecursiveN(n, 5000);
        return UpDecreasing(n, 1);
    }

    /// <summary>Accumulator recursion with increasing indices, accumulating on the descent.</summary>
    public double RecursiveSumDownIncreasing(int n)
    {
        ValidateRecursiveN(n, 5000);
        return DownIncreasing(1, n, 0);
    }

    /// <summary>Accumulator recursion with decreasing indices, accumulating on the descent.</summary>
    public double RecursiveSumDownDecreasing(int n)
    {
        ValidateRecursiveN(n, 5000);
        return DownDecreasing(n, 1, 0);
    }

    /// <summary>
    /// Recursively computes b[n]=b1*q^(n-1) and S[n]=sum(k=1..n)b[k].
    /// The depth reported for each recursion is the number of calls in its longest
    /// chain, so the base case at level n-1 yields a depth of n.
    /// </summary>
    public ProgressionResult CalculateProgression(double first, double ratio, int n)
    {
        EnsureFinite(first, nameof(first)); EnsureFinite(ratio, nameof(ratio));
        ValidateRecursiveN(n, 1000);
        var termMaxLevel = 0;
        var nthTerm = ProgressionTerm(first, ratio, n, 0, ref termMaxLevel);
        var sumMaxLevel = 0;
        var sum = ProgressionSum(first, ratio, n, 0, ref sumMaxLevel);
        return new ProgressionResult(
            nthTerm,
            sum,
            termMaxLevel + 1,
            sumMaxLevel + 1);
    }

    private static double H(double x, double y, double z) => (x + y + z) / (x * x + y * y + z * z);
    private static double Term(int i) => Math.Sin(i) / (1 + Math.Cos(i));
    private static double UpIncreasing(int i, int n) => i > n ? 0 : Term(i) + UpIncreasing(i + 1, n);
    private static double UpDecreasing(int i, int lower) => i < lower ? 0 : Term(i) + UpDecreasing(i - 1, lower);
    private static double DownIncreasing(int i, int n, double accumulator) => i > n ? accumulator : DownIncreasing(i + 1, n, accumulator + Term(i));
    private static double DownDecreasing(int i, int lower, double accumulator) => i < lower ? accumulator : DownDecreasing(i - 1, lower, accumulator + Term(i));

    private static double ProgressionTerm(double first, double ratio, int n, int level, ref int maximumLevel)
    {
        maximumLevel = Math.Max(maximumLevel, level);
        return n == 1 ? first : ProgressionTerm(first, ratio, n - 1, level + 1, ref maximumLevel) * ratio;
    }

    private static double ProgressionSum(double first, double ratio, int n, int level, ref int maximumLevel)
    {
        maximumLevel = Math.Max(maximumLevel, level);
        return n == 1 ? first : ProgressionSum(first, ratio, n - 1, level + 1, ref maximumLevel) +
            ProgressionTermValue(first, ratio, n);
    }

    private static double ProgressionTermValue(double first, double ratio, int n) =>
        n == 1 ? first : ProgressionTermValue(first, ratio, n - 1) * ratio;

    private static void ValidateRecursiveN(int n, int maximum)
    {
        if (n < 1 || n > maximum) throw new ArgumentOutOfRangeException(nameof(n), $"N must be in [1, {maximum}].");
    }

    private static void EnsureFinite(double value, string name)
    {
        if (!double.IsFinite(value)) throw new ArgumentOutOfRangeException(name, "Value must be finite.");
    }
}
