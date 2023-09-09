
using GameEngine;

namespace GrimOwlGameEngine;

public class ModifyAttackStatAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected IStatContainer attackFul = null!;

    
    protected int deltaValue;

    
    protected int deltaBaseValue;

    protected ModifyAttackStatAction() { }

    public ModifyAttackStatAction(
        IStatContainer attackFul,
        int deltaValue,
        int deltaBaseValue,
        bool isAborted = false
        ) : base(isAborted)
    {
        this.attackFul = attackFul;
        this.deltaValue = deltaValue;
        this.deltaBaseValue = deltaBaseValue;
    }

    
    public IStatContainer AttackFul
    {
        get => attackFul;
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
        int baseValue = AttackFul.GetBaseValue(StatKeys.Attack);
        int value = AttackFul.GetValue(StatKeys.Attack);

        if (baseValue + DeltaBaseValue > StatKeys.AttackMax)
        {
            DeltaBaseValue = StatKeys.AttackMax - baseValue;
        }

        if (value + DeltaValue > baseValue + DeltaBaseValue)
        {
            DeltaValue = baseValue + DeltaBaseValue - value;
        }
        AttackFul.AddStat(StatKeys.Attack, new Stat(DeltaValue, DeltaBaseValue));
    }
}
