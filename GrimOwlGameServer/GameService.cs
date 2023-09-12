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

    public GrimOwlPlayer Player
    {
        get
        {
            return tokenToGrimOwlPlayer[Token];
        }
    }

    public GameService()
    {
        game = TestScenarios.TestScenario4();
        game.OnNewGameState += SendGameUpdate;
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        if (!IsAuthenticated)
        {
            Console.WriteLine($"Received message from {Token}: {e.Data}");
            return;
        }
        Console.WriteLine($"Received message from {Token}: {e.Data}");

        HandleMessage(e.Data);
    }

    protected override void OnOpen()
    {
        base.OnOpen();

        if (IsAuthenticated)
        {
            tokenToGrimOwlPlayer[Token] = (GrimOwlPlayer)game.State.Players.ElementAt(Token);

        }
    }


    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);

        if (IsAuthenticated)
        {
            tokenToGrimOwlPlayer.Remove(Token);
        }
    }


    public void ProcessPlayerCommand(NetworkMessage clientMessage)
    {

        GrimOwlPlayerCommand? playerCommand = JsonSerializer.FromJson<GrimOwlPlayerCommand>(clientMessage.Payload);

        if (playerCommand == null)
        {
            Console.WriteLine($"Player {Player} sent invalid command");
            Send("Command Error");
            return;
        }

        if (playerCommand.Player != Player)
        {
            Console.WriteLine($"Player {playerCommand.Player} is not {Player}");
            return;
        }



        bool newGameState = Player.CommandController.HandleCommand(game, "ok");
        if (!newGameState)
        {
            Console.WriteLine($"Command Error");

            // send message to player
            Send("Command Error");
            return;
        }
    }

    public void SendGameUpdate()
    {
        Console.WriteLine($"Sending game update to {Token}");

        NetworkMessage message = new NetworkMessage
        {
            Id = (int)MClient.GameStateUpdate,
            Payload = JsonSerializer.ToJson(game)
    };

        string serializedMessage = JsonSerializer.ToJson(message);

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
                SendGameUpdate();
                break;
            case (int)MClient.PlayerCommand:
                ProcessPlayerCommand(clientMessage);
                break;
            default:
                Console.WriteLine("Comando non riconosciuto");
                break;
        }

    }


}