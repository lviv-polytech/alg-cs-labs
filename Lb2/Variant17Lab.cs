using Lpnu.Lb2.UI;

namespace alg_cs_labs.Lb2;

/// <summary>
/// Static adapter for Lab 2 preserving compatibility with legacy runners.
/// </summary>
public static class Variant17Lab
{
    private static readonly Variant17App App = new();

    /// <summary>
    /// Executes the interactive input/output loop.
    /// </summary>
    /// <param name="input">Input text stream.</param>
    /// <param name="output">Output text stream.</param>
    public static void Run(TextReader input, TextWriter output)
    {
        App.Run(input, output);
    }
}
