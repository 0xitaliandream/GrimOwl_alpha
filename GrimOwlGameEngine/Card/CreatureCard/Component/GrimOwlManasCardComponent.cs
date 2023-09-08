using GameEngine;

namespace GrimOwlGameEngine;

public class GrimOwlManasCardComponent : CardComponent
{
    protected GrimOwlManasCardComponent() { }

    public GrimOwlManasCardComponent(int mana, int manaSpecial)
        : base(true)
    {
        AddStat(StatKeys.Mana, new Stat(mana, mana));
        AddStat(StatKeys.ManaSpecial, new Stat(manaSpecial, manaSpecial));
    }
}
