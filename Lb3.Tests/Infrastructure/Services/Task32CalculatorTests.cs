using Lpnu.Lb3.Core.Services;
using Lpnu.Lb3.Infrastructure;
using Xunit;

namespace Lpnu.Lb3.Tests.Infrastructure.Services;

/// <summary>
/// Unit tests for Lab 3 Task 3.2: parameterized branching function F(x).
/// Verifies both branching algorithms (short-form vs full-form), boundary points, and singularities.
/// </summary>
public class Task32CalculatorTests
{
    private readonly ITask32Calculator _calculator = Lb3ServiceFactory.CreateTask32Calculator();
    private const double Precision = 1e-7;

    [Theory]
    [InlineData(2.0, 3.0, 1.0, -15.0, 468.0)]
    [InlineData(1.0, 5.0, -2.0, -11.0, 104.0)]
    public void Calculate_Condition1_XPlus10NegativeAndBNonZero_ComputesQuadraticForm(
        double a, double b, double c, double x, double expected)
    {
        var result = _calculator.Calculate(a, b, c, x);

        Assert.Equal(expected, result.ResultShortForm, Precision);
        Assert.Equal(expected, result.ResultFullForm, Precision);
        Assert.True(result.ResultsMatch);
    }

    [Theory]
    [InlineData(4.0, 0.0, 2.0, 0.0, 2.0)]
    [InlineData(1.0, 0.0, 5.0, -5.0, 0.6)]
    public void Calculate_Condition2_XPlus10PositiveAndBZero_ComputesRationalForm(
        double a, double b, double c, double x, double expected)
    {
        var result = _calculator.Calculate(a, b, c, x);

        Assert.Equal(expected, result.ResultShortForm, Precision);
        Assert.Equal(expected, result.ResultFullForm, Precision);
        Assert.True(result.ResultsMatch);
    }

    [Theory]
    [InlineData(5.0, 2.0, 1.0, -10.0, 2.5)]   // x + 10 == 0
    [InlineData(4.0, 3.0, 2.0, 5.0, -2.5)]     // x + 10 > 0 but b != 0
    [InlineData(5.0, 0.0, 2.0, -12.0, 4.0)]    // x + 10 < 0 but b == 0
    public void Calculate_Condition3_Otherwise_ComputesLinearQuotientForm(
        double a, double b, double c, double x, double expected)
    {
        var result = _calculator.Calculate(a, b, c, x);

        Assert.Equal(expected, result.ResultShortForm, Precision);
        Assert.Equal(expected, result.ResultFullForm, Precision);
        Assert.True(result.ResultsMatch);
    }

    [Fact]
    public void Calculate_Branch2Singularity_XEqualsC_ThrowsArgumentException()
    {
        // x + 10 > 0, b == 0, but x == c (division by x - c)
        const double a = 3.0;
        const double b = 0.0;
        const double c = 4.0;
        const double x = 4.0;

        Assert.Throws<ArgumentException>(() => _calculator.Calculate(a, b, c, x));
        Assert.Throws<ArgumentException>(() => _calculator.CalculateShortForm(a, b, c, x));
        Assert.Throws<ArgumentException>(() => _calculator.CalculateFullForm(a, b, c, x));
    }

    [Fact]
    public void Calculate_Branch3Singularity_AEqualsC_ThrowsArgumentException()
    {
        // Otherwise branch, but a == c (division by a - c)
        const double a = 2.0;
        const double b = 5.0;
        const double c = 2.0;
        const double x = 0.0;

        Assert.Throws<ArgumentException>(() => _calculator.Calculate(a, b, c, x));
        Assert.Throws<ArgumentException>(() => _calculator.CalculateShortForm(a, b, c, x));
        Assert.Throws<ArgumentException>(() => _calculator.CalculateFullForm(a, b, c, x));
    }

    [Theory]
    [InlineData(double.NaN, 0, 0, 0)]
    [InlineData(0, double.NaN, 0, 0)]
    [InlineData(0, 0, double.NaN, 0)]
    [InlineData(0, 0, 0, double.NaN)]
    public void Calculate_NonFiniteInput_ThrowsArgumentOutOfRangeException(double a, double b, double c, double x)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(a, b, c, x));
    }
}
