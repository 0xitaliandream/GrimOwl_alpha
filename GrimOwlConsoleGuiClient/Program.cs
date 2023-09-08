
using GameEngine;
using GrimOwlGameEngine;
using GrimOwlRiptideClient;

namespace GrimOwlConsoleGuiClient;


class MainClass
{

    public static void Main(string[] args)
    {
        GrimOwlClient grimOwlClient = new GrimOwlClient("1", 1);

        grimOwlClient.OnNewGameUpdate += OnNewGameState;

        grimOwlClient.Run();
    }

    public static void OnNewGameState(string test)
    {
        Console.WriteLine("New game state received");
        Console.WriteLine(test);
    }

}