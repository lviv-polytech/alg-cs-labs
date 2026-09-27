using System.Globalization;

namespace alg_cs_labs.Lb2;

public static class Variant17Lab
{
    public static void Run(TextReader input, TextWriter output)
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

            if (!double.TryParse(line, NumberStyles.Float | NumberStyles.AllowThousands,
                    CultureInfo.CurrentCulture, out var m) ||
                !double.IsFinite(m))
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
                var result = Variant17Calculator.Calculate(m);
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
