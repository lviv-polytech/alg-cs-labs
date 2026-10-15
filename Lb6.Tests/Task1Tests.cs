namespace Lpnu.Lb6.Tests;

public class Task1Tests
{
    // 25-element reference test vector from mathematical specification (Variant 17)
    private static readonly int[] ReferenceArray =
    [
        12, 10, 15, 20, 30, // Group 1: i=0..4, r[0]=12 is divisible by 6
        25, 10, 15, 20, 30, // Group 2: i=5..9, r[5]=25 is NOT divisible by 6 (preserved)
        42, 10, 15, 20, 30, // Group 3: i=10..14, r[10]=42 is divisible by 6
        19, 10, 15, 20, 30, // Group 4: i=15..19, r[15]=19 is NOT divisible by 6 (preserved)
        73, 10, 15, 20, 30  // Group 5: i=20..24, r[20]=73 is NOT divisible by 6 (preserved)
    ];

    [Fact(DisplayName = "Subtask 1 (Iterative): Reference array yields Count=22, Sum=429, and selective zeroing")]
    public void IterativeMethodsMatchReferenceCalculations()
    {
        var testArray = (int[])ReferenceArray.Clone();

        var count = Task1Iterative.CountElements(testArray);
        var sum = Task1Iterative.SumElements(testArray);

        Assert.Equal(22, count);
        Assert.Equal(429, sum);

        Task1Iterative.ZeroElements(testArray);

        Assert.Equal(25, testArray[5]);
        Assert.Equal(19, testArray[15]);
        Assert.Equal(73, testArray[20]);

        for (var i = 0; i < testArray.Length; i++)
        {
            if (i != 5 && i != 15 && i != 20)
            {
                Assert.Equal(0, testArray[i]);
            }
        }
    }

    [Fact(DisplayName = "Subtask 1 (Iterative): ProcessSelection returns combined result in correct sequence")]
    public void IterativeProcessSelectionMatches()
    {
        var testArray = (int[])ReferenceArray.Clone();
        var result = Task1Iterative.ProcessSelection(testArray);

        Assert.Equal(22, result.Count);
        Assert.Equal(429, result.Sum);
        Assert.Equal(25, testArray[5]);
        Assert.Equal(19, testArray[15]);
        Assert.Equal(73, testArray[20]);
    }

    [Fact(DisplayName = "Subtask 1 (Iterative): Validates array size and throws ArgumentException")]
    public void IterativeThrowsOnInvalidInputs()
    {
        Assert.Throws<ArgumentNullException>(() => Task1Iterative.CountElements(null!));
        Assert.Throws<ArgumentException>(() => Task1Iterative.CountElements(new int[10]));
        Assert.Throws<ArgumentException>(() => Task1Iterative.SumElements(new int[0]));
        Assert.Throws<ArgumentException>(() => Task1Iterative.ZeroElements(new int[30]));
    }

    [Fact(DisplayName = "Subtask 1 (Recursive): Reference array yields Count=22, Sum=429, and selective zeroing recursively")]
    public void RecursiveMethodsMatchReferenceCalculations()
    {
        var testArray = (int[])ReferenceArray.Clone();

        var count = Task1Recursive.CountElementsRec(testArray);
        var sum = Task1Recursive.SumElementsRec(testArray);

        Assert.Equal(22, count);
        Assert.Equal(429, sum);

        Task1Recursive.ZeroElementsRec(testArray);

        Assert.Equal(25, testArray[5]);
        Assert.Equal(19, testArray[15]);
        Assert.Equal(73, testArray[20]);

        for (var i = 0; i < testArray.Length; i++)
        {
            if (i != 5 && i != 15 && i != 20)
            {
                Assert.Equal(0, testArray[i]);
            }
        }
    }

    [Fact(DisplayName = "Subtask 1 (Recursive): ProcessSelectionRec returns combined result recursively")]
    public void RecursiveProcessSelectionMatches()
    {
        var testArray = (int[])ReferenceArray.Clone();
        var result = Task1Recursive.ProcessSelectionRec(testArray);

        Assert.Equal(22, result.Count);
        Assert.Equal(429, result.Sum);
        Assert.Equal(25, testArray[5]);
        Assert.Equal(19, testArray[15]);
        Assert.Equal(73, testArray[20]);
    }

    [Fact(DisplayName = "Subtask 1 (Recursive): Validates array size and throws ArgumentException")]
    public void RecursiveThrowsOnInvalidInputs()
    {
        Assert.Throws<ArgumentNullException>(() => Task1Recursive.CountElementsRec(null!));
        Assert.Throws<ArgumentException>(() => Task1Recursive.CountElementsRec(new int[10]));
        Assert.Throws<ArgumentException>(() => Task1Recursive.SumElementsRec(new int[0]));
        Assert.Throws<ArgumentException>(() => Task1Recursive.ZeroElementsRec(new int[30]));
    }

    [Fact(DisplayName = "Subtask 1: Iterative and recursive approaches produce equivalent results on random array")]
    public void IterativeAndRecursiveAreEquivalent()
    {
        var original = new int[25];
        Task1Iterative.GenerateArray(original, 4, 73);

        var arrIterative = (int[])original.Clone();
        var arrRecursive = (int[])original.Clone();

        var itCount = Task1Iterative.CountElements(arrIterative);
        var recCount = Task1Recursive.CountElementsRec(arrRecursive);
        Assert.Equal(itCount, recCount);

        var itSum = Task1Iterative.SumElements(arrIterative);
        var recSum = Task1Recursive.SumElementsRec(arrRecursive);
        Assert.Equal(itSum, recSum);

        Task1Iterative.ZeroElements(arrIterative);
        Task1Recursive.ZeroElementsRec(arrRecursive);
        Assert.Equal(arrIterative, arrRecursive);
    }
}
