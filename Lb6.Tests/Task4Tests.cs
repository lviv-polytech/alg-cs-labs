using System;
using System.IO;
using Xunit;

namespace Lpnu.Lb6.Tests;

public class Task4Tests
{
    private const double Epsilon = 1e-9;

    // =========================================================================
    // Canonical Verification Matrix Test Cases (TC-01 through TC-07)
    // =========================================================================

    [Theory(DisplayName = "TC-01: Complex array with two zeros")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase01_ComplexArray(bool recursive)
    {
        var a = new[] { 3.5, 0.0, -2.4, 1.8, 0.0, 4.2, 0.9 };

        var count = recursive ? Task4Recursive.CountPositiveRec(a) : Task4Iterative.CountPositive(a);
        Assert.Equal(4, count);

        var lastZero = recursive
            ? Task4Recursive.FindLastZeroIndexRec(a, a.Length - 1)
            : Task4Iterative.FindLastZeroIndex(a);
        Assert.Equal(4, lastZero);

        var sum = recursive
            ? Task4Recursive.SumAfterLastZeroRec(a)
            : Task4Iterative.SumAfterLastZero(a);
        Assert.Equal(5.1, sum, 9);

        if (recursive) Task4Recursive.PartitionByIntPartRec(a);
        else Task4Iterative.PartitionByIntPart(a);

        var expected = new[] { 0.0, -2.4, 1.8, 0.0, 0.9, 3.5, 4.2 };
        Assert.Equal(expected.Length, a.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], a[i], 9);
        }
    }

    [Theory(DisplayName = "TC-02: No zero elements in array")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase02_NoZeroElements(bool recursive)
    {
        var a = new[] { 2.5, -1.2, 5.0, 1.1 };

        var count = recursive ? Task4Recursive.CountPositiveRec(a) : Task4Iterative.CountPositive(a);
        Assert.Equal(3, count);

        var lastZero = recursive
            ? Task4Recursive.FindLastZeroIndexRec(a, a.Length - 1)
            : Task4Iterative.FindLastZeroIndex(a);
        Assert.Equal(-1, lastZero);

        var sum = recursive
            ? Task4Recursive.SumAfterLastZeroRec(a)
            : Task4Iterative.SumAfterLastZero(a);
        Assert.Equal(0.0, sum, 9);

        if (recursive) Task4Recursive.PartitionByIntPartRec(a);
        else Task4Iterative.PartitionByIntPart(a);

        var expected = new[] { -1.2, 1.1, 2.5, 5.0 };
        Assert.Equal(expected.Length, a.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], a[i], 9);
        }
    }

    [Theory(DisplayName = "TC-03: Last zero at boundary index n - 1")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase03_LastZeroAtEnd(bool recursive)
    {
        var a = new[] { 4.0, 0.0, 2.5, -3.5, 0.0 };

        var count = recursive ? Task4Recursive.CountPositiveRec(a) : Task4Iterative.CountPositive(a);
        Assert.Equal(2, count);

        var lastZero = recursive
            ? Task4Recursive.FindLastZeroIndexRec(a, a.Length - 1)
            : Task4Iterative.FindLastZeroIndex(a);
        Assert.Equal(4, lastZero);

        var sum = recursive
            ? Task4Recursive.SumAfterLastZeroRec(a)
            : Task4Iterative.SumAfterLastZero(a);
        Assert.Equal(0.0, sum, 9);

        if (recursive) Task4Recursive.PartitionByIntPartRec(a);
        else Task4Iterative.PartitionByIntPart(a);

        var expected = new[] { 0.0, -3.5, 0.0, 4.0, 2.5 };
        Assert.Equal(expected.Length, a.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], a[i], 9);
        }
    }

    [Theory(DisplayName = "TC-04: Non-positive array (zero is non-positive) with sum after zero")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase04_NoPositives(bool recursive)
    {
        var a = new[] { -5.5, -0.1, 0.0, -2.0 };

        var count = recursive ? Task4Recursive.CountPositiveRec(a) : Task4Iterative.CountPositive(a);
        Assert.Equal(0, count);

        var lastZero = recursive
            ? Task4Recursive.FindLastZeroIndexRec(a, a.Length - 1)
            : Task4Iterative.FindLastZeroIndex(a);
        Assert.Equal(2, lastZero);

        var sum = recursive
            ? Task4Recursive.SumAfterLastZeroRec(a)
            : Task4Iterative.SumAfterLastZero(a);
        Assert.Equal(-2.0, sum, 9);

        if (recursive) Task4Recursive.PartitionByIntPartRec(a);
        else Task4Iterative.PartitionByIntPart(a);

        var expected = new[] { -5.5, -0.1, 0.0, -2.0 };
        Assert.Equal(expected.Length, a.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], a[i], 9);
        }
    }

    [Theory(DisplayName = "TC-05: Critical boundary partition test around trunc(x) <= 1")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase05_BoundaryValues(bool recursive)
    {
        var a = new[] { 2.0, 2.01, 1.99, 1.0, -2.5 };

        var count = recursive ? Task4Recursive.CountPositiveRec(a) : Task4Iterative.CountPositive(a);
        Assert.Equal(4, count);

        var lastZero = recursive
            ? Task4Recursive.FindLastZeroIndexRec(a, a.Length - 1)
            : Task4Iterative.FindLastZeroIndex(a);
        Assert.Equal(-1, lastZero);

        var sum = recursive
            ? Task4Recursive.SumAfterLastZeroRec(a)
            : Task4Iterative.SumAfterLastZero(a);
        Assert.Equal(0.0, sum, 9);

        if (recursive) Task4Recursive.PartitionByIntPartRec(a);
        else Task4Iterative.PartitionByIntPart(a);

        var expected = new[] { 1.99, 1.0, -2.5, 2.0, 2.01 };
        Assert.Equal(expected.Length, a.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], a[i], 9);
        }
    }

    [Theory(DisplayName = "TC-06: All elements with trunc(x) > 1 retain initial sequence")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase06_AllGreaterThanOne(bool recursive)
    {
        var a = new[] { 10.5, 20.1, 3.14 };

        var count = recursive ? Task4Recursive.CountPositiveRec(a) : Task4Iterative.CountPositive(a);
        Assert.Equal(3, count);

        var lastZero = recursive
            ? Task4Recursive.FindLastZeroIndexRec(a, a.Length - 1)
            : Task4Iterative.FindLastZeroIndex(a);
        Assert.Equal(-1, lastZero);

        var sum = recursive
            ? Task4Recursive.SumAfterLastZeroRec(a)
            : Task4Iterative.SumAfterLastZero(a);
        Assert.Equal(0.0, sum, 9);

        if (recursive) Task4Recursive.PartitionByIntPartRec(a);
        else Task4Iterative.PartitionByIntPart(a);

        var expected = new[] { 10.5, 20.1, 3.14 };
        Assert.Equal(expected.Length, a.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], a[i], 9);
        }
    }

    [Theory(DisplayName = "TC-07: Single zero at index 0 sums all remaining elements")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase07_ZeroAtStart(bool recursive)
    {
        var a = new[] { 0.0, 1.5, -2.5, 3.0 };

        var count = recursive ? Task4Recursive.CountPositiveRec(a) : Task4Iterative.CountPositive(a);
        Assert.Equal(2, count);

        var lastZero = recursive
            ? Task4Recursive.FindLastZeroIndexRec(a, a.Length - 1)
            : Task4Iterative.FindLastZeroIndex(a);
        Assert.Equal(0, lastZero);

        var sum = recursive
            ? Task4Recursive.SumAfterLastZeroRec(a)
            : Task4Iterative.SumAfterLastZero(a);
        Assert.Equal(2.0, sum, 9);

        if (recursive) Task4Recursive.PartitionByIntPartRec(a);
        else Task4Iterative.PartitionByIntPart(a);

        var expected = new[] { 0.0, 1.5, -2.5, 3.0 };
        Assert.Equal(expected.Length, a.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], a[i], 9);
        }
    }

    // =========================================================================
    // Edge Cases, Formatting, and Algorithmic Equivalence Tests
    // =========================================================================

    [Fact(DisplayName = "Helper: IsZero and HasIntPartAtMostOne correctness")]
    public void HelperPredicates_WorkCorrectly()
    {
        Assert.True(Task4Iterative.IsZero(0.0));
        Assert.True(Task4Iterative.IsZero(1e-10));
        Assert.True(Task4Iterative.IsZero(-1e-10));
        Assert.False(Task4Iterative.IsZero(1e-8));

        Assert.True(Task4Iterative.HasIntPartAtMostOne(1.999));
        Assert.True(Task4Iterative.HasIntPartAtMostOne(1.0));
        Assert.True(Task4Iterative.HasIntPartAtMostOne(0.5));
        Assert.True(Task4Iterative.HasIntPartAtMostOne(0.0));
        Assert.True(Task4Iterative.HasIntPartAtMostOne(-0.5));
        Assert.True(Task4Iterative.HasIntPartAtMostOne(-1.5));
        Assert.False(Task4Iterative.HasIntPartAtMostOne(2.0));
        Assert.False(Task4Iterative.HasIntPartAtMostOne(2.0001));

        Assert.True(Task4Recursive.IsZero(0.0));
        Assert.True(Task4Recursive.HasIntPartAtMostOne(1.999));
        Assert.False(Task4Recursive.HasIntPartAtMostOne(2.0));
    }

    [Fact(DisplayName = "Formatting: Iterative and Recursive format output match specification")]
    public void FormattedOutput_MatchesSpecification()
    {
        var a = new[] { 1.5, -2.345, 0.0 };
        var itStr = Task4Iterative.FormatArray(a, 8, 2);
        var recStr = Task4Recursive.FormatArrayRec(a, 0, 8, 2);

        Assert.Equal(itStr, recStr);
        using var sw = new StringWriter();
        Task4Iterative.PrintArray(a, sw, 8, 2);
        Assert.Equal(itStr + Environment.NewLine, sw.ToString());
    }

    [Fact(DisplayName = "Equivalence: Iterative and Recursive produce identical results on random array")]
    public void AlgorithmicEquivalence_OnRandomArray()
    {
        var rng = new Random(42);
        var original = new double[20];
        for (var i = 0; i < original.Length; i++)
        {
            original[i] = Math.Round(rng.NextDouble() * 10.0 - 5.0, 2);
        }
        original[3] = 0.0;
        original[12] = 0.0;

        var itArr = (double[])original.Clone();
        var recArr = (double[])original.Clone();

        Assert.Equal(Task4Iterative.CountPositive(itArr), Task4Recursive.CountPositiveRec(recArr));
        Assert.Equal(Task4Iterative.FindLastZeroIndex(itArr), Task4Recursive.FindLastZeroIndexRec(recArr, recArr.Length - 1));
        Assert.Equal(Task4Iterative.SumAfterLastZero(itArr), Task4Recursive.SumAfterLastZeroRec(recArr), 9);

        Task4Iterative.PartitionByIntPart(itArr);
        Task4Recursive.PartitionByIntPartRec(recArr);

        Assert.Equal(itArr, recArr);
    }

    [Fact(DisplayName = "Edge case: Empty and single-element arrays")]
    public void EdgeCases_EmptyAndSingleElement()
    {
        var empty = Array.Empty<double>();
        Assert.Equal(0, Task4Iterative.CountPositive(empty));
        Assert.Equal(0, Task4Recursive.CountPositiveRec(empty));
        Assert.Equal(-1, Task4Iterative.FindLastZeroIndex(empty));
        Assert.Equal(-1, Task4Recursive.FindLastZeroIndexRec(empty, empty.Length - 1));
        Assert.Equal(0.0, Task4Iterative.SumAfterLastZero(empty), 9);
        Assert.Equal(0.0, Task4Recursive.SumAfterLastZeroRec(empty), 9);

        Task4Iterative.PartitionByIntPart(empty);
        Task4Recursive.PartitionByIntPartRec(empty);
        Assert.Empty(empty);

        var singleZero = new[] { 0.0 };
        Assert.Equal(0, Task4Iterative.CountPositive(singleZero));
        Assert.Equal(0, Task4Iterative.FindLastZeroIndex(singleZero));
        Assert.Equal(0.0, Task4Iterative.SumAfterLastZero(singleZero), 9);
        Task4Iterative.PartitionByIntPart(singleZero);
        Assert.Equal(new[] { 0.0 }, singleZero);
    }
}
