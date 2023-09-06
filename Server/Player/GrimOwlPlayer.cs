using GameEngine;

namespace GrimOwl;

public class GrimOwlPlayer : Player
{
    protected GrimOwlPlayer() { }

    public int relativeTurn;

    public GrimOwlCreatureCard king = null!;

    public GrimOwlPlayer(bool _ = true) : base(_)
    {

        AddStat(StatKeys.Mana, new Stat(0, 0));
        AddStat(StatKeys.ManaSpecial, new Stat(0, 0));

        AddCardCollection(CardCollectionKeys.Deck, new CardCollection());
        AddCardCollection(CardCollectionKeys.Hand, new CardCollection());
        AddCardCollection(CardCollectionKeys.Graveyard, new CardCollection());
    }

    public void SetKing(GrimOwlCreatureCard king)
    {
        King = king;
    }

    public GrimOwlCreatureCard King
    {
        get
        {
            return king;
        }
        set
        {
            king = value;
        }
    }


    public void DrawCard(GrimOwlGame game)
    {
        game.Execute(new DrawCardAction(this));
    }

    public void SummonCreature(GrimOwlGame game, GrimOwlCreatureCard creatureCard, int x, int y)
    {
        if (!creatureCard.IsSummonable(game.State))
        {
            throw new GameException("Tried to play a card that is " +
                "not playable!");
        }

        game.Execute(new SummonCreatureAction(this, creatureCard, x, y));
    }

    public void SpawnCreature(GrimOwlGame game, GrimOwlCreatureCard creatureCard, int x, int y)
    {
        game.Execute(new SpawnCreatureAction(this, creatureCard, x, y));
    }

    public void MoveCreature(GrimOwlGame game, GrimOwlCreatureCard creatureCard, int x, int y)
    {
        game.Execute(new MoveCreatureAction(this, creatureCard, x, y));
    }

    public List<GrimOwlPermanentCard> GetPermanents(GrimOwlGame game)
    {
        return game.State.Grid.GetPlayerPermanents(this);
    }
}
