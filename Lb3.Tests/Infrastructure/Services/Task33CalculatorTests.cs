using Lpnu.Lb3.Core.Services;
using Lpnu.Lb3.Infrastructure;
using Xunit;

namespace Lpnu.Lb3.Tests.Infrastructure.Services;

/// <summary>
/// Unit tests for Lab 3 Task 3.3: graph-based piecewise function y(x, R).
/// </summary>
public class Task33CalculatorTests
{
    private readonly ITask33Calculator _calculator = Lb3ServiceFactory.CreateTask33Calculator();
    private const double Precision = 1e-6;

    [Theory]
    [InlineData(-5.0, 2.0, 1.0)]
    [InlineData(-3.0, 2.0, 1.0)]   // Boundary x = -1 - R = -3
    [InlineData(-10.0, 5.0, 1.0)]
    public void Calculate_Interval1_XLessThanOrEqualToMinusOneMinusR_ReturnsOne(
        double x, double r, double expectedY)
    {
        var result = _calculator.Calculate(x, r);

        Assert.Equal(expectedY, result.Y, Precision);
        Assert.Contains("Interval 1", result.IntervalDescription);
    }

    [Theory]
    [InlineData(-1.0, 2.0, -2.0)]              // Right boundary of arc: x = -1 => y = -R
    [InlineData(-2.0, 2.0, -1.732050807568877)] // -sqrt(4 - (-1)^2) = -sqrt(3)
    public void Calculate_Interval2_ArcSegment_ComputesLowerArcCorrectly(
        double x, double r, double expectedY)
    {
        var result = _calculator.Calculate(x, r);

        Assert.Equal(expectedY, result.Y, Precision);
        Assert.Contains("Interval 2", result.IntervalDescription);
    }

    [Theory]
    [InlineData(0.0, 2.0, -2.0)]
    [InlineData(1.0, 3.0, -3.0)]
    [InlineData(2.0, 4.0, -4.0)]               // Boundary x = 2
    public void Calculate_Interval3_HorizontalSegment_ReturnsNegativeR(
        double x, double r, double expectedY)
    {
        var result = _calculator.Calculate(x, r);

        Assert.Equal(expectedY, result.Y, Precision);
        Assert.Contains("Interval 3", result.IntervalDescription);
    }

    [Theory]
    [InlineData(4.0, 2.0, 0.0)]                 // Intersection with x-axis: (4, 0)
    [InlineData(6.0, 2.0, 2.0)]                 // 2 * (6 - 4) / 2 = 2
    [InlineData(3.0, 4.0, -2.0)]                // 4 * (3 - 4) / 2 = -2
    public void Calculate_Interval4_LinearRay_ComputesInclinedLine(
        double x, double r, double expectedY)
    {
        var result = _calculator.Calculate(x, r);

        Assert.Equal(expectedY, result.Y, Precision);
        Assert.Contains("Interval 4", result.IntervalDescription);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(-10.0)]
    public void Calculate_NonPositiveRadius_ThrowsArgumentOutOfRangeException(double nonPositiveR)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(0.0, nonPositiveR));
    }

    [Theory]
    [InlineData(double.NaN, 2.0)]
    [InlineData(0.0, double.NaN)]
    [InlineData(double.PositiveInfinity, 2.0)]
    [InlineData(0.0, double.PositiveInfinity)]
    public void Calculate_NonFiniteArguments_ThrowsArgumentOutOfRangeException(double x, double r)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(x, r));
    }
}
