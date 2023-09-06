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
    protected GrimOwlPermanentCard?[,] cells = null!;

    public GrimOwlGrid(int columns, int rows)
    {
        this.columns = columns;
        this.rows = rows;
        cells = new GrimOwlPermanentCard?[columns, rows];
    }

    [JsonIgnore]
    public int Columns => columns;

    [JsonIgnore] 
    public int Rows => rows;

    [JsonIgnore]
    public GrimOwlPermanentCard?[,] Cells => cells;

    public bool IsFree(int column, int row)
    {
        return cells[column, row] == null;
    }

    public void Add(GrimOwlPermanentCard card, int x, int y)
    {
        if (IsFree(x, y))
        {
            cells[x, y] = card;
            ((GrimOwlPermanentCard)card).X = x;
            ((GrimOwlPermanentCard)card).Y = y;
        }
    }

    public void Remove(GrimOwlPermanentCard card)
    {
        int x = ((GrimOwlPermanentCard)card).X;
        int y = ((GrimOwlPermanentCard)card).Y;

        cells[x, y] = null;
    }

    [JsonIgnore]
    public GrimOwlPermanentCard? this[int column, int row]
    {
        get => cells[column, row];
    }

    public GrimOwlPermanentCard? GetByUniqueId(int uniqueId)
    {
        foreach (GrimOwlPermanentCard? card in cells)
        {
            if (card != null && card.UniqueId == uniqueId)
            {
                return card;
            }
        }

        return null;
    }

    public int Distance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    public void GetAvailableStrategistMoves(int currentEnergy, int x, int y, List<(int, int)> moves)
    {
        //Add to moves all cell that: 1) are free, 2) are in range, by energy 3) only on the axis of x and y
        for (int i = 1; i <= currentEnergy; i++)
        {
            if (x + i < columns && IsFree(x + i, y))
            {
                moves.Add((x + i, y));
            }
            if (x - i >= 0 && IsFree(x - i, y))
            {
                moves.Add((x - i, y));
            }
            if (y + i < rows && IsFree(x, y + i))
            {
                moves.Add((x, y + i));
            }
            if (y - i >= 0 && IsFree(x, y - i))
            {
                moves.Add((x, y - i));
            }
        }
    }

    public void GetAvailableAvantGradeMoves(int currentEnergy, int x, int y, List<(int, int)> moves)
    {
        //Add to moves all cell that: 1) are free, 2) are in range, by energy 3) only on diagonal from x and y

        for (int i = 1; i <= currentEnergy; i++)
        {
            if (x + i < columns && y + i < rows && IsFree(x + i, y + i))
            {
                moves.Add((x + i, y + i));
            }
            if (x - i >= 0 && y - i >= 0 && IsFree(x - i, y - i))
            {
                moves.Add((x - i, y - i));
            }
            if (x + i < columns && y - i >= 0 && IsFree(x + i, y - i))
            {
                moves.Add((x + i, y - i));
            }
            if (x - i >= 0 && y + i < rows && IsFree(x - i, y + i))
            {
                moves.Add((x - i, y + i));
            }
        }

    }

    public List<GrimOwlPermanentCard> GetPlayerPermanents(GrimOwlPlayer player)
    {
        List<GrimOwlPermanentCard> playerCards = new();

        foreach (GrimOwlPermanentCard? card in cells)
        {
            if (card != null && card.Owner == player)
            {
                playerCards.Add(card);
            }
        }

        return playerCards;
    }

}