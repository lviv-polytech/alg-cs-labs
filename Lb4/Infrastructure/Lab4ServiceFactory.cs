using Lpnu.Lb4.Core;

namespace Lpnu.Lb4.Infrastructure;

/// <summary>Creates Lab 4 service contracts.</summary>
public static class Lab4ServiceFactory
{
    /// <summary>Creates the loop and tabulation calculator.</summary>
    public static ILab4Calculator CreateCalculator() => new Lab4Calculator();
}
