using Lpnu.Lb6.Core;

namespace Lpnu.Lb6.Infrastructure;

/// <summary>Internal implementation of Lab 6 array algorithms in both loop and recursive forms.</summary>
internal sealed class Lab6ArrayAlgorithms : ILab6ArrayAlgorithms
{
    /// <summary>
    /// Selects r[i] exactly when r[i] is divisible by six, or its zero-based index
    /// is not divisible by five. Thus only nonmultiples of six at indices 0,5,10,...
    /// are excluded.
    /// </summary>
    public bool IsSelected(int value, int index) => Task1Iterative.IsSelected(value, index);

    /// <summary>Counts and sums values selected by the Lab 6.1 predicate.</summary>
    public (int Count, int Sum) AnalyzeSelected(int[] values, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        return recursive
            ? (Task1Recursive.CountElementsRec(values), Task1Recursive.SumElementsRec(values))
            : (Task1Iterative.CountElements(values), Task1Iterative.SumElements(values));
    }

    /// <summary>Replaces all selected values with zero, preserving all other elements.</summary>
    public void ZeroSelected(int[] values, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        if (recursive) Task1Recursive.ZeroElementsRec(values);
        else Task1Iterative.ZeroElements(values);
    }

    /// <summary>
    /// Returns the first index holding the largest even value. Zero and negative even
    /// values qualify; -1 denotes that the array contains no even value.
    /// </summary>
    public int FindMaximumEvenIndex(int[] values, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        return recursive ? Task2Recursive.FindMaxEvenIndexRec(values, 0, -1) : Task2Iterative.FindMaxEvenIndex(values);
    }

    /// <summary>Reads array values using the dedicated int overload, iteratively or recursively.</summary>
    public void InputIntArray(int[] values, TextReader input, TextWriter output, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(output);
        if (values.Length == 0) throw new ArgumentException("Array size must be positive.", nameof(values));
        if (recursive) InputIntRecursive(values, 0, input, output);
        else
            for (var i = 0; i < values.Length; i++)
                values[i] = ReadIntElement(i, input, output);
    }

    /// <summary>Reads typed values via a caller-supplied parser, demonstrating generic input.</summary>
    public void InputGenericArray<T>(T[] values, TextReader input, TextWriter output, Func<string, T> parser, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values); ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(output); ArgumentNullException.ThrowIfNull(parser);
        if (values.Length == 0) throw new ArgumentException("Array size must be positive.", nameof(values));
        if (recursive) InputRecursive(values, 0, input, output, parser);
        else for (var i = 0; i < values.Length; i++) values[i] = ReadElement(i, input, output, parser);
    }

    /// <summary>Counts positive values in the array.</summary>
    public int CountPositive(double[] values, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        return recursive ? CountPositiveRecursive(values, 0) : values.Count(value => value > 0);
    }

    /// <summary>Returns the last index exactly equal to zero, or -1 when no zero exists.</summary>
    public int FindLastZeroIndex(double[] values, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        return recursive ? FindLastZeroRecursive(values, values.Length - 1) :
            Array.FindLastIndex(values, value => value == 0.0);
    }

    /// <summary>Sums elements strictly after the specified index.</summary>
    public double SumAfterIndex(double[] values, int index, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        if (index < -1 || index >= values.Length) throw new ArgumentOutOfRangeException(nameof(index));
        return recursive ? SumAfterRecursive(values, index + 1) : values.Skip(index + 1).Sum();
    }

    /// <summary>
    /// Stable-partitions in place by trunc(x)&lt;=1, equivalent for real values to x&lt;2.
    /// The temporary storage preserves the original order within each partition.
    /// </summary>
    public void PartitionByIntegerPart(double[] values, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        var output = new double[values.Length];
        if (recursive) PartitionRecursive(values, output, 0, 0, CountFirstGroup(values, 0));
        else
        {
            var first = 0;
            foreach (var value in values) if (value < 2) output[first++] = value;
            var second = first;
            foreach (var value in values) if (value >= 2) output[second++] = value;
        }
        Array.Copy(output, values, values.Length);
    }


    private static void InputRecursive<T>(T[] values, int i, TextReader input, TextWriter output, Func<string, T> parser)
    {
        if (i >= values.Length) return;
        values[i] = ReadElement(i, input, output, parser);
        InputRecursive(values, i + 1, input, output, parser);
    }

    private static T ReadElement<T>(int i, TextReader input, TextWriter output, Func<string, T> parser)
    {
        output.Write($"a[{i}] = ");
        return parser(input.ReadLine() ?? throw new EndOfStreamException("Input ended before all array elements were read."));
    }

    private static int ReadIntElement(int i, TextReader input, TextWriter output)
    {
        output.Write($"a[{i}] = ");
        var text = input.ReadLine() ?? throw new EndOfStreamException("Input ended before all array elements were read.");
        return int.TryParse(text, out var value) ? value : throw new FormatException("Expected an integer.");
    }

    private static void InputIntRecursive(int[] values, int i, TextReader input, TextWriter output)
    {
        if (i >= values.Length) return;
        values[i] = ReadIntElement(i, input, output);
        InputIntRecursive(values, i + 1, input, output);
    }

    private static int CountPositiveRecursive(double[] values, int i) =>
        i == values.Length ? 0 : (values[i] > 0 ? 1 : 0) + CountPositiveRecursive(values, i + 1);

    private static int FindLastZeroRecursive(double[] values, int i) =>
        i < 0 ? -1 : values[i] == 0.0 ? i : FindLastZeroRecursive(values, i - 1);

    private static double SumAfterRecursive(double[] values, int i) =>
        i >= values.Length ? 0 : values[i] + SumAfterRecursive(values, i + 1);

    private static int CountFirstGroup(double[] values, int i) =>
        i == values.Length ? 0 : (values[i] < 2 ? 1 : 0) + CountFirstGroup(values, i + 1);

    private static void PartitionRecursive(double[] source, double[] target, int i, int firstPosition, int firstCount)
    {
        if (i == source.Length) return;
        var position = source[i] < 2 ? firstPosition : firstCount + i - firstPosition;
        target[position] = source[i];
        PartitionRecursive(source, target, i + 1, firstPosition + (source[i] < 2 ? 1 : 0), firstCount);
    }
}
