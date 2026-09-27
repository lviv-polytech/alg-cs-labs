# Lab 3 (Lb3) - Branching Algorithms (Variant 17)

## 1. Task Description

The objective of Laboratory Work 3 is to master algorithmic branching constructs in C#. The assignment consists of three distinct sub-tasks unified in a modular console application.

---

### Task 3.1: Piecewise Function of a Single Variable

Calculates the function $Y = m^2 + 1 + \Phi(m)$ for user-entered real arguments $m$ and $n$, where $\Phi(m)$ is defined piecewise:

$$\Phi(m) = \begin{cases} 4m^7 - m^5 + m^3 - 2, & m < 4 \\ \arctan\left(\frac{|m| + 1}{2}\right) + 8.3m, & 4 \le m < 7 \\ \ln(|2m + e^{4m + 1}|), & m \ge 7 \end{cases}$$

---

### Task 3.2: Parameterized Branching Function

Calculates $F(x)$ given real parameters $a$, $b$, $c$ and argument $x$:

$$F(x) = \begin{cases} ax^2 - cx + b, & x + 10 < 0 \text{ and } b \ne 0 \\ \frac{x - a}{x - c}, & x + 10 > 0 \text{ and } b = 0 \\ -\frac{x}{a - c}, & \text{otherwise} \end{cases}$$

#### Implementation Requirement:
The algorithm is evaluated simultaneously using two branching forms:
1. **Short-form branching:** using strictly `if` statements without `else`.
2. **Full-form branching:** using `if ... else if ... else` structures.

The results of both methods must match. Special care is taken to handle singularities where denominators $(x - c)$ or $(a - c)$ vanish.

---

### Task 3.3: Graph-Based Piecewise Function

Calculates the function $y(x, R)$ given argument $x$ and scale/radius parameter $R > 0$, partitioned across four geometric intervals:

$$y = \begin{cases} 1, & x \le -1 - R \\ -\sqrt{R^2 - (x + 1)^2}, & -1 - R < x \le -1 \\ -R, & -1 < x \le 2 \\ \frac{R(x - 4)}{2}, & x > 2 \end{cases}$$

#### Geometric Analysis:
- **Interval 1 ($x \le -1 - R$):** Constant horizontal line $y = 1$.
- **Interval 2 ($-1 - R < x \le -1$):** Lower circular arc of radius $R$ centered at $(-1, 0)$.
- **Interval 3 ($-1 < x \le 2$):** Constant horizontal line $y = -R$.
- **Interval 4 ($x > 2$):** Linear ray passing through $(2, -R)$ and intersecting the x-axis at $(4, 0)$.

---

## 2. Execution Guide

Execute commands from the repository root using the .NET CLI:

### Build
```sh
dotnet build Lb3/Lb3.csproj
```

### Run
```sh
dotnet run --project Lb3/Lb3.csproj
```

### Run Unit Tests
```sh
dotnet test Lb3.Tests/Lb3.Tests.csproj
```

---

## 3. Architecture & Diagrams

### Algorithmic Flowchart (`flowchart TD`)

```mermaid
flowchart TD
    Start(["Start Lab 3"]) --> Select["Select Task: 1 (3.1), 2 (3.2), 3 (3.3), 4 (All), 0 (Exit)"]
    Select --> ChoiceCheck{"Condition: (User selection)"}
    
    ChoiceCheck -- "1" --> T1["Read m, n"]
    T1 --> T1Check{"Condition: (m < 4?)"}
    T1Check -- "Yes" --> T1P1["Phi = 4m^7 - m^5 + m^3 - 2"]
    T1Check -- "No" --> T1MidCheck{"Condition: (m < 7?)"}
    T1MidCheck -- "Yes" --> T1P2["Phi = arctan((|m|+1)/2) + 8.3m"]
    T1MidCheck -- "No" --> T1P3["Phi = ln(|2m + e^(4m+1)|)"]
    T1P1 --> T1Final["Y = m^2 + 1 + Phi"]
    T1P2 --> T1Final
    T1P3 --> T1Final
    T1Final --> Print1["Display Y and Phi"] --> Select

    ChoiceCheck -- "2" --> T2["Read a, b, c, x"]
    T2 --> T2Branch{"Condition: (x + 10 < 0 and b != 0?)"}
    T2Branch -- "Yes" --> T2F1["F = a*x^2 - c*x + b"]
    T2Branch -- "No" --> T2Branch2{"Condition: (x + 10 > 0 and b == 0?)"}
    T2Branch2 -- "Yes" --> T2Div1{"Condition: (x == c?)"}
    T2Div1 -- "Yes" --> T2Err1["Throw: Division by zero"]
    T2Div1 -- "No" --> T2F2["F = (x - a) / (x - c)"]
    T2Branch2 -- "No" --> T2Div2{"Condition: (a == c?)"}
    T2Div2 -- "Yes" --> T2Err2["Throw: Division by zero"]
    T2Div2 -- "No" --> T2F3["F = -x / (a - c)"]
    T2F1 --> T2Verify["Compare Short-form vs Full-form results"]
    T2F2 --> T2Verify
    T2F3 --> T2Verify
    T2Verify --> Print2["Display F(x) and equality check"] --> Select

    ChoiceCheck -- "3" --> T3["Read x, R (R > 0)"]
    T3 --> T3B1{"Condition: (x <= -1 - R?)"}
    T3B1 -- "Yes" --> T3Y1["y = 1"]
    T3B1 -- "No" --> T3B2{"Condition: (x <= -1?)"}
    T3B2 -- "Yes" --> T3Y2["y = -sqrt(R^2 - (x+1)^2)"]
    T3B2 -- "No" --> T3B3{"Condition: (x <= 2?)"}
    T3B3 -- "Yes" --> T3Y3["y = -R"]
    T3B3 -- "No" --> T3Y4["y = R * (x - 4) / 2"]
    T3Y1 --> Print3["Display y(x, R) and interval"] --> Select
    T3Y2 --> Print3
    T3Y3 --> Print3
    T3Y4 --> Print3

    ChoiceCheck -- "0" --> Exit(["Exit"])
```

### UML Activity Diagram (`stateDiagram-v2`)

```mermaid
stateDiagram-v2
    [*] --> MenuIdle
    MenuIdle --> Task31Executing : Select 1
    MenuIdle --> Task32Executing : Select 2
    MenuIdle --> Task33Executing : Select 3
    MenuIdle --> RunAllExecuting : Select 4
    MenuIdle --> [*] : Select 0

    state Task31Executing {
        [*] --> ReadInputs31
        ReadInputs31 --> BranchEvaluation31 : Input m, n
        BranchEvaluation31 --> ComputeOutput31 : Evaluate Phi(m)
        ComputeOutput31 --> [*] : Return Task31Result
    }

    state Task32Executing {
        [*] --> ReadInputs32
        ReadInputs32 --> EvaluateShortForm : Input a, b, c, x
        EvaluateShortForm --> EvaluateFullForm : Compute using 'if'
        EvaluateFullForm --> CompareResults : Compute using 'if-else'
        CompareResults --> [*] : Return Task32Result
    }

    state Task33Executing {
        [*] --> ReadInputs33
        ReadInputs33 --> DetermineInterval : Input x, R > 0
        DetermineInterval --> EvaluateGeometricBranch : Match interval
        EvaluateGeometricBranch --> [*] : Return Task33Result
    }

    Task31Executing --> MenuIdle : Complete
    Task32Executing --> MenuIdle : Complete
    Task33Executing --> MenuIdle : Complete
    RunAllExecuting --> MenuIdle : Complete
```

### Structural Diagram (`classDiagram`)

```mermaid
classDiagram
    class ITask31Calculator {
        <<interface>>
        +Calculate(double m, double n) Task31Result
    }

    class ITask32Calculator {
        <<interface>>
        +Calculate(double a, double b, double c, double x) Task32Result
        +CalculateShortForm(double a, double b, double c, double x) double
        +CalculateFullForm(double a, double b, double c, double x) double
    }

    class ITask33Calculator {
        <<interface>>
        +Calculate(double x, double r) Task33Result
    }

    class Task31Result {
        <<struct>>
        +double M
        +double N
        +double Phi
        +double Y
    }

    class Task32Result {
        <<struct>>
        +double A
        +double B
        +double C
        +double X
        +double ResultShortForm
        +double ResultFullForm
        +bool ResultsMatch
    }

    class Task33Result {
        <<struct>>
        +double X
        +double R
        +double Y
        +string IntervalDescription
    }

    class Task31Calculator {
        +Calculate(double m, double n) Task31Result
    }

    class Task32Calculator {
        +Calculate(double a, double b, double c, double x) Task32Result
        +CalculateShortForm(double a, double b, double c, double x) double
        +CalculateFullForm(double a, double b, double c, double x) double
    }

    class Task33Calculator {
        +Calculate(double x, double r) Task33Result
    }

    class Lb3ServiceFactory {
        +CreateTask31Calculator() ITask31Calculator
        +CreateTask32Calculator() ITask32Calculator
        +CreateTask33Calculator() ITask33Calculator
    }

    class Lb3App {
        -ITask31Calculator _task31Calculator
        -ITask32Calculator _task32Calculator
        -ITask33Calculator _task33Calculator
        -IConsoleInputReader _inputReader
        +Run() void
    }

    ITask31Calculator <|.. Task31Calculator : implements
    ITask32Calculator <|.. Task32Calculator : implements
    ITask33Calculator <|.. Task33Calculator : implements

    ITask31Calculator --> Task31Result : returns
    ITask32Calculator --> Task32Result : returns
    ITask33Calculator --> Task33Result : returns

    Lb3ServiceFactory ..> ITask31Calculator : creates
    Lb3ServiceFactory ..> ITask32Calculator : creates
    Lb3ServiceFactory ..> ITask33Calculator : creates

    Lb3App --> ITask31Calculator : uses
    Lb3App --> ITask32Calculator : uses
    Lb3App --> ITask33Calculator : uses
```
