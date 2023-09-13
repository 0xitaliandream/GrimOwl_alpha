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

    public void Update(GrimOwlPlayer player)
    {
        List<string> list = new List<string>
        {
            $"Turn: {player.relativeTurn}",
            $"Is Your Turn: {player == GrimOwlConsoleGui.currentGameState.GameInfo.State.ActivePlayer}",
            $"Mana: {player.GetValue(StatKeys.Mana)} / {player.GetBaseValue(StatKeys.Mana)}",
            $"Mana Special: {player.GetValue(StatKeys.ManaSpecial)} / {player.GetBaseValue(StatKeys.ManaSpecial)}"
        };

        this.listView.SetSource(list);
    }

}
