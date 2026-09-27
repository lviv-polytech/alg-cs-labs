using alg_cs_labs.Lb1;
using alg_cs_labs.Lb2;
using Lpnu.Lb3.UI;

while (true)
{
    Console.WriteLine("1 - Factorial (Lb1)");
    Console.WriteLine("2 - Variant 17 calculation (Lb2)");
    Console.WriteLine("3 - Branching algorithms (Lb3)");
    Console.WriteLine("0 - Exit");
    Console.Write("Select module: ");

    switch (Console.ReadLine()?.Trim())
    {
        case "0":
            return;
        case "1":
            FactorialLab.Run(Console.Out);
            break;
        case "2":
            Variant17Lab.Run(Console.In, Console.Out);
            break;
        case "3":
            new Lb3App().Run();
            break;
        default:
            Console.WriteLine("Invalid selection.");
            break;
    }

    Console.WriteLine();
}
