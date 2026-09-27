# alg-cs-labs

Cross-platform .NET multi-project solution conforming to the modular project guidelines specified in `AGENT_INSTRUCTIONS.md`.

## Solution Structure

The workspace is organized into modular projects:

* **`CommonUtils`:** Shared Class Library providing centralized input handling, console readers, and mathematical utilities.
* **`Lb1`:** Lab 1 Console Application (migrated from `FirstSetupCpp`), implementing iterative factorial computation with three-layer architecture (UI, Core, Infrastructure).
* **`Lb1.Tests`:** Comprehensive xUnit test suite for `Lb1` covering typical calculations, boundaries, and error cases.
* **`Lb2`:** Lab 2 Console Application, implementing Variant 17 algebraic calculations ($z_1$ and $z_2$) with three-layer architecture.
* **`Lb2.Tests`:** Comprehensive xUnit test suite for `Lb2` covering positive values, domain restrictions, edge cases, and singularity handling ($m = 2/3$).
* **`alg-cs-labs`:** Root console application orchestrator providing an interactive menu to launch individual lab modules.

## Execution Guide

### Build the entire solution
```sh
dotnet build alg-cs-labs.sln
```

### Run all tests
```sh
dotnet test alg-cs-labs.sln
```

### Run individual modules
* **Lab 1 (Factorial):**
  ```sh
  dotnet run --project Lb1/Lb1.csproj
  ```
* **Lab 2 (Variant 17):**
  ```sh
  dotnet run --project Lb2/Lb2.csproj
  ```
* **Root interactive runner:**
  ```sh
  dotnet run --project alg-cs-labs.csproj
  ```
