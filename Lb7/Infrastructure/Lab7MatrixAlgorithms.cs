using Lpnu.Lb7.Core;

namespace Lpnu.Lb7.Infrastructure;

/// <summary>Implements Lab 7 matrix exercises in iterative and recursive forms.</summary>
internal sealed class Lab7MatrixAlgorithms : ILab7MatrixAlgorithms
{
    /// <summary>True when an integer is odd or not divisible by three, equivalently not divisible by six.</summary>
    public bool IsSelected(int value) => value % 6 != 0;

    /// <summary>
    /// Counts and sums selected elements and replaces them with zero. The recursive
    /// variant scans row-major order just like the iterative variant.
    /// </summary>
    public (int Count, long Sum) ZeroSelected(int[,] matrix, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        var result = recursive ? ScanAndZeroRecursive(matrix, 0, 0) : ScanAndZeroIterative(matrix);
        return result;
    }

    /// <summary>
    /// Stable-sorts complete rows lexicographically by columns 0, 1, and 3.
    /// The recursive implementation is merge sort; equal keys retain source order.
    /// </summary>
    public void SortRows(int[,] matrix, bool recursive)
    {
        ValidateColumns(matrix, 5);
        var rows = Enumerable.Range(0, matrix.GetLength(0))
            .Select(row => Enumerable.Range(0, matrix.GetLength(1)).Select(column => matrix[row, column]).ToArray())
            .ToArray();
        if (recursive) MergeSort(rows, 0, rows.Length);
        else
        {
            for (var i = 1; i < rows.Length; i++)
            {
                var row = rows[i];
                var j = i;
                while (j > 0 && CompareRows(row, rows[j - 1]) < 0) { rows[j] = rows[j - 1]; j--; }
                rows[j] = row;
            }
        }
        WriteRows(matrix, rows, 0);
    }

    /// <summary>
    /// For each complete pair of rows, swaps the first minimum of the odd-numbered
    /// (zero-based even) row with the first maximum of the following row.
    /// </summary>
    public void SwapRowExtremes(int[,] matrix, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        if (matrix.GetLength(1) == 0) throw new ArgumentException("Matrix must have at least one column.", nameof(matrix));
        if (recursive) SwapPairsRecursive(matrix, 0);
        else for (var row = 0; row + 1 < matrix.GetLength(0); row += 2) ProcessPair(matrix, row, false);
    }

    /// <summary>
    /// Places the matrix's n largest values on its main diagonal in descending order.
    /// At stage d, it selects the row-major first maximum not on an already-filled
    /// diagonal cell and swaps it into (d,d), preserving deterministic tie handling.
    /// </summary>
    public void PlaceLargestValuesOnDiagonal(double[,] matrix, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        var n = matrix.GetLength(0);
        if (n == 0 || matrix.GetLength(1) != n) throw new ArgumentException("Matrix must be non-empty and square.", nameof(matrix));
        if (recursive) PlaceDiagonalStage(matrix, 0);
        else
        {
            for (var d = 0; d < n; d++)
            {
                var maxRow = -1; var maxColumn = -1;
                for (var row = 0; row < n; row++)
                    for (var column = 0; column < n; column++)
                        if (!(row == column && row < d) &&
                            (maxRow < 0 || matrix[row, column] > matrix[maxRow, maxColumn]))
                        { maxRow = row; maxColumn = column; }
                Swap(matrix, d, d, maxRow, maxColumn);
            }
        }
    }

    /// <summary>Returns the first row containing no positive value, or -1 if all rows do.</summary>
    public int FindFirstRowWithoutPositive(double[,] matrix, bool recursive)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        return recursive ? FindRowRecursive(matrix, 0) : FindRowIterative(matrix);
    }

    private (int Count, long Sum) ScanAndZeroIterative(int[,] matrix)
    {
        var count = 0; long sum = 0;
        for (var r = 0; r < matrix.GetLength(0); r++)
            for (var c = 0; c < matrix.GetLength(1); c++)
                if (IsSelected(matrix[r, c])) { count++; sum += matrix[r, c]; matrix[r, c] = 0; }
        return (count, sum);
    }

    private (int Count, long Sum) ScanAndZeroRecursive(int[,] matrix, int row, int column)
    {
        if (row >= matrix.GetLength(0)) return (0, 0);
        var nextRow = column + 1 == matrix.GetLength(1) ? row + 1 : row;
        var nextColumn = column + 1 == matrix.GetLength(1) ? 0 : column + 1;
        var rest = ScanAndZeroRecursive(matrix, nextRow, nextColumn);
        if (!IsSelected(matrix[row, column])) return rest;
        var value = matrix[row, column];
        matrix[row, column] = 0;
        return (rest.Count + 1, rest.Sum + value);
    }

    private static void ValidateColumns(int[,] matrix, int minimumColumns)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        if (matrix.GetLength(0) == 0 || matrix.GetLength(1) < minimumColumns)
            throw new ArgumentException($"Matrix must have rows and at least {minimumColumns} columns.", nameof(matrix));
    }

    private static int CompareRows(int[] left, int[] right)
    {
        foreach (var column in new[] { 0, 1, 3 })
        {
            var comparison = left[column].CompareTo(right[column]);
            if (comparison != 0) return comparison;
        }
        return 0;
    }

    private static void MergeSort(int[][] rows, int start, int end)
    {
        if (end - start < 2) return;
        var middle = start + (end - start) / 2;
        MergeSort(rows, start, middle);
        MergeSort(rows, middle, end);
        var merged = new int[end - start][];
        var left = start; var right = middle; var target = 0;
        while (left < middle && right < end)
            merged[target++] = CompareRows(rows[left], rows[right]) <= 0 ? rows[left++] : rows[right++];
        while (left < middle) merged[target++] = rows[left++];
        while (right < end) merged[target++] = rows[right++];
        Array.Copy(merged, 0, rows, start, merged.Length);
    }

    private static void WriteRows(int[,] matrix, int[][] rows, int row)
    {
        if (row == rows.Length) return;
        for (var column = 0; column < matrix.GetLength(1); column++) matrix[row, column] = rows[row][column];
        WriteRows(matrix, rows, row + 1);
    }

    private static void ProcessPair(int[,] matrix, int firstRow, bool recursive)
    {
        var minIndex = recursive ? FindMinColumnRecursive(matrix, firstRow, 1, 0) : 0;
        var maxIndex = recursive ? FindMaxColumnRecursive(matrix, firstRow + 1, 1, 0) : 0;
        if (!recursive)
        {
            for (var column = 1; column < matrix.GetLength(1); column++)
            {
                if (matrix[firstRow, column] < matrix[firstRow, minIndex]) minIndex = column;
                if (matrix[firstRow + 1, column] > matrix[firstRow + 1, maxIndex]) maxIndex = column;
            }
        }
        (matrix[firstRow, minIndex], matrix[firstRow + 1, maxIndex]) =
            (matrix[firstRow + 1, maxIndex], matrix[firstRow, minIndex]);
    }

    private static void SwapPairsRecursive(int[,] matrix, int firstRow)
    {
        if (firstRow + 1 >= matrix.GetLength(0)) return;
        ProcessPair(matrix, firstRow, true);
        SwapPairsRecursive(matrix, firstRow + 2);
    }

    private static int FindMinColumnRecursive(int[,] matrix, int row, int column, int best) =>
        column == matrix.GetLength(1) ? best :
        FindMinColumnRecursive(matrix, row, column + 1, matrix[row, column] < matrix[row, best] ? column : best);

    private static int FindMaxColumnRecursive(int[,] matrix, int row, int column, int best) =>
        column == matrix.GetLength(1) ? best :
        FindMaxColumnRecursive(matrix, row, column + 1, matrix[row, column] > matrix[row, best] ? column : best);

    private static void PlaceDiagonalStage(double[,] matrix, int d)
    {
        var n = matrix.GetLength(0);
        if (d == n) return;
        var best = FindMaximumCell(matrix, d, 0, 0, -1, -1);
        Swap(matrix, d, d, best.Row, best.Column);
        PlaceDiagonalStage(matrix, d + 1);
    }

    private static (int Row, int Column) FindMaximumCell(
        double[,] matrix, int d, int row, int column, int bestRow, int bestColumn)
    {
        var n = matrix.GetLength(0);
        if (row == n) return (bestRow, bestColumn);
        var eligible = !(row == column && row < d);
        if (eligible && (bestRow < 0 || matrix[row, column] > matrix[bestRow, bestColumn]))
        { bestRow = row; bestColumn = column; }
        var nextRow = column + 1 == n ? row + 1 : row;
        var nextColumn = column + 1 == n ? 0 : column + 1;
        return FindMaximumCell(matrix, d, nextRow, nextColumn, bestRow, bestColumn);
    }

    private static int FindRowIterative(double[,] matrix)
    {
        for (var row = 0; row < matrix.GetLength(0); row++)
        {
            var hasPositive = false;
            for (var column = 0; column < matrix.GetLength(1); column++)
                if (matrix[row, column] > 0) { hasPositive = true; break; }
            if (!hasPositive) return row;
        }
        return -1;
    }

    private static int FindRowRecursive(double[,] matrix, int row)
    {
        if (row == matrix.GetLength(0)) return -1;
        return RowHasPositive(matrix, row, 0) ? FindRowRecursive(matrix, row + 1) : row;
    }

    private static bool RowHasPositive(double[,] matrix, int row, int column) =>
        column == matrix.GetLength(1) ? false :
        matrix[row, column] > 0 || RowHasPositive(matrix, row, column + 1);

    private static void Swap(double[,] matrix, int r1, int c1, int r2, int c2) =>
        (matrix[r1, c1], matrix[r2, c2]) = (matrix[r2, c2], matrix[r1, c1]);
}
