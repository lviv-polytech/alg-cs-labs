# Lab 5 — Functions and Recursion

## Tasks

The calculator implements the introductory isolated calculation (5.0: `calculate(x, y) = (x^2 + y) / 2.0`), the rational helper-function expression (5.1), the logarithm Taylor series and its recurrent term calculation (5.2), the six-term piecewise helper and nested table expression (5.3), five iterative/recursive implementations of the trigonometric sum (5.4), and recursive geometric-progression term/sum calculations with call-chain depths (5.5). Series routines validate their convergence domains and include the terminal term below epsilon. The stated check `h(0.5) ≈ 1.2553` conflicts with the specified sum `sum(x^i/(2i)!, i=0..5)`; evaluating that exact six-term formula gives approximately `1.2605918`, which is implemented.

## Run and test

```sh
dotnet build Lb5/Lb5.csproj
dotnet run --project Lb5/Lb5.csproj
dotnet test Lb5.Tests/Lb5.Tests.csproj
```

## Algorithm flowchart

```mermaid
flowchart TD
    Start([Start]) --> Read["Read task input"]
    Read --> Check{"Valid domain and bounds?"}
    Check -- No --> Error["Report input error"]
    Check -- Yes --> Select{"Task"}
    Select --> Formula["Evaluate helper-function formula"]
    Select --> Series["Recur terms and accumulate sum"]
    Select --> Recursion["Evaluate base and recursive cases"]
    Formula --> Print["Display results"]
    Series --> Print
    Recursion --> Print
    Error --> End([End])
    Print --> End
```

## UML activity diagram

```mermaid
stateDiagram-v2
    [*] --> AwaitingTask
    AwaitingTask --> ReadingParameters
    ReadingParameters --> RejectingInput : Outside domain
    RejectingInput --> AwaitingTask
    ReadingParameters --> Computing : Valid parameters
    Computing --> Presenting
    Presenting --> [*]
```

## Structure

```mermaid
classDiagram
    class ILab5Calculator {
        <<interface>>
        +Calculate(double,double) double
        +CalculateExpression(double,double) double
        +CalculateLogSeries(double,double) SeriesResult
        +TabulateHelperExpression(double,double,int) IReadOnlyList
        +RecursiveSumUpIncreasing(int) double
        +CalculateProgression(double,double,int) ProgressionResult
    }
    class Lab5Calculator
    class Lab5ServiceFactory
    class SeriesResult
    class ProgressionResult
    Lab5Calculator ..|> ILab5Calculator
    Lab5ServiceFactory ..> ILab5Calculator : creates
    ILab5Calculator ..> SeriesResult : returns
    ILab5Calculator ..> ProgressionResult : returns
```
