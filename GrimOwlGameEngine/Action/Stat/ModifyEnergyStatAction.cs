
using GameEngine;

namespace GrimOwlGameEngine;

public class ModifyEnergyStatAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected IStatContainer energyful = null!;

    
    protected int deltaValue;

    
    protected int deltaBaseValue;

    protected ModifyEnergyStatAction() { }

    public ModifyEnergyStatAction(
        IStatContainer energyful,
        int deltaValue,
        int deltaBaseValue,
        bool isAborted = false
        ) : base(isAborted)
    {
        this.energyful = energyful;
        this.deltaValue = deltaValue;
        this.deltaBaseValue = deltaBaseValue;
    }

    
    public IStatContainer Energyful
    {
        get => energyful;
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
        int baseValue = Energyful.GetBaseValue(StatKeys.Energy);
        int value = Energyful.GetValue(StatKeys.Energy);

        if (baseValue + DeltaBaseValue > StatKeys.EnergyMax)
        {
            DeltaBaseValue = StatKeys.EnergyMax - baseValue;
        }

        if (value + DeltaValue > baseValue + DeltaBaseValue)
        {
            DeltaValue = baseValue + DeltaBaseValue - value;
        }

        Energyful.AddStat(StatKeys.Energy, new Stat(DeltaValue, DeltaBaseValue));
    }
}
