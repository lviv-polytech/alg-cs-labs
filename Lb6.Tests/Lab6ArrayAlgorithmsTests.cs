using Lpnu.Lb6.Infrastructure;

namespace Lpnu.Lb6.Tests;

public class Lab6ArrayAlgorithmsTests
{
    private readonly Lpnu.Lb6.Core.ILab6ArrayAlgorithms _algorithms = Lab6ServiceFactory.CreateArrayAlgorithms();

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void SelectionCountSumAndZeroingMatchReference(bool recursive)
    {
        var values = new[] { 7, 12, 25, 31, 44, 9, 18, 50, 61, 5, 30, 73, 4, 66, 27, 35, 48, 13, 59, 70, 8, 42, 16, 37, 55 };
        var result = _algorithms.AnalyzeSelected(values, recursive);
        Assert.Equal((21, 786), result);
        _algorithms.ZeroSelected(values, recursive);
        Assert.Equal(new[] { 7, 0, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 35, 0, 0, 0, 0, 8, 0, 0, 0, 0 }, values);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void MaximumEvenSearchHandlesTiesNegativesAndAbsence(bool recursive)
    {
        Assert.Equal(2, _algorithms.FindMaximumEvenIndex(new[] { 3, -8, 14, 7, 14, 2, 9 }, recursive));
        Assert.Equal(1, _algorithms.FindMaximumEvenIndex(new[] { -4, -2, -10 }, recursive));
        Assert.Equal(0, _algorithms.FindMaximumEvenIndex(new[] { 0 }, recursive));
        Assert.Equal(-1, _algorithms.FindMaximumEvenIndex(new[] { 1, 3, 5 }, recursive));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DynamicArrayOperationsAndStablePartition(bool recursive)
    {
        var values = new[] { 1.5, 0, -3.2, 2.0, 0.7, 0, 4.5, -1.0, 2.5 };
        Assert.Equal(5, _algorithms.CountPositive(values, recursive));
        var lastZero = _algorithms.FindLastZeroIndex(values, recursive);
        Assert.Equal(5, lastZero);
        Assert.Equal(6, _algorithms.SumAfterIndex(values, lastZero, recursive));
        _algorithms.PartitionByIntegerPart(values, recursive);
        Assert.Equal(new[] { 1.5, 0.0, -3.2, 0.7, 0.0, -1.0, 2.0, 4.5, 2.5 }, values);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GenericInputSupportsNumericAndTextTypes(bool recursive)
    {
        var ints = new int[3];
        _algorithms.InputIntArray(ints, new StringReader("3\n5\n7\n"), TextWriter.Null, recursive);
        Assert.Equal(new[] { 3, 5, 7 }, ints);
        var doubles = new double[2];
        _algorithms.InputGenericArray(doubles, new StringReader("1.5\n2.25\n"), TextWriter.Null,
            text => double.Parse(text, System.Globalization.CultureInfo.InvariantCulture), recursive);
        Assert.Equal(new[] { 1.5, 2.25 }, doubles);
        var strings = new string[2];
        _algorithms.InputGenericArray(strings, new StringReader("a\nbb\n"), TextWriter.Null, text => text, recursive);
        Assert.Equal(new[] { "a", "bb" }, strings);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ZeroSearchAndTailSumHandleNoZeroAndTerminalZero(bool recursive)
    {
        Assert.Equal(-1, _algorithms.FindLastZeroIndex(new[] { 2.0, 3.0 }, recursive));
        Assert.Equal(1, _algorithms.FindLastZeroIndex(new[] { 2.0, 0.0 }, recursive));
        Assert.Equal(0, _algorithms.SumAfterIndex(new[] { 2.0, 0.0 }, 1, recursive));
    }
}
