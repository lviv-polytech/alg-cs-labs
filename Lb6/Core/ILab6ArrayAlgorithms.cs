namespace Lpnu.Lb6.Core;

/// <summary>Contracts for iterative and recursive one-dimensional array exercises.</summary>
public interface ILab6ArrayAlgorithms
{
    bool IsSelected(int value, int index);
    (int Count, int Sum) AnalyzeSelected(int[] values, bool recursive);
    void ZeroSelected(int[] values, bool recursive);
    int FindMaximumEvenIndex(int[] values, bool recursive);
    void InputIntArray(int[] values, TextReader input, TextWriter output, bool recursive);
    void InputGenericArray<T>(T[] values, TextReader input, TextWriter output, Func<string, T> parser, bool recursive);
    int CountPositive(double[] values, bool recursive);
    int FindLastZeroIndex(double[] values, bool recursive);
    double SumAfterIndex(double[] values, int index, bool recursive);
    void PartitionByIntegerPart(double[] values, bool recursive);
}
