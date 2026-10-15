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
    Start["Start Task 6.3: Stream Array Input & Print"] --> ChooseMode{"Choose Paradigm: Iterative or Recursive?"}
    
    ChooseMode -- "Iterative" --> ItLoop["For i = 0 to n - 1"]
    ItLoop --> ItPrompt["Optional prompt: a[i] = "]
    ItPrompt --> ItRead["Read token via StreamTokenReader.ReadNextToken"]
    ItRead --> ItCheck{"i == n - 1?"}
    ItCheck -- "No" --> ItLoop
    ItCheck -- "Yes" --> ItPrint["Format print array elements with fixed width"]
    
    ChooseMode -- "Recursive" --> RecCall["Recursive step at index i (initial: 0)"]
    RecCall --> BaseCase{"i >= n?"}
    BaseCase -- "Yes" --> Terminate["Return (stop recursion)"]
    BaseCase -- "No" --> RecPrompt["Optional prompt: a[i] = "]
    RecPrompt --> RecRead["Read token via StreamTokenReader.ReadNextToken"]
    RecRead --> RecNext["Recursive call for index i + 1"]
    RecNext --> BaseCase
    
    ItPrint --> Finish["End"]
    Terminate --> RecPrint["Format print array elements recursively"]
    RecPrint --> Finish
```

### Activity / State Diagram (`stateDiagram-v2`)

```mermaid
stateDiagram-v2
    [*] --> Setup
    Setup --> StreamInjection: "Inject TextReader (Console.In or StringReader)"
    StreamInjection --> InputPhase
    
    state InputPhase {
        [*] --> CheckBoundary: "Check index i < n"
        CheckBoundary --> ReadToken: "StreamTokenReader extraction into a[i]"
        ReadToken --> AdvanceIndex: "Increment index (loop or tail call)"
        AdvanceIndex --> CheckBoundary
        CheckBoundary --> [*]: "i >= n"
    }
    
    InputPhase --> FormatPrintPhase: "Pass populated array to output formatter"
    
    state FormatPrintPhase {
        [*] --> FormatElement: "PadLeft(fieldWidth, a[i])"
        FormatElement --> NextPrint: "Advance to i + 1"
        NextPrint --> FormatElement: "i < n"
        NextPrint --> [*]: "i >= n -> return formatted string"
    }
    
    FormatPrintPhase --> [*]
```

### Class Diagram (`classDiagram`)

```mermaid
classDiagram
    class StreamTokenReader {
        +ReadNextToken(reader: TextReader) string
    }

    class Task3Iterative {
        +InputArray(array: int[], reader: TextReader, writer: TextWriter) void
        +PrintArray(array: int[], writer: TextWriter, fieldWidth: int) void
        +FormatArray(array: int[], fieldWidth: int) string
        +InputArrayTemplate~T~(array: T[], reader: TextReader, parser: Func~string, T~, writer: TextWriter) void
        +PrintArrayTemplate~T~(array: T[], writer: TextWriter, fieldWidth: int) void
        +FormatArrayTemplate~T~(array: T[], fieldWidth: int) string
    }

    class Task3Recursive {
        +InputArrayRec(array: int[], reader: TextReader, index: int, writer: TextWriter) void
        +PrintArrayRec(array: int[], writer: TextWriter, index: int, fieldWidth: int) void
        +FormatArrayRec(array: int[], index: int, fieldWidth: int) string
        +InputArrayTemplateRec~T~(array: T[], reader: TextReader, parser: Func~string, T~, index: int, writer: TextWriter) void
        +PrintArrayTemplateRec~T~(array: T[], writer: TextWriter, index: int, fieldWidth: int) void
        +FormatArrayTemplateRec~T~(array: T[], index: int, fieldWidth: int) string
    }

    class ILab6ArrayAlgorithms {
        <<interface>>
        +InputIntArray(values: int[], input: TextReader, output: TextWriter, recursive: bool) void
        +InputGenericArray~T~(values: T[], input: TextReader, output: TextWriter, parser: Func~string, T~, recursive: bool) void
    }

    class Lab6ArrayAlgorithms {
        +InputIntArray(values: int[], input: TextReader, output: TextWriter, recursive: bool) void
        +InputGenericArray~T~(values: T[], input: TextReader, output: TextWriter, parser: Func~string, T~, recursive: bool) void
    }

    ILab6ArrayAlgorithms <|.. Lab6ArrayAlgorithms : implements
    Lab6ArrayAlgorithms ..> Task3Iterative : delegates
    Lab6ArrayAlgorithms ..> Task3Recursive : delegates
    Task3Iterative ..> StreamTokenReader : uses
    Task3Recursive ..> StreamTokenReader : uses
```
