using GameEngine;

namespace GrimOwlGameEngine;

public class NextPlayerAction : GameEngine.Action<GrimOwlGameState>
{
    protected NextPlayerAction() { }

    public NextPlayerAction(bool isAborted = false)
        : base(isAborted)
    {
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        GrimOwlGameState state = game.State;
        int newActivePlayerIndex = state.Players.ToList().IndexOf(state.ActivePlayer);
        newActivePlayerIndex = (newActivePlayerIndex + 1) % state.Players.Count();
        GrimOwlPlayer newActivePlayer = (GrimOwlPlayer)state.Players.ElementAt(newActivePlayerIndex);

        game.Execute(new ModifyActivePlayerAction(newActivePlayer));
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return true;
    }
}
