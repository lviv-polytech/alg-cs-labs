using Lpnu.Lb7.Infrastructure;

namespace Lpnu.Lb7.Tests;

public class Lab7MatrixAlgorithmsTests
{
    private readonly Lpnu.Lb7.Core.ILab7MatrixAlgorithms _algorithms = Lab7ServiceFactory.CreateMatrixAlgorithms();

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void MatrixSelectionCountsSumsAndZeros(bool recursive)
    {
        int[,] matrix = { { 11, 12, 18 }, { 24, 30, 35 } };
        var result = _algorithms.ZeroSelected(matrix, recursive);
        Assert.Equal((2, 46L), result);
        Assert.Equal(new[,] { { 0, 12, 18 }, { 24, 30, 0 } }, matrix);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void MatrixWithOnlyMultiplesOfSixHasNoSelectedElements(bool recursive)
    {
        int[,] matrix = { { 6, 12 }, { 18, 24 } };
        Assert.Equal((0, 0L), _algorithms.ZeroSelected(matrix, recursive));
        Assert.Equal(new[,] { { 6, 12 }, { 18, 24 } }, matrix);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RowSortUsesStableLexicographicKeys(bool recursive)
    {
        int[,] matrix = { { 30, 20, 11, 50, 13 }, { 15, 40, 22, 35, 17 }, { 15, 40, 29, 20, 19 }, { 15, 25, 61, 60, 12 } };
        _algorithms.SortRows(matrix, recursive);
        Assert.Equal(new[,] {
            { 15, 25, 61, 60, 12 }, { 15, 40, 29, 20, 19 },
            { 15, 40, 22, 35, 17 }, { 30, 20, 11, 50, 13 }
        }, matrix);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void SwapRowExtremesUsesFirstMatchingColumnAndLeavesUnpairedRow(bool recursive)
    {
        int[,] matrix = { { 5, 8, 2 }, { 9, 1, 7 }, { 4, 6, 3 }, { 0, 12, -2 }, { 1, 2, 3 } };
        _algorithms.SwapRowExtremes(matrix, recursive);
        Assert.Equal(new[,] {
            { 5, 8, 9 }, { 2, 1, 7 }, { 4, 6, 12 }, { 0, 3, -2 }, { 1, 2, 3 }
        }, matrix);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void LargestMatrixValuesBecomeDescendingDiagonal(bool recursive)
    {
        double[,] matrix = { { 4, 9, 1 }, { 7, 2, 8 }, { 5, 3, 6 } };
        _algorithms.PlaceLargestValuesOnDiagonal(matrix, recursive);
        Assert.Equal(new[,] { { 9.0, 4.0, 1.0 }, { 6.0, 8.0, 2.0 }, { 5.0, 3.0, 7.0 } }, matrix);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void FindsFirstRowWithoutPositiveOnOriginalValues(bool recursive)
    {
        double[,] matrix = { { 1.5, -2, 3 }, { -1, 0, -4.5 }, { 2, 2, 2 } };
        Assert.Equal(1, _algorithms.FindFirstRowWithoutPositive(matrix, recursive));
        Assert.Equal(1, _algorithms.FindFirstRowWithoutPositive(new[,] { { 1.0, 0.0 }, { -1.0, 0.0 } }, recursive));
        Assert.Equal(-1, _algorithms.FindFirstRowWithoutPositive(new[,] { { 1.0, 2.0 } }, recursive));
    }
}
