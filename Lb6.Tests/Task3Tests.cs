using System;
using System.Globalization;
using System.IO;
using Xunit;

namespace Lpnu.Lb6.Tests;

public class Task3Tests
{
    // =========================================================================
    // Canonical Verification Matrix Test Cases (TC-01 through TC-07)
    // =========================================================================

    [Theory(DisplayName = "TC-01: Ordinary int function reads signed values and zero accurately")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase01_OrdinaryInt(bool recursive)
    {
        var array = new int[5];
        using var reader = new StringReader("10 -25 0 42 -7");
        if (recursive) Task3Recursive.InputArrayRec(array, reader);
        else Task3Iterative.InputArray(array, reader);

        Assert.Equal(new[] { 10, -25, 0, 42, -7 }, array);
    }

    [Theory(DisplayName = "TC-02: Template function with T = int matches ordinary behavior")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase02_TemplateInt(bool recursive)
    {
        var array = new int[5];
        using var reader = new StringReader("10 -25 0 42 -7");
        if (recursive) Task3Recursive.InputArrayTemplateRec(array, reader, int.Parse);
        else Task3Iterative.InputArrayTemplate(array, reader, int.Parse);

        Assert.Equal(new[] { 10, -25, 0, 42, -7 }, array);
    }

    [Theory(DisplayName = "TC-03: Template function with T = double reads real numbers")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase03_TemplateDouble(bool recursive)
    {
        var array = new double[4];
        using var reader = new StringReader("3.14 -0.005 0.0 100.25");
        Func<string, double> parser = s => double.Parse(s, CultureInfo.InvariantCulture);
        if (recursive) Task3Recursive.InputArrayTemplateRec(array, reader, parser);
        else Task3Iterative.InputArrayTemplate(array, reader, parser);

        Assert.Equal(3.14, array[0], precision: 9);
        Assert.Equal(-0.005, array[1], precision: 9);
        Assert.Equal(0.0, array[2], precision: 9);
        Assert.Equal(100.25, array[3], precision: 9);
    }

    [Theory(DisplayName = "TC-04: Template function with T = string reads text tokens")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase04_TemplateString(bool recursive)
    {
        var array = new string[3];
        using var reader = new StringReader("alpha beta gamma");
        if (recursive) Task3Recursive.InputArrayTemplateRec(array, reader, s => s);
        else Task3Iterative.InputArrayTemplate(array, reader, s => s);

        Assert.Equal(new[] { "alpha", "beta", "gamma" }, array);
    }

    [Theory(DisplayName = "TC-05: Single element vector (n = 1) populated correctly")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase05_SingleElement(bool recursive)
    {
        var array = new int[1];
        using var reader = new StringReader("-999");
        if (recursive) Task3Recursive.InputArrayRec(array, reader);
        else Task3Iterative.InputArray(array, reader);

        Assert.Equal(new[] { -999 }, array);
    }

    [Theory(DisplayName = "TC-06: Degenerate vector (n = 0) performs zero stream reads")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase06_ZeroElements(bool recursive)
    {
        var array = Array.Empty<int>();
        using var reader = new StringReader("100 200");
        if (recursive) Task3Recursive.InputArrayRec(array, reader);
        else Task3Iterative.InputArray(array, reader);

        var nextToken = StreamTokenReader.ReadNextToken(reader);
        Assert.Equal("100", nextToken);
    }

    [Theory(DisplayName = "TC-07: Whitespace, tabs, newlines handling and extra tokens retention")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase07_WhitespaceAndExtraTokens(bool recursive)
    {
        var array = new int[3];
        using var reader = new StringReader("  5 \n\t 12 \n  -8  99 100");
        if (recursive) Task3Recursive.InputArrayRec(array, reader);
        else Task3Iterative.InputArray(array, reader);

        Assert.Equal(new[] { 5, 12, -8 }, array);
        var remainingToken = StreamTokenReader.ReadNextToken(reader);
        Assert.Equal("99", remainingToken);
    }

    // =========================================================================
    // Formatted Output and Exception Handling Tests
    // =========================================================================

    [Fact(DisplayName = "FormatArray and PrintArray produce uniform columns")]
    public void FormatAndPrintArrayProduceFormattedOutput()
    {
        var array = new[] { 1, 2, 3 };
        var formattedIt = Task3Iterative.FormatArray(array, 4);
        var formattedRec = Task3Recursive.FormatArrayRec(array, 0, 4);

        Assert.Equal("   1   2   3", formattedIt);
        Assert.Equal("   1   2   3", formattedRec);

        using var swIt = new StringWriter();
        Task3Iterative.PrintArray(array, swIt, 4);
        Assert.Equal("   1   2   3" + Environment.NewLine, swIt.ToString());

        using var swRec = new StringWriter();
        Task3Recursive.PrintArrayRec(array, swRec, 0, 4);
        Assert.Equal("   1   2   3" + Environment.NewLine, swRec.ToString());
    }

    [Fact(DisplayName = "InputArray throws on null array or exhausted stream")]
    public void InputArrayThrowsOnInvalidInputs()
    {
        Assert.Throws<ArgumentNullException>(() => Task3Iterative.InputArray(null!, TextReader.Null));
        Assert.Throws<ArgumentNullException>(() => Task3Recursive.InputArrayRec(null!, TextReader.Null));

        using var emptyReader = new StringReader("");
        var array = new int[3];
        Assert.Throws<EndOfStreamException>(() => Task3Iterative.InputArray(array, emptyReader));
    }
}
