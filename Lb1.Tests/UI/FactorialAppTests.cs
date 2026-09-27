using Lpnu.Lb1.UI;
using Xunit;

namespace Lpnu.Lb1.Tests.UI;

/// <summary>
/// Unit tests for the presentation layer of Lab 1.
/// </summary>
public class FactorialAppTests
{
    [Fact]
    public void Run_WritesGreetingAndFactorialOutput()
    {
        // Arrange
        using var stringWriter = new StringWriter();
        var app = new FactorialApp();

        // Act
        app.Run(stringWriter);
        var output = stringWriter.ToString();

        // Assert
        Assert.Contains("Hello, World!", output);
        Assert.Contains("P: 3628800 i: 11", output);
    }
}
