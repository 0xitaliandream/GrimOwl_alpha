namespace GrimOwlGameEngine;

public class GrimOwlPlayerCommandController
{

    private GrimOwlPlayer player;

    public GrimOwlPlayerCommandController(GrimOwlPlayer player)
    {
        this.player = player;
    }


    public const string CommandSummon = "S";
    public const string CommandMove = "M";
    public const string CommandAttack = "A";
    public const string CommandEndTurn = "E";
    public const string CommandQuit = "Q";

    public bool HandleCommand(GrimOwlGame game, string input)
    {
        bool status = false;
        try
        {
            GrimOwlGameState state = game.State;
            GrimOwlPlayer activePlayer = state.ActivePlayer;

            if (activePlayer != player)
            {
                status = false;
                return status;
            }

            if (game.isGameStarted == false)
            {
                status = false;
                return status;
            }

            string[] inputParams = input.Split(' ');
            switch (inputParams[0].ToUpper())
            {
                case CommandSummon:
                    GrimOwlCreatureCard creatureCard = (GrimOwlCreatureCard)GetObjectById(game, inputParams[1]);
                    activePlayer.SummonCreature(game, creatureCard, int.Parse(inputParams[2]), int.Parse(inputParams[3]));
                    status = true;
                    break;
                case CommandAttack:
                    GrimOwlCreatureCard attacker = (GrimOwlCreatureCard)GetObjectById(game, inputParams[1]);
                    GrimOwlCreatureCard defender = (GrimOwlCreatureCard)GetObjectById(game, inputParams[2]);
                    activePlayer.AttackCreature(game, attacker, defender);
                    status = true;
                    break;
                case CommandMove:
                    GrimOwlCreatureCard creatureCard2 = (GrimOwlCreatureCard)GetObjectById(game, inputParams[1]);
                    activePlayer.MoveCreature(game, creatureCard2, int.Parse(inputParams[2]), int.Parse(inputParams[3]));
                    status = true;
                    break;
                case CommandEndTurn:
                    game.NextTurn();
                    status = true;
                    break;
                case CommandQuit:
                    break;
                default:
                    status = false;
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message + "\n" + e.StackTrace);
            status = true;
        }
        return status;
    }

    private static object GetObjectById(GrimOwlGame game, string id)
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
