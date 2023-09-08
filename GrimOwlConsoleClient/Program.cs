
using GameEngine;
using GrimOwlGameEngine;

namespace GrimOwlConsoleClient;


class MainClass
{
    private const string CommandSummon = "S";
    private const string CommandMove = "M";
    private const string CommandAttack = "A";
    private const string CommandEndTurn = "E";
    private const string CommandQuit = "Q";

    public static void Main(string[] args)
    {
        GrimOwlGame game = TestScenario3();

        string info = string.Empty;
        string input;
        do
        {
            Console.Clear();
            Console.WriteLine(info + "\n");
            ConsoleUtil.PrintGame(game.State);
            Console.WriteLine(GetOptions());
            input = Console.ReadLine() ?? "";
            info = ProcessInput(game, input);
        } while (input.ToUpper() != CommandQuit);
    }

    public static GrimOwlGame TestScenario2()
    {
        GrimOwlGameState gameState = new GrimOwlGameState();
        for (int i = 0; i < 2; ++i)
        {
            IPlayer player = new GrimOwlPlayer();

            GrimOwlKingCard creatureCard = new MalikII();
            ((GrimOwlPlayer)player).SetKing(creatureCard);

            ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);
            for (int n = 0; n < 0; ++n)
            {
                deck.Add(new WallGrappler());
            }
            deck.Shuffle();

            gameState.AddPlayer(player);
        }

        GrimOwlGrid grimOwlGrid = new GrimOwlGrid(2, 1);

        gameState.AddGrid(grimOwlGrid);

        gameState.Grid[0, 0]!.SetNature(StatKeys.Feral);


        GrimOwlGame game = new GrimOwlGame(gameState);
        game.NextTurn();
        gameState.ActivePlayer.SpawnCreature(game, gameState.ActivePlayer.King, 0, 0);

        return game;
    }

    public static GrimOwlGame TestScenario1()
    {
        GrimOwlGameState gameState = new GrimOwlGameState();
        for (int i = 0; i < 2; ++i)
        {
            IPlayer player = new GrimOwlPlayer();

            GrimOwlKingCard creatureCard = new MalikII();
            ((GrimOwlPlayer)player).SetKing(creatureCard);

            ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);
            for (int n = 0; n < 3; ++n)
            {
                deck.Add(new WallGrappler());
            }
            deck.Shuffle();

            gameState.AddPlayer(player);
        }

        GrimOwlGrid grimOwlGrid = new GrimOwlGrid(5, 1);

        gameState.AddGrid(grimOwlGrid);

        GrimOwlGame game = new GrimOwlGame(gameState);

        for (int i = 0; i < 2; ++i)
        {
            GrimOwlPlayer player = (GrimOwlPlayer)gameState.Players.ElementAt(i);
            if (i == 0)
            {
                player.SpawnCreature(game, player.King, 0, 0);
            }
            else
            {
                player.SpawnCreature(game, player.King, 4, 0);
            }
        }


        foreach (GrimOwlPlayer player in gameState.Players)
        {
            for (int i = 0; i < 3; ++i)
            {
                player.DrawCard(game);
            }
        }




        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();

        GrimOwlPlayer activePlayer = game.State.ActivePlayer;

        activePlayer.SummonCreature(game, (GrimOwlCreatureCard)activePlayer.GetCardCollection(CardCollectionKeys.Hand).First, 1, 0);
        activePlayer.MoveCreature(game, (GrimOwlCreatureCard)game.State.Grid[1, 0]!.PermanentCard, 2, 0);

        return game;
    }

    public static GrimOwlGame TestScenario3()
    {
        GrimOwlGameState gameState = new GrimOwlGameState();
        for (int i = 0; i < 2; ++i)
        {
            IPlayer player = new GrimOwlPlayer();

            ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);
            for (int n = 0; n < 3; ++n)
            {
                deck.Add(new WallGrappler());
            }
            deck.Shuffle();

            gameState.AddPlayer(player);
        }

        GrimOwlGrid grimOwlGrid = new GrimOwlGrid(3, 1);

        gameState.AddGrid(grimOwlGrid);

        GrimOwlGame game = new GrimOwlGame(gameState);

        foreach (GrimOwlPlayer player in gameState.Players)
        {
            for (int i = 0; i < 3; ++i)
            {
                player.DrawCard(game);
            }
        }

        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();

        GrimOwlPlayer activePlayer = game.State.ActivePlayer;

        game.State.ActivePlayer.SummonCreature(game, (GrimOwlCreatureCard)activePlayer.GetCardCollection(CardCollectionKeys.Hand).First, 0, 0);
        game.NextTurn();

        activePlayer = game.State.ActivePlayer;
        game.State.ActivePlayer.SummonCreature(game, (GrimOwlCreatureCard)activePlayer.GetCardCollection(CardCollectionKeys.Hand).First, 2, 0);

        return game;
    }

    private static string GetOptions()
    {
        string options = "Choose command:\n";
        options += CommandSummon + " <id> <x> <y>: Summon permanent card from hand (with <id>) to the board\n";
        options += CommandAttack + " <id1> <id2>: Attack creature card (with <id>)\n";
        options += CommandMove + " <id> <x> <y>: Move creature card (with <id>) on the board\n";
        options += CommandEndTurn + ": End Turn\n";
        options += CommandQuit + ": Quit\n";
        return options;
    }

    private static string ProcessInput(GrimOwlGame game, string input)
    {
        string output = string.Empty;
        try
        {
            GrimOwlGameState state = game.State;
            GrimOwlPlayer activePlayer = (GrimOwlPlayer)state.ActivePlayer;

            string[] inputParams = input.Split(' ');
            switch (inputParams[0].ToUpper())
            {
                case CommandSummon:
                    GrimOwlCreatureCard creatureCard = (GrimOwlCreatureCard)GetObjectById(game, inputParams[1]);
                    activePlayer.SummonCreature(game, creatureCard, int.Parse(inputParams[2]), int.Parse(inputParams[3]));
                    output = "Cast monster card";
                    break;
                case CommandAttack:
                    GrimOwlCreatureCard attacker = (GrimOwlCreatureCard)GetObjectById(game, inputParams[1]);
                    GrimOwlCreatureCard defender = (GrimOwlCreatureCard)GetObjectById(game, inputParams[2]);
                    activePlayer.AttackCreature(game, attacker, defender);
                    output = "Move monster card";
                    break;
                case CommandMove:
                    GrimOwlCreatureCard creatureCard2 = (GrimOwlCreatureCard)GetObjectById(game, inputParams[1]);
                    activePlayer.MoveCreature(game, creatureCard2, int.Parse(inputParams[2]), int.Parse(inputParams[3]));
                    output = "Move monster card";
                    break;
                case CommandEndTurn:
                    game.NextTurn();
                    output = "Player ended turn.";
                    break;
                case CommandQuit:
                    break;
                default:
                    output = "Invalid command: " + input;
                    break;
            }
        }
        catch (Exception e)
        {
            output = e.Message + "\n" + e.StackTrace;
        }
        return output;
    }

    private static Object GetObjectById(GrimOwlGame game, string id)
    {
        GrimOwlGameState state = game.State;
        switch (int.Parse(id.Substring(0, 1)))
        {
            case 0:
                return state.NonActivePlayers.First();
            case 1:
                return state.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Hand).GetByUniqueId(int.Parse(id.Substring(1)));
            case 2:
                return state.Grid.GetCardByUniqueId(int.Parse(id.Substring(1)))!;
            case 3:
                return state.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand).GetByUniqueId(int.Parse(id.Substring(1)));
            case 4:
                return state.ActivePlayer;
            default:
                throw new Exception("Unparsable id: " + id);
        }
    }
}