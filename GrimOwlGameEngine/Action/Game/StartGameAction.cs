using GameEngine;

namespace GrimOwlGameEngine;

public class StartGameAction : GameEngine.Action<GrimOwlGameState>
{
    protected StartGameAction() { }

    public StartGameAction(bool isAborted = false)
        : base(isAborted)
    {
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        GrimOwlGameState state = game.State;
        ((GrimOwlGame)game).isGameStarted = true;
        game.ExecuteSequentially(new List<IAction>
        {
            new NextTurnAction()
        });
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return true;
    }
}
