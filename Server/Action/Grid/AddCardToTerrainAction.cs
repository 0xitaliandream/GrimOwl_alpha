
using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class AddCardToTerrainAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected GrimOwlGrid grid = null!;

    [JsonProperty]
    protected GrimOwlPermanentCard card = null!;

    [JsonProperty]
    protected int x;

    [JsonProperty]
    protected int y;

    protected AddCardToTerrainAction() { }

    public AddCardToTerrainAction(GrimOwlGrid grid, GrimOwlPermanentCard card, int x, int y, bool isAborted = false)
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

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return Card != null && Grid[X, Y].IsFree();
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        Grid[X, Y].Add(Card);
    }
}
