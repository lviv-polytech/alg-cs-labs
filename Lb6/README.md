# Lab 6 — Array Processing

## Tasks

Lab 6 exposes iterative and recursive forms for the selected-element array scan (6.1), maximum-even-index search (6.2), ordinary and generic array input (6.3), and dynamic-array statistics plus stable partitioning (6.4). Recursive alternatives use an explicit index parameter. The 6.4 stable partition groups values `x < 2` before values `x >= 2`; the last-zero search returns `-1` when no zero exists.

## Run and test

```sh
dotnet build Lb6/Lb6.csproj
dotnet run --project Lb6/Lb6.csproj
dotnet test Lb6.Tests/Lb6.Tests.csproj
```

## Algorithm flowchart

```mermaid
flowchart TD
    Start([Start]) --> Create["Read or create an array"]
    Create --> Select{"Choose array operation"}
    Select --> Scan["Scan using loop or recursion"]
    Select --> Search["Search maximum even or last zero"]
    Select --> Partition["Stable partition by x < 2"]
    Scan --> Result["Return values without printing in algorithms"]
    Search --> Result
    Partition --> Result
    Result --> End([End])
```

## UML activity diagram

```mermaid
stateDiagram-v2
    [*] --> SelectingMode
    SelectingMode --> IterativeTraversal : Loop mode
    SelectingMode --> RecursiveTraversal : Recursive mode
    IterativeTraversal --> UpdatingResult
    RecursiveTraversal --> UpdatingResult
    UpdatingResult --> DisplayingArray
    DisplayingArray --> [*]
```

## Structure

```mermaid
classDiagram
    class ILab6ArrayAlgorithms {
        <<interface>>
        +AnalyzeSelected(int[],bool) tuple
        +FindMaximumEvenIndex(int[],bool) int
        +InputGenericArray~T~(T[],TextReader,TextWriter,Func,bool) void
        +PartitionByIntegerPart(double[],bool) void
    }
    class Lab6ArrayAlgorithms
    class Lab6ServiceFactory
    Lab6ArrayAlgorithms ..|> ILab6ArrayAlgorithms
    Lab6ServiceFactory ..> ILab6ArrayAlgorithms : creates
```
