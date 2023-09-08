using GameEngine;

namespace GrimOwlGameEngine;

public class DieOnModifyLifeStatActionReaction : CardReaction<GrimOwlGameState, GrimOwlGame, ModifyLifeStatAction>
{
    protected DieOnModifyLifeStatActionReaction() { }

    public DieOnModifyLifeStatActionReaction(GrimOwlCreatureCard parentCard)
        : base(parentCard) { }

    public override void ReactAfter(GrimOwlGame game, ModifyLifeStatAction action)
    {
        if (ParentCard is GrimOwlCreatureCard creatureCard && creatureCard.GetValue(StatKeys.Life) <= 0)
        {
            game.ExecuteSequentially(new List<IAction> { 
                new DieAction(creatureCard)
            });
        }
    }   
}
