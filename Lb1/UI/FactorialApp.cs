using Lpnu.Lb1.Core.Services;
using Lpnu.Lb1.Infrastructure;

namespace Lpnu.Lb1.UI;

/// <summary>
/// Presentation layer orchestrating the user interface and console output for Lab 1.
/// </summary>
public class FactorialApp
{
    private readonly IFactorialCalculator _calculator;

    /// <summary>
    /// Initializes a new instance of the <see cref="FactorialApp"/> class.
    /// </summary>
    /// <param name="calculator">Factorial calculator contract instance.</param>
    public FactorialApp(IFactorialCalculator? calculator = null)
    {
        _calculator = calculator ?? FactorialServiceFactory.CreateCalculator();
    }

    /// <summary>
    /// Executes the standard Lab 1 assignment routine, writing greeting and factorial results to the specified output stream.
    /// </summary>
    /// <param name="output">Target output writer (defaults to <see cref="Console.Out"/>).</param>
    public void Run(TextWriter? output = null)
    {
        var writer = output ?? Console.Out;
        writer.WriteLine("Hello, World!");

        var result = _calculator.Calculate(10);
        writer.WriteLine($"P: {result.Product} i: {result.LastIterationIndex}");
    }
}
