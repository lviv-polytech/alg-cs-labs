# alg-cs-labs

Cross-platform .NET multi-project modular solution for algorithm and computer science laboratory assignments, adhering strictly to the architecture, git, and testing standards defined in `AGENT_INSTRUCTIONS.md`.

## Solution Architecture

The repository is structured into modular, isolated projects:

* **`CommonUtils`:** Shared Class Library providing centralized input handling, console readers, finite-value validation, and mathematical helpers.
* **`Lb1`:** Lab 1 Console Application (migrated from `FirstSetupCpp`), implementing iterative factorial computation ($10!$) using a three-layer architecture (UI, Core, Infrastructure).
* **`Lb1.Tests`:** xUnit test suite for `Lb1` covering typical values, boundary conditions ($0!$, $1!$, $20!$), and invalid input handling.
* **`Lb2`:** Lab 2 Console Application, evaluating Variant 17 expressions ($z_1$ and $z_2$) with three-layer architecture and singularity detection at $m = 2/3$.
* **`Lb2.Tests`:** xUnit test suite for `Lb2` verifying domains, algebraic simplification ($\text{sgn}(3m-2)\sqrt{m}$), and boundary singularities.
* **`Lb3`:** Lab 3 Console Application (Variant 17), implementing branching algorithms:
  * **Task 3.1:** Single-variable piecewise function $Y = m^2 + 1 + \Phi(m)$.
  * **Task 3.2:** Parameterized branching function $F(x)$ implemented in two forms (short-form `if` vs full-form `if-else`).
  * **Task 3.3:** Graph-based piecewise function $y(x, R)$ evaluating 4 geometric segments.
* **`Lb3.Tests`:** xUnit test suite for `Lb3` validating all interval branches, transition points, edge cases, and equivalence between short and full branching forms.
* **`alg-cs-labs`:** Root console orchestrator offering an interactive menu to launch any lab module.

---

## Key CLI Commands

All commands can be executed from the solution root using the .NET CLI:

### 1. Build and Restore
* **Restore all project dependencies:**
  ```sh
  dotnet restore
  ```
* **Build entire solution (Debug):**
  ```sh
  dotnet build
  ```
* **Build entire solution in Release configuration:**
  ```sh
  dotnet build -c Release
  ```
* **Clean build artifacts:**
  ```sh
  dotnet clean
  ```

### 2. Testing
* **Run all unit tests across the solution:**
  ```sh
  dotnet test
  ```
* **Run unit tests for a specific module:**
  ```sh
  dotnet test Lb1.Tests/Lb1.Tests.csproj
  dotnet test Lb2.Tests/Lb2.Tests.csproj
  dotnet test Lb3.Tests/Lb3.Tests.csproj
  ```
* **Run tests with detailed console output:**
  ```sh
  dotnet test --logger "console;verbosity=normal"
  ```

### 3. Running Modules
* **Run the main interactive selector menu (All Labs):**
  ```sh
  dotnet run --project alg-cs-labs.csproj
  ```
* **Run Lab 1 (Factorial):**
  ```sh
  dotnet run --project Lb1/Lb1.csproj
  ```
* **Run Lab 2 (Variant 17 Formula Calculation):**
  ```sh
  dotnet run --project Lb2/Lb2.csproj
  ```
* **Run Lab 3 (Branching Algorithms - Tasks 3.1, 3.2, 3.3):**
  ```sh
  dotnet run --project Lb3/Lb3.csproj
  ```

### 4. Code Quality and Formatting
* **Verify or apply code formatting:**
  ```sh
  dotnet format
  ```
