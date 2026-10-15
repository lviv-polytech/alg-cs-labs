using System;
using System.IO;

namespace Lpnu.Lb6;

/// <summary>
/// Iterative implementation of Task 6.2 (Variant 17):
/// Finding the index of the maximum even element in a one-dimensional array.
/// </summary>
public static class Task2Iterative
{
    public const int DefaultMinVal = -50;
    public const int DefaultMaxVal = 50;

    /// <summary>
    /// Checks whether an integer is even.
    /// Condition: value % 2 == 0.
    /// Accurately classifies positive evens, negative evens, and zero.
    /// </summary>
    /// <param name="value">Integer value to evaluate.</param>
    /// <returns>True if divisible by 2, false otherwise.</returns>
    public static bool IsEven(int value) => value % 2 == 0;

    /// <summary>
    /// Populates the array iteratively with uniform pseudo-random integers in [minVal, maxVal].
    /// </summary>
    /// <param name="array">Array to populate.</param>
    /// <param name="minVal">Inclusive minimum value (default: -50).</param>
    /// <param name="maxVal">Inclusive maximum value (default: 50).</param>
    /// <param name="random">Optional custom Random instance.</param>
    public static void InitArray(int[] array, int minVal = DefaultMinVal, int maxVal = DefaultMaxVal, Random? random = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (minVal > maxVal)
            throw new ArgumentException("Min value cannot be greater than max value.", nameof(minVal));

        var rng = random ?? Random.Shared;
        for (var i = 0; i < array.Length; i++)
        {
            array[i] = rng.Next(minVal, maxVal + 1);
        }
    }

    /// <summary>
    /// Prints the array elements formatted into a single line with fixed 5-character column width.
    /// </summary>
    /// <param name="array">Array of integers to print.</param>
    /// <param name="writer">Output text writer.</param>
    public static void PrintArray(int[] array, TextWriter writer)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        for (var i = 0; i < array.Length; i++)
        {
            writer.Write($"{array[i],5}");
        }
        writer.WriteLine();
    }

    /// <summary>
    /// Formats the array elements into a single line string with fixed 5-character column width.
    /// </summary>
    /// <param name="array">Array of integers to format.</param>
    /// <returns>Formatted string representation.</returns>
    public static string FormatArray(int[]? array)
    {
        if (array == null) return string.Empty;
        using var sw = new StringWriter();
        PrintArray(array, sw);
        return sw.ToString().TrimEnd();
    }

    /// <summary>
    /// Finds the zero-based index of the maximum even element iteratively.
    ///
    /// Mathematical model:
    ///   k_{-1} = -1
    ///   k_i = i, if (a[i] % 2 == 0) and (k_{i-1} == -1 or a[i] > a[k_{i-1}])
    ///   k_i = k_{i-1}, otherwise
    ///
    /// Invariant: k_i contains the first occurrence of the largest even element on prefix a[0..i].
    /// Complexity: Time O(n), Space O(1).
    /// Pure function: does not perform console output.
    /// </summary>
    /// <param name="array">Input array of integers.</param>
    /// <returns>Index of first maximum even element, or -1 if no even elements exist.</returns>
    public static int FindMaxEvenIndex(int[]? array)
    {
        if (array == null || array.Length == 0) return -1;

        var bestIndex = -1;
        for (var i = 0; i < array.Length; i++)
        {
            if (IsEven(array[i]))
            {
                if (bestIndex == -1 || array[i] > array[bestIndex])
                {
                    bestIndex = i;
                }
            }
        }
        return bestIndex;
    }
}

/// <summary>
/// Recursive implementation of Task 6.2 (Variant 17):
/// Finding the index of the maximum even element in a one-dimensional array.
/// </summary>
public static class Task2Recursive
{
    public const int DefaultMinVal = -50;
    public const int DefaultMaxVal = 50;

    /// <summary>
    /// Checks whether an integer is even.
    /// </summary>
    public static bool IsEven(int value) => value % 2 == 0;

    /// <summary>
    /// Populates the array recursively with uniform pseudo-random integers in [minVal, maxVal].
    /// </summary>
    public static void InitArrayRec(int[] array, int minVal = DefaultMinVal, int maxVal = DefaultMaxVal, int index = 0, Random? random = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (minVal > maxVal)
            throw new ArgumentException("Min value cannot be greater than max value.", nameof(minVal));
        if (index >= array.Length) return;

        var rng = random ?? Random.Shared;
        array[index] = rng.Next(minVal, maxVal + 1);
        InitArrayRec(array, minVal, maxVal, index + 1, rng);
    }

    /// <summary>
    /// Prints the array elements recursively formatted into a single line with fixed 5-character column width.
    /// </summary>
    public static void PrintArrayRec(int[] array, TextWriter writer, int index = 0)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        if (index >= array.Length)
        {
            writer.WriteLine();
            return;
        }
        writer.Write($"{array[index],5}");
        PrintArrayRec(array, writer, index + 1);
    }

    /// <summary>
    /// Formats the array elements recursively into a single line string.
    /// </summary>
    public static string FormatArrayRec(int[]? array, int index = 0)
    {
        if (array == null || index >= array.Length) return string.Empty;
        return $"{array[index],5}" + FormatArrayRec(array, index + 1);
    }

    /// <summary>
    /// Finds the zero-based index of the maximum even element recursively starting from index 0.
    /// Pure function: does not perform console output.
    /// </summary>
    public static int FindMaxEvenIndexRec(int[]? array)
    {
        if (array == null || array.Length == 0) return -1;
        return FindMaxEvenIndexRec(array, 0, -1);
    }

    /// <summary>
    /// Tail-recursive implementation of maximum even index search.
    ///
    /// Recurrence:
    ///   F(A, n, i, best) = best, if i >= n
    ///   F(A, n, i, best) = F(A, n, i + 1, i), if (a[i] % 2 == 0) and (best == -1 or a[i] > a[best])
    ///   F(A, n, i, best) = F(A, n, i + 1, best), otherwise
    /// </summary>
    /// <param name="array">Input array of integers.</param>
    /// <param name="index">Current inspection index.</param>
    /// <param name="bestIndex">Index of current best candidate.</param>
    /// <returns>Index of first maximum even element, or -1 if no even elements exist.</returns>
    public static int FindMaxEvenIndexRec(int[]? array, int index, int bestIndex)
    {
        if (array == null || index >= array.Length) return bestIndex;

        var nextBest = bestIndex;
        if (IsEven(array[index]))
        {
            if (bestIndex == -1 || array[index] > array[bestIndex])
            {
                nextBest = index;
            }
        }
        return FindMaxEvenIndexRec(array, index + 1, nextBest);
    }
}
