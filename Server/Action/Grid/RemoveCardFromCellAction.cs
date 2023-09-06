
using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class RemoveCardFromCellAction : GameEngine.Action
{
    [JsonProperty]
    protected GrimOwlGrid grid = null!;

    [JsonProperty]
    protected GrimOwlPermanentCard card = null!;

    protected RemoveCardFromCellAction() { }

    public RemoveCardFromCellAction(GrimOwlGrid grid, GrimOwlPermanentCard card, bool isAborted = false)
        : base(isAborted)
    {
        this.grid = grid;
        this.card = card;
    }


    [JsonIgnore]
    public GrimOwlGrid Grid
    {
        get => grid;
    }

    [JsonIgnore]
    public GrimOwlPermanentCard Card
    {
        get => card;
    }

    public override void Execute(IGame game)
    {
        Grid.Remove(Card);
    }

    public override bool IsExecutable(IGameState gameState)
    {
        int x = Card.X;
        int y = Card.Y;
        return Card != null && !Grid.IsFree(x, y) && Grid[x, y] == Card;
    }
}
