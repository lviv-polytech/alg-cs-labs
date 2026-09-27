using Lpnu.Lb1.UI;

namespace alg_cs_labs.Lb1;

/// <summary>
/// Static entry wrapper preserving compatibility with legacy runners.
/// </summary>
public static class FactorialLab
{
    private static readonly FactorialApp App = new();

    /// <summary>
    /// Executes the Lab 1 factorial routine.
    /// </summary>
    /// <param name="output">Output stream destination.</param>
    public static void Run(TextWriter output)
    {
        App.Run(output);
    }
}
