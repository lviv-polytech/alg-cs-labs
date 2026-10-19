using System.Globalization;
using Lpnu.Lb6;
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
            Console.WriteLine("=== Task 6.1: Array Selection (Variant 17) ===");
            var values = new int[25];
            if (recursive)
            {
                Console.WriteLine("[Recursive approach]");
                Lpnu.Lb6.Task1Recursive.GenerateArrayRec(values, Lpnu.Lb6.Task1Recursive.DefaultLow, Lpnu.Lb6.Task1Recursive.DefaultHigh);
                Console.WriteLine("Original array:");
                Lpnu.Lb6.Task1Recursive.PrintArrayRec(values, Console.Out);

                var count = Lpnu.Lb6.Task1Recursive.CountElementsRec(values);
                var sum = Lpnu.Lb6.Task1Recursive.SumElementsRec(values);
                Lpnu.Lb6.Task1Recursive.ZeroElementsRec(values);

                Console.WriteLine($"count = {count}; sum = {sum}");
                Console.WriteLine("Modified array (selected elements replaced by 0):");
                Lpnu.Lb6.Task1Recursive.PrintArrayRec(values, Console.Out);
            }
            else
            {
                Console.WriteLine("[Iterative approach]");
                Lpnu.Lb6.Task1Iterative.GenerateArray(values, Lpnu.Lb6.Task1Iterative.DefaultLow, Lpnu.Lb6.Task1Iterative.DefaultHigh);
                Console.WriteLine("Original array:");
                Lpnu.Lb6.Task1Iterative.PrintArray(values, Console.Out);

                var count = Lpnu.Lb6.Task1Iterative.CountElements(values);
                var sum = Lpnu.Lb6.Task1Iterative.SumElements(values);
                Lpnu.Lb6.Task1Iterative.ZeroElements(values);

                Console.WriteLine($"count = {count}; sum = {sum}");
                Console.WriteLine("Modified array (selected elements replaced by 0):");
                Lpnu.Lb6.Task1Iterative.PrintArray(values, Console.Out);
            }
            break;
        }
        case "6.2":
        {
            Console.WriteLine("=== Task 6.2: Search for Maximum Even Element Index (Variant 17) ===");
            var n = ReadInt("n (>0): ");
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "n must be positive.");
            var values = new int[n];
            if (recursive)
            {
                Console.WriteLine("[Recursive approach]");
                Lpnu.Lb6.Task2Recursive.InitArrayRec(values);
                Console.WriteLine("Array elements:");
                Lpnu.Lb6.Task2Recursive.PrintArrayRec(values, Console.Out);
                var index = Lpnu.Lb6.Task2Recursive.FindMaxEvenIndexRec(values);
                Console.WriteLine(index < 0
                    ? "Result: No even elements found in the array."
                    : $"Result: Max Even Value = {values[index]} at Index = {index}");
            }
            else
            {
                Console.WriteLine("[Iterative approach]");
                Lpnu.Lb6.Task2Iterative.InitArray(values);
                Console.WriteLine("Array elements:");
                Lpnu.Lb6.Task2Iterative.PrintArray(values, Console.Out);
                var index = Lpnu.Lb6.Task2Iterative.FindMaxEvenIndex(values);
                Console.WriteLine(index < 0
                    ? "Result: No even elements found in the array."
                    : $"Result: Max Even Value = {values[index]} at Index = {index}");
            }
            break;
        }
        case "6.3":
        {
            Console.WriteLine("=== Task 6.3: Array Input via Ordinary and Generic Functions ===");
            var n = ReadInt("Array size (>0): ");
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "Array size must be positive.");
            var integers = new int[n];
            var doubles = new double[n];
            var strings = new string[n];

            if (recursive)
            {
                Console.WriteLine("[Recursive approach]");
                Console.WriteLine("Enter integer elements:");
                Lpnu.Lb6.Task3Recursive.InputArrayRec(integers, Console.In, 0, Console.Out);
                Console.WriteLine("Formatted integers:");
                Lpnu.Lb6.Task3Recursive.PrintArrayRec(integers, Console.Out);

                Console.WriteLine("Enter double elements:");
                Lpnu.Lb6.Task3Recursive.InputArrayTemplateRec(doubles, Console.In,
                    text => double.Parse(text, NumberStyles.Float, CultureInfo.InvariantCulture), 0, Console.Out);
                Console.WriteLine("Formatted doubles:");
                Lpnu.Lb6.Task3Recursive.PrintArrayTemplateRec(doubles, Console.Out);

                Console.WriteLine("Enter string elements:");
                Lpnu.Lb6.Task3Recursive.InputArrayTemplateRec(strings, Console.In, text => text, 0, Console.Out);
                Console.WriteLine("Formatted strings:");
                Lpnu.Lb6.Task3Recursive.PrintArrayTemplateRec(strings, Console.Out);
            }
            else
            {
                Console.WriteLine("[Iterative approach]");
                Console.WriteLine("Enter integer elements:");
                Lpnu.Lb6.Task3Iterative.InputArray(integers, Console.In, Console.Out);
                Console.WriteLine("Formatted integers:");
                Lpnu.Lb6.Task3Iterative.PrintArray(integers, Console.Out);

                Console.WriteLine("Enter double elements:");
                Lpnu.Lb6.Task3Iterative.InputArrayTemplate(doubles, Console.In,
                    text => double.Parse(text, NumberStyles.Float, CultureInfo.InvariantCulture), Console.Out);
                Console.WriteLine("Formatted doubles:");
                Lpnu.Lb6.Task3Iterative.PrintArrayTemplate(doubles, Console.Out);

                Console.WriteLine("Enter string elements:");
                Lpnu.Lb6.Task3Iterative.InputArrayTemplate(strings, Console.In, text => text, Console.Out);
                Console.WriteLine("Formatted strings:");
                Lpnu.Lb6.Task3Iterative.PrintArrayTemplate(strings, Console.Out);
            }
            break;
        }
        case "6.4":
        {
            Console.WriteLine("=== Task 6.4: Dynamic Real Array Analysis and In-Place Stable Partition ===");
            var n = ReadInt("Array size (>0): ");
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "Array size must be positive.");
            var values = new double[n];
            Console.Write("Initialize randomly [y/N]? ");
            var genChoice = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (genChoice is "y" or "yes")
            {
                if (recursive) Task4Recursive.InitArrayRec(values);
                else Task4Iterative.InitArray(values);
            }
            else
            {
                for (var i = 0; i < n; i++) values[i] = ReadDouble($"a[{i}]: ");
            }

            Console.WriteLine(recursive ? "[Recursive approach]" : "[Iterative approach]");
            Console.WriteLine("Initial array:");
            if (recursive) Task4Recursive.PrintArrayRec(values, Console.Out);
            else Task4Iterative.PrintArray(values, Console.Out);

            var positiveCount = recursive ? Task4Recursive.CountPositiveRec(values) : Task4Iterative.CountPositive(values);
            Console.WriteLine($"Positive elements count: {positiveCount}");

            var lastZero = recursive
                ? Task4Recursive.FindLastZeroIndexRec(values, values.Length - 1)
                : Task4Iterative.FindLastZeroIndex(values);

            if (lastZero < 0)
            {
                Console.WriteLine("No zero elements found in array; sum after last zero is 0.00.");
            }
            else
            {
                var sumAfter = recursive
                    ? Task4Recursive.SumFromIndexRec(values, lastZero + 1)
                    : Task4Iterative.SumAfterIndex(values, lastZero);
                Console.WriteLine($"Sum after last zero (at index {lastZero}): {sumAfter:F2}");
            }

            if (recursive) Task4Recursive.PartitionByIntPartRec(values);
            else Task4Iterative.PartitionByIntPart(values);

            Console.WriteLine("Modified array (stable partition: trunc(x) <= 1 first):");
            if (recursive) Task4Recursive.PrintArrayRec(values, Console.Out);
            else Task4Iterative.PrintArray(values, Console.Out);
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
