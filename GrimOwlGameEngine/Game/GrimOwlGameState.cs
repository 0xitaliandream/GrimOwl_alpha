using System.Collections.Immutable;
using GameEngine;


namespace GrimOwlGameEngine;


public class GrimOwlGameState : GameState
{
    
    protected int activePlayerIndex;

    
    protected GrimOwlGrid grid = null!;
    
    protected GrimOwlGameState()
    {
    }

    public GrimOwlGameState(bool _ = true) : base(_)
    {
        this.activePlayerIndex = 0;
    }

    
    public GrimOwlPlayer ActivePlayer
    {
        get => (GrimOwlPlayer)Players.ElementAt(activePlayerIndex);
        set
        {
            activePlayerIndex = Players.ToList().IndexOf(value);
        }
    }

    
    public GrimOwlGrid Grid
    {
        get
        {
            return grid;
        }
        set
        {
            grid = value;
        }
    }

    
    public IEnumerable<IPlayer> NonActivePlayers
    {
        get
        {
            return Players.Where(p => p != ActivePlayer).ToImmutableList();
        }
    }

    public void AddGrid(GrimOwlGrid grimOwlGrid)
    {
        Grid = grimOwlGrid;
    }
}
