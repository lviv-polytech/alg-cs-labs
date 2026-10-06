# Lab 4 — Loops and Function Tabulation

## Tasks

This module implements the Variant 17 loop exercises: the same trigonometric sum in four loop forms (4.1), formula/parameter/graph tabulation (4.2–4.4), four nested-loop forms (4.6), and the recurrent Taylor series for `ln(x)` (4.7). Service methods validate ranges, calculate points as `start + i * step`, and represent undefined values as `NaN` or a missing series result.

**Task 4.5 (Target Hit Detection)** is now implemented: determines whether a point (x, y) falls within a target region consisting of two quarter-circle regions (Parts A and B), generates random shots uniformly in [-R, R] × [-R, R], and displays formatted results. The task uses complex logical expressions (&&, ||) and random number generation as specified in the requirements.

## Run and test

```sh
dotnet build Lb4/Lb4.csproj
dotnet run --project Lb4/Lb4.csproj
dotnet test Lb4.Tests/Lb4.Tests.csproj
```

## Algorithm flowchart

```mermaid
flowchart TD
    Start([Start]) --> Input["Read task parameters"]
    Input --> Valid{"Valid range and step?"}
    Valid -- No --> Error["Report invalid input"]
    Valid -- Yes --> Select{"Selected calculation"}
    Select --> Sum["Evaluate loop sum"]
    Select --> Table["Evaluate each x = start + i * step"]
    Select --> Taylor["Add recurrent Taylor terms"]
    Select --> Target["Check point in target region"]
    Sum --> Output["Print results"]
    Table --> Output
    Taylor --> Output
    Target --> Output
    Error --> End([End])
    Output --> End
```

## UML activity diagram

```mermaid
stateDiagram-v2
    [*] --> ChooseTask
    ChooseTask --> ValidateInputs
    ValidateInputs --> ChooseTask : Invalid input
    ValidateInputs --> Calculate : Valid input
    Calculate --> DisplayResults
    DisplayResults --> [*]
```

## Structure

```mermaid
classDiagram
    class ILab4Calculator {
        <<interface>>
        +SumWithWhile(int) double
        +TabulateFormula(double,double,double) IReadOnlyList
        +TabulateParameterized(double,double,double,double,double,double) IReadOnlyList
        +TabulateGraph(double,double,double,double) IReadOnlyList
        +TabulateLogSeries(double,double,double,double) IReadOnlyList
    }
    class ITargetCalculator {
        <<interface>>
        +InTarget(double,double,double) bool
        +GenerateRandomShot(double) tuple
    }
    class Lab4Calculator
    class TargetCalculator
    class Lab4ServiceFactory
    class TargetCalculatorFactory
    class TableRow
    Lab4Calculator ..|> ILab4Calculator
    TargetCalculator ..|> ITargetCalculator
    Lab4ServiceFactory ..> ILab4Calculator : creates
    TargetCalculatorFactory ..> ITargetCalculator : creates
    ILab4Calculator ..> TableRow : returns
```
