namespace Lpnu.Lb7.Core;

/// <summary>Contracts for matrix scans, stable ordering, swaps, and diagonal selection.</summary>
public interface ILab7MatrixAlgorithms
{
    bool IsSelected(int value);
    (int Count, long Sum) ZeroSelected(int[,] matrix, bool recursive);
    void SortRows(int[,] matrix, bool recursive);
    void SwapRowExtremes(int[,] matrix, bool recursive);
    void PlaceLargestValuesOnDiagonal(double[,] matrix, bool recursive);
    int FindFirstRowWithoutPositive(double[,] matrix, bool recursive);
}
