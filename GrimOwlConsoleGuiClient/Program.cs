

namespace GrimOwlConsoleGuiClient;

class MainClass
{
    public static void Main(string[] args)
    {
        string arg1 = "1"; // Default value for arg1
        int arg2 = 1;      // Default value for arg2

        if (args.Length >= 2)
        {
            arg1 = args[0];  // Override the default value for arg1
            if (!int.TryParse(args[1], out arg2))
            {
                Console.WriteLine("Invalid second argument. Using default value 1.");
                arg2 = 1;
            }
        }
        else
        {
            Console.WriteLine("Not enough arguments. Using default values \"1\" and 1.");
        }

    }


}
