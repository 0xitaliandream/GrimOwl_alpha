using GameEngine;

namespace GrimOwlGameEngine;

public class GrimOwlPlayer : Player
{
    protected GrimOwlPlayer() { }

    public int relativeTurn;

    public GrimOwlKingCard king = null!;

    private GrimOwlPlayerCommandController commandController = null!;

    public GrimOwlPlayer(bool _ = true) : base(_)
    {

        CommandController = new GrimOwlPlayerCommandController(this);

        AddStat(StatKeys.Mana, new Stat(0, 0));
        AddStat(StatKeys.ManaSpecial, new Stat(0, 0));

        AddCardCollection(CardCollectionKeys.Deck, new CardCollection());
        AddCardCollection(CardCollectionKeys.Hand, new CardCollection());
        AddCardCollection(CardCollectionKeys.Graveyard, new CardCollection());
        AddCardCollection(CardCollectionKeys.Permanent, new CardCollection());

    }

    public void SetKing(GrimOwlKingCard king)
    {
        King = king;
    }

    public GrimOwlKingCard King
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

    public GrimOwlPlayerCommandController CommandController
    {
        get
        {
            return commandController;
        }
        set
        {
            commandController = value;
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

    public void AttackCreature(GrimOwlGame game, GrimOwlCreatureCard attacker, GrimOwlCreatureCard defender)
    {
        game.Execute(new AttackCreatureAction(attacker, defender));
    }

    public void MoveCreature(GrimOwlGame game, GrimOwlCreatureCard creatureCard, int x, int y)
    {
        game.Execute(new MoveCreatureAction(this, creatureCard, x, y));
    }
}
