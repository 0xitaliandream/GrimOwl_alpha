using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class CardCollectionView : FrameView
{

    ListView listView = null!;

    public CardCollectionView(string v) : base(v)
    {
    }

    public void Initialize()
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

    public void UpdateMyHandView()
    {
        
    }

}
