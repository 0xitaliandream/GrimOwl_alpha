using GameEngine;

using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace GrimOwlGameEngine;

public class GrimOwlCreatureCard : GrimOwlPermanentCard
{
    [JsonProperty]
    protected bool isReadyToAttack;

    protected GrimOwlCreatureCard()
    {
    }

    public GrimOwlCreatureCard(int mana, int manaSpecial, int attack, int life, int range, int energy, List<string> movement, List<string> natures) : base(mana, manaSpecial, natures)
    {
        this.isReadyToAttack = true;

        AddComponent(new GrimOwlCreatureStatsCardComponent(attack, life, range, energy));

        AddComponent(new GrimOwlCreatureMovementsCardComponent(movement));

        AddComponent(new GrimOwlCreatureBuffStatsStatsCardComponent());

        AddReaction(new AddTerrainModificatorsReaction(this));

        AddReaction(new DieOnModifyLifeStatActionReaction(this));

    }

    [JsonIgnore]
    public bool IsReadyToAttack
    {
        get => isReadyToAttack;
        set => isReadyToAttack = value;
    }

    [JsonProperty]
    public List<string> Movements
    {
        get
        {

            List<string> movements = new List<string>();
            CardComponent cardComponent = GetComponent<GrimOwlCreatureMovementsCardComponent>() ?? null!;

            if (cardComponent != null)
            {
                foreach (KeyValuePair<string, IList<IStat>> movement in cardComponent.Stats)
                {
                    if (this.GetValue(movement.Key) > 0)
                    {
                        movements.Add(movement.Key);
                    }
                }
            }

            return movements;

        }
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

        foreach (string movement in Movements)
        {
            switch (movement)
            {
                case StatKeys.AvantGrade:
                    gameState.Grid.GetAvailableAvantGradeMoves(currentEnergy, x, y, moves);
                    break;
                case StatKeys.Strategist:
                    gameState.Grid.GetAvailableStrategistMoves(currentEnergy, x, y, moves);
                    break;

            }
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

    public virtual List<IStatContainer> GetPotentialTargets(GrimOwlGameState gameState)
    {
        int x = this.X;
        int y = this.Y;
        int currentRange = this.GetValue(StatKeys.Range);

        List<IStatContainer> potentialTargets = new List<IStatContainer>();
        gameState.Grid.GetPotentialCreatureTargets(Owner!, currentRange, x, y, potentialTargets);
        return potentialTargets;
    }

}
