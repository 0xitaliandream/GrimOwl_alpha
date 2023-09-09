

using GameEngine;
using ProtoBuf;

namespace GrimOwlGameEngine;

public class GrimOwlGame : Game<GrimOwlGameState>
{

    public int test { get; set; } = 3;


    protected GrimOwlGame()
    {
    }

    public GrimOwlGame(GrimOwlGameState gameState) : base(gameState)
    {
    }

    public void NextTurn()
    {
        Execute(new NextTurnAction());
    }
}
