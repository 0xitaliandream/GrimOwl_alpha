using GameEngine;

namespace GrimOwlGameEngine;

public class GrimOwlCreatureMovementsCardComponent : CardComponent
{
    protected GrimOwlCreatureMovementsCardComponent() { }

    public GrimOwlCreatureMovementsCardComponent(List<string> movements)
        : base(true)
    {
        foreach (var movement in movements)
        {
            AddStat(movement, new Stat(1, 1));
        }
    }

    public virtual ISet<IStatContainer> GetPotentialTargets(IGameState gameState)
    {
        return new HashSet<IStatContainer>();
    }
}
