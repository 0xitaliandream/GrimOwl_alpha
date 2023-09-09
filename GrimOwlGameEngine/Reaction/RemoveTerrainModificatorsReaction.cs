using GameEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimOwlGameEngine;


public class RemoveTerrainModificatorsReaction : CardReaction<GrimOwlGameState, GrimOwlGame, RemoveCardFromTerrainAction>
{
    
    protected IStatContainer statContainer;

    public RemoveTerrainModificatorsReaction(GrimOwlCreatureCard parentCard, IStatContainer statContainer) : base(parentCard)
    {
        this.statContainer = statContainer;
    }

    public override void ReactAfter(GrimOwlGame game, RemoveCardFromTerrainAction action)
    {
        if (action.Card == parentCard)
        {
            statContainer.InvertStats();
            game.Execute(new ModifyBuffStatsCreatureAction((GrimOwlCreatureCard)action.Card, statContainer));
            parentCard.RemoveReaction(this);
        }
    }
}