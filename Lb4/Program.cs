using System.Globalization;
using Lpnu.Lb4.Core;
using Lpnu.Lb4.Infrastructure;

var calculator = Lab4ServiceFactory.CreateCalculator();
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
            Console.WriteLine("Unavailable: the task statement omits the target region, so no correct hit predicate can be derived.");
            break;
        case "4.6":
            var nested = calculator.CalculateNestedSums();
            Console.WriteLine($"while: {nested.While:F10}\ndo-while: {nested.DoWhile:F10}\nfor ++: {nested.ForIncreasing:F10}\nfor --: {nested.ForDecreasing:F10}");
            break;
        case "4.7":
            var rows = calculator.TabulateLogSeries(ReadDouble("X start: "), ReadDouble("X end: "),
                ReadDouble("dX: "), ReadDouble("epsilon: "));
            Console.WriteLine("+------------+------------+------------+--------+");
            Console.WriteLine("| x          | ln(x)      | series     | terms  |");
            Console.WriteLine("+------------+------------+------------+--------+");
            foreach (var row in rows)
                Console.WriteLine(row.Sum is null
                    ? $"| {row.X,10:F2} | {Math.Log(row.X),10:F5} | out of convergence domain |"
                    : $"| {row.X,10:F2} | {Math.Log(row.X),10:F5} | {row.Sum.Value,10:F5} | {row.Terms,6} |");
            Console.WriteLine("+------------+------------+------------+--------+");
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
    Console.WriteLine($"+------------+------------+\n| x          | {valueHeading,-10} |\n+------------+------------+");
    foreach (var row in rows)
        Console.WriteLine(double.IsNaN(row.Value)
            ? $"| {row.X,10:F2} | {"undefined",10} |"
            : $"| {row.X,10:F2} | {row.Value,10:F3} |");
    Console.WriteLine("+------------+------------+");
}
