using GameEngine;


namespace GrimOwlGameEngine;

public class GrimOwlPermanentCard : GrimOwlCard
{

    
    protected int x = -1;
    
    protected int y = -1;

    protected GrimOwlPermanentCard()
    {
    }

    public GrimOwlPermanentCard(int mana, int manaSpecial, List<string> natures) : base(mana, manaSpecial, natures)
    {
        
    }

    
    public int X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    
    public int Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }
}
