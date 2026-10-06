using System.Globalization;
using Lpnu.Lb5.Infrastructure;

var calculator = Lab5ServiceFactory.CreateCalculator();
Console.WriteLine("Lab 5 — select 5.1, 5.2, 5.3, 5.4, or 5.5:");
Console.Write("> ");
try
{
    switch (Console.ReadLine())
    {
        case "5.1":
            Console.WriteLine($"R = {calculator.CalculateExpression(ReadDouble("a: "), ReadDouble("b: ")):F10}");
            break;
        case "5.2":
            var start = ReadDouble("X start: "); var end = ReadDouble("X end: ");
            var step = ReadDouble("dX: "); var epsilon = ReadDouble("epsilon: ");
            if (start > end || step <= 0) throw new ArgumentOutOfRangeException(nameof(step), "Require X start <= X end and dX > 0.");
            Console.WriteLine("| x | ln(x) | series | terms |");
            for (var i = 0; ; i++)
            {
                var x = start + i * step;
                if (x > end + 1e-9) break;
                if (x <= 0.5) Console.WriteLine($"{x:F2} | {Math.Log(x):F6} | out of convergence domain |");
                else
                {
                    var series = calculator.CalculateLogSeries(x, epsilon);
                    Console.WriteLine($"{x:F2} | {Math.Log(x):F6} | {series.Sum:F6} | {series.Terms}");
                }
            }
            break;
        case "5.3":
            var table = calculator.TabulateHelperExpression(ReadDouble("g start: "), ReadDouble("g end: "), ReadInt("n (>=1): "));
            Console.WriteLine("| g | E(g) |");
            foreach (var row in table)
                Console.WriteLine(row.Value is null ? $"{row.Argument:F3} | undefined" : $"{row.Argument:F3} | {row.Value.Value:F8}");
            break;
        case "5.4":
            var count = ReadInt("N (1..5000): ");
            Console.WriteLine($"iter: {calculator.IterativeSum(count):F10}");
            Console.WriteLine($"rec up ++: {calculator.RecursiveSumUpIncreasing(count):F10}");
            Console.WriteLine($"rec up --: {calculator.RecursiveSumUpDecreasing(count):F10}");
            Console.WriteLine($"rec down ++: {calculator.RecursiveSumDownIncreasing(count):F10}");
            Console.WriteLine($"rec down --: {calculator.RecursiveSumDownDecreasing(count):F10}");
            break;
        case "5.5":
            var first = ReadDouble("b1: "); var ratio = ReadDouble("q: "); var n = ReadInt("n (1..1000): ");
            var result = calculator.CalculateProgression(first, ratio, n);
            Console.WriteLine($"b[n] = {result.NthTerm:G12}\nS[n] = {result.Sum:G12}");
            Console.WriteLine($"Term recursion depth: {result.TermDepth}; sum recursion depth: {result.SumDepth}");
            break;
        default:
            Console.WriteLine("Unknown task.");
            break;
    }
}
catch (ArgumentException ex) { Console.WriteLine($"Input error: {ex.Message}"); }
catch (FormatException ex) { Console.WriteLine($"Input error: {ex.Message}"); }
catch (InvalidOperationException ex) { Console.WriteLine($"Calculation error: {ex.Message}"); }

static double ReadDouble(string prompt)
{
    Console.Write(prompt);
    if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out var value) && double.IsFinite(value))
        return value;
    throw new FormatException("Enter a finite number using '.' as the decimal separator.");
}

static int ReadInt(string prompt)
{
    Console.Write(prompt);
    return int.TryParse(Console.ReadLine(), out var value) ? value : throw new FormatException("Enter an integer.");
}
