using GrimOwlGameEngine;
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class GridView : FrameView
{

    public TerrainView?[,] terrains = null!;
    public int cols;
    public int rows;

    public GridView(string v) : base(v)
    {
        CanFocus = false;
    }

    public void Update(GrimOwlGrid grid)
    {

        this.cols = grid.Columns;
        this.rows = grid.Rows;

        this.terrains = null!;
        this.terrains = new TerrainView[this.cols, this.rows];

        Dim col = Dim.Percent(100 / this.cols);
        Dim row = Dim.Percent(100 / this.rows);

        for (int x = 0; x < this.cols; x++)
        {
            for (int y = 0; y < this.rows; y++)
            {

                this.terrains[x, y] = new TerrainView(x, y)
                {
                    X = Pos.Percent(x * 100 / this.cols),
                    Y = Pos.Percent(y * 100 / this.rows),
                    Width = col,
                    Height = row,
                    CanFocus = true,
                };

                this.terrains[x, y]!.Update(grid.Terrains[x,y]);

                this.Add(this.terrains[x, y]);
            }
        }
    }


}
