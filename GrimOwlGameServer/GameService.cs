using GameEngine;
using GrimOwlCommon;
using GrimOwlGameEngine;
using Newtonsoft.Json.Linq;
using System.Numerics;
using WebSocketSharp;
using WebSocketSharp.Server;
using static GrimOwlGameServer.Server;

namespace GrimOwlGameServer;

public class GameService : AuthenticatedWebSocketBehavior
{
    public static Dictionary<int, GrimOwlPlayer> tokenToGrimOwlPlayer = new Dictionary<int, GrimOwlPlayer>();
    public GrimOwlGame game = null!;

    public GrimOwlPlayer ContextPlayer
    {
        get
        {
            return tokenToGrimOwlPlayer[ContextToken];
        }
    }

    public string GetPlayerSession(GrimOwlPlayer player)
    {
        int token = tokenToGrimOwlPlayer.FirstOrDefault(x => x.Value == player).Key;
        return GetTokenSession(token);
    }

    public GameService()
    {
        game = TestScenarios.TestScenario4();
        game.OnNewGameState += SendGameUpdateToPlayers;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        if (!ContextIsAuthenticated)
        {
            Console.WriteLine($"Received message from {ContextToken}: {e.Data}");
            return;
        }
        Console.WriteLine($"Received message from {ContextToken}: {e.Data}");

        HandleMessage(e.Data);
    }

    protected override void OnOpen()
    {
        base.OnOpen();

        if (ContextIsAuthenticated)
        {
            tokenToGrimOwlPlayer[ContextToken] = (GrimOwlPlayer)game.State.Players.ElementAt(ContextToken);

        }
    }


    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);

        if (ContextIsAuthenticated)
        {
            tokenToGrimOwlPlayer.Remove(ContextToken);
        }
    }


    public void ProcessPlayerCommand(NetworkMessage clientMessage)
    {

        GrimOwlPlayerCommand? playerCommand = JsonSerializer.FromJson<GrimOwlPlayerCommand>(clientMessage.Payload);

        if (playerCommand == null)
        {
            Console.WriteLine($"Player {ContextPlayer} sent invalid command");
            Send("Command Error");
            return;
        }

        if (playerCommand.Player != ContextPlayer)
        {
            Console.WriteLine($"Player {playerCommand.Player} is not {ContextPlayer}");
            return;
        }



        bool newGameState = ContextPlayer.CommandController.HandleCommand(game, "ok");
        if (!newGameState)
        {
            Console.WriteLine($"Command Error");

            // send message to player
            Send("Command Error");
            return;
        }
    }

    public void SendGameUpdateToPlayers(List<IAction> actions)
    {
        Console.WriteLine($"Sending game update to players");

        foreach (KeyValuePair<int, GrimOwlPlayer> entry in tokenToGrimOwlPlayer)
        {
            SendGameUpdateToPlayer(entry.Value, actions);
        }
    }


    public void SendGameUpdateToPlayer(GrimOwlPlayer player, List<IAction> actions)
    {
        Console.WriteLine($"Sending game update to {player}");

        GrimOwlGameUpdatePlayerContext gameUpdatePlayerContext = BuildGameUpdateForPlayer(player, actions);

        NetworkMessage message = new NetworkMessage
        {
            Id = (int)MClient.GameStateUpdate,
            Payload = JsonSerializer.ToJson(gameUpdatePlayerContext)
        };

        string serializedMessage = JsonSerializer.ToJson(message);

        string session = GetPlayerSession(player);

        Sessions.SendTo(serializedMessage, session);
    }

    public void HandleMessage(string message)
    {
        NetworkMessage? clientMessage = JsonSerializer.FromJson<NetworkMessage>(message);

        if (clientMessage == null)
        {
            Console.WriteLine($"Client sent invalid command");
        }

        switch (clientMessage!.Id)
        {
            case (int)MClient.GameStateUpdate:
                SendGameUpdateToPlayer(ContextPlayer, new List<IAction>());
                break;
            case (int)MClient.PlayerCommand:
                ProcessPlayerCommand(clientMessage);
                break;
            default:
                Console.WriteLine("Comando non riconosciuto");
                break;
        }

    }


    public GrimOwlGameUpdatePlayerContext BuildGameUpdateForPlayer(GrimOwlPlayer player, List<IAction> actions)
    {
        return new GrimOwlGameUpdatePlayerContext(game, player, actions);
    }


}