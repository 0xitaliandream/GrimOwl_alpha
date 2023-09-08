using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrimOwlGameEngine;

public static class TestScenarios
{
    public static GrimOwlGame TestScenario2()
    {
        GrimOwlGameState gameState = new GrimOwlGameState();
        for (int i = 0; i < 2; ++i)
        {
            IPlayer player = new GrimOwlPlayer();

            GrimOwlKingCard creatureCard = new MalikII();
            ((GrimOwlPlayer)player).SetKing(creatureCard);

            ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);
            for (int n = 0; n < 0; ++n)
            {
                deck.Add(new WallGrappler());
            }
            deck.Shuffle();

            gameState.AddPlayer(player);
        }

        GrimOwlGrid grimOwlGrid = new GrimOwlGrid(2, 1);

        gameState.AddGrid(grimOwlGrid);

        gameState.Grid[0, 0]!.SetNature(StatKeys.Feral);


        GrimOwlGame game = new GrimOwlGame(gameState);
        game.NextTurn();
        gameState.ActivePlayer.SpawnCreature(game, gameState.ActivePlayer.King, 0, 0);

        return game;
    }

    public static GrimOwlGame TestScenario1()
    {
        GrimOwlGameState gameState = new GrimOwlGameState();
        for (int i = 0; i < 2; ++i)
        {
            IPlayer player = new GrimOwlPlayer();

            GrimOwlKingCard creatureCard = new MalikII();
            ((GrimOwlPlayer)player).SetKing(creatureCard);

            ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);
            for (int n = 0; n < 3; ++n)
            {
                deck.Add(new WallGrappler());
            }
            deck.Shuffle();

            gameState.AddPlayer(player);
        }

        GrimOwlGrid grimOwlGrid = new GrimOwlGrid(5, 1);

        gameState.AddGrid(grimOwlGrid);

        GrimOwlGame game = new GrimOwlGame(gameState);

        for (int i = 0; i < 2; ++i)
        {
            GrimOwlPlayer player = (GrimOwlPlayer)gameState.Players.ElementAt(i);
            if (i == 0)
            {
                player.SpawnCreature(game, player.King, 0, 0);
            }
            else
            {
                player.SpawnCreature(game, player.King, 4, 0);
            }
        }


        foreach (GrimOwlPlayer player in gameState.Players)
        {
            for (int i = 0; i < 3; ++i)
            {
                player.DrawCard(game);
            }
        }




        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();

        GrimOwlPlayer activePlayer = game.State.ActivePlayer;

        activePlayer.SummonCreature(game, (GrimOwlCreatureCard)activePlayer.GetCardCollection(CardCollectionKeys.Hand).First, 1, 0);
        activePlayer.MoveCreature(game, (GrimOwlCreatureCard)game.State.Grid[1, 0]!.PermanentCard, 2, 0);

        return game;
    }

    public static GrimOwlGame TestScenario3()
    {
        GrimOwlGameState gameState = new GrimOwlGameState();
        for (int i = 0; i < 2; ++i)
        {
            IPlayer player = new GrimOwlPlayer();

            ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);
            for (int n = 0; n < 3; ++n)
            {
                deck.Add(new WallGrappler());
            }
            deck.Shuffle();

            gameState.AddPlayer(player);
        }

        GrimOwlGrid grimOwlGrid = new GrimOwlGrid(3, 1);

        gameState.AddGrid(grimOwlGrid);

        GrimOwlGame game = new GrimOwlGame(gameState);

        foreach (GrimOwlPlayer player in gameState.Players)
        {
            for (int i = 0; i < 3; ++i)
            {
                player.DrawCard(game);
            }
        }

        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();
        game.NextTurn();

        GrimOwlPlayer activePlayer = game.State.ActivePlayer;

        game.State.ActivePlayer.SummonCreature(game, (GrimOwlCreatureCard)activePlayer.GetCardCollection(CardCollectionKeys.Hand).First, 0, 0);
        game.NextTurn();

        activePlayer = game.State.ActivePlayer;
        game.State.ActivePlayer.SummonCreature(game, (GrimOwlCreatureCard)activePlayer.GetCardCollection(CardCollectionKeys.Hand).First, 2, 0);

        return game;
    }
}
