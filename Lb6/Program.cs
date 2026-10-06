using System.Globalization;
using Lpnu.Lb6.Infrastructure;

var algorithms = Lab6ServiceFactory.CreateArrayAlgorithms();
Console.WriteLine("Lab 6 — select 6.1, 6.2, 6.3, or 6.4:");
Console.Write("> ");
try
{
    var task = Console.ReadLine();
    var recursive = task is "6.1" or "6.2" or "6.3" or "6.4" ? ReadMode() : false;
    switch (task)
    {
        case "6.1":
        {
            var random = new Random();
            var values = Enumerable.Range(0, 25).Select(_ => random.Next(4, 74)).ToArray();
            Console.WriteLine($"Initial: {string.Join(" ", values)}");
            var result = algorithms.AnalyzeSelected(values, recursive);
            algorithms.ZeroSelected(values, recursive);
            Console.WriteLine($"count = {result.Count}; sum = {result.Sum}");
            Console.WriteLine($"Modified: {string.Join(" ", values)}");
            break;
        }
        case "6.2":
        {
            var n = ReadInt("n (>0): ");
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "n must be positive.");
            var random = new Random();
            var values = Enumerable.Range(0, n).Select(_ => random.Next(-50, 51)).ToArray();
            Console.WriteLine(string.Join(" ", values));
            var index = algorithms.FindMaximumEvenIndex(values, recursive);
            Console.WriteLine(index < 0 ? "No even elements." : $"First maximum even index: {index}; value: {values[index]}");
            break;
        }
        case "6.3":
        {
            var n = ReadInt("Array size (>0): ");
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "Array size must be positive.");
            var integers = new int[n];
            algorithms.InputIntArray(integers, Console.In, Console.Out, recursive);
            Console.WriteLine($"int: {{{string.Join(", ", integers)}}}");
            var doubles = new double[n];
            algorithms.InputGenericArray(doubles, Console.In, Console.Out,
                text => double.Parse(text, NumberStyles.Float, CultureInfo.InvariantCulture), recursive);
            Console.WriteLine($"double: {{{string.Join(", ", doubles)}}}");
            var strings = new string[n];
            algorithms.InputGenericArray(strings, Console.In, Console.Out, text => text, recursive);
            Console.WriteLine($"string: {{{string.Join(", ", strings)}}}");
            break;
        }
        case "6.4":
        {
            var n = ReadInt("Array size (>0): ");
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "Array size must be positive.");
            var values = new double[n];
            for (var i = 0; i < n; i++) values[i] = ReadDouble($"a[{i}]: ");
            Console.WriteLine($"Initial: {Format(values)}");
            Console.WriteLine($"Positive count: {algorithms.CountPositive(values, recursive)}");
            var lastZero = algorithms.FindLastZeroIndex(values, recursive);
            if (lastZero < 0) Console.WriteLine("There are no zero elements; the requested sum is undefined.");
            else Console.WriteLine($"Sum after last zero (index {lastZero}): {algorithms.SumAfterIndex(values, lastZero, recursive):F2}");
            algorithms.PartitionByIntegerPart(values, recursive);
            Console.WriteLine($"Stable partition: {Format(values)}");
            break;
        }
        default:
            Console.WriteLine("Unknown task.");
            break;
    }
}
catch (ArgumentException ex) { Console.WriteLine($"Input error: {ex.Message}"); }
catch (FormatException ex) { Console.WriteLine($"Input error: {ex.Message}"); }

static bool ReadMode()
{
    Console.Write("Mode (i=iterative, r=recursive): ");
    var mode = Console.ReadLine()?.Trim().ToLowerInvariant();
    return mode switch { "r" => true, "i" => false, _ => throw new FormatException("Choose i or r.") };
}

static int ReadInt(string prompt)
{
    Console.Write(prompt);
    return int.TryParse(Console.ReadLine(), out var value) ? value : throw new FormatException("Enter an integer.");
}

static double ReadDouble(string prompt)
{
    Console.Write(prompt);
    if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out var value) && double.IsFinite(value))
        return value;
    throw new FormatException("Enter a finite number using '.' as the decimal separator.");
}

static string Format(IEnumerable<double> values) =>
    string.Join(" ", values.Select(value => value.ToString("F2", CultureInfo.InvariantCulture)));
