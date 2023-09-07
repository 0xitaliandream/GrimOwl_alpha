using GameEngine;


namespace GrimOwl;

public class WallGrappler : GrimOwlCreatureCard
{
    protected WallGrappler() { }

    public WallGrappler(bool _ = true) 
        : base(
            mana: 3,
            manaSpecial: 0,
            attack: 1,
            life: 1,
            range: 2,
            energy: 1,
            movement: new List<string> { StatKeys.Strategist },
            natures: new List<string> { StatKeys.Feral }
            )
    {
    }
}
