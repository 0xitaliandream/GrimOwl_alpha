


namespace GameEngine;

public class RemoveCardFromCardCollectionAction : Action
{
    
    protected ICardCollection cardCollection = null!;

    
    protected ICard card = null!;

    protected RemoveCardFromCardCollectionAction() { }

    public RemoveCardFromCardCollectionAction(ICardCollection cardCollection, ICard card, bool isAborted = false)
        : base(isAborted)
    {
        this.cardCollection = cardCollection;
        this.card = card;
    }

    
    public ICardCollection CardCollection
    {
        get => cardCollection;
    }

    
    public ICard Card
    {
        get => card;
    }

    public override void Execute(IGame game)
    {
        CardCollection.Remove(Card);
    }

    public override bool IsExecutable(IGameState gameState)
    {
        return CardCollection.Contains(Card);
    }
}
