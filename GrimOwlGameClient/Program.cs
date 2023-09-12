
namespace GrimOwlGameClient;
class MainClass
{
    static void Main(string[] args)
    {

        int arg1 = 1; // Valore predefinito per arg1

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


        Client client = new Client("ws://127.0.0.1:8080/GameService", arg1);

        client.ConnectAll();

        // Tenere la console aperta.
        Console.WriteLine("Premi un tasto per uscire...");
        Console.ReadKey(true);

        client.DisconnectAll();
    }
}