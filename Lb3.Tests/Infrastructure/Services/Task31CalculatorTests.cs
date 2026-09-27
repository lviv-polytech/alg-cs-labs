using Lpnu.Lb3.Core.Services;
using Lpnu.Lb3.Infrastructure;
using Xunit;

namespace Lpnu.Lb3.Tests.Infrastructure.Services;

/// <summary>
/// Unit tests for Lab 3 Task 3.1: piecewise function of a single variable.
/// </summary>
public class Task31CalculatorTests
{
    private readonly ITask31Calculator _calculator = Lb3ServiceFactory.CreateTask31Calculator();
    private const double Precision = 1e-6;

    [Theory]
    [InlineData(0.0, 1.0, -2.0, -1.0)]
    [InlineData(1.0, 5.0, 2.0, 4.0)]
    [InlineData(-1.0, 0.0, -6.0, -4.0)]
    public void Calculate_Interval1_ValuesLessThanFour_ComputesPolynomialCorrectly(
        double m, double n, double expectedPhi, double expectedY)
    {
        var result = _calculator.Calculate(m, n);

        Assert.Equal(m, result.M);
        Assert.Equal(n, result.N);
        Assert.Equal(expectedPhi, result.Phi, Precision);
        Assert.Equal(expectedY, result.Y, Precision);
    }

    [Fact]
    public void Calculate_Interval2_BoundaryAtFour_ComputesAtanBranchCorrectly()
    {
        // At m = 4:
        // Phi = atan((|4| + 1) / 2) + 8.3 * 4 = atan(2.5) + 33.2
        const double m = 4.0;
        const double n = 10.0;
        var expectedPhi = Math.Atan(2.5) + 33.2;
        var expectedY = m * m + 1.0 + expectedPhi;

        var result = _calculator.Calculate(m, n);

        Assert.Equal(expectedPhi, result.Phi, Precision);
        Assert.Equal(expectedY, result.Y, Precision);
    }

    [Theory]
    [InlineData(5.0)]
    [InlineData(6.5)]
    [InlineData(6.99)]
    public void Calculate_Interval2_BetweenFourAndSeven_ComputesCorrectly(double m)
    {
        var expectedPhi = Math.Atan((Math.Abs(m) + 1.0) / 2.0) + 8.3 * m;
        var expectedY = m * m + 1.0 + expectedPhi;

        var result = _calculator.Calculate(m, 0.0);

        Assert.Equal(expectedPhi, result.Phi, Precision);
        Assert.Equal(expectedY, result.Y, Precision);
    }

    [Fact]
    public void Calculate_Interval3_BoundaryAtSeven_ComputesLogBranchCorrectly()
    {
        const double m = 7.0;
        var expectedPhi = Math.Log(Math.Abs(2.0 * m + Math.Exp(4.0 * m + 1.0)));
        var expectedY = m * m + 1.0 + expectedPhi;

        var result = _calculator.Calculate(m, 0.0);

        Assert.Equal(expectedPhi, result.Phi, Precision);
        Assert.Equal(expectedY, result.Y, Precision);
    }

    [Fact]
    public void Calculate_Interval3_LargeArgument_DoesNotOverflowDouble()
    {
        // For m = 200, exp(801) exceeds standard double, asymptotic expansion prevents overflow
        const double m = 200.0;
        var result = _calculator.Calculate(m, 0.0);

        Assert.True(double.IsFinite(result.Phi));
        Assert.True(double.IsFinite(result.Y));
        Assert.True(result.Y > 0);
    }

    [Theory]
    [InlineData(double.NaN, 0.0)]
    [InlineData(0.0, double.NaN)]
    [InlineData(double.PositiveInfinity, 0.0)]
    [InlineData(0.0, double.NegativeInfinity)]
    public void Calculate_NonFiniteInput_ThrowsArgumentOutOfRangeException(double m, double n)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(m, n));
    }
}
