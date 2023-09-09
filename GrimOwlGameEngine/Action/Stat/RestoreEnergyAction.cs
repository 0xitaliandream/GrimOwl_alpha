using GameEngine;


namespace GrimOwlGameEngine;

public class RestoreEnergyAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected IPlayer player = null!;

    protected RestoreEnergyAction() { }

    public RestoreEnergyAction(IPlayer player, bool isAborted = false)
        : base(isAborted)
    {
        this.player = player;
    }

    
    public IPlayer Player
    {
        get => player;
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        ICardCollection playerPermanents = ((GrimOwlPlayer)player).GetCardCollection(CardCollectionKeys.Permanent);
        for (int i = 0; i < playerPermanents.Size; i++)
        {
            GrimOwlPermanentCard permanent = (GrimOwlPermanentCard)playerPermanents[i];
            int energyDelta = permanent.GetBaseValue(StatKeys.Energy) - permanent.GetValue(StatKeys.Energy);
            game.Execute(new ModifyEnergyStatAction(permanent, energyDelta, 0));
        }
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return true;
    }
}
