using GameEngine;


namespace GrimOwlGameEngine;

public class MalikII : GrimOwlKingCard
{
    protected MalikII() { }

    public MalikII(bool _ = true) 
        : base(
            mana: 0,
            manaSpecial: 0,
            attack: 1,
            life: 1,
            range: 1,
            energy: 1,
            movement: new List<string> { StatKeys.Strategist },
            natures: new List<string> { StatKeys.Feral }
            )
    {
    }
}
