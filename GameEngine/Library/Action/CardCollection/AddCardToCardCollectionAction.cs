


namespace GameEngine;

public class AddCardToCardCollectionAction : Action
{
    
    protected ICardCollection cardCollection = null!;

    
    protected ICard card = null!;

    protected AddCardToCardCollectionAction() { }

    public AddCardToCardCollectionAction(ICardCollection cardCollection, ICard card, bool isAborted = false)
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
        CardCollection.Add(Card);
    }

    public override bool IsExecutable(IGameState gameState)
    {
        return Card != null && !CardCollection.IsFull;
    }
}
