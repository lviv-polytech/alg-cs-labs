using Lpnu.Lb5.Infrastructure;

namespace Lpnu.Lb5.Tests;

public class Lab5CalculatorTests
{
    private readonly Lpnu.Lb5.Core.ILab5Calculator _calculator = Lab5ServiceFactory.CreateCalculator();

    [Fact]
    public void Calculate_WithGivenSampleValues_ReturnsExpectedResult()
    {
        // 1. Вхідні параметри
        double test_x = 2.0;
        double test_y = 6.0;

        // 2. Очікуваний результат: (2^2 + 6) / 2 = 5.0
        double expected = 5.0;

        // 3. Фактичний виклик піддослідної функції
        double actual = _calculator.Calculate(test_x, test_y);

        // 4. Твердження рівності (похибка дельта 0.00001 -> 5 десяткових знаків)
        Assert.Equal(expected, actual, 5);
    }

    [Theory]
    [InlineData(0.0, 0.0, 0.0)]
    [InlineData(-2.0, 6.0, 5.0)]
    [InlineData(3.0, 1.0, 5.0)]
    [InlineData(1.5, 2.5, 2.375)]
    [InlineData(-3.0, -1.0, 4.0)]
    public void Calculate_WithVariousInputs_ComputesCorrectly(double x, double y, double expected)
    {
        var actual = _calculator.Calculate(x, y);
        Assert.Equal(expected, actual, 5);
    }

    [Fact]
    public void Calculate_WithNonFiniteInputs_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(double.NaN, 1.0));
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(1.0, double.PositiveInfinity));
    }

    [Theory]
    [InlineData(1, 1, 1.25)]
    [InlineData(0, 0, 1)]
    [InlineData(2, -1, 0.5416666667)]
    public void ExpressionMatchesReference(double a, double b, double expected) =>
        Assert.Equal(expected, _calculator.CalculateExpression(a, b), 8);

    [Fact]
    public void CalculateNextLogTermComputesRecurrenceAndValidatesInputs()
    {
        var a0 = (2.0 - 1.0) / 2.0; // 0.5
        var a1 = _calculator.CalculateNextLogTerm(2.0, 1, a0);
        Assert.Equal(0.125, a1, 12);

        var a2 = _calculator.CalculateNextLogTerm(2.0, 2, a1);
        Assert.Equal(1.0 / 24.0, a2, 12);

        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.CalculateNextLogTerm(0.0, 1, a0));
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.CalculateNextLogTerm(2.0, 0, a0));
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.CalculateNextLogTerm(double.NaN, 1, a0));
    }

    [Fact]
    public void TaylorSeriesMatchesLogarithmAndIncludesToleranceTerm()
    {
        var atCenter = _calculator.CalculateLogSeries(1.0, 1e-7);
        Assert.Equal(1, atCenter.Terms);
        Assert.Equal(0.0, atCenter.Sum, 12);

        var atTwo = _calculator.CalculateLogSeries(2.0, 1e-4);
        Assert.Equal(10, atTwo.Terms);
        Assert.Equal(0.69306485615079365, atTwo.Sum, 12);

        var atPointSix = _calculator.CalculateLogSeries(0.6, 1e-4);
        Assert.Equal(16, atPointSix.Terms);
        Assert.Equal(-0.51078897, atPointSix.Sum, 7);

        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.CalculateLogSeries(0.5, 1e-4));
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.CalculateLogSeries(-1.0, 1e-4));
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.CalculateLogSeries(2.0, 0.0));
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.CalculateLogSeries(2.0, -1e-4));
    }

    [Fact]
    public void TabulateLogSeriesHandlesGridWithoutDriftAndMixedDomains()
    {
        var rows = _calculator.TabulateLogSeries(0.6, 2.0, 0.2, 1e-4);
        Assert.Equal(8, rows.Count);
        Assert.Equal(0.6, rows[0].Argument, 12);
        Assert.Equal(16, rows[0].Terms);
        Assert.Equal(0.8, rows[1].Argument, 12);
        Assert.Equal(6, rows[1].Terms);
        Assert.Equal(1.0, rows[2].Argument, 12);
        Assert.Equal(1, rows[2].Terms);
        Assert.Equal(2.0, rows[7].Argument, 12);
        Assert.Equal(10, rows[7].Terms);

        var mixed = _calculator.TabulateLogSeries(-0.2, 1.0, 0.4, 1e-4);
        Assert.Equal(4, mixed.Count);
        Assert.Null(mixed[0].ExactValue); // x = -0.2
        Assert.Null(mixed[0].SeriesSum);
        Assert.NotNull(mixed[1].ExactValue); // x = 0.2
        Assert.Null(mixed[1].SeriesSum);
        Assert.NotNull(mixed[2].SeriesSum); // x = 0.6
        Assert.NotNull(mixed[3].SeriesSum); // x = 1.0

        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.TabulateLogSeries(2.0, 1.0, 0.2, 1e-4));
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.TabulateLogSeries(1.0, 2.0, 0.0, 1e-4));
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.TabulateLogSeries(1.0, 2.0, 0.2, 0.0));
    }

    [Fact]
    public void HelperUsesSixTermPolynomialAndDetectsSingularities()
    {
        Assert.Equal(1, _calculator.CalculateHelper(0));
        Assert.Equal(1.2605918, _calculator.CalculateHelper(0.5)!.Value, 7);
        Assert.Null(_calculator.CalculateHelper(Math.PI));
    }

    [Fact]
    public void HelperExpressionHasIntervalsPlusOneRows()
    {
        var rows = _calculator.TabulateHelperExpression(-3, 3, 24);
        Assert.Equal(25, rows.Count);
        Assert.Contains(rows, row => row.Value.HasValue);
    }

    [Theory]
    [InlineData(1, 0.5463024898)]
    [InlineData(3, 16.205130105)]
    [InlineData(5, 13.273067857)]
    public void RecursiveSumsMatchControl(int n, double expected)
    {
        var values = new[] { _calculator.IterativeSum(n), _calculator.RecursiveSumUpIncreasing(n),
            _calculator.RecursiveSumUpDecreasing(n), _calculator.RecursiveSumDownIncreasing(n),
            _calculator.RecursiveSumDownDecreasing(n) };
        Assert.All(values, value => Assert.InRange(Math.Abs(value - expected), 0, 1e-6));
        Assert.InRange(values.Max() - values.Min(), 0, 1e-9);
    }

    [Fact]
    public void ProgressionReportsTermSumAndCallDepth()
    {
        var result = _calculator.CalculateProgression(2, 3, 5);
        Assert.Equal(162, result.NthTerm);
        Assert.Equal(242, result.Sum);
        Assert.Equal(5, result.TermDepth);
        Assert.Equal(5, result.SumDepth);
    }
}

