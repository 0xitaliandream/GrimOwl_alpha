using GameEngine;

namespace GrimOwl;

public class AddTerrainModificatorsReaction : CardReaction<GrimOwlGameState, GrimOwlGame, AddCardToTerrainAction>
{
    public AddTerrainModificatorsReaction(GrimOwlCreatureCard parentCard) : base(parentCard)
    {

    }

    public override void ReactAfter(GrimOwlGame game, AddCardToTerrainAction action)
    {
        if (action.Card == parentCard)
        {
            CardComponent component = GrimOwlNatureGroups.GetNatureModificatorActions(game, (GrimOwlCreatureCard)parentCard, action.Grid[action.X, action.Y]);
            action.Card.AddReaction(new RemoveTerrainModificatorsReaction((GrimOwlCreatureCard)action.Card, component));
            game.Execute(new ModifyBuffStatsCreatureAction((GrimOwlCreatureCard)action.Card, component));
        }
    }
}