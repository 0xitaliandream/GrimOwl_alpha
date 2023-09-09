using System.Collections.Immutable;


namespace GameEngine;

public class Player : StatContainer, IPlayer
{
    
    protected List<IReaction> reactions = null!;

    
    protected IDictionary<string, ICardCollection> cardCollections = null!;

    protected Player() { }

    /// <summary>
    /// Represents a Player.
    /// </summary>
    public Player(bool _ = true) : base(_)
    {
        cardCollections = new Dictionary<string, ICardCollection>();
        reactions = new List<IReaction>();
    }

    
    public IEnumerable<ICard> AllCards
    {
        get
        {
            List<ICard> allCards = new List<ICard>();
            foreach (ICardCollection cardCollection in cardCollections.Values)
            {
                allCards.AddRange(cardCollection.Cards);
            }
            return allCards.ToImmutableList();
        }
    }

    
    public IEnumerable<IReaction> Reactions
    {
        get => reactions.ToImmutableList();
    }

    public ICardCollection GetCardCollection(string key)
    {
        return cardCollections[key];
    }

    public void AddCardCollection(string key, ICardCollection cardCollection)
    {
        cardCollections.Add(key, cardCollection);
        cardCollection.Owner = this;
    }

    public bool RemoveCardCollection(string key)
    {
        if (!cardCollections.ContainsKey(key))
        {
            return false;
        }
        return cardCollections.Remove(key);
    }

    public IEnumerable<IReaction> AllReactions()
    {
        List<IReaction> allReactions = new List<IReaction>(Reactions);
        foreach (ICard card in AllCards)
        {
            allReactions.AddRange(card.AllReactions());
        }
        return allReactions.ToImmutableList();
    }

    public void AddReaction(IReaction reaction)
    {
        reactions.Add(reaction);
    }

    public bool RemoveReaction(IReaction reaction)
    {
        return reactions.Remove(reaction);
    }
}
