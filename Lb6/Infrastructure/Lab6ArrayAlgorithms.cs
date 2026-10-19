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
        if (recursive) Task3Recursive.InputArrayRec(values, input, 0, output);
        else Task3Iterative.InputArray(values, input, output);
    }

    /// <summary>Reads typed values via a caller-supplied parser, demonstrating generic input.</summary>
    public void InputGenericArray<T>(T[] values, TextReader input, TextWriter output, Func<string, T> parser, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(output);
        ArgumentNullException.ThrowIfNull(parser);
        if (values.Length == 0) throw new ArgumentException("Array size must be positive.", nameof(values));
        if (recursive) Task3Recursive.InputArrayTemplateRec(values, input, parser, 0, output);
        else Task3Iterative.InputArrayTemplate(values, input, parser, output);
    }

    /// <summary>Counts positive values in the array.</summary>
    public int CountPositive(double[] values, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        return recursive ? Task4Recursive.CountPositiveRec(values) : Task4Iterative.CountPositive(values);
    }

    /// <summary>Returns the last index equal to zero within epsilon tolerance, or -1 when no zero exists.</summary>
    public int FindLastZeroIndex(double[] values, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        return recursive
            ? Task4Recursive.FindLastZeroIndexRec(values, values.Length - 1)
            : Task4Iterative.FindLastZeroIndex(values);
    }

    /// <summary>Sums elements strictly after the specified index.</summary>
    public double SumAfterIndex(double[] values, int index, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        if (index < -1 || index >= values.Length) throw new ArgumentOutOfRangeException(nameof(index));
        return recursive
            ? Task4Recursive.SumFromIndexRec(values, index + 1)
            : Task4Iterative.SumAfterIndex(values, index);
    }

    /// <summary>
    /// Stable-partitions in place by trunc(x)&lt;=1, equivalent for real values to x&lt;2.
    /// Preserves original relative order with O(1) auxiliary space.
    /// </summary>
    public void PartitionByIntegerPart(double[] values, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(values);
        if (recursive)
        {
            Task4Recursive.PartitionByIntPartRec(values);
        }
        else
        {
            Task4Iterative.PartitionByIntPart(values);
        }
    }
}
