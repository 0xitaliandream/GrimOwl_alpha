
using GameEngine;


namespace GrimOwlGameEngine;

public class AddCardToTerrainAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected GrimOwlGrid grid = null!;

    
    protected GrimOwlPermanentCard card = null!;

    
    protected int x;

    
    protected int y;

    protected AddCardToTerrainAction() { }

    public AddCardToTerrainAction(GrimOwlGrid grid, GrimOwlPermanentCard card, int x, int y, bool isAborted = false)
        : base(isAborted)
    {
        this.grid = grid;
        this.card = card;

        this.x = x;
        this.y = y;
    }

    
    public int X
    {
        get => x;
    }

    
    public int Y
    {
        get => y;
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
        return Card != null && Grid.TerrainExists(X, Y) && Grid[X, Y]!.IsFree();
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        Grid[X, Y]!.Add(Card);
    }
}
