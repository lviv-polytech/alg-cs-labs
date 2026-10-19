using System.Globalization;
using Lpnu.Lb4.Core;
using Lpnu.Lb4.Infrastructure;

var calculator = Lab4ServiceFactory.CreateCalculator();
var targetCalculator = TargetCalculatorFactory.CreateTargetCalculator();
Console.WriteLine("Lab 4 — select 4.1, 4.2, 4.3, 4.4, 4.5, 4.6, or 4.7:");
Console.Write("> ");
var choice = Console.ReadLine();
try
{
    switch (choice)
    {
        case "4.1":
            var n = ReadInt("N (>=1): ");
            Console.WriteLine($"while: {calculator.SumWithWhile(n):F10}");
            Console.WriteLine($"do-while: {calculator.SumWithDoWhile(n):F10}");
            Console.WriteLine($"for ++: {calculator.SumWithForIncreasing(n):F10}");
            Console.WriteLine($"for --: {calculator.SumWithForDecreasing(n):F10}");
            break;
        case "4.2":
            PrintRows(calculator.TabulateFormula(ReadDouble("X start: "), ReadDouble("X end: "), ReadDouble("dX: ")), "y");
            break;
        case "4.3":
            var a = ReadDouble("a: "); var b = ReadDouble("b: "); var c = ReadDouble("c: ");
            PrintRows(calculator.TabulateParameterized(a, b, c, ReadDouble("X start: "), ReadDouble("X end: "), ReadDouble("dX: ")), "F");
            break;
        case "4.4":
            var radius = ReadDouble("R (>0): ");
            PrintRows(calculator.TabulateGraph(radius, ReadDouble("X start: "), ReadDouble("X end: "), ReadDouble("dX: ")), "y");
            break;
        case "4.5":
            ExecuteTask45(targetCalculator);
            break;
        case "4.6":
            var nested = calculator.CalculateNestedSums();
            Console.WriteLine($"while: {nested.While:F10}\ndo-while: {nested.DoWhile:F10}\nfor ++: {nested.ForIncreasing:F10}\nfor --: {nested.ForDecreasing:F10}");
            break;
        case "4.7":
            var rows = calculator.TabulateLogSeries(ReadDouble("X start: "), ReadDouble("X end: "),
                ReadDouble("dX: "), ReadDouble("epsilon: "));
            const string logBorder = "+--------------+--------------------+--------------------+--------------+";
            Console.WriteLine(logBorder);
            Console.WriteLine($"| {"      Taylor Series Tabulation for ln(x) (Variant 17)",-69} |");
            Console.WriteLine(logBorder);
            Console.WriteLine($"| {"x",12} | {"ln(x) (Exact)",18} | {"Taylor Sum",18} | {"Terms",12} |");
            Console.WriteLine(logBorder);
            foreach (var row in rows)
            {
                var exactStr = row.X > 0 ? row.X.ToString("F0") is var _ && double.IsFinite(Math.Log(row.X)) ? Math.Log(row.X).ToString("F8", CultureInfo.InvariantCulture) : "undefined" : "undefined";
                Console.WriteLine(row.Sum is null
                    ? $"| {row.X,12:F4} | {exactStr,18} | {"outside domain",18} | {"-",12} |"
                    : $"| {row.X,12:F4} | {exactStr,18} | {row.Sum.Value,18:F8} | {row.Terms,12} |");
            }
            Console.WriteLine(logBorder);
            break;
        default:
            Console.WriteLine("Unknown task.");
            break;
    }
}
catch (ArgumentException ex) { Console.WriteLine($"Input error: {ex.Message}"); }
catch (FormatException ex) { Console.WriteLine($"Input error: {ex.Message}"); }

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

static void PrintRows(IReadOnlyList<TableRow> rows, string valueHeading)
{
    const string border = "+--------------+--------------------+";
    Console.WriteLine(border);
    Console.WriteLine($"| {"    Function Tabulation Table",-33} |");
    Console.WriteLine(border);
    Console.WriteLine($"| {"x",12} | {valueHeading,18} |");
    Console.WriteLine(border);
    foreach (var row in rows)
        Console.WriteLine(double.IsNaN(row.Value)
            ? $"| {row.X,12:F4} | {"undefined",18} |"
            : $"| {row.X,12:F4} | {row.Value,18:F6} |");
    Console.WriteLine(border);
}

static void ExecuteTask45(ITargetCalculator target)
{
    var radius = ReadDouble("R (>0): ");
    if (radius <= 0)
        throw new ArgumentException("R must be positive.");

    Console.WriteLine("\n--- 10 Manual Shots ---");
    for (int i = 0; i < 10; i++)
    {
        var x = ReadDouble($"Shot {i + 1} X: ");
        var y = ReadDouble($"Shot {i + 1} Y: ");
        Console.WriteLine(target.InTarget(x, y, radius) ? "yes" : "no");
    }

    Console.WriteLine("\n--- 10 Random Shots ---");
    Console.WriteLine("+--------+--------+---------+");
    Console.WriteLine("| x      | y      | hit     |");
    Console.WriteLine("+--------+--------+---------+");

    for (int i = 0; i < 10; i++)
    {
        var (x, y) = target.GenerateRandomShot(radius);
        var hit = target.InTarget(x, y, radius) ? "yes" : "no";
        Console.WriteLine($"| {x,6:F4} | {y,6:F4} | {hit,-7} |");
    }

    Console.WriteLine("+--------+--------+---------+");
}
