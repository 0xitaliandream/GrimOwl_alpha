using System;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace GrimOwlGameClient
{
    public class Client
    {
        public GameService GameService { get; private set; }
        public static int Token { get; private set; }
        public static string Host { get; private set; } = null!;

        public Client(string host, int token)
        {
            Host = host;
            Token = token;

            // Crea le varie WebSocket per ciascun servizio
            this.GameService = new GameService();
            // this.ChatSocket = new ChatSocket($"{Host}/Chat", Token);
        }

        public void ConnectAll()
        {
            // Connette tutte le WebSockets
            GameService.Connect();
            // ChatSocket.Connect();
        }

        public void DisconnectAll()
        {
            // Disconnette tutte le WebSockets
            GameService.Disconnect();
            // ChatSocket.Disconnect();
        }

        // Aggiungi altri metodi utili qui
    }
}
