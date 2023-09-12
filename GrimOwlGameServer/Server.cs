using GrimOwlGameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GrimOwlGameServer;

public class Server
{
    private readonly WebSocketServer webSocketServer = null!;

    public Server()
    {
        webSocketServer = new WebSocketServer(8080);
        webSocketServer.AddWebSocketService<GameService>("/GameService");
        // webSocketServer.AddWebSocketService<GameService>("/Chat");
    }

    public void Start()
    {
        webSocketServer.Start();
    }

    public void Stop()
    {
        webSocketServer.Stop();
    }
}


public class AuthenticatedWebSocketBehavior : WebSocketBehavior
{
    public static Dictionary<string, int> sessionToToken = new Dictionary<string, int>();

    public bool IsAuthenticated
    {
        get
        {
            return sessionToToken.ContainsKey(this.ID);
        }
    }

    public int Token
    {
        get
        {
            return sessionToToken[this.ID];
        }
    }

    protected override void OnOpen()
    {
        Console.WriteLine("WebSocketClient connection opened");

        string? authCookie = this.Context.CookieCollection["Authorization"]?.Value;

        if (authCookie == null)
        {
            Console.WriteLine("No Authorization cookie found");
            this.Context.WebSocket.Close();
            return;
        }

        int token = int.Parse(authCookie.Substring("Bearer ".Length).Trim() ?? "0");

        Console.WriteLine($"Token: {token}");

        if (token == 0)
        {
            this.Context.WebSocket.Close();
            return;
        }

        // Salva la sessione del giocatore
        string sessionId = this.ID;
        sessionToToken[sessionId] = token;
    }

    protected override void OnClose(CloseEventArgs e)
    {
        // Gestisci la chiusura della connessione, ad esempio rimuovendo il giocatore dai dizionari

        Console.WriteLine("WebSocketClient connection closed");

        if (sessionToToken.ContainsKey(this.ID))
        {
            sessionToToken.Remove(this.ID);
        }
    }
}