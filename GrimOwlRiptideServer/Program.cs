using GrimOwlGameEngine;

namespace GrimOwlRiptideServer;

class MainClass
{
    public static void Main(string[] args)
    {
        Console.WriteLine("GrimOwlServer");

        int arg1 = 1; // Valore predefinito per arg1
        int arg2 = 2; // Valore predefinito per arg2

        if (args.Length >= 2)
        {
            if (int.TryParse(args[0], out arg1) && int.TryParse(args[1], out arg2))
            {
                // arg1 e arg2 sono stati correttamente impostati dai parametri della linea di comando
            }
            else
            {
                Console.WriteLine("Invalid arguments. Using default values 1 and 2.");
            }
        }
        else
        {
            Console.WriteLine("Not enough arguments. Using default values 1 and 2.");
        }

        GrimOwlServer.Run(arg1, arg2);
    }
}