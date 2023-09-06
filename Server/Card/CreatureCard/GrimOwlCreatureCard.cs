using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class GrimOwlCreatureCard : GrimOwlPermanentCard
{
    [JsonProperty]
    protected bool isReadyToAttack;

    protected GrimOwlCreatureCard()
    {
    }

    public GrimOwlCreatureCard(int mana, int manaSpecial, int attack, int life, int range, int energy, List<string> movement) : base(mana, manaSpecial)
    {
        this.isReadyToAttack = false;

        AddComponent(new GrimOwlCreatureStatsCardComponent(attack, life, range, energy));

        AddComponent(new GrimOwlCreatureMovementsCardComponent(movement));

    }

    [JsonIgnore]
    public bool IsReadyToAttack
    {
        get => isReadyToAttack;
        set => isReadyToAttack = value;
    }

    public virtual bool IsSummonable(GrimOwlGameState gameState)
    {
        GrimOwlGameState state = gameState;
        return Owner != null
                && Owner.GetCardCollection(CardCollectionKeys.Hand).Contains(this)
                && Owner == state.ActivePlayer
                && GetValue(StatKeys.Mana) <= state.ActivePlayer.GetValue(StatKeys.Mana)
                && GetValue(StatKeys.ManaSpecial) <= state.ActivePlayer.GetValue(StatKeys.ManaSpecial);
    }

    public List<(int, int)> GetPossibleMoves(GrimOwlGameState gameState)
    {
        List<(int, int)> moves = new List<(int, int)>();

        int x = this.X;
        int y = this.Y;
        int currentEnergy = this.GetValue(StatKeys.Energy);

        if (this.GetValue(StatKeys.AvantGrade) > 0)
        {
            gameState.Grid.GetAvailableAvantGradeMoves(currentEnergy, x, y, moves);
        }
        if (this.GetValue(StatKeys.Strategist) > 0)
        {
            gameState.Grid.GetAvailableStrategistMoves(currentEnergy, x, y, moves);
        }

        return moves;
    }

    public virtual bool CanMoveTo(GrimOwlGameState gameState, int x, int y)
    {
        GrimOwlGameState state = gameState;

        List<(int, int)> possibleMoves = this.GetPossibleMoves(gameState);

        return Owner != null
                && Owner == state.ActivePlayer
                && possibleMoves.Contains((x, y));
    }
}
