﻿using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class GrimOwlGrid
{

    [JsonProperty]
    protected int columns;

    [JsonProperty]
    protected int rows;

    [JsonProperty]
    protected GrimOwlTerrain[,] terrains = null!;

    public GrimOwlGrid(int columns, int rows)
    {
        this.columns = columns;
        this.rows = rows;
        terrains = new GrimOwlTerrain[columns, rows];

        for (int col = 0; col < columns; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                terrains[col, row] = new GrimOwlTerrain(col,row);
            }
        }

    }

    [JsonIgnore]
    public int Columns => columns;

    [JsonIgnore] 
    public int Rows => rows;

    [JsonIgnore]
    public GrimOwlTerrain[,] Terrains => terrains;

    [JsonIgnore]
    public GrimOwlTerrain this[int column, int row]
    {
        get => terrains[column, row];
    }

    public GrimOwlPermanentCard GetCardByUniqueId(int uniqueId)
    {
        foreach (GrimOwlTerrain terrain in terrains)
        {
            if (terrain.PermanentCard != null && terrain.PermanentCard.UniqueId == uniqueId)
            {
                return terrain.PermanentCard;
            }
        }

        return null!;
    }

    public int Distance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    public void GetAvailableStrategistMoves(int currentEnergy, int x, int y, List<(int, int)> moves)
    {
        //Add to moves all cell that: 1) are free (check using terrain.IsFree(), 2) are in range, by energy 3) only on the axis of x and y
        for (int i = 1; i <= currentEnergy; i++)
        {
            if (x + i < columns && terrains[x + i, y].IsFree())
            {
                moves.Add((x + i, y));
            }
            if (x - i >= 0 && terrains[x - i, y].IsFree())
            {
                moves.Add((x - i, y));
            }
            if (y + i < rows && terrains[x, y + i].IsFree())
            {
                moves.Add((x, y + i));
            }
            if (y - i >= 0 && terrains[x, y - i].IsFree())
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
            if (x + i < columns && y + i < rows && terrains[x + i, y + i].IsFree())
            {
                moves.Add((x + i, y + i));
            }
            if (x - i >= 0 && y - i >= 0 && terrains[x - i, y - i].IsFree())
            {
                moves.Add((x - i, y - i));
            }
            if (x + i < columns && y - i >= 0 && terrains[x + i, y - i].IsFree())
            {
                moves.Add((x + i, y - i));
            }
            if (x - i >= 0 && y + i < rows && terrains[x - i, y + i].IsFree())
            {
                moves.Add((x - i, y + i));
            }
        }

    }

    public void GetPotentialCreatureTargets(IPlayer grimOwlPlayer, int currentRange, int x, int y, List<IStatContainer> targets)
    {
        //Add to targets all card that: 1) Owner is different than grimOwlPlayer, 2) are in range, by range

        for (int i = 1; i <= currentRange; i++)
        {
            if (x + i < columns && terrains[x + i, y].PermanentCard != null && terrains[x + i, y].PermanentCard.Owner != grimOwlPlayer)
            {
                targets.Add(terrains[x + i, y].PermanentCard);
            }
            if (x - i >= 0 && terrains[x - i, y].PermanentCard != null && terrains[x - i, y].PermanentCard.Owner != grimOwlPlayer)
            {
                targets.Add(terrains[x - i, y].PermanentCard);
            }
            if (y + i < rows && terrains[x, y + i].PermanentCard != null && terrains[x, y + i].PermanentCard.Owner != grimOwlPlayer)
            {
                targets.Add(terrains[x, y + i].PermanentCard);
            }
            if (y - i >= 0 && terrains[x, y - i].PermanentCard != null && terrains[x, y - i].PermanentCard.Owner != grimOwlPlayer)
            {
                targets.Add(terrains[x, y - i].PermanentCard);
            }
        }

    }

    public List<GrimOwlPermanentCard> GetPlayerPermanents(GrimOwlPlayer player)
    {
        List<GrimOwlPermanentCard> playerCards = new();

        foreach (GrimOwlTerrain terrain in terrains)
        {
            if (terrain.PermanentCard != null && terrain.PermanentCard.Owner == player)
            {
                playerCards.Add(terrain.PermanentCard);
            }
        }

        return playerCards;
    }

    public int GetAdiacentTerrainSameNatureCount(int x, int y, string terrainNature, List<(int, int)> visited = null!)
    {
        int count = 1;

        if (visited == null)
        {
            visited = new List<(int, int)>();
        }
        //recursive function

        if (visited.Contains((x, y)))
        {
            return 0;
        }

        visited.Add((x, y));

        if (x + 1 < columns && terrains[x + 1, y].Nature == terrainNature)
        {
            count += GetAdiacentTerrainSameNatureCount(x + 1, y, terrainNature, visited);
        }

        if (x - 1 >= 0 && terrains[x - 1, y].Nature == terrainNature)
        {
            count += GetAdiacentTerrainSameNatureCount(x - 1, y, terrainNature, visited);
        }

        if (y + 1 < rows && terrains[x, y + 1].Nature == terrainNature)
        {
            count += GetAdiacentTerrainSameNatureCount(x, y + 1, terrainNature, visited);
        }

        if (y - 1 >= 0 && terrains[x, y - 1].Nature == terrainNature)
        {
            count += GetAdiacentTerrainSameNatureCount(x, y - 1, terrainNature, visited);
        }


        return count;


    }

}