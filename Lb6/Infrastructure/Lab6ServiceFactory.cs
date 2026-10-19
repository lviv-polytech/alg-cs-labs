using Lpnu.Lb6.Core;

namespace Lpnu.Lb6.Infrastructure;

/// <summary>Creates Lab 6 array algorithm services.</summary>
public static class Lab6ServiceFactory
{
    /// <summary>Creates an implementation supporting iterative and recursive forms.</summary>
    public static ILab6ArrayAlgorithms CreateArrayAlgorithms() => new Lab6ArrayAlgorithms();
}
