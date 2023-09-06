
using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class AddCardToCellAction : GameEngine.Action
{
    [JsonProperty]
    protected GrimOwlGrid grid = null!;

    [JsonProperty]
    protected GrimOwlPermanentCard card = null!;

    [JsonProperty]
    protected int x;

    [JsonProperty]
    protected int y;

    protected AddCardToCellAction() { }

    public AddCardToCellAction(GrimOwlGrid grid, GrimOwlPermanentCard card, int x, int y, bool isAborted = false)
        : base(isAborted)
    {
        this.grid = grid;
        this.card = card;

        this.x = x;
        this.y = y;
    }

    [JsonIgnore]
    public int X
    {
        get => x;
    }

    [JsonIgnore]
    public int Y
    {
        get => y;
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
        Grid.Add(Card, X, Y);
    }

    public override bool IsExecutable(IGameState gameState)
    {
        return Card != null && Grid.IsFree(X, Y);
    }
}
