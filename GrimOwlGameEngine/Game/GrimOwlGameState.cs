using System.Collections.Immutable;
using GameEngine;
using Newtonsoft.Json;

namespace GrimOwlGameEngine;

public class GrimOwlGameState : GameState
{
    [JsonProperty]
    protected int activePlayerIndex;

    [JsonProperty]
    protected GrimOwlGrid grid = null!;
    
    protected GrimOwlGameState()
    {
    }

    public GrimOwlGameState(bool _ = true) : base(_)
    {
        this.activePlayerIndex = 0;
    }

    [JsonIgnore]
    public GrimOwlPlayer ActivePlayer
    {
        get => (GrimOwlPlayer)Players.ElementAt(activePlayerIndex);
        set
        {
            activePlayerIndex = Players.ToList().IndexOf(value);
        }
    }

    [JsonIgnore]
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

    [JsonIgnore]
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
