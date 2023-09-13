using GameEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimOwlGameEngine;

public class GrimOwlGameUpdatePlayerContext
{
    public GrimOwlGameUpdatePlayerContext(GrimOwlGame game, GrimOwlPlayer player, List<IAction> updateActions)
    {
        GameInfo = game;
        Player = player;
        EnemyPlayer = (GrimOwlPlayer)game.State.Players.First(p => p != player);
        UpdateActions = updateActions;
    }

    public GrimOwlGameUpdatePlayerContext()
    {
        
    }

    [JsonProperty]
    public GrimOwlPlayer Player { get; set; } = null!;

    [JsonProperty]
    public GrimOwlPlayer EnemyPlayer { get; set; } = null!;

    [JsonProperty]
    public GrimOwlGame GameInfo { get; set; } = null!;

    [JsonIgnore]
    public List<IAction> UpdateActions { get; set; } = null!;

}
