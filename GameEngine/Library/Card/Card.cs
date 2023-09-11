

using Newtonsoft.Json;

namespace GameEngine;

public abstract class Card : ReactiveCompound, ICard
{
    [JsonProperty]
    protected IPlayer? owner;

    [JsonProperty]
    protected int uniqueId;

    protected Card() { }


    public Card(bool _ = true) : base(_)
    {
        uniqueId = Guid.NewGuid().GetHashCode();
    }

    [JsonIgnore]
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

    [JsonIgnore]
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
