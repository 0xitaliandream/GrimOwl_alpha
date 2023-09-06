using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class SummonCreatureAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected GrimOwlPlayer player = null!;

    [JsonProperty]
    protected GrimOwlCreatureCard creatureCard = null!;

    [JsonProperty]
    protected int x = 0;

    [JsonProperty] 
    protected int y = 0;

    protected SummonCreatureAction() { }

    public SummonCreatureAction(GrimOwlPlayer player, GrimOwlCreatureCard creatureCard, int x, int y, bool isAborted = false
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
        game.ExecuteSequentially(new List<IAction> {
            new ModifyManaStatAction(Player, -CreatureCard.GetValue(StatKeys.Mana), 0),
            new ModifyManaSpecialStatAction(Player, -CreatureCard.GetValue(StatKeys.ManaSpecial), 0),
            new RemoveCardFromCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Hand), CreatureCard),
            new AddCardToGridAction(game.State.Grid, CreatureCard, X, Y)
        });
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return CreatureCard.IsSummonable(gameState) && gameState.Grid.IsFree(X,Y);
            
    }
}
