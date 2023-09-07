using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class GrimOwlKingCard : GrimOwlCreatureCard
{

    protected GrimOwlKingCard()
    {
    }

    public GrimOwlKingCard(int mana, int manaSpecial, int attack, int life, int range, int energy, List<string> movement, List<string> natures) : base(mana, manaSpecial, attack, life, range, energy, movement, natures)
    {
        
    }

}
