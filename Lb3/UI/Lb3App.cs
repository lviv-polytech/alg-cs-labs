using CommonUtils.Input;
using Lpnu.Lb3.Core.Services;
using Lpnu.Lb3.Infrastructure;

namespace Lpnu.Lb3.UI;

/// <summary>
/// Presentation layer for Lab 3, managing user interactions and module selection.
/// </summary>
public class Lb3App
{
    private readonly ITask31Calculator _task31Calculator;
    private readonly ITask32Calculator _task32Calculator;
    private readonly ITask33Calculator _task33Calculator;
    private readonly IConsoleInputReader _inputReader;
    private readonly TextWriter _output;

    /// <summary>
    /// Initializes a new instance of <see cref="Lb3App"/>.
    /// </summary>
    public Lb3App(
        ITask31Calculator? task31 = null,
        ITask32Calculator? task32 = null,
        ITask33Calculator? task33 = null,
        IConsoleInputReader? inputReader = null,
        TextWriter? output = null)
    {
        _output = output ?? Console.Out;
        _task31Calculator = task31 ?? Lb3ServiceFactory.CreateTask31Calculator();
        _task32Calculator = task32 ?? Lb3ServiceFactory.CreateTask32Calculator();
        _task33Calculator = task33 ?? Lb3ServiceFactory.CreateTask33Calculator();
        _inputReader = inputReader ?? new ConsoleInputReader(Console.In, _output);
    }

    /// <summary>
    /// Runs the interactive menu loop for Lab 3.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            _output.WriteLine("=== Lab 3: Branching Algorithms (Variant 17) ===");
            _output.WriteLine("1 - Task 3.1: Single-variable piecewise function Y(m)");
            _output.WriteLine("2 - Task 3.2: Parameterized function F(x) (short vs full if)");
            _output.WriteLine("3 - Task 3.3: Graph-based piecewise function y(x, R)");
            _output.WriteLine("4 - Run all tasks sequentially");
            _output.WriteLine("0 - Exit");

            var choice = _inputReader.ReadLine("Select task: ")?.Trim();
            if (choice is null || choice == "0")
            {
                return;
            }

            switch (choice)
            {
                case "1":
                    RunTask31();
                    break;
                case "2":
                    RunTask32();
                    break;
                case "3":
                    RunTask33();
                    break;
                case "4":
                    RunTask31();
                    _output.WriteLine();
                    RunTask32();
                    _output.WriteLine();
                    RunTask33();
                    break;
                default:
                    _output.WriteLine("Invalid selection. Please choose an option from the menu.");
                    break;
            }

            _output.WriteLine();
        }
    }

    /// <summary>
    /// Interactive routine for Task 3.1.
    /// </summary>
    public void RunTask31()
    {
        _output.WriteLine("--- Task 3.1: Piecewise function Y = m^2 + 1 + Phi(m) ---");
        var m = _inputReader.ReadDouble("Enter m: ");
        if (!m.HasValue) return;

        var n = _inputReader.ReadDouble("Enter n: ");
        if (!n.HasValue) return;

        try
        {
            var result = _task31Calculator.Calculate(m.Value, n.Value);
            _output.WriteLine($"Phi(m) = {result.Phi:F6}");
            _output.WriteLine($"Y = {result.Y:F6}");
        }
        catch (Exception ex)
        {
            _output.WriteLine($"Error evaluating Task 3.1: {ex.Message}");
        }
    }

    /// <summary>
    /// Interactive routine for Task 3.2.
    /// </summary>
    public void RunTask32()
    {
        _output.WriteLine("--- Task 3.2: Parameterized Branching F(x) ---");
        var a = _inputReader.ReadDouble("Enter parameter a: ");
        if (!a.HasValue) return;

        var b = _inputReader.ReadDouble("Enter parameter b: ");
        if (!b.HasValue) return;

        var c = _inputReader.ReadDouble("Enter parameter c: ");
        if (!c.HasValue) return;

        var x = _inputReader.ReadDouble("Enter argument x: ");
        if (!x.HasValue) return;

        try
        {
            var result = _task32Calculator.Calculate(a.Value, b.Value, c.Value, x.Value);
            _output.WriteLine($"Result (Short-form 'if'): {result.ResultShortForm:F6}");
            _output.WriteLine($"Result (Full-form 'if-else'): {result.ResultFullForm:F6}");
            _output.WriteLine($"Results match: {result.ResultsMatch}");
        }
        catch (ArgumentException ex)
        {
            _output.WriteLine($"Calculation undefined: {ex.Message}");
        }
        catch (Exception ex)
        {
            _output.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Interactive routine for Task 3.3.
    /// </summary>
    public void RunTask33()
    {
        _output.WriteLine("--- Task 3.3: Graph-based Piecewise Function y(x, R) ---");
        var x = _inputReader.ReadDouble("Enter argument x: ");
        if (!x.HasValue) return;

        var r = _inputReader.ReadDouble("Enter radius/scale R (R > 0): ", val => val > 0, "R must be strictly positive (R > 0).");
        if (!r.HasValue) return;

        try
        {
            var result = _task33Calculator.Calculate(x.Value, r.Value);
            _output.WriteLine($"Interval: {result.IntervalDescription}");
            _output.WriteLine($"y = {result.Y:F6}");
        }
        catch (Exception ex)
        {
            _output.WriteLine($"Error evaluating Task 3.3: {ex.Message}");
        }
    }
}
