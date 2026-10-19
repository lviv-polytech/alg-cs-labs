using Lpnu.Lb7.Core;

namespace Lpnu.Lb7.Infrastructure;

/// <summary>Creates Lab 7 matrix algorithm services.</summary>
public static class Lab7ServiceFactory
{
    /// <summary>Creates the iterative/recursive matrix algorithm implementation.</summary>
    public static ILab7MatrixAlgorithms CreateMatrixAlgorithms() => new Lab7MatrixAlgorithms();
}
