# Lab 7 — Matrix Algorithms

## Tasks

The module provides iterative and recursive matrix variants for the non-multiple-of-six scan and zeroing (7.1), row-pair minimum/maximum swaps (7.2), and largest-value diagonal placement plus first non-positive row lookup (7.3). Rows are stably ordered by columns 1, 2, and 4 (one-based), and the diagonal stage breaks ties in row-major order.

## Run and test

```sh
dotnet build Lb7/Lb7.csproj
dotnet run --project Lb7/Lb7.csproj
dotnet test Lb7.Tests/Lb7.Tests.csproj
```

## Algorithm flowchart

```mermaid
flowchart TD
    Start([Start]) --> Input["Read or generate matrix"]
    Input --> Operation{"Select matrix task"}
    Operation --> Scan["Count, sum, and zero selected cells"]
    Operation --> Sort["Stable-sort rows by selected keys"]
    Operation --> Swap["Swap extrema in adjacent row pairs"]
    Operation --> Diagonal["Select remaining maximum for each diagonal"]
    Scan --> Print["Display result matrices"]
    Sort --> Print
    Swap --> Print
    Diagonal --> Print
    Print --> End([End])
```

## UML activity diagram

```mermaid
stateDiagram-v2
    [*] --> ChoosingTraversal
    ChoosingTraversal --> IterativeMatrixWalk : Iterative
    ChoosingTraversal --> RecursiveMatrixWalk : Recursive
    IterativeMatrixWalk --> ApplyingOperation
    RecursiveMatrixWalk --> ApplyingOperation
    ApplyingOperation --> ShowingOutput
    ShowingOutput --> [*]
```

## Structure

```mermaid
classDiagram
    class ILab7MatrixAlgorithms {
        <<interface>>
        +ZeroSelected(int[,],bool) tuple
        +SortRows(int[,],bool) void
        +SwapRowExtremes(int[,],bool) void
        +PlaceLargestValuesOnDiagonal(double[,],bool) void
        +FindFirstRowWithoutPositive(double[,],bool) int
    }
    class Lab7MatrixAlgorithms
    class Lab7ServiceFactory
    Lab7MatrixAlgorithms ..|> ILab7MatrixAlgorithms
    Lab7ServiceFactory ..> ILab7MatrixAlgorithms : creates
```
