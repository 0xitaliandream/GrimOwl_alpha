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

    public GameService()
    {
    }

    public GrimOwlPlayer ContextPlayer { get; set; } = null!;

    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"Received message from {ContextToken}: {e.Data}");
        HandleMessage(e.Data);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        ContextPlayer = (GrimOwlPlayer)DataHandler.Instance.Game.State.Players.ElementAt(ContextToken);
        DataHandler.Instance.Game.OnNewGameState += SendGameUpdateToPlayer;
    }


    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
        ContextPlayer = null!;
    }


    public void ProcessPlayerCommand(string payload)
    {
        bool newGameState = ContextPlayer.CommandController.HandleCommand(DataHandler.Instance.Game, payload);
        if (!newGameState)
        {
            Console.WriteLine($"Command Error");

            // send message to player
            Send("Command Error");
            return;
        }
    }


    public void SendGameUpdateToPlayer(List<IAction> actions)
    {
        Console.WriteLine($"Sending game update to {ContextToken}");

        GrimOwlGameUpdatePlayerContext gameUpdatePlayerContext = BuildGameUpdateForPlayer(actions);

        NetworkMessage message = new NetworkMessage
        {
            Id = (int)MClient.GameStateUpdate,
            Payload = JsonSerializer.ToJson(gameUpdatePlayerContext)
        };

        string serializedMessage = JsonSerializer.ToJson(message);


        Console.WriteLine($"Sending game update to {ContextToken}: {message.Payload}");

        Send(serializedMessage);
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
                SendGameUpdateToPlayer(new List<IAction>());
                break;
            case (int)MClient.PlayerCommand:
                ProcessPlayerCommand(clientMessage.Payload);
                break;
            default:
                Console.WriteLine("Comando non riconosciuto");
                break;
        }

    }

    public GrimOwlGameUpdatePlayerContext BuildGameUpdateForPlayer(List<IAction> actions)
    {
        return new GrimOwlGameUpdatePlayerContext(DataHandler.Instance.Game, ContextPlayer, actions);
    }


}