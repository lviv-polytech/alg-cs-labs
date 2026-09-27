namespace alg_cs_labs.FirstSetup;

public static class FactorialLab
{
    public static void Run(TextWriter output)
    {
        output.WriteLine("Hello, World!");

        var product = 1;
        var i = 1;

        while (i <= 10)
        {
            product *= i;
            i++;
        }

        output.WriteLine($"P: {product} i: {i}");
    }
}
