
using GameEngine;

namespace GrimOwlGameEngine;

public class ModifyLifeStatAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected IStatContainer lifeFul = null!;

    
    protected int deltaValue;

    
    protected int deltaBaseValue;

    protected ModifyLifeStatAction() { }

    public ModifyLifeStatAction(
        IStatContainer lifeFul,
        int deltaValue,
        int deltaBaseValue,
        bool isAborted = false
        ) : base(isAborted)
    {
        this.lifeFul = lifeFul;
        this.deltaValue = deltaValue;
        this.deltaBaseValue = deltaBaseValue;
    }

    
    public IStatContainer LifeFul
    {
        get => lifeFul;
    }

    
    public int DeltaValue
    {
        get => deltaValue;
        set => deltaValue = value;
    }

    
    public int DeltaBaseValue
    {
        get => deltaBaseValue;
        set => deltaBaseValue = value;
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return true;
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        int baseValue = LifeFul.GetBaseValue(StatKeys.Life);
        int value = LifeFul.GetValue(StatKeys.Life);

        if (baseValue + DeltaBaseValue > StatKeys.LifeMax)
        {
            DeltaBaseValue = StatKeys.LifeMax - baseValue;
        }

        if (value + DeltaValue > baseValue + DeltaBaseValue)
        {
            DeltaValue = baseValue + DeltaBaseValue - value;
        }
        LifeFul.AddStat(StatKeys.Life, new Stat(DeltaValue, DeltaBaseValue));
    }
}
