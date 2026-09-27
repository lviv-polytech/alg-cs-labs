using System.Globalization;

namespace CommonUtils.Input;

/// <summary>
/// Default implementation of <see cref="IConsoleInputReader"/> providing robust text reading and retry logic.
/// </summary>
public class ConsoleInputReader : IConsoleInputReader
{
    private readonly TextReader _input;
    private readonly TextWriter _output;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleInputReader"/> class using the specified streams,
    /// defaulting to standard console in and out.
    /// </summary>
    /// <param name="input">Input stream reader (defaults to <see cref="Console.In"/>).</param>
    /// <param name="output">Output stream writer (defaults to <see cref="Console.Out"/>).</param>
    public ConsoleInputReader(TextReader? input = null, TextWriter? output = null)
    {
        _input = input ?? Console.In;
        _output = output ?? Console.Out;
    }

    /// <inheritdoc />
    public string? ReadLine(string prompt)
    {
        _output.Write(prompt);
        return _input.ReadLine();
    }

    /// <inheritdoc />
    public int ReadInt(string prompt, int? min = null, int? max = null)
    {
        while (true)
        {
            _output.Write(prompt);
            var line = _input.ReadLine();

            if (line is null)
            {
                throw new EndOfStreamException("End of input stream reached while expecting an integer.");
            }

            if (!int.TryParse(line.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value) &&
                !int.TryParse(line.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out value))
            {
                _output.WriteLine("Invalid integer format. Please try again.");
                continue;
            }

            if (min.HasValue && value < min.Value)
            {
                _output.WriteLine($"Value must be at least {min.Value}. Please try again.");
                continue;
            }

            if (max.HasValue && value > max.Value)
            {
                _output.WriteLine($"Value must not exceed {max.Value}. Please try again.");
                continue;
            }

            return value;
        }
    }

    /// <inheritdoc />
    public double? ReadDouble(string prompt, Func<double, bool>? validator = null, string? validationErrorMessage = null)
    {
        while (true)
        {
            _output.Write(prompt);
            var line = _input.ReadLine();

            if (line is null)
            {
                return null;
            }

            var trimmed = line.Trim();
            if (!double.TryParse(trimmed, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var value) &&
                !double.TryParse(trimmed, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out value))
            {
                _output.WriteLine("Write a valid finite number.");
                continue;
            }

            if (!double.IsFinite(value))
            {
                _output.WriteLine("Write a valid finite number.");
                continue;
            }

            if (validator != null && !validator(value))
            {
                _output.WriteLine(validationErrorMessage ?? "Input value does not satisfy the required constraints.");
                continue;
            }

            return value;
        }
    }
}
