using GameEngine;

namespace GrimOwlGameEngine;

class MainClass
{
    public static void Main(string[] args)
    {
        GrimOwlGame game = TestScenarios.TestScenario3();

        string serializedGame = JsonSerializer.ToJson(game);
        Console.WriteLine(serializedGame);

        Console.WriteLine(System.Text.ASCIIEncoding.Unicode.GetByteCount(serializedGame));


        GrimOwlGame? game2 = JsonSerializer.FromJson<GrimOwlGame>(serializedGame);

        Console.WriteLine(game2!.isGameStarted);
    }
}