using Lpnu.Lb1.Core.Services;
using Lpnu.Lb1.Infrastructure;
using Xunit;

namespace Lpnu.Lb1.Tests.Infrastructure.Services;

/// <summary>
/// Unit tests verifying factorial computation logic and boundary constraints for Lab 1.
/// </summary>
public class FactorialCalculatorTests
{
    private readonly IFactorialCalculator _calculator = FactorialServiceFactory.CreateCalculator();

    [Fact]
    public void Calculate_Ten_ReturnsCorrectFactorialAndTerminalIndex()
    {
        // Act
        var result = _calculator.Calculate(10);

        // Assert
        Assert.Equal(10, result.N);
        Assert.Equal(3628800L, result.Product);
        Assert.Equal(11, result.LastIterationIndex);
    }

    [Theory]
    [InlineData(0, 1L, 1)]
    [InlineData(1, 1L, 2)]
    [InlineData(2, 2L, 3)]
    [InlineData(3, 6L, 4)]
    [InlineData(4, 24L, 5)]
    [InlineData(5, 120L, 6)]
    public void Calculate_KnownValues_ReturnsExpectedProducts(int n, long expectedProduct, int expectedIndex)
    {
        // Act
        var result = _calculator.Calculate(n);

        // Assert
        Assert.Equal(expectedProduct, result.Product);
        Assert.Equal(expectedIndex, result.LastIterationIndex);
    }

    [Fact]
    public void Calculate_MaximumSupportedValue_ReturnsAccurateValue()
    {
        // 20! is the maximum factorial fitting in a signed 64-bit integer
        const long expected20Fact = 2432902008176640000L;

        var result = _calculator.Calculate(20);

        Assert.Equal(expected20Fact, result.Product);
        Assert.Equal(21, result.LastIterationIndex);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Calculate_NegativeInput_ThrowsArgumentOutOfRangeException(int negativeN)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(negativeN));
    }

    [Theory]
    [InlineData(21)]
    [InlineData(50)]
    public void Calculate_ValueExceedingLongCapacity_ThrowsArgumentOutOfRangeException(int overflowN)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Calculate(overflowN));
    }
}
