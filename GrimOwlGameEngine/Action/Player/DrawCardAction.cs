using GameEngine;


namespace GrimOwlGameEngine;

public class DrawCardAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected IPlayer player = null!;

    
    protected ICard? drawnCard;

    protected DrawCardAction() { }

    public DrawCardAction(IPlayer player, bool isAborted = false)
        : base(isAborted)
    {
        this.player = player;
    }

    
    public IPlayer Player
    {
        get => player;
    }

    
    public ICard? DrawnCard
    {
        get => drawnCard;
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        drawnCard = player.GetCardCollection(CardCollectionKeys.Deck).Last;
        game.ExecuteSequentially(new List<IAction> {
            new RemoveCardFromCardCollectionAction(player.GetCardCollection(CardCollectionKeys.Deck), drawnCard),
            new AddCardToCardCollectionAction(player.GetCardCollection(CardCollectionKeys.Hand), drawnCard)
        });
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return !player.GetCardCollection(CardCollectionKeys.Deck).IsEmpty;
    }
}
