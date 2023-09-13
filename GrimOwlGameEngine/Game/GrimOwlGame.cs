

using GameEngine;
using Newtonsoft.Json;

namespace GrimOwlGameEngine;

public class GrimOwlGame : Game<GrimOwlGameState>
{
    [JsonProperty]
    public bool isGameStarted = false;

    public event System.Action<List<IAction>> OnNewGameState = delegate { };

    [JsonIgnore]
    private Mutex mutex = new Mutex();

    protected GrimOwlGame()
    {
    }

    public GrimOwlGame(GrimOwlGameState gameState) : base(gameState)
    {
    }

    public List<IAction> ExecuteRootAction(IAction action, bool withReactions = true)
    {

        mutex.WaitOne();

        try
        {
            actionChain.Clear();

            List<IAction> executed = ExecuteSimultaneously(new List<IAction> { action }, withReactions);

            OnNewGameState?.Invoke(actionChain);

            return executed;
        }
        finally
        {
            mutex.ReleaseMutex(); // Rilascia il mutex per permettere ad altri thread di entrare
        }
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
