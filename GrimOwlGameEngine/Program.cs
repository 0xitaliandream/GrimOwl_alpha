using GameEngine;

namespace GrimOwlGameEngine;

class MainClass
{
    public static void Main(string[] args)
    {
        GrimOwlGame game = TestScenarios.TestScenario3();

        GrimOwlGameUpdatePlayerContext grimOwlGameUpdatePlayerContext = new GrimOwlGameUpdatePlayerContext(game,game.State.ActivePlayer, new List<IAction>() );


        string serializedGame = JsonSerializer.ToJson(grimOwlGameUpdatePlayerContext);
        Console.WriteLine(serializedGame);
    }
}