# alg-cs-labs

Cross-platform .NET console application containing the migrated exercises from
`FirstSetupCpp` and `Lb2` in the legacy C++ repository.

Run from this directory with:

```sh
dotnet run --project alg-cs-labs.csproj
```

Select **Factorial** to calculate `10!`, or **Variant 17** to enter a positive
value of `m` and calculate `z1` and `z2`. Entering `0` exits the application or
the Variant 17 input loop. Variant 17 is undefined at `m = 2/3`, so that value
is rejected and input is requested again.
