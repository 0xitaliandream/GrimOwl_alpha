using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class GrimOwlPermanentCard : Card
{

    [JsonProperty]
    protected int x = -1;
    [JsonProperty]
    protected int y = -1;

    protected GrimOwlPermanentCard()
    {
    }

    public GrimOwlPermanentCard(int mana, int manaSpecial) : base(true)
    {
        AddComponent(new GrimOwlManasCardComponent(mana, manaSpecial));
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
