using GameEngine;

namespace GrimOwl;

public class GrimOwlCardComponent : CardComponent
{
    protected GrimOwlCardComponent() { }

    public GrimOwlCardComponent(int mana, int manaSpecial)
        : base(true)
    {
        AddStat(StatKeys.Mana, new Stat(mana, mana));
        AddStat(StatKeys.ManaSpecial, new Stat(manaSpecial, manaSpecial));
    }
}
