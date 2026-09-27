using CommonUtils.Input;
using Lpnu.Lb3.UI;
using Xunit;

namespace Lpnu.Lb3.Tests.UI;

/// <summary>
/// Unit tests for Lab 3 user interface presentation layer.
/// </summary>
public class Lb3AppTests
{
    [Fact]
    public void Run_ZeroInput_ExitsImmediately()
    {
        using var reader = new StringReader("0\n");
        using var writer = new StringWriter();
        var inputReader = new ConsoleInputReader(reader, writer);
        var app = new Lb3App(inputReader: inputReader, output: writer);

        app.Run();
        var output = writer.ToString();

        Assert.Contains("=== Lab 3: Branching Algorithms (Variant 17) ===", output);
        Assert.Contains("0 - Exit", output);
    }

    [Fact]
    public void RunTask31_ComputesAndDisplaysResult()
    {
        using var reader = new StringReader("1\n5\n");
        using var writer = new StringWriter();
        var inputReader = new ConsoleInputReader(reader, writer);
        var app = new Lb3App(inputReader: inputReader, output: writer);

        app.RunTask31();
        var output = writer.ToString();

        Assert.Contains("Phi(m) = 2.000000", output);
        Assert.Contains("Y = 4.000000", output);
    }

    [Fact]
    public void RunTask32_ComputesAndDisplaysBothFormsAndMatch()
    {
        using var reader = new StringReader("2\n3\n1\n-15\n");
        using var writer = new StringWriter();
        var inputReader = new ConsoleInputReader(reader, writer);
        var app = new Lb3App(inputReader: inputReader, output: writer);

        app.RunTask32();
        var output = writer.ToString();

        Assert.Contains("Result (Short-form 'if'): 468.000000", output);
        Assert.Contains("Result (Full-form 'if-else'): 468.000000", output);
        Assert.Contains("Results match: True", output);
    }

    [Fact]
    public void RunTask33_ComputesAndDisplaysIntervalAndY()
    {
        using var reader = new StringReader("4\n2\n");
        using var writer = new StringWriter();
        var inputReader = new ConsoleInputReader(reader, writer);
        var app = new Lb3App(inputReader: inputReader, output: writer);

        app.RunTask33();
        var output = writer.ToString();

        Assert.Contains("Interval: Interval 4", output);
        Assert.Contains("y = 0.000000", output);
    }
}
