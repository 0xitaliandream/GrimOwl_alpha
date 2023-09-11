

using GameEngine;
using Newtonsoft.Json;

namespace GrimOwlGameEngine;

public class GrimOwlGame : Game<GrimOwlGameState>
{
    [JsonProperty]
    public bool isGameStarted = false;

    [JsonProperty]
    private bool isExecuting = false;

    public event System.Action OnNewGameState = delegate { };


    protected GrimOwlGame()
    {
    }

    public GrimOwlGame(GrimOwlGameState gameState) : base(gameState)
    {
    }

    public List<IAction> ExecuteRootAction(IAction action, bool withReactions = true)
    {
        if (isExecuting)
        {
            throw new InvalidOperationException("Another Execute operation is already in progress.");
        }

        isExecuting = true;

        List<IAction> executed = ExecuteSimultaneously(new List<IAction> { action }, withReactions);

        isExecuting = false;

        OnNewGameState?.Invoke();

        actionChain.Clear();

        return executed;
    }

    public void NextTurn()
    {
        ExecuteRootAction(new NextTurnAction());
    }

    public void StartGame()
    {
        ExecuteRootAction(new StartGameAction());
    }
}
