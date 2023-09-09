
using GameEngine;

namespace GrimOwlGameEngine;

public class ModifyManaStatAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected IStatContainer manaful = null!;

    
    protected int deltaValue;

    
    protected int deltaBaseValue;

    protected ModifyManaStatAction() { }

    public ModifyManaStatAction(
        IStatContainer manaful,
        int deltaValue,
        int deltaBaseValue,
        bool isAborted = false
        ) : base(isAborted)
    {
        this.manaful = manaful;
        this.deltaValue = deltaValue;
        this.deltaBaseValue = deltaBaseValue;
    }

    
    public IStatContainer Manaful
    {
        get => manaful;
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
        int baseValue = Manaful.GetBaseValue(StatKeys.Mana);
        int value = Manaful.GetValue(StatKeys.Mana);

        if (baseValue + DeltaBaseValue > StatKeys.ManaMax)
        {
            DeltaBaseValue = StatKeys.ManaMax - baseValue;
        }

        if (value + DeltaValue > baseValue + DeltaBaseValue)
        {
            DeltaValue = baseValue + DeltaBaseValue - value;
        }
        Manaful.AddStat(StatKeys.Mana, new Stat(DeltaValue, DeltaBaseValue));
    }
}
