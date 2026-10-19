using System.Globalization;
using Lpnu.Lb5.Core;
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
            var logRows = calculator.TabulateLogSeries(start, end, step, epsilon);
            PrintLogTable(logRows);
            break;
        case "5.3":
            var table = calculator.TabulateHelperExpression(ReadDouble("g start: "), ReadDouble("g end: "), ReadInt("n (>=1): "));
            PrintHelperTable(table);
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

static void PrintLogTable(IReadOnlyList<LogTableRow> rows)
{
    const string border = "+--------------+--------------------+--------------------+--------------+";
    Console.WriteLine(border);
    Console.WriteLine($"| {"      Taylor Series Tabulation for ln(x) (Variant 17)",-69} |");
    Console.WriteLine(border);
    Console.WriteLine($"| {"x",12} | {"ln(x) (Exact)",18} | {"Taylor Sum",18} | {"Terms",12} |");
    Console.WriteLine(border);
    foreach (var row in rows)
    {
        var xCell = FormatCell(row.Argument, 12, 4);
        var exactCell = row.ExactValue.HasValue ? FormatCell(row.ExactValue.Value, 18, 8) : "undefined";
        var sumCell = row.SeriesSum.HasValue ? FormatCell(row.SeriesSum.Value, 18, 8) : "outside domain";
        var termsCell = row.SeriesSum.HasValue ? row.Terms.ToString(CultureInfo.InvariantCulture) : "-";
        Console.WriteLine($"| {xCell,12} | {exactCell,18} | {sumCell,18} | {termsCell,12} |");
    }
    Console.WriteLine(border);
}

static void PrintHelperTable(IReadOnlyList<TableRow> rows)
{
    const string border = "+--------------+--------------------+";
    Console.WriteLine(border);
    Console.WriteLine($"| {"    Function Tabulation Table",-33} |");
    Console.WriteLine(border);
    Console.WriteLine($"| {"g",12} | {"E(g)",18} |");
    Console.WriteLine(border);
    foreach (var row in rows)
    {
        var gCell = FormatCell(row.Argument, 12, 4);
        var valCell = row.Value.HasValue ? FormatCell(row.Value.Value, 18, 8) : "undefined";
        Console.WriteLine($"| {gCell,12} | {valCell,18} |");
    }
    Console.WriteLine(border);
}

static string FormatCell(double value, int width, int precision)
{
    if (!double.IsFinite(value)) return "undefined";
    var fixedStr = value.ToString($"F{precision}", CultureInfo.InvariantCulture);
    if (fixedStr.Length <= width) return fixedStr;
    var sciPrecision = Math.Max(1, width - 7);
    return value.ToString($"E{sciPrecision}", CultureInfo.InvariantCulture);
}

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

