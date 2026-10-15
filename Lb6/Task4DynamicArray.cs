using System;
using System.Globalization;
using System.IO;

namespace Lpnu.Lb6;

/// <summary>
/// Iterative implementation of Task 6.4:
/// Processing a dynamic one-dimensional array of real numbers (positive count, sum after last zero, in-place stable partition).
/// </summary>
public static class Task4Iterative
{
    public const double Epsilon = 1e-9;
    public const int DefaultFieldWidth = 8;
    public const int DefaultPrecision = 2;

    /// <summary>
    /// Checks if a floating-point number is considered zero within tolerance eps.
    /// </summary>
    public static bool IsZero(double x, double eps = Epsilon) => Math.Abs(x) < eps;

    /// <summary>
    /// Checks whether the integer part (truncation towards zero) of x is at most 1.
    /// Mathematical equivalent: trunc(x) &lt;= 1 &lt;=&gt; x &lt; 2.0.
    /// </summary>
    public static bool HasIntPartAtMostOne(double x) => Math.Truncate(x) <= 1.0;

    /// <summary>
    /// Initializes an array iteratively with uniform pseudo-random real values in [minVal, maxVal].
    /// </summary>
    public static void InitArray(double[] array, double minVal = -5.0, double maxVal = 5.0, Random? random = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (minVal > maxVal)
            throw new ArgumentException("Min value cannot be greater than max value.", nameof(minVal));

        var rng = random ?? Random.Shared;
        for (var i = 0; i < array.Length; i++)
        {
            array[i] = minVal + (maxVal - minVal) * rng.NextDouble();
        }
    }

    /// <summary>
    /// Prints array elements formatted into a single line with fixed column width and precision.
    /// </summary>
    public static void PrintArray(double[] array, TextWriter writer, int fieldWidth = DefaultFieldWidth, int precision = DefaultPrecision)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        writer.WriteLine(FormatArray(array, fieldWidth, precision));
    }

    /// <summary>
    /// Formats array into a single line string with fixed column width and precision.
    /// </summary>
    public static string FormatArray(double[] array, int fieldWidth = DefaultFieldWidth, int precision = DefaultPrecision)
    {
        ArgumentNullException.ThrowIfNull(array);
        var formatSpec = string.Format(CultureInfo.InvariantCulture, "{{0,{0}:F{1}}}", fieldWidth, precision);
        var sb = new System.Text.StringBuilder();
        foreach (var v in array)
        {
            sb.AppendFormat(CultureInfo.InvariantCulture, formatSpec, v);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Task 1.1: Counts positive elements (a[i] &gt; 0.0) iteratively.
    /// Zero is non-positive by mathematical definition.
    /// </summary>
    public static int CountPositive(double[] array)
    {
        ArgumentNullException.ThrowIfNull(array);
        var count = 0;
        foreach (var v in array)
        {
            if (v > 0.0) count++;
        }
        return count;
    }

    /// <summary>
    /// Task 1.2 (Step A): Finds index of the last zero element searching from right to left.
    /// </summary>
    /// <returns>Index in [0, n - 1] or -1 if no zero exists.</returns>
    public static int FindLastZeroIndex(double[] array, double eps = Epsilon)
    {
        ArgumentNullException.ThrowIfNull(array);
        for (var i = array.Length - 1; i >= 0; i--)
        {
            if (IsZero(array[i], eps)) return i;
        }
        return -1;
    }

    /// <summary>
    /// Task 1.2 (Step B): Sums elements located strictly after startIndex.
    /// </summary>
    public static double SumAfterIndex(double[] array, int startIndex)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (startIndex < -1 || startIndex >= array.Length) return 0.0;
        var sum = 0.0;
        for (var i = startIndex + 1; i < array.Length; i++)
        {
            sum += array[i];
        }
        return sum;
    }

    /// <summary>
    /// Task 1.2: Computes sum of elements after the last zero element iteratively.
    /// Returns 0.0 if no zeros exist or if the last zero is the last element.
    /// </summary>
    public static double SumAfterLastZero(double[] array, double eps = Epsilon)
    {
        ArgumentNullException.ThrowIfNull(array);
        var lastZero = FindLastZeroIndex(array, eps);
        if (lastZero == -1 || lastZero >= array.Length - 1) return 0.0;
        return SumAfterIndex(array, lastZero);
    }

    /// <summary>
    /// Task 2: In-place stable partition using cyclic shift: elements with trunc(x) &lt;= 1 precede all
    /// other elements, preserving relative order with O(1) auxiliary space.
    /// </summary>
    public static void PartitionByIntPart(double[] array)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length <= 1) return;

        var writePos = 0;
        for (var i = 0; i < array.Length; i++)
        {
            if (HasIntPartAtMostOne(array[i]))
            {
                var temp = array[i];
                for (var k = i; k > writePos; k--)
                {
                    array[k] = array[k - 1];
                }
                array[writePos] = temp;
                writePos++;
            }
        }
    }
}

/// <summary>
/// Recursive implementation of Task 6.4:
/// Processing a dynamic one-dimensional array of real numbers (positive count, sum after last zero, in-place stable partition).
/// </summary>
public static class Task4Recursive
{
    public const double Epsilon = 1e-9;
    public const int DefaultFieldWidth = 8;
    public const int DefaultPrecision = 2;

    /// <summary>
    /// Checks if a floating-point number is considered zero within tolerance eps.
    /// </summary>
    public static bool IsZero(double x, double eps = Epsilon) => Math.Abs(x) < eps;

    /// <summary>
    /// Checks whether the integer part (truncation towards zero) of x is at most 1.
    /// Mathematical equivalent: trunc(x) &lt;= 1 &lt;=&gt; x &lt; 2.0.
    /// </summary>
    public static bool HasIntPartAtMostOne(double x) => Math.Truncate(x) <= 1.0;

    /// <summary>
    /// Initializes an array recursively with uniform pseudo-random real values in [minVal, maxVal].
    /// </summary>
    public static void InitArrayRec(double[] array, double minVal = -5.0, double maxVal = 5.0, int index = 0, Random? random = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (minVal > maxVal)
            throw new ArgumentException("Min value cannot be greater than max value.", nameof(minVal));
        if (index >= array.Length) return;

        var rng = random ?? Random.Shared;
        array[index] = minVal + (maxVal - minVal) * rng.NextDouble();
        InitArrayRec(array, minVal, maxVal, index + 1, rng);
    }

    /// <summary>
    /// Prints array elements formatted recursively into a single line.
    /// </summary>
    public static void PrintArrayRec(double[] array, TextWriter writer, int fieldWidth = DefaultFieldWidth, int precision = DefaultPrecision)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        writer.WriteLine(FormatArrayRec(array, 0, fieldWidth, precision));
    }

    /// <summary>
    /// Formats array recursively into a single line string with fixed column width and precision.
    /// </summary>
    public static string FormatArrayRec(double[] array, int index = 0, int fieldWidth = DefaultFieldWidth, int precision = DefaultPrecision)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (index >= array.Length) return string.Empty;

        var formatSpec = string.Format(CultureInfo.InvariantCulture, "{{0,{0}:F{1}}}", fieldWidth, precision);
        var current = string.Format(CultureInfo.InvariantCulture, formatSpec, array[index]);
        return current + FormatArrayRec(array, index + 1, fieldWidth, precision);
    }

    /// <summary>
    /// Task 1.1: Counts positive elements (a[i] &gt; 0.0) recursively.
    /// </summary>
    public static int CountPositiveRec(double[] array, int index = 0)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (index >= array.Length) return 0;
        var current = array[index] > 0.0 ? 1 : 0;
        return current + CountPositiveRec(array, index + 1);
    }

    /// <summary>
    /// Task 1.2 (Step A): Recursively searches for last zero element from right to left (index = n - 1 downto 0).
    /// </summary>
    /// <returns>Index in [0, n - 1] or -1 if no zero exists.</returns>
    public static int FindLastZeroIndexRec(double[] array, int index, double eps = Epsilon)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (index < 0) return -1;
        if (IsZero(array[index], eps)) return index;
        return FindLastZeroIndexRec(array, index - 1, eps);
    }

    /// <summary>
    /// Task 1.2 (Step B): Recursively sums suffix of array starting from index.
    /// </summary>
    public static double SumFromIndexRec(double[] array, int index)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (index >= array.Length) return 0.0;
        return array[index] + SumFromIndexRec(array, index + 1);
    }

    /// <summary>
    /// Task 1.2: Computes sum of elements after the last zero element recursively.
    /// </summary>
    public static double SumAfterLastZeroRec(double[] array, double eps = Epsilon)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length == 0) return 0.0;
        var lastZero = FindLastZeroIndexRec(array, array.Length - 1, eps);
        if (lastZero == -1 || lastZero >= array.Length - 1) return 0.0;
        return SumFromIndexRec(array, lastZero + 1);
    }

    /// <summary>
    /// Helper for Task 2: Recursively shifts sub-array a[writePos .. k - 1] one position right.
    /// </summary>
    public static void ShiftRightRec(double[] array, int writePos, int k)
    {
        if (k <= writePos) return;
        array[k] = array[k - 1];
        ShiftRightRec(array, writePos, k - 1);
    }

    /// <summary>
    /// Task 2: In-place recursive stable partition.
    /// </summary>
    public static void PartitionByIntPartRec(double[] array, int index = 0, int writePos = 0)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (index >= array.Length) return;

        var nextWritePos = writePos;
        if (HasIntPartAtMostOne(array[index]))
        {
            var temp = array[index];
            ShiftRightRec(array, writePos, index);
            array[writePos] = temp;
            nextWritePos = writePos + 1;
        }

        PartitionByIntPartRec(array, index + 1, nextWritePos);
    }
}
