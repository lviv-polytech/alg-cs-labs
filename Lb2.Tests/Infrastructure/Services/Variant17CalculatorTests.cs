using Lpnu.Lb2.Core.Services;
using Lpnu.Lb2.Infrastructure;
using Xunit;

namespace Lpnu.Lb2.Tests.Infrastructure.Services;

/// <summary>
/// Unit tests for Variant 17 mathematical calculation logic and boundary edge cases.
/// </summary>
public class Variant17CalculatorTests
{
    private readonly IVariant17Calculator _calculator = Variant17ServiceFactory.CreateCalculator();
    private const double Precision = 1e-7;

    [Theory]
    [InlineData(1.0, 1.0, 1.0)]
    [InlineData(4.0, 2.0, 2.0)]
    [InlineData(9.0, 3.0, 3.0)]
    [InlineData(16.0, 4.0, 4.0)]
    public void Calculate_ValuesGreaterThanTwoThirds_YieldsZ1EqualsZ2(double m, double expectedZ1, double expectedZ2)
    {
        // Act
        var result = _calculator.Calculate(m);

        // Assert
        Assert.Equal(expectedZ1, result.Z1, Precision);
        Assert.Equal(expectedZ2, result.Z2, Precision);
    }

    [Theory]
    [InlineData(0.25, -0.5, 0.5)]
    [InlineData(0.16, -0.4, 0.4)]
    [InlineData(0.01, -0.1, 0.1)]
    public void Calculate_ValuesLessThanTwoThirds_YieldsZ1EqualsNegativeZ2(double m, double expectedZ1, double expectedZ2)
    {
        // Act
        var result = _calculator.Calculate(m);

        // Assert
        Assert.Equal(expectedZ1, result.Z1, Precision);
        Assert.Equal(expectedZ2, result.Z2, Precision);
    }

    [Fact]
    public void Calculate_ExactSingularity_ThrowsArgumentException()
    {
        // At m = 2/3, 3m - 2 = 0, causing division by zero in the denominator
        const double m = 2.0 / 3.0;

        var exception = Assert.Throws<ArgumentException>(() => _calculator.Calculate(m));
        Assert.Contains("2/3", exception.Message);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-0.001)]
    [InlineData(-1.0)]
    [InlineData(-50.0)]
    public void Calculate_ZeroOrNegativeValue_ThrowsArgumentOutOfRangeException(double nonPositiveM)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(nonPositiveM));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_NonFiniteInput_ThrowsArgumentOutOfRangeException(double nonFiniteM)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(nonFiniteM));
    }
}
