using System.Globalization;
using CommonUtils.Input;
using Lpnu.Lb2.Core.Services;
using Lpnu.Lb2.Infrastructure;

namespace Lpnu.Lb2.UI;

/// <summary>
/// Presentation layer for Lab 2 (Variant 17) managing user interaction, console prompts, and validation feedback.
/// </summary>
public class Variant17App
{
    private readonly IVariant17Calculator _calculator;
    private readonly IConsoleInputReader _inputReader;
    private readonly TextWriter _output;

    /// <summary>
    /// Initializes a new instance of the <see cref="Variant17App"/> class.
    /// </summary>
    /// <param name="calculator">Variant 17 calculator contract instance.</param>
    /// <param name="inputReader">Console input reader instance.</param>
    /// <param name="output">Target output writer stream.</param>
    public Variant17App(
        IVariant17Calculator? calculator = null,
        IConsoleInputReader? inputReader = null,
        TextWriter? output = null)
    {
        _output = output ?? Console.Out;
        _calculator = calculator ?? Variant17ServiceFactory.CreateCalculator();
        _inputReader = inputReader ?? new ConsoleInputReader(Console.In, _output);
    }

    /// <summary>
    /// Runs the interactive prompt loop asking the user for parameter m, evaluating formulas, and displaying results.
    /// </summary>
    public void Run()
    {
        Run(Console.In, _output);
    }

    /// <summary>
    /// Executes the interactive input/output loop using specified input and output streams.
    /// </summary>
    /// <param name="input">Source input reader.</param>
    /// <param name="output">Destination output writer.</param>
    public void Run(TextReader input, TextWriter output)
    {
        while (true)
        {
            output.WriteLine("0 - Exit");
            output.Write("Write number (m): ");

            var line = input.ReadLine();
            if (line is null)
            {
                return;
            }

            var trimmed = line.Trim();
            if (!double.TryParse(trimmed, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out var m) &&
                !double.TryParse(trimmed, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out m))
            {
                output.WriteLine("Write a valid finite number.");
                continue;
            }

            if (!double.IsFinite(m))
            {
                output.WriteLine("Write a valid finite number.");
                continue;
            }

            if (m == 0)
            {
                return;
            }

            if (m < 0)
            {
                output.WriteLine("Don't write m < 0");
                continue;
            }

            try
            {
                var result = _calculator.Calculate(m);
                output.WriteLine($"z1={result.Z1}");
                output.WriteLine($"z2={result.Z2}");
                return;
            }
            catch (ArgumentException)
            {
                output.WriteLine("The expression is undefined for m = 2/3.");
            }
        }
    }
}
