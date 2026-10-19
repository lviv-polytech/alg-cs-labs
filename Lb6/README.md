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

2. **Task 6.2 (Variant 17): Search for the Index of the Maximum Even Element**
   - **Data Domain:** One-dimensional array $a = (a_0, a_1, \dots, a_{n-1})$ of arbitrary dimension $n \ge 1$ with $a_i \in [-50, 50]$.
   - **Evenness Predicate:** $P(x) \iff (x \bmod 2 = 0)$ (correctly handles positive evens, negative evens, and zero: $0 \bmod 2 = 0$).
   - **Solution Existence & Tie-Breaking:**
     - If no even numbers exist ($I_{\text{even}} = \varnothing$), returns sentinel value $k^* = -1$.
     - If duplicates of the maximum even value exist, returns the first occurrence (smallest index): $k^* = \min \{ i \in I_{\text{even}} \mid a_i = \max_{j \in I_{\text{even}}} a_j \}$.
   - **Decomposition (Single Responsibility):** Independent pure methods in `Task2Iterative` and `Task2Recursive` (`IsEven`, `InitArray`, `PrintArray`, `FindMaxEvenIndex` and recursive equivalents `InitArrayRec`, `PrintArrayRec`, `FindMaxEvenIndexRec`).

3. **Task 6.3: Stream & Keyboard Array Input with Ordinary and Generic Functions**
   - **Data Domain:** Sequential reading and formatted printing of 1D arrays of size $n$ from input streams (`TextReader`).
   - **Dual Typification:**
     - **Part A (Ordinary Functions):** Type-specialized for `int`: `InputArray`, `PrintArray`, `FormatArray` in `Task3Iterative` and `Task3Recursive`.
     - **Part B (Generic Functions):** Parametric polymorphism for generic type $T$ (`int`, `double`, `string`): `InputArrayTemplate<T>`, `PrintArrayTemplate<T>`, `FormatArrayTemplate<T>` in `Task3Iterative` and `Task3Recursive`.
   - **Deterministic Testing:** Decoupled stream reading using `StreamTokenReader` allowing automated Unit-testing over `StringReader`.

4. **Task 6.4: Dynamic Real Array Analysis and In-Place Stable Partition**
   - **Data Domain:** Dynamic array of $n$ double-precision floating-point numbers $a = (a_0, \dots, a_{n-1})$, $a_i \in \mathbb{R}$.
   - **Calculations:**
     - **1.1. Count Positive Elements:** $P(x) \iff x > 0.0$ (zero is strictly non-positive).
     - **1.2. Sum After Last Zero:**
       - Search for the last zero: $k^* = \max \{ i \in \{0, \dots, n - 1\} \mid |a_i| < 10^{-9} \}$ (searching right-to-left).
       - Suffix sum strictly after $k^*$: $S = \sum_{j=k^*+1}^{n-1} a_j$.
       - Returns $0.0$ if no zero exists ($k^* = -1$) or if the last zero is the final element ($k^* = n - 1$).
   - **In-Place Stable Partition:**
     - Predicate: Integer part $\operatorname{trunc}(x) \le 1 \iff x < 2.0$.
     - Stable reordering preserving relative order using in-place cyclic shift with $O(1)$ auxiliary space (no secondary arrays).
   - **Decomposition:** Pure static methods in `Task4Iterative` and `Task4Recursive` delegated by `ILab6ArrayAlgorithms` / `Lab6ArrayAlgorithms`.

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
    Start["Start Task 6.4 Pipeline"] --> ReadN["Read size n and allocate array a[n]"]
    ReadN --> InitArray["Populate array (Random or Input)"]
    InitArray --> PrintInit["Formatted print of original array"]
    PrintInit --> CountPos["Count positive elements: a[i] > 0.0"]
    CountPos --> FindZero["Find last zero index k (right-to-left, |a[i]| < 1e-9)"]
    FindZero --> CheckZero{"k >= 0 and k < n - 1?"}
    CheckZero -- "Yes" --> SumSuffix["Compute sum of suffix: sum(a[k+1 .. n-1])"]
    CheckZero -- "No" --> ZeroSum["Sum after last zero = 0.0"]
    SumSuffix --> Partition["In-place stable partition: trunc(x) <= 1 first"]
    ZeroSum --> Partition
    Partition --> PrintMod["Formatted print of modified array"]
    PrintMod --> Finish["End Task 6.4 Pipeline"]
```

### Activity / State Diagram (`stateDiagram-v2`)

```mermaid
stateDiagram-v2
    [*] --> Allocation
    Allocation --> DataInitialization: "Generate or input n real elements"
    DataInitialization --> DisplayInitial: "Formatted print to output"
    DisplayInitial --> AnalysisPhase
    
    state AnalysisPhase {
        [*] --> ComputePositive: "Count positive elements (x > 0.0)"
        ComputePositive --> LocateLastZero: "Search for last zero (right-to-left)"
        LocateLastZero --> ComputeSumAfter: "Sum suffix strictly after last zero"
        ComputeSumAfter --> [*]
    }
    
    AnalysisPhase --> InPlacePartition: "Stable partition: shift elements with trunc(x) <= 1"
    InPlacePartition --> DisplayModified: "Formatted print of partitioned array"
    DisplayModified --> [*]
```

### Class Diagram (`classDiagram`)

```mermaid
classDiagram
    class Task4Iterative {
        +IsZero(x: double, eps: double) bool
        +HasIntPartAtMostOne(x: double) bool
        +InitArray(array: double[], minVal: double, maxVal: double, random: Random) void
        +PrintArray(array: double[], writer: TextWriter, fieldWidth: int, precision: int) void
        +FormatArray(array: double[], fieldWidth: int, precision: int) string
        +CountPositive(array: double[]) int
        +FindLastZeroIndex(array: double[], eps: double) int
        +SumAfterIndex(array: double[], startIndex: int) double
        +SumAfterLastZero(array: double[], eps: double) double
        +PartitionByIntPart(array: double[]) void
    }

    class Task4Recursive {
        +IsZero(x: double, eps: double) bool
        +HasIntPartAtMostOne(x: double) bool
        +InitArrayRec(array: double[], minVal: double, maxVal: double, index: int, random: Random) void
        +PrintArrayRec(array: double[], writer: TextWriter, fieldWidth: int, precision: int) void
        +FormatArrayRec(array: double[], index: int, fieldWidth: int, precision: int) string
        +CountPositiveRec(array: double[], index: int) int
        +FindLastZeroIndexRec(array: double[], index: int, eps: double) int
        +SumFromIndexRec(array: double[], index: int) double
        +SumAfterLastZeroRec(array: double[], eps: double) double
        +ShiftRightRec(array: double[], writePos: int, k: int) void
        +PartitionByIntPartRec(array: double[], index: int, writePos: int) void
    }

    class ILab6ArrayAlgorithms {
        <<interface>>
        +CountPositive(values: double[], recursive: bool) int
        +FindLastZeroIndex(values: double[], recursive: bool) int
        +SumAfterIndex(values: double[], index: int, recursive: bool) double
        +PartitionByIntegerPart(values: double[], recursive: bool) void
    }

    class Lab6ArrayAlgorithms {
        +CountPositive(values: double[], recursive: bool) int
        +FindLastZeroIndex(values: double[], recursive: bool) int
        +SumAfterIndex(values: double[], index: int, recursive: bool) double
        +PartitionByIntegerPart(values: double[], recursive: bool) void
    }

    ILab6ArrayAlgorithms <|.. Lab6ArrayAlgorithms : implements
    Lab6ArrayAlgorithms ..> Task4Iterative : delegates
    Lab6ArrayAlgorithms ..> Task4Recursive : delegates
```
