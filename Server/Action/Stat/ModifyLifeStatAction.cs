using Newtonsoft.Json;
using GameEngine;

namespace GrimOwl;

public class ModifyLifeStatAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected IStatContainer lifeFul = null!;

    [JsonProperty]
    protected int deltaValue;

    [JsonProperty]
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

    [JsonIgnore]
    public IStatContainer LifeFul
    {
        get => lifeFul;
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
