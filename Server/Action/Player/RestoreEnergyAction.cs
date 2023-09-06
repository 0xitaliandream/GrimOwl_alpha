using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class RestoreEnergyAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected IPlayer player = null!;

    protected RestoreEnergyAction() { }

    public RestoreEnergyAction(IPlayer player, bool isAborted = false)
        : base(isAborted)
    {
        this.player = player;
    }

    [JsonIgnore]
    public IPlayer Player
    {
        get => player;
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        List<GrimOwlPermanentCard> playerPermanents = ((GrimOwlPlayer)player).GetPermanents((GrimOwlGame)game);
        foreach (GrimOwlPermanentCard permanent in playerPermanents)
        {
            if (permanent is GrimOwlCreatureCard creatureCard)
            {
                int energyDelta = creatureCard.GetBaseValue(StatKeys.Energy) - creatureCard.GetValue(StatKeys.Energy);
                game.Execute(new ModifyEnergyStatAction(creatureCard, energyDelta, 0));
            }
        }
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return true;
    }
}
