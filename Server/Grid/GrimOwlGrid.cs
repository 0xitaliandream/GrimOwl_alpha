using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class GrimOwlGrid
{

    [JsonProperty]
    protected int columns;

    [JsonProperty]
    protected int rows;

    [JsonProperty]
    protected ICard?[,] cells = null!;

    public GrimOwlGrid(int columns, int rows)
    {
        this.columns = columns;
        this.rows = rows;
        cells = new ICard?[columns, rows];
    }

    [JsonIgnore]
    public int Columns => columns;

    [JsonIgnore] 
    public int Rows => rows;

    [JsonIgnore]
    public ICard?[,] Cells => cells;

    public bool IsFree(int column, int row)
    {
        return cells[column, row] == null;
    }

    public void Add(ICard card, int x, int y)
    {
        if (IsFree(x, y))
        {
            cells[x, y] = card;
        }
    }

}
