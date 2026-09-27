using Lpnu.Lb2.UI;
using Xunit;

namespace Lpnu.Lb2.Tests.UI;

/// <summary>
/// Unit tests for the user interface and input processing logic of Lab 2 (Variant 17).
/// </summary>
public class Variant17AppTests
{
    [Fact]
    public void Run_EnteringZero_ExitsLoopImmediately()
    {
        // Arrange
        using var stringReader = new StringReader("0\n");
        using var stringWriter = new StringWriter();
        var app = new Variant17App();

        // Act
        app.Run(stringReader, stringWriter);
        var output = stringWriter.ToString();

        // Assert
        Assert.Contains("0 - Exit", output);
        Assert.Contains("Write number (m):", output);
        Assert.DoesNotContain("z1=", output);
    }

    [Fact]
    public void Run_EnteringValidNumber_ComputesAndOutputsResult()
    {
        // Arrange
        using var stringReader = new StringReader("4\n");
        using var stringWriter = new StringWriter();
        var app = new Variant17App();

        // Act
        app.Run(stringReader, stringWriter);
        var output = stringWriter.ToString();

        // Assert
        Assert.Contains("z1=2", output);
        Assert.Contains("z2=2", output);
    }

    [Fact]
    public void Run_EnteringNegativeThenValid_PromptsNegativeWarningAndComputes()
    {
        // Arrange
        using var stringReader = new StringReader("-5\n1\n");
        using var stringWriter = new StringWriter();
        var app = new Variant17App();

        // Act
        app.Run(stringReader, stringWriter);
        var output = stringWriter.ToString();

        // Assert
        Assert.Contains("Don't write m < 0", output);
        Assert.Contains("z1=1", output);
        Assert.Contains("z2=1", output);
    }

    [Fact]
    public void Run_EnteringInvalidStringThenValid_PromptsInvalidWarningAndComputes()
    {
        // Arrange
        using var stringReader = new StringReader("not_a_number\n9\n");
        using var stringWriter = new StringWriter();
        var app = new Variant17App();

        // Act
        app.Run(stringReader, stringWriter);
        var output = stringWriter.ToString();

        // Assert
        Assert.Contains("Write a valid finite number.", output);
        Assert.Contains("z1=3", output);
        Assert.Contains("z2=3", output);
    }
}
