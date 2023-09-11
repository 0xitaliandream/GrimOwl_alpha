
using GameEngine;
using GrimOwlGameEngine;

namespace GrimOwlConsoleClient;


class MainClass
{

    public static void Main(string[] args)
    {
        GrimOwlGame game = TestScenarios.TestScenario4();

        bool status = true;
        string input;
        do
        {
            GrimOwlPlayer activePlayer = game.State.ActivePlayer;
            if (status)
            {
                Console.Clear();
                Console.WriteLine(status + "\n");
                ConsoleUtil.PrintGame(game.State);
                Console.WriteLine(GetOptions());
            }
            input = Console.ReadLine() ?? "";
            status = activePlayer.CommandController.HandleCommand(game, input);
        } while (input.ToUpper() != GrimOwlPlayerCommandController.CommandQuit);
    }
    private static string GetOptions()
    {
        string options = "Choose command:\n";
        options += GrimOwlPlayerCommandController.CommandSummon + " <id> <x> <y>: Summon permanent card from hand (with <id>) to the board\n";
        options += GrimOwlPlayerCommandController.CommandAttack + " <id1> <id2>: Attack creature card (with <id>)\n";
        options += GrimOwlPlayerCommandController.CommandMove + " <id> <x> <y>: Move creature card (with <id>) on the board\n";
        options += GrimOwlPlayerCommandController.CommandEndTurn + ": End Turn\n";
        options += GrimOwlPlayerCommandController.CommandQuit + ": Quit\n";
        return options;
    }
}