using GameEngine;

namespace GrimOwlGameEngine;

public class GrimOwlCreatureStatsCardComponent : CardComponent
{
    protected GrimOwlCreatureStatsCardComponent() { }

    public GrimOwlCreatureStatsCardComponent(int attack, int life, int range, int energy)
        : base(true)
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
