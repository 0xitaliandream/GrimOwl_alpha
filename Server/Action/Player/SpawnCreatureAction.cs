using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class SpawnCreatureAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected GrimOwlPlayer player = null!;

    [JsonProperty]
    protected GrimOwlCreatureCard creatureCard = null!;

    [JsonProperty]
    protected int x = 0;

    [JsonProperty] 
    protected int y = 0;

    protected SpawnCreatureAction() { }

    public SpawnCreatureAction(GrimOwlPlayer player, GrimOwlCreatureCard creatureCard, int x, int y, bool isAborted = false
        ) : base(isAborted)
    {
        this.player = player;
        this.creatureCard = creatureCard;

        this.x = x;
        this.y = y;

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
    public int X
    {
        get => x;
    }

    [JsonIgnore]
    public int Y
    {
        get => y;
    }


    public override void Execute(IGame<GrimOwlGameState> game)
    {
        CreatureCard.Owner = Player;
        game.ExecuteSequentially(new List<IAction> {
            new AddCardToCellAction(game.State.Grid, CreatureCard, X, Y)
        });
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return gameState.Grid.IsFree(X,Y);
    }
}
