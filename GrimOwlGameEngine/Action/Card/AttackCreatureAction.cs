using GameEngine;
using Newtonsoft.Json;

namespace GrimOwlGameEngine;

public class AttackCreatureAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected GrimOwlCreatureCard attacker = null!;

    [JsonProperty]
    protected IStatContainer target = null!;

    protected AttackCreatureAction() { }

    public AttackCreatureAction(GrimOwlCreatureCard attacker, IStatContainer target, bool isAborted = false)
        : base(isAborted)
    {
        this.attacker = attacker;
        this.target = target;
    }

    [JsonIgnore]
    public GrimOwlCreatureCard Attacker
    {
        get => attacker;
    }

    [JsonIgnore]
    public IStatContainer Target
    {
        get => target;
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        game.ExecuteSimultaneously(new List<IAction> {
            new ModifyEnergyStatAction(Attacker, -1, 0),
            new ModifyLifeStatAction(Target, -Attacker.GetValue(StatKeys.Attack), 0),
            new ModifyLifeStatAction(Attacker, -Target.GetValue(StatKeys.Attack), 0)
        });
        //game.Execute(new ModifyReadyToAttackAction(Attacker, false));
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return Attacker.IsReadyToAttack
            && Attacker.GetValue(StatKeys.Energy) > 0
            && Attacker.GetPotentialTargets(gameState).Contains(Target);
    }
}
