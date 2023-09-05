using System.Collections.Immutable;
using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class GrimOwlGameState : GameState
{
    [JsonProperty]
    protected int activePlayerIndex;
    
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
    public IEnumerable<IPlayer> NonActivePlayers
    {
        get
        {
            return Players.Where(p => p != ActivePlayer).ToImmutableList();
        }
    }
}
