using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class GrimOwlCreatureCard : Card
{
    [JsonProperty]
    protected bool isReadyToAttack;

    protected GrimOwlCreatureCard()
    {
    }

    public GrimOwlCreatureCard(int mana, int manaSpecial, int attack, int life, int range, int energy) : base(true)
    {
        this.isReadyToAttack = false;

        AddComponent(new GrimOwlCreatureCardComponent(mana, manaSpecial, attack, life, range, energy));
    }

    [JsonIgnore]
    public bool IsReadyToAttack
    {
        get => isReadyToAttack;
        set => isReadyToAttack = value;
    }

    public virtual bool IsSummonable(GrimOwlGameState gameState)
    {
        GrimOwlGameState state = gameState;
        return Owner != null
                && Owner.GetCardCollection(CardCollectionKeys.Hand).Contains(this)
                && Owner == state.ActivePlayer
                && GetValue(StatKeys.Mana) <= state.ActivePlayer.GetValue(StatKeys.Mana)
                && GetValue(StatKeys.ManaSpecial) <= state.ActivePlayer.GetValue(StatKeys.ManaSpecial);
    }
}
