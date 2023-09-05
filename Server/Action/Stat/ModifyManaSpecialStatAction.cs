using Newtonsoft.Json;
using GameEngine;

namespace GrimOwl;

public class ModifyManaSpecialStatAction : GameEngine.Action
{
    [JsonProperty]
    protected IStatContainer manaful = null!;

    [JsonProperty]
    protected int deltaValue;

    [JsonProperty]
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

    [JsonIgnore]
    public IStatContainer Manaful
    {
        get => manaful;
    }

    [JsonIgnore]
    public int DeltaValue
    {
        get => deltaValue;
        set => deltaValue = value;
    }

    [JsonIgnore]
    public int DeltaBaseValue
    {
        get => deltaBaseValue;
        set => deltaBaseValue = value;
    }

    public override void Execute(IGame game)
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

    public override bool IsExecutable(IGameState gameState)
    {
        return true;
    }
}
