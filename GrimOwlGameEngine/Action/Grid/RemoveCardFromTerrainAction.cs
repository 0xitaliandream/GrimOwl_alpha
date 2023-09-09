
using GameEngine;


namespace GrimOwlGameEngine;

public class RemoveCardFromTerrainAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected GrimOwlGrid grid = null!;

    
    protected GrimOwlPermanentCard card = null!;

    protected RemoveCardFromTerrainAction() { }

    public RemoveCardFromTerrainAction(GrimOwlGrid grid, GrimOwlPermanentCard card, bool isAborted = false)
        : base(isAborted)
    {
        this.grid = grid;
        this.card = card;
    }


    
    public GrimOwlGrid Grid
    {
        get => grid;
    }

    
    public GrimOwlPermanentCard Card
    {
        get => card;
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        int x = Card.X;
        int y = Card.Y;
        return Card != null && !Grid[card.X, card.Y]!.IsFree() && Grid[card.X, card.Y]!.PermanentCard == Card;
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        Grid[card.X, card.Y]!.RemoveCard();
    }
}
