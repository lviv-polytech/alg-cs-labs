using System.Globalization;
using Lpnu.Lb7.Infrastructure;

var algorithms = Lab7ServiceFactory.CreateMatrixAlgorithms();
Console.WriteLine("Lab 7 — select 7.1, 7.2, or 7.3:");
Console.Write("> ");
try
{
    var task = Console.ReadLine();
    var recursive = task is "7.1" or "7.2" or "7.3" ? ReadMode() : false;
    switch (task)
    {
        case "7.1":
        {
            var random = new Random();
            var original = new int[8, 5];
            for (var row = 0; row < 8; row++)
                for (var column = 0; column < 5; column++) original[row, column] = random.Next(11, 65);
            var modified = (int[,])original.Clone();
            var sorted = (int[,])original.Clone();
            Console.WriteLine("Original matrix:");
            Print(original);
            var summary = algorithms.ZeroSelected(modified, recursive);
            Console.WriteLine($"Selected count = {summary.Count}; sum = {summary.Sum}\nAfter zeroing:");
            Print(modified);
            algorithms.SortRows(sorted, recursive);
            Console.WriteLine("Rows sorted by columns 1, 2, and 4:");
            Print(sorted);
            break;
        }
        case "7.2":
        {
            var rowCount = ReadInt("Rows (k): "); var columnCount = ReadInt("Columns (n): ");
            if (rowCount < 1 || columnCount < 1) throw new ArgumentOutOfRangeException(nameof(rowCount), "Dimensions must be positive.");
            var original = new int[rowCount, columnCount];
            for (var row = 0; row < rowCount; row++)
                for (var column = 0; column < columnCount; column++) original[row, column] = ReadInt($"a[{row},{column}]: ");
            var changed = (int[,])original.Clone();
            Console.WriteLine("Original:"); Print(original);
            algorithms.SwapRowExtremes(changed, recursive);
            Console.WriteLine("After paired row swaps:"); Print(changed);
            break;
        }
        case "7.3":
        {
            var n = ReadInt("Square matrix dimension n (>0): ");
            if (n < 1) throw new ArgumentOutOfRangeException(nameof(n), "n must be positive.");
            var original = new double[n, n];
            for (var row = 0; row < n; row++)
                for (var column = 0; column < n; column++) original[row, column] = ReadDouble($"a[{row},{column}]: ");
            var rearranged = (double[,])original.Clone();
            Console.WriteLine("Original:"); PrintReal(original);
            var index = algorithms.FindFirstRowWithoutPositive(original, recursive);
            Console.WriteLine(index < 0 ? "No row without positive values." : $"First row without positive values: {index}");
            algorithms.PlaceLargestValuesOnDiagonal(rearranged, recursive);
            Console.WriteLine("Largest values on descending diagonal:"); PrintReal(rearranged);
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

static void Print(int[,] matrix)
{
    for (var row = 0; row < matrix.GetLength(0); row++)
        Console.WriteLine(string.Join(" ", Enumerable.Range(0, matrix.GetLength(1)).Select(column => $"{matrix[row, column],4}")));
}

static void PrintReal(double[,] matrix)
{
    for (var row = 0; row < matrix.GetLength(0); row++)
        Console.WriteLine(string.Join(" ", Enumerable.Range(0, matrix.GetLength(1))
            .Select(column => matrix[row, column].ToString("F2", CultureInfo.InvariantCulture).PadLeft(8))));
}
