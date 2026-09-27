using alg_cs_labs.FirstSetup;
using alg_cs_labs.Lb2;

while (true)
{
    Console.WriteLine("1 - Factorial (FirstSetupCpp)");
    Console.WriteLine("2 - Variant 17 calculation (Lb2)");
    Console.WriteLine("0 - Exit");
    Console.Write("Select module: ");

    switch (Console.ReadLine())
    {
        case "0":
            return;
        case "1":
            FactorialLab.Run(Console.Out);
            break;
        case "2":
            Variant17Lab.Run(Console.In, Console.Out);
            break;
        default:
            Console.WriteLine("Invalid selection.");
            break;
    }

    Console.WriteLine();
}
