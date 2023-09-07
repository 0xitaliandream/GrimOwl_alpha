using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class GrimOwlPermanentCard : GrimOwlCard
{

    [JsonProperty]
    protected int x = -1;
    [JsonProperty]
    protected int y = -1;

    protected GrimOwlPermanentCard()
    {
    }

    public GrimOwlPermanentCard(int mana, int manaSpecial, List<string> natures) : base(mana, manaSpecial, natures)
    {
        
    }

    [JsonIgnore]
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

    [JsonIgnore]
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
