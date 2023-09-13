using GrimOwlGameEngine;
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class GameInfoView : FrameView
{

    ListView listView = null!;

    public GameInfoView(string v) : base(v)
    {
        this.listView = new ListView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            AllowsMarking = false,
            CanFocus = false,
        };

        this.Add(this.listView);
    }

    public void Update()
    {
        List<string> list = new List<string>
        {
            $"Turn: {GrimOwlConsoleGui.currentGameState.Player.relativeTurn}",
            $"Is Your Turn: {GrimOwlConsoleGui.currentGameState.Player == GrimOwlConsoleGui.currentGameState.GameInfo.State.ActivePlayer}",
            $"Mana: {GrimOwlConsoleGui.currentGameState.Player.GetValue(StatKeys.Mana)} / {GrimOwlConsoleGui.currentGameState.Player.GetBaseValue(StatKeys.Mana)}",
            $"Mana Special: {GrimOwlConsoleGui.currentGameState.Player.GetValue(StatKeys.ManaSpecial)} / {GrimOwlConsoleGui.currentGameState.Player.GetBaseValue(StatKeys.ManaSpecial)}"
        };

        this.listView.SetSource(list);
    }

}
