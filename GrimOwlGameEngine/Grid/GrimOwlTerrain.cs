using GameEngine;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimOwlGameEngine;

public class GrimOwlTerrain : StatContainer
{
 
    private int x;
    private int y;
    private GrimOwlPermanentCard permanentCard = null!;

    public GrimOwlTerrain(int x, int y, string nature = StatKeys.Wild) : base(true)
    {
        this.x = x;
        this.y = y;

        AddStat(nature, new Stat(1, 1));
    }

    
    public string Nature
    {
        get
        {
            return Stats.First().Key.ToString();
        }
    }

    public void SetNature(string nature)
    {
        ResetStats();
        AddStat(nature, new Stat(1, 1));
    }


    public int X
    {
        get => x;
    }

    public int Y
    {
        get => y;
    }

    public GrimOwlPermanentCard PermanentCard
    {
        get => permanentCard;
        set
        {
            permanentCard = value;
        }
    }

    public bool IsFree()
    {
        return permanentCard == null;
    }

    public void Add(GrimOwlPermanentCard card)
    {
        if (IsFree())
        {
            PermanentCard = card;
            card.X = X;
            card.Y = Y;
        }
    }

    public void RemoveCard()
    {
        PermanentCard = null!;
    }
}
