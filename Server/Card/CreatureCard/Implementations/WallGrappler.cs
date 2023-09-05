using GameEngine;

namespace GrimOwl;

public class WallGrappler : GrimOwlCreatureCard
{
    protected WallGrappler() { }

    public WallGrappler(bool _ = true) 
        : base(
            mana: 3,
            manaSpecial: 0,
            attack: 0,
            life: 0,
            range: 0,
            energy: 0
            )
    {
    }
}
