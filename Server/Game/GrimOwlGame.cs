

using GameEngine;

namespace GrimOwl;

public class GrimOwlGame : Game<GrimOwlGameState>
{
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
