using GrimOwlCommon;
using GrimOwlGameEngine;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace GrimOwlGameClient;

public class GameService
{
    private readonly WebSocket ws;
    public event Action<GrimOwlGameUpdatePlayerContext> OnGrimOwlGameStateUpdate = delegate { };

    public GameService()
    {
        ws = new WebSocket(Client.Host);

        ws.OnMessage += OnMessageReceived;
        ws.OnOpen += OnConnectionOpened;
        ws.OnError += OnErrorOccurred;
        ws.OnClose += OnConnectionClosed;
    }

    private void OnErrorOccurred(object? sender, WebSocketSharp.ErrorEventArgs e)
    {
        Console.WriteLine($"Errore: {e.Message}");
    }

    private void OnMessageReceived(object? sender, MessageEventArgs e)
    {
        HandleMessage(e.Data);
    }

    private void OnConnectionOpened(object? sender, EventArgs e)
    {
        GameUpdateRequest();
    }

    private void OnConnectionClosed(object? sender, CloseEventArgs e)
    {
        Console.WriteLine($"Connessione chiusa: {e.Reason}");
    }

    public void Connect()
    {
        ws.SetCookie(new Cookie("Authorization", $"Bearer {Client.Token}"));
        ws.Connect();
    }

    public void Disconnect()
    {
        ws.Close();
    }


    public void GameUpdateRequest()
    {
        NetworkMessage message = new NetworkMessage
        {
            Id = (int)MClient.GameStateUpdate,
            Payload = ""
        };

        string serializedGame = JsonSerializer.ToJson(message);

        ws.Send(serializedGame);
    }

    public void DeserializeGameState(string serializedGame)
    {
        Console.WriteLine(serializedGame);

        GrimOwlGameUpdatePlayerContext? game = JsonSerializer.FromJson<GrimOwlGameUpdatePlayerContext>(serializedGame);

        if (game == null)
        {
            Console.WriteLine($"Client sent invalid command");
            return;
        }

        OnGrimOwlGameStateUpdate.Invoke(game!);

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
            case (int)MServer.GameStateUpdate:
                DeserializeGameState(clientMessage.Payload);
                break;
            default:
                Console.WriteLine("Comando non riconosciuto");
                break;
        }

    }

    public void SendCommand(string command)
    {
        NetworkMessage message = new NetworkMessage
        {
            Id = (int)MClient.PlayerCommand,
            Payload = command
        };

        string serialized = JsonSerializer.ToJson(message);

        ws.Send(serialized);
    }
}
