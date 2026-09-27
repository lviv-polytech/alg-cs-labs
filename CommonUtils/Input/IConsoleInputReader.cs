using System.Globalization;

namespace CommonUtils.Input;

/// <summary>
/// Defines contracts for safe and robust console input reading with retry logic.
/// </summary>
public interface IConsoleInputReader
{
    /// <summary>
    /// Reads a string line from the configured input source with a prompt.
    /// </summary>
    /// <param name="prompt">Prompt message displayed to the user.</param>
    /// <returns>The line entered by the user, or <c>null</c> if EOF is encountered.</returns>
    string? ReadLine(string prompt);

    /// <summary>
    /// Safely reads a 32-bit integer with optional range constraints and automatic retry on invalid input.
    /// </summary>
    /// <param name="prompt">Prompt message displayed to the user.</param>
    /// <param name="min">Optional minimum acceptable value (inclusive).</param>
    /// <param name="max">Optional maximum acceptable value (inclusive).</param>
    /// <returns>The validated integer entered by the user.</returns>
    int ReadInt(string prompt, int? min = null, int? max = null);

    /// <summary>
    /// Safely reads a finite double-precision number with custom validation and automatic retry logic.
    /// </summary>
    /// <param name="prompt">Prompt message displayed to the user.</param>
    /// <param name="validator">Optional custom predicate to validate the number.</param>
    /// <param name="validationErrorMessage">Error message displayed when the validation predicate fails.</param>
    /// <returns>The validated double number entered by the user, or <c>null</c> if the input stream terminates.</returns>
    double? ReadDouble(string prompt, Func<double, bool>? validator = null, string? validationErrorMessage = null);
}
