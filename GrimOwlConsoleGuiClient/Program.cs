

using GrimOwlGameClient;

namespace GrimOwlConsoleGuiClient;

class MainClass
{
    public static void Main(string[] args)
    {
        int arg1 = 0; // Valore predefinito per arg1

        if (args.Length >= 1)
        {
            if (int.TryParse(args[0], out arg1))
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

        GrimOwlConsoleGui App = new GrimOwlConsoleGui(arg1);

    }




}
