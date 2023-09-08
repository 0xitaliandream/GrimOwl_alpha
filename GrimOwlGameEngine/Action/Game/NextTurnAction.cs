using GameEngine;

namespace GrimOwlGameEngine;

public class NextTurnAction : GameEngine.Action<GrimOwlGameState>
{
    protected NextTurnAction() { }

    public NextTurnAction(bool isAborted = false)
        : base(isAborted)
    {
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        GrimOwlGameState state = game.State;

        bool wasExecuted = game.Execute(new NextPlayerAction()).Count == 1;
        if (wasExecuted)
        {
            GrimOwlPlayer activePlayer = state.ActivePlayer;

            List<IAction> actions = new List<IAction>();
            int manaDelta = activePlayer.GetBaseValue(StatKeys.Mana) + 1 - activePlayer.GetValue(StatKeys.Mana);
            actions.Add(new ModifyManaStatAction(state.ActivePlayer, manaDelta, 1));

            if (activePlayer.relativeTurn % 3 == 0 && activePlayer.relativeTurn != 0)
            {
                int manaSpecialDelta = activePlayer.GetBaseValue(StatKeys.ManaSpecial) + 1 - activePlayer.GetValue(StatKeys.ManaSpecial);
                actions.Add(new ModifyManaSpecialStatAction(state.ActivePlayer, manaSpecialDelta, 1));
            }
            actions.Add(new RestoreEnergyAction(state.ActivePlayer));
            actions.Add(new DrawCardAction(activePlayer));

            game.ExecuteSimultaneously(actions);
        }
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return true;
    }
}
