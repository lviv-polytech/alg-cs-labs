using System.IO;

namespace Lpnu.Lb6;

/// <summary>
/// Result record for Task 6.1 (Variant 17) holding calculation metrics.
/// </summary>
/// <param name="Count">Number of elements satisfying the criterion.</param>
/// <param name="Sum">Cumulative sum of elements satisfying the criterion.</param>
public readonly record struct Task1Result(int Count, int Sum);

/// <summary>
/// Iterative implementation of Task 6.1 (Variant 17) array operations.
/// Single responsibility: each method performs strictly one role.
/// </summary>
public static class Task1Iterative
{
    public const int RequiredSize = 25;
    public const int DefaultLow = 4;
    public const int DefaultHigh = 73;

    /// <summary>
    /// Evaluates whether an element satisfies the Variant 17 criterion:
    /// (value % 6 == 0) || (index % 5 != 0).
    /// </summary>
    /// <param name="value">Integer value of the element.</param>
    /// <param name="index">Zero-based index of the element.</param>
    /// <returns>True if the condition is satisfied, false otherwise.</returns>
    public static bool IsSelected(int value, int index) => (value % 6 == 0) || (index % 5 != 0);

    /// <summary>
    /// Populates the array iteratively with uniform pseudo-random integers in [low, high].
    /// </summary>
    /// <param name="array">Array of 25 integers to populate.</param>
    /// <param name="low">Inclusive lower bound (default: 4).</param>
    /// <param name="high">Inclusive upper bound (default: 73).</param>
    /// <param name="random">Optional custom Random instance.</param>
    public static void GenerateArray(int[] array, int low = DefaultLow, int high = DefaultHigh, Random? random = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length != RequiredSize)
            throw new ArgumentException($"Array must have exactly {RequiredSize} elements.", nameof(array));
        if (low > high)
            throw new ArgumentException("Low bound cannot be greater than high bound.", nameof(low));

        var rng = random ?? Random.Shared;
        for (var i = 0; i < array.Length; i++)
        {
            array[i] = rng.Next(low, high + 1);
        }
    }

    /// <summary>
    /// Prints the array in a single formatted row with 4-character column width.
    /// </summary>
    /// <param name="array">Array of integers to print.</param>
    /// <param name="writer">Output text writer.</param>
    public static void PrintArray(int[] array, TextWriter writer)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        for (var i = 0; i < array.Length; i++)
        {
            writer.Write($"{array[i],4}");
        }
        writer.WriteLine();
    }

    /// <summary>
    /// Formats the array in a single string with 4-character column width per element.
    /// </summary>
    /// <param name="array">Array of integers to format.</param>
    /// <returns>Formatted string representation of the array.</returns>
    public static string FormatArray(int[] array)
    {
        ArgumentNullException.ThrowIfNull(array);
        using var sw = new StringWriter();
        PrintArray(array, sw);
        return sw.ToString();
    }

    /// <summary>
    /// Computes count of elements matching the criterion iteratively.
    /// Invariant: 20 &lt;= count &lt;= 25 for any valid 25-element array.
    /// </summary>
    /// <param name="array">Array of 25 integers.</param>
    /// <returns>Count of matching elements.</returns>
    public static int CountElements(int[] array)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length != RequiredSize)
            throw new ArgumentException($"Array must have exactly {RequiredSize} elements.", nameof(array));

        var count = 0;
        for (var i = 0; i < array.Length; i++)
        {
            if (IsSelected(array[i], i)) count++;
        }
        return count;
    }

    /// <summary>
    /// Computes sum of elements matching the criterion iteratively.
    /// Invariant: 80 &lt;= sum &lt;= 1825 for any valid 25-element array in [4, 73].
    /// </summary>
    /// <param name="array">Array of 25 integers.</param>
    /// <returns>Cumulative sum of matching elements.</returns>
    public static int SumElements(int[] array)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length != RequiredSize)
            throw new ArgumentException($"Array must have exactly {RequiredSize} elements.", nameof(array));

        var sum = 0;
        for (var i = 0; i < array.Length; i++)
        {
            if (IsSelected(array[i], i)) sum += array[i];
        }
        return sum;
    }

    /// <summary>
    /// Replaces elements matching the criterion with zero iteratively in-place.
    /// CRITICAL: Must be invoked strictly AFTER CountElements and SumElements.
    /// </summary>
    /// <param name="array">Array of 25 integers to modify.</param>
    public static void ZeroElements(int[] array)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length != RequiredSize)
            throw new ArgumentException($"Array must have exactly {RequiredSize} elements.", nameof(array));

        for (var i = 0; i < array.Length; i++)
        {
            if (IsSelected(array[i], i)) array[i] = 0;
        }
    }

    /// <summary>
    /// High-level pipeline: counts, sums, then zeros elements in-place in correct order.
    /// </summary>
    /// <param name="array">Array of 25 integers.</param>
    /// <returns>Task1Result containing count and sum.</returns>
    public static Task1Result ProcessSelection(int[] array)
    {
        var count = CountElements(array);
        var sum = SumElements(array);
        ZeroElements(array);
        return new Task1Result(count, sum);
    }
}

/// <summary>
/// Recursive implementation of Task 6.1 (Variant 17) array operations.
/// </summary>
public static class Task1Recursive
{
    public const int RequiredSize = 25;
    public const int DefaultLow = 4;
    public const int DefaultHigh = 73;

    public static bool IsSelected(int value, int index) => (value % 6 == 0) || (index % 5 != 0);

    public static void GenerateArrayRec(int[] array, int low = DefaultLow, int high = DefaultHigh, int index = 0, Random? random = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length != RequiredSize)
            throw new ArgumentException($"Array must have exactly {RequiredSize} elements.", nameof(array));
        if (index >= array.Length) return;

        var rng = random ?? Random.Shared;
        array[index] = rng.Next(low, high + 1);
        GenerateArrayRec(array, low, high, index + 1, rng);
    }

    public static void PrintArrayRec(int[] array, TextWriter writer, int index = 0)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        if (index >= array.Length)
        {
            writer.WriteLine();
            return;
        }
        writer.Write($"{array[index],4}");
        PrintArrayRec(array, writer, index + 1);
    }

    public static string FormatArrayRec(int[] array, int index = 0)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (index >= array.Length) return Environment.NewLine;
        return $"{array[index],4}" + FormatArrayRec(array, index + 1);
    }

    public static int CountElementsRec(int[] array, int index = 0)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length != RequiredSize)
            throw new ArgumentException($"Array must have exactly {RequiredSize} elements.", nameof(array));
        if (index >= array.Length) return 0;

        var match = IsSelected(array[index], index) ? 1 : 0;
        return match + CountElementsRec(array, index + 1);
    }

    public static int SumElementsRec(int[] array, int index = 0)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length != RequiredSize)
            throw new ArgumentException($"Array must have exactly {RequiredSize} elements.", nameof(array));
        if (index >= array.Length) return 0;

        var contribution = IsSelected(array[index], index) ? array[index] : 0;
        return contribution + SumElementsRec(array, index + 1);
    }

    public static void ZeroElementsRec(int[] array, int index = 0)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length != RequiredSize)
            throw new ArgumentException($"Array must have exactly {RequiredSize} elements.", nameof(array));
        if (index >= array.Length) return;

        if (IsSelected(array[index], index)) array[index] = 0;
        ZeroElementsRec(array, index + 1);
    }

    public static Task1Result ProcessSelectionRec(int[] array, int index = 0)
    {
        var count = CountElementsRec(array, index);
        var sum = SumElementsRec(array, index);
        ZeroElementsRec(array, index);
        return new Task1Result(count, sum);
    }
}
