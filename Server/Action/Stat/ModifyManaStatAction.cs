using Newtonsoft.Json;
using GameEngine;

namespace GrimOwl;

public class ModifyManaStatAction : GameEngine.Action
{
    [JsonProperty]
    protected IStatContainer manaful = null!;

    [JsonProperty]
    protected int deltaValue;

    [JsonProperty]
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

    public override bool IsExecutable(IGameState gameState)
    {
        return true;
    }
}
