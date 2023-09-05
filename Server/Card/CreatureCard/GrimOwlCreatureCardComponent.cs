using GameEngine;

namespace GrimOwl;

public class GrimOwlCreatureCardComponent : GrimOwlCardComponent
{
    protected GrimOwlCreatureCardComponent() { }

    public GrimOwlCreatureCardComponent(int mana, int manaSpecial, int attack, int life, int range, int energy)
        : base(mana, manaSpecial)
    {
        AddStat(StatKeys.Attack, new Stat(attack, attack));
        AddStat(StatKeys.Life, new Stat(life, life));
        AddStat(StatKeys.Range, new Stat(range, range));
        AddStat(StatKeys.Energy, new Stat(energy, energy));

    }

    public virtual ISet<IStatContainer> GetPotentialTargets(IGameState gameState)
    {
        return new HashSet<IStatContainer>();
    }
}
