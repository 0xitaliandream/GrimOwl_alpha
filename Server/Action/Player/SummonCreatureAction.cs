using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class SummonCreatureAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected GrimOwlPlayer player = null!;

    [JsonProperty]
    protected GrimOwlCreatureCard creatureCard = null!;

    protected SummonCreatureAction() { }

    public SummonCreatureAction(GrimOwlPlayer player, GrimOwlCreatureCard creatureCard, bool isAborted = false
        ) : base(isAborted)
    {
        this.player = player;
        this.creatureCard = creatureCard;
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

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        game.ExecuteSequentially(new List<IAction> {
            new ModifyManaStatAction(Player, -CreatureCard.GetValue(StatKeys.Mana), 0),
            new ModifyManaSpecialStatAction(Player, -CreatureCard.GetValue(StatKeys.ManaSpecial), 0),
            new RemoveCardFromCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Hand), CreatureCard),
            new AddCardToCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Board), CreatureCard)
        });
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return CreatureCard.IsSummonable(gameState)
            && !Player.GetCardCollection(CardCollectionKeys.Board).IsFull;
    }
}
