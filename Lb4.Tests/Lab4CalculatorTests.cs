using Lpnu.Lb4.Infrastructure;

namespace Lpnu.Lb4.Tests;

public class Lab4CalculatorTests
{
    private readonly Lpnu.Lb4.Core.ILab4Calculator _calculator = Lab4ServiceFactory.CreateCalculator();

    [Theory]
    [InlineData(1, 0.5463024898)]
    [InlineData(3, 16.205130105)]
    [InlineData(5, 13.273067857)]
    public void FourCycleOrdersAgreeWithReference(int n, double expected)
    {
        var values = new[] { _calculator.SumWithWhile(n), _calculator.SumWithDoWhile(n),
            _calculator.SumWithForIncreasing(n), _calculator.SumWithForDecreasing(n) };
        Assert.All(values, value => Assert.InRange(Math.Abs(value - expected), 0, 1e-6));
        Assert.InRange(values.Max() - values.Min(), 0, 1e-9);
    }

    [Fact]
    public void RejectsInvalidSumBound() => Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.SumWithDoWhile(0));

    [Fact]
    public void FormulaTableIncludesBranchBoundaries()
    {
        var rows = _calculator.TabulateFormula(0, 7, 1);
        Assert.Equal(-1, rows[0].Value, 8);
        Assert.Equal(51.390290, rows[4].Value, 5);
        Assert.Equal(79, rows[7].Value, 8);
    }

    [Fact]
    public void GraphUsesArcAtLeftBoundaryAndClampsRadicand()
    {
        var rows = _calculator.TabulateGraph(2, -4, 6, 1);
        Assert.Equal(1, rows[0].Value);
        Assert.Equal(0, rows[1].Value);
        Assert.Equal(-2, rows[3].Value);
        Assert.Equal(2, rows[^1].Value);
    }

    [Fact]
    public void ParametricFunctionMarksUndefinedDenominators()
    {
        var rows = _calculator.TabulateParameterized(2, 0, 4, 0, 4, 1);
        Assert.Equal(0.5, rows[0].Value);
        Assert.True(double.IsNaN(rows[4].Value));
    }

    [Fact]
    public void ParametricFunctionCoversAllBranchesAndTheMinusTenBoundary()
    {
        var rows = _calculator.TabulateParameterized(2, 3, 4, -12, -9, 1);
        Assert.Equal(339, rows[0].Value);
        Assert.Equal(-5, rows[2].Value);
        Assert.Equal(-4.5, rows[3].Value);
    }

    [Fact]
    public void LogSeriesIncludesFirstTermBelowEpsilon()
    {
        var row = _calculator.TabulateLogSeries(2, 2, 1, 1e-4)[0];
        Assert.Equal(10, row.Terms);
        Assert.Equal(0.693065, row.Sum!.Value, 6);
        Assert.Null(_calculator.TabulateLogSeries(0.5, 0.5, 1, 1e-4)[0].Sum);
    }

    [Fact]
    public void NestedImplementationsAgree() 
    {
        var result = _calculator.CalculateNestedSums();
        Assert.InRange(Math.Abs(result.While - result.DoWhile), 0, 1e-9);
        Assert.InRange(Math.Abs(result.While - result.ForIncreasing), 0, 1e-9);
        Assert.InRange(Math.Abs(result.While - result.ForDecreasing), 0, 1e-9);
    }
}

public class TargetCalculatorTests
{
    private readonly Lpnu.Lb4.Core.ITargetCalculator _target = TargetCalculatorFactory.CreateTargetCalculator();

    [Theory]
    [InlineData(2, -1, 1, true)]      // Part A: center
    [InlineData(2, -2, 2, true)]      // Part A: corner at boundary
    [InlineData(2, -2, 0, true)]      // Part A: left edge
    [InlineData(2, 0, 0, true)]       // Origin: boundary point
    [InlineData(2, 1, -1, true)]      // Part B: center
    [InlineData(2, 2, -2, true)]      // Part B: corner at boundary
    [InlineData(2, 2, 0, true)]       // Part B: right edge
    [InlineData(2, -0.1, 1.9, false)] // Outside arc in Part A
    [InlineData(2, 0.1, -1.9, false)] // Outside arc in Part B
    [InlineData(2, -3, 1, false)]     // Left of Part A bound (x < -R)
    [InlineData(2, 1, 1, false)]      // Wrong quadrant (positive y in right half)
    [InlineData(2, -1, -1, false)]    // Wrong quadrant (negative y in left half)
    public void InTargetReturnsCorrectResultForTestCases(double radius, double x, double y, bool expected)
    {
        Assert.Equal(expected, _target.InTarget(x, y, radius));
    }

    [Fact]
    public void GenerateRandomShotReturnsCoordinatesWithinBounds()
    {
        var radius = 3.5;
        for (int i = 0; i < 100; i++)
        {
            var (x, y) = _target.GenerateRandomShot(radius);
            Assert.InRange(x, -radius, radius);
            Assert.InRange(y, -radius, radius);
        }
    }

    [Fact]
    public void PartABoundaryConditionsHold()
    {
        var radius = 1.0;
        // Boundary: x = -R, y = 0
        Assert.True(_target.InTarget(-1, 0, radius));
        // Boundary: x = 0, y = 0
        Assert.True(_target.InTarget(0, 0, radius));
        // Boundary: x = -R, y = R
        Assert.True(_target.InTarget(-1, 1, radius));
        // Outside left
        Assert.False(_target.InTarget(-1.1, 0.5, radius));
    }

    [Fact]
    public void PartBBoundaryConditionsHold()
    {
        var radius = 1.0;
        // Boundary: x = R, y = 0
        Assert.True(_target.InTarget(1, 0, radius));
        // Boundary: x = 0, y = 0
        Assert.True(_target.InTarget(0, 0, radius));
        // Boundary: x = R, y = -R
        Assert.True(_target.InTarget(1, -1, radius));
        // Outside right
        Assert.False(_target.InTarget(1.1, -0.5, radius));
    }

    [Theory]
    [InlineData(0.1)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(2.5)]
    [InlineData(10.0)]
    public void OriginIsAlwaysInTarget(double radius)
    {
        Assert.True(_target.InTarget(0, 0, radius));
    }
}
