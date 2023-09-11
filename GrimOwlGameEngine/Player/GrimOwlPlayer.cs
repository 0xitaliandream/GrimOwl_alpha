using GameEngine;

using Newtonsoft.Json;

namespace GrimOwlGameEngine;

public class GrimOwlPlayer : Player
{
    protected GrimOwlPlayer() { }

    [JsonProperty]
    public int relativeTurn;

    [JsonIgnore]
    public GrimOwlKingCard king = null!;

    [JsonIgnore]
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

    [JsonIgnore]
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

    [JsonIgnore]
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
        game.ExecuteRootAction(new DrawCardAction(this));
    }

    public void SummonCreature(GrimOwlGame game, GrimOwlCreatureCard creatureCard, int x, int y)
    {
        if (!creatureCard.IsSummonable(game.State))
        {
            throw new GameException("Tried to play a card that is " +
                "not playable!");
        }

        game.ExecuteRootAction(new SummonCreatureAction(this, creatureCard, x, y));
    }

    public void SpawnCreature(GrimOwlGame game, GrimOwlCreatureCard creatureCard, int x, int y)
    {
        game.ExecuteRootAction(new SpawnCreatureAction(this, creatureCard, x, y));
    }

    public void AttackCreature(GrimOwlGame game, GrimOwlCreatureCard attacker, GrimOwlCreatureCard defender)
    {
        game.ExecuteRootAction(new AttackCreatureAction(attacker, defender));
    }

    public void MoveCreature(GrimOwlGame game, GrimOwlCreatureCard creatureCard, int x, int y)
    {
        game.ExecuteRootAction(new MoveCreatureAction(this, creatureCard, x, y));
    }
}
