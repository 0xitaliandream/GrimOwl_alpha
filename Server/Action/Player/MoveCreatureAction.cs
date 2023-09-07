using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class MoveCreatureAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected GrimOwlPlayer player = null!;

    [JsonProperty]
    protected GrimOwlCreatureCard creatureCard = null!;

    [JsonProperty]
    protected int x2 = -1;

    [JsonProperty]
    protected int y2 = -1;

    protected MoveCreatureAction() { }

    public MoveCreatureAction(GrimOwlPlayer player, GrimOwlCreatureCard creatureCard, int x2, int y2, bool isAborted = false
        ) : base(isAborted)
    {
        this.player = player;
        this.creatureCard = creatureCard;
        this.x2 = x2;
        this.y2 = y2;

    }

    [JsonIgnore]
    public GrimOwlPlayer Player
    {
        get => player;
    }

    [JsonIgnore]
    public GrimOwlCreatureCard CreatureCard
    {
        get => creatureCard;
    }

    [JsonIgnore]
    public int X2
    {
        get => x2;
    }

    [JsonIgnore]
    public int Y2
    {
        get => y2;
    }



    public override void Execute(IGame<GrimOwlGameState> game)
    {
        game.ExecuteSequentially(new List<IAction> {
            new ModifyEnergyStatAction(CreatureCard, -game.State.Grid.Distance(CreatureCard.X, CreatureCard.Y, X2, Y2), 0),
            new RemoveCardFromTerrainAction(game.State.Grid, CreatureCard),
            new AddCardToTerrainAction(game.State.Grid, CreatureCard, X2, Y2)
        });
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        int x = CreatureCard.X;
        int y = CreatureCard.Y;
        return CreatureCard.CanMoveTo(gameState, X2, Y2) && !gameState.Grid[x, y].IsFree() && gameState.Grid[x, y].PermanentCard == CreatureCard && gameState.Grid[X2,Y2].IsFree();
    }
}
