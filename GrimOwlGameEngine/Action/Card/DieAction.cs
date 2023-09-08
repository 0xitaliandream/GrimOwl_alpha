using GameEngine;
using Newtonsoft.Json;

namespace GrimOwlGameEngine;

public class DieAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected GrimOwlCreatureCard creatureCard = null!;

    protected DieAction() { }

    public DieAction(GrimOwlCreatureCard creatureCard, bool isAborted = false)
        : base(isAborted)
    {
        this.creatureCard = creatureCard;
    }

    [JsonIgnore]
    public GrimOwlCreatureCard CreatureCard
    {
        get => creatureCard;
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        IPlayer owner = CreatureCard.Owner!;
        game.ExecuteSequentially(new List<IAction> {
            new RemoveCardFromTerrainAction(game.State.Grid, CreatureCard),
            new RemoveCardFromCardCollectionAction(owner.GetCardCollection(CardCollectionKeys.Permanent), CreatureCard),
            new AddCardToCardCollectionAction(owner.GetCardCollection(CardCollectionKeys.Graveyard), CreatureCard)
        });
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        IPlayer? owner = CreatureCard.Owner;
        return owner != null
            && owner.GetCardCollection(CardCollectionKeys.Permanent).Contains(CreatureCard);
    }
}
