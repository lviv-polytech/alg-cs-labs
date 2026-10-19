using System;
using System.IO;
using Xunit;

namespace Lpnu.Lb6.Tests;

public class Task2Tests
{
    // =========================================================================
    // Canonical Verification Matrix Test Cases (TC-01 through TC-09)
    // =========================================================================

    [Theory(DisplayName = "TC-01: Standard mixed array returns first maximum even element index")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase01_StandardMixed(bool recursive)
    {
        var array = new[] { 3, 10, -8, 24, 7, 24, -2, 15 };
        var index = recursive ? Task2Recursive.FindMaxEvenIndexRec(array) : Task2Iterative.FindMaxEvenIndex(array);
        Assert.Equal(3, index);
        Assert.Equal(24, array[index]);
    }

    [Theory(DisplayName = "TC-02: All odd positive array returns sentinel -1")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase02_AllOddPositive(bool recursive)
    {
        var array = new[] { 1, 3, 5, 7, 9 };
        var index = recursive ? Task2Recursive.FindMaxEvenIndexRec(array) : Task2Iterative.FindMaxEvenIndex(array);
        Assert.Equal(-1, index);
    }

    [Theory(DisplayName = "TC-03: All odd negative array returns sentinel -1")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase03_AllOddNegative(bool recursive)
    {
        var array = new[] { -1, -3, -5, -7 };
        var index = recursive ? Task2Recursive.FindMaxEvenIndexRec(array) : Task2Iterative.FindMaxEvenIndex(array);
        Assert.Equal(-1, index);
    }

    [Theory(DisplayName = "TC-04: Single even element returns index 0")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase04_SingleEven(bool recursive)
    {
        var array = new[] { 42 };
        var index = recursive ? Task2Recursive.FindMaxEvenIndexRec(array) : Task2Iterative.FindMaxEvenIndex(array);
        Assert.Equal(0, index);
        Assert.Equal(42, array[index]);
    }

    [Theory(DisplayName = "TC-05: Single odd element returns sentinel -1")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase05_SingleOdd(bool recursive)
    {
        var array = new[] { 17 };
        var index = recursive ? Task2Recursive.FindMaxEvenIndexRec(array) : Task2Iterative.FindMaxEvenIndex(array);
        Assert.Equal(-1, index);
    }

    [Theory(DisplayName = "TC-06: Zero as sole even element returns correct index")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase06_ZeroAsSoleEven(bool recursive)
    {
        var array = new[] { -5, 0, -3, 1 };
        var index = recursive ? Task2Recursive.FindMaxEvenIndexRec(array) : Task2Iterative.FindMaxEvenIndex(array);
        Assert.Equal(1, index);
        Assert.Equal(0, array[index]);
    }

    [Theory(DisplayName = "TC-07: All even negative array returns first occurrence of maximum")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase07_AllEvenNegative(bool recursive)
    {
        var array = new[] { -14, -8, -2, -20, -2 };
        var index = recursive ? Task2Recursive.FindMaxEvenIndexRec(array) : Task2Iterative.FindMaxEvenIndex(array);
        Assert.Equal(2, index);
        Assert.Equal(-2, array[index]);
    }

    [Theory(DisplayName = "TC-08: Maximum even at first index 0")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase08_MaxAtFirstIndex(bool recursive)
    {
        var array = new[] { 100, 2, 4, 6 };
        var index = recursive ? Task2Recursive.FindMaxEvenIndexRec(array) : Task2Iterative.FindMaxEvenIndex(array);
        Assert.Equal(0, index);
        Assert.Equal(100, array[index]);
    }

    [Theory(DisplayName = "TC-09: Maximum even at last index n - 1")]
    [InlineData(false)]
    [InlineData(true)]
    public void TestCase09_MaxAtLastIndex(bool recursive)
    {
        var array = new[] { 2, 4, 6, 100 };
        var index = recursive ? Task2Recursive.FindMaxEvenIndexRec(array) : Task2Iterative.FindMaxEvenIndex(array);
        Assert.Equal(3, index);
        Assert.Equal(100, array[index]);
    }

    // =========================================================================
    // Boundary, Auxiliary, and Parity Predicate Tests
    // =========================================================================

    [Theory(DisplayName = "Parity predicate accurately classifies all integer classes")]
    [InlineData(0, true)]
    [InlineData(2, true)]
    [InlineData(-2, true)]
    [InlineData(100, true)]
    [InlineData(-100, true)]
    [InlineData(1, false)]
    [InlineData(-1, false)]
    [InlineData(73, false)]
    [InlineData(-49, false)]
    public void ParityPredicateWorksCorrectly(int value, bool expected)
    {
        Assert.Equal(expected, Task2Iterative.IsEven(value));
        Assert.Equal(expected, Task2Recursive.IsEven(value));
    }

    [Fact(DisplayName = "Null and empty arrays return sentinel -1")]
    public void NullAndEmptyArraysReturnSentinel()
    {
        Assert.Equal(-1, Task2Iterative.FindMaxEvenIndex(null));
        Assert.Equal(-1, Task2Iterative.FindMaxEvenIndex(Array.Empty<int>()));
        Assert.Equal(-1, Task2Recursive.FindMaxEvenIndexRec(null));
        Assert.Equal(-1, Task2Recursive.FindMaxEvenIndexRec(Array.Empty<int>()));
    }

    [Fact(DisplayName = "InitArray populates within requested bounds")]
    public void InitArrayPopulatesWithinBounds()
    {
        var array = new int[50];
        Task2Iterative.InitArray(array, -20, 20);
        foreach (var val in array)
        {
            Assert.InRange(val, -20, 20);
        }

        var arrayRec = new int[50];
        Task2Recursive.InitArrayRec(arrayRec, -20, 20);
        foreach (var val in arrayRec)
        {
            Assert.InRange(val, -20, 20);
        }
    }

    [Fact(DisplayName = "InitArray throws on invalid range or null")]
    public void InitArrayThrowsOnInvalidRange()
    {
        Assert.Throws<ArgumentNullException>(() => Task2Iterative.InitArray(null!));
        Assert.Throws<ArgumentException>(() => Task2Iterative.InitArray(new int[10], 50, -50));
        Assert.Throws<ArgumentNullException>(() => Task2Recursive.InitArrayRec(null!));
        Assert.Throws<ArgumentException>(() => Task2Recursive.InitArrayRec(new int[10], 50, -50));
    }

    [Fact(DisplayName = "FormatArray and PrintArray produce formatted columns")]
    public void FormatArrayProducesFormattedColumns()
    {
        var array = new[] { 1, -2, 42 };
        var formatted = Task2Iterative.FormatArray(array);
        Assert.Equal("    1   -2   42", formatted);

        var formattedRec = Task2Recursive.FormatArrayRec(array);
        Assert.Equal("    1   -2   42", formattedRec);
    }
}
