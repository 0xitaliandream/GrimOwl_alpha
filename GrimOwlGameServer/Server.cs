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
        Console.WriteLine("GrimOwl Server started");
    }

    public void Stop()
    {
        webSocketServer.Stop();
    }
}


public class AuthenticatedWebSocketBehavior : WebSocketBehavior
{
    public int ContextToken { get; set; }

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

        int token = int.Parse(authCookie.Substring("Bearer ".Length).Trim() ?? "-1");

        Console.WriteLine($"Token: {token}");

        //CHECK TOKEN IS VALID AND NOT EXPIRED

        if (token == -1)
        {
            this.Context.WebSocket.Close();
            return;
        }

        ContextToken = token;
    }

    protected override void OnClose(CloseEventArgs e)
    {
        Console.WriteLine("WebSocketClient connection closed");
        ContextToken = -1;
    }
}