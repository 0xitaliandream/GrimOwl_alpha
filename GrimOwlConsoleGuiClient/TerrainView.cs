
using GameEngine;
using GrimOwlGameEngine;
using System.Text;
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class TerrainView : FrameView
{
    public int x;
    public int y;

    private GrimOwlTerrain Terrain = null!;
    
    public View mainView;
    public TextView infoView;

    public TerrainView(int x, int y) : base($"{x},{y}")
    {
        this.x = x;
        this.y = y;

        this.mainView = new View()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(0),
            Height = Dim.Fill(0),
            CanFocus = false,
            ColorScheme = new ColorScheme() { Normal = new Terminal.Gui.Attribute(Color.White, Color.BrightBlue) }
        };

        this.infoView = new TextView()
        {
            X = 1,
            Y = 1,
            Width = Dim.Fill(1),
            Height = Dim.Fill(1),
            CanFocus = false,
            ColorScheme = new ColorScheme() { Normal = new Terminal.Gui.Attribute(Color.White, Color.Black) },
            ReadOnly = true,
        };

        mainView.Add(infoView);

        MouseClick += (MouseEventArgs me) =>
        {
            if (me.MouseEvent.Flags == MouseFlags.Button1Clicked)
            {
                Application.Run(new TerrainModalView(Terrain));
            }

        };

        this.Add(this.mainView);
    }

    public void Update(GrimOwlTerrain terrain)
    {
        Terrain = terrain;

        SetTerrainColor();

        StringBuilder sb = new StringBuilder();

        sb.Append($"Terrain Nature: {terrain.Nature}\n");

        if (terrain.PermanentCard != null)
        {
            sb.Append($"Card: {CardModalView.BuildCardShortView(terrain.PermanentCard)}\n");
        }

        infoView.Text = sb.ToString();

    }

    public void SetTerrainColor()
    {
        var colorScheme2 = new ColorScheme() { Normal = new Terminal.Gui.Attribute(Color.White, Color.Red) };
        var colorScheme1 = new ColorScheme() { Normal = new Terminal.Gui.Attribute(Color.White, Color.BrightBlue) };
        var colorScheme3 = new ColorScheme() { Normal = new Terminal.Gui.Attribute(Color.White, Color.Green) };

        if (Terrain.PermanentCard != null)
        {
            if (Terrain.PermanentCard.Owner == GrimOwlConsoleGui.currentGameState.Player)
            {
                mainView.ColorScheme = colorScheme1;
            }
            else
            {
                mainView.ColorScheme = colorScheme2;
            }
        }
        else
        {
            mainView.ColorScheme = colorScheme3;
        }
    }
}
