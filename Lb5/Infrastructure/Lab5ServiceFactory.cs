using Lpnu.Lb5.Core;

namespace Lpnu.Lb5.Infrastructure;

/// <summary>Creates Lab 5 calculator services.</summary>
public static class Lab5ServiceFactory
{
    /// <summary>Creates the Lab 5 calculator.</summary>
    public static ILab5Calculator CreateCalculator() => new Lab5Calculator();
}
