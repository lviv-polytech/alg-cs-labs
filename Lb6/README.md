# Lab 6 — One-Dimensional Arrays (C# / .NET)

Modular .NET solution demonstrating one-dimensional array algorithms implemented via both iterative loops and pure recursive techniques.

## Subtasks Overview

1. **Task 6.1 (Variant 17): Array Selection, Counting, Summing, and Zeroing**
   - **Data Domain:** 25 integer elements with uniform distribution in $[4, 73]$.
   - **Selection Criterion:** $(r_i \bmod 6 = 0) \lor (i \bmod 5 \neq 0)$.
   - **Invariants:**
     - Matching element count: $20 \le K \le 25$.
     - Cumulative sum: $80 \le S \le 1825$.
     - Execution sequence: $\text{Generate} \to \text{Print} \to (\text{Count}, \text{Sum}) \to \text{Zero} \to \text{Print}$.
   - **Decomposition (Single Responsibility):** Independent pure methods in `Task1Iterative` and `Task1Recursive` (`GenerateArray`, `PrintArray`, `CountElements`, `SumElements`, `ZeroElements`).
2. **Task 6.2:** Search for the first index of the maximum even integer (returns `-1` if absent).
3. **Task 6.3:** Ordinary and generic input handlers reading typed values from `TextReader`.
4. **Task 6.4:** Dynamic array manipulation: positive count, sum after last zero, and stable partition around value $2.0$.

## Execution Guide

Build, run, and test the project using the standard .NET CLI:

```bash
# Build the project
dotnet build Lb6/Lb6.csproj

# Run interactive console application
dotnet run --project Lb6/Lb6.csproj

# Execute test suite
dotnet test Lb6.Tests/Lb6.Tests.csproj
```

## Diagrams (Mermaid.js)

### Flowchart (`flowchart TD`)

```mermaid
flowchart TD
    Start["Start Task 6.1: Variant 17"] --> Gen["Generate array: 25 integers in range [4, 73]"]
    Gen --> PrintInit["Print initial formatted array: width = 4"]
    PrintInit --> ProcessLoop["Iterate / Recurse over indices: i = 0 .. 24"]
    ProcessLoop --> CheckMatch{"(r[i] % 6 == 0) || (i % 5 != 0)?"}
    CheckMatch -- "Yes" --> Accumulate["Increment count K, accumulate sum S"]
    CheckMatch -- "No" --> Pass["Retain element intact"]
    Accumulate --> Next["Advance to index i + 1"]
    Pass --> Next
    Next --> More{"i < 25?"}
    More -- "Yes" --> CheckMatch
    More -- "No" --> ZeroStep["Zero out matching elements in-place"]
    ZeroStep --> PrintResults["Print Count K and Sum S"]
    PrintResults --> PrintFinal["Print modified formatted array (with zeros)"]
    PrintFinal --> Finish["End"]
```

### Activity / State Diagram (`stateDiagram-v2`)

```mermaid
stateDiagram-v2
    [*] --> Initialization
    Initialization --> ArrayGeneration: "Generate 25 elements in [4, 73]"
    ArrayGeneration --> DisplayInitial: "Formatted print to stdout"
    DisplayInitial --> Processing
    
    state Processing {
        [*] --> CountElements: "Count matching elements (iterative / recursive)"
        CountElements --> SumElements: "Sum matching elements (iterative / recursive)"
        SumElements --> ZeroElements: "In-place zeroing after metrics calculated"
        ZeroElements --> [*]
    }
    
    Processing --> DisplayMetrics: "Print K and S"
    DisplayMetrics --> DisplayModified: "Formatted print of modified array"
    DisplayModified --> [*]
```

### Class Diagram (`classDiagram`)

```mermaid
classDiagram
    class Task1Result {
        <<readonly record struct>>
        +Count: int
        +Sum: int
    }

    class Task1Iterative {
        +IsSelected(value: int, index: int) bool
        +GenerateArray(array: int[], low: int, high: int) void
        +PrintArray(array: int[], writer: TextWriter) void
        +FormatArray(array: int[]) string
        +CountElements(array: int[]) int
        +SumElements(array: int[]) int
        +ZeroElements(array: int[]) void
        +ProcessSelection(array: int[]) Task1Result
    }

    class Task1Recursive {
        +IsSelected(value: int, index: int) bool
        +GenerateArrayRec(array: int[], low: int, high: int, index: int) void
        +PrintArrayRec(array: int[], writer: TextWriter, index: int) void
        +FormatArrayRec(array: int[], index: int) string
        +CountElementsRec(array: int[], index: int) int
        +SumElementsRec(array: int[], index: int) int
        +ZeroElementsRec(array: int[], index: int) void
        +ProcessSelectionRec(array: int[], index: int) Task1Result
    }

    Task1Iterative --> Task1Result : creates
    Task1Recursive --> Task1Result : creates
```
