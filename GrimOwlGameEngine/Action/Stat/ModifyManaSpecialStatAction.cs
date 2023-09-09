
using GameEngine;

namespace GrimOwlGameEngine;

public class ModifyManaSpecialStatAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected IStatContainer manaful = null!;

    
    protected int deltaValue;

    
    protected int deltaBaseValue;

    protected ModifyManaSpecialStatAction() { }

    public ModifyManaSpecialStatAction(
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
        int baseValue = Manaful.GetBaseValue(StatKeys.ManaSpecial);
        int value = Manaful.GetValue(StatKeys.ManaSpecial);

        if (baseValue + DeltaBaseValue > StatKeys.ManaSpecialMax)
        {
            DeltaBaseValue = StatKeys.ManaSpecialMax - baseValue;
        }

        if (value + DeltaValue > baseValue + DeltaBaseValue)
        {
            DeltaValue = baseValue + DeltaBaseValue - value;
        }

        Manaful.AddStat(StatKeys.ManaSpecial, new Stat(DeltaValue, DeltaBaseValue));
    }
}
