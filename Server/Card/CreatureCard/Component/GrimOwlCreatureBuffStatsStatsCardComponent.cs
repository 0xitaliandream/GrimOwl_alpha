using GameEngine;

namespace GrimOwl;

public class GrimOwlCreatureBuffStatsStatsCardComponent : CardComponent
{
    public GrimOwlCreatureBuffStatsStatsCardComponent()
        : base(true)
    {
    }

    public virtual ISet<IStatContainer> GetPotentialTargets(IGameState gameState)
    {
        return new HashSet<IStatContainer>();
    }
}
