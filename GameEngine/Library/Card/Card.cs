

namespace GameEngine;

public abstract class Card : ReactiveCompound, ICard
{
    
    protected IPlayer? owner;

    
    protected int uniqueId;

    protected Card() { }


    public Card(bool _ = true) : base(_)
    {
        uniqueId = Guid.NewGuid().GetHashCode();
    }

    
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

    
    public int UniqueId
    {
        get
        {
            return uniqueId;
        }
        set
        {
            uniqueId = value;
        }
    }

    public override void AddComponent(ICardComponent cardComponent)
    {
        base.AddComponent(cardComponent);
        cardComponent.ParentCard = this;
    }
}
