using System.Collections.Immutable;


namespace GameEngine;

public class CardCollection : ICardCollection
{
    
    protected IPlayer? owner;

    
    protected int? maxSize;

    
    protected List<ICard> cards = null!;

    protected CardCollection() { }

    public CardCollection(int? maxSize = null)
    {
        this.maxSize = maxSize;
        cards = new List<ICard>();
    }

    
    public IEnumerable<ICard> Cards => cards.ToImmutableList();

    
    public bool IsEmpty => cards.Count == 0;

    
    public bool IsFull => maxSize != null && cards.Count >= maxSize;

    
    public int Size => cards.Count;

    
    public IPlayer? Owner
    {
        get
        {
            return owner;
        }
        set
        {
            owner = value;
        }
    }

    
    public int? MaxSize
    {
        get
        {
            return maxSize;
        }
        set
        {
            maxSize = value;
        }
    }

    
    public ICard this[int index]
    {
        get => cards[index];
    }

    
    public ICard First
    {
        get => cards[0];
    }

    
    public ICard Last
    {
        get => cards[cards.Count - 1];
    }

    public bool Contains(ICard card)
    {
        return cards.Contains(card);
    }

    public void Add(ICard card)
    {
        if (MaxSize != null && cards.Count >= MaxSize)
        {
            throw new GameException("Cannot add ICard to CardCollection is its maximum size has been reached");
        }
        cards.Add(card);
        card.Owner = Owner;
    }

    public void Remove(ICard card)
    {
        cards.Remove(card);
    }

    public void Shuffle()
    {
        ICard[] cardsCopy = cards.ToArray();
        cards.Clear();
        foreach (ICard card in cardsCopy.OrderBy(x => new Random().Next()))
        {
            Add(card);
        }
    }

    public ICard GetByUniqueId(int uniqueId)
    {
        return cards.First(x => x.UniqueId == uniqueId);
    }
}
