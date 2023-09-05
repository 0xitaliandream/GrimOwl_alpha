using GameEngine;
using Newtonsoft.Json;

namespace GrimOwl;

public class ModifyActivePlayerAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected GrimOwlPlayer newActivePlayer = null!;

    protected ModifyActivePlayerAction() { }

    public ModifyActivePlayerAction(GrimOwlPlayer newActivePlayer, bool isAborted = false)
        : base(isAborted)
    {
        this.newActivePlayer = newActivePlayer;
    }

    [JsonIgnore]
    public GrimOwlPlayer NewActivePlayer
    {
        get => newActivePlayer;
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {
        game.State.ActivePlayer = NewActivePlayer;
        game.State.ActivePlayer.relativeTurn += 1;
    }

    public override bool IsExecutable(GrimOwlGameState state)
    {
        if (!state.Players.Contains(NewActivePlayer))
        {
            throw new GameException("Could not change the active " +
                "player because the specified player is not involved " +
                "in the game!");
        }
        return NewActivePlayer != state.ActivePlayer;
    }
}
