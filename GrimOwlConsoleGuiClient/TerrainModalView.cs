using GameEngine;
using GrimOwlGameEngine;
using System.Numerics;
using System.Text;
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class TerrainModalView : Dialog
{
    GrimOwlTerrain Terrain = null!;
    ListView listView = null!;

    public TerrainModalView(GrimOwlTerrain terrain) : base()
    {

        Terrain = terrain;

        Title = "Terrain Infos";
        Width = 50;
        Height = 30;

        var closeBtn = new Button("Close")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(this) - 1,
        };
        closeBtn.Clicked += () => Application.RequestStop();

        AddButton(closeBtn);


        this.listView = new ListView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill() - 2, // Lascia spazio per il pulsante
            AllowsMarking = false,
            CanFocus = false,
        };

        this.Add(this.listView);

        List<string> terrainInfos = BuildTerrainListView(Terrain);
        terrainInfos.Add(" ");
        List<string> cardInfos = new List<string>();
        if (terrain.PermanentCard != null)
        {
            terrainInfos.Add("Permanent:");
            cardInfos = CardModalView.BuildCardListView(terrain.PermanentCard);
        }

        
        terrainInfos.AddRange(cardInfos);

        listView.SetSource(terrainInfos);

    }

    public static List<string> BuildTerrainListView(GrimOwlTerrain terrain)
    {

        List<string> list = new List<string>()
        {
            $"Nature: {terrain.Nature}",
            $"Position: {terrain.X} {terrain.Y}",
        };


        return list;
    }

}
