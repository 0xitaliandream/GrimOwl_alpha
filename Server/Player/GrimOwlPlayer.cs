using GameEngine;

namespace GrimOwl;

public class GrimOwlPlayer : Player
{
    protected GrimOwlPlayer() { }

    public int relativeTurn;

    public GrimOwlPlayer(bool _ = true) : base(_)
    {

        AddStat(StatKeys.Mana, new Stat(0, 0));
        AddStat(StatKeys.ManaSpecial, new Stat(0, 0));

        AddCardCollection(CardCollectionKeys.Deck, new CardCollection());
        AddCardCollection(CardCollectionKeys.Hand, new CardCollection());
        AddCardCollection(CardCollectionKeys.Board, new CardCollection());
        AddCardCollection(CardCollectionKeys.Graveyard, new CardCollection());
    }

    public void DrawCard(GrimOwlGame game)
    {
        game.Execute(new DrawCardAction(this));
    }

    public void SummonCreature(GrimOwlGame game, GrimOwlCreatureCard creatureCard)
    {
        if (!creatureCard.IsSummonable(game.State))
        {
            throw new GameException("Tried to play a card that is " +
                "not playable!");
        }

        game.Execute(new SummonCreatureAction(this, creatureCard));
    }
}
