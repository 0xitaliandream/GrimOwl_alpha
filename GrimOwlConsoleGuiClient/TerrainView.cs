
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class TerrainView : FrameView
{
    public int x;
    public int y;
    View mainView;
    TextView cardView = null!;

    public TerrainView(int x, int y) : base($"{x},{y}")
    {
        this.x = x;
        this.y = y;

        this.mainView = new View()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            CanFocus = false,
        };


        this.Add(this.mainView);
    }
}
