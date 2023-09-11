using GrimOwlGameEngine;
using Riptide;
using GrimOwlCommon;
using ProtoBuf;
using System.Diagnostics;
using GameEngine;

namespace GrimOwlRiptideClient;

public class GrimOwlClient
{
    public static GrimOwlClient Instance { get; private set; } = null!;

    public Client client = null!;
    internal Thread clientThread = null!;
    internal bool isRunning = true;

    internal string playerJwtAuthToken = null!;
    internal int serverIdToken;

    public event System.Action<GrimOwlGame> OnNewGameUpdate = delegate { };

    public GrimOwlClient(string playerJwtAuthTokenInit, int serverIdTokenInit)
    {
        playerJwtAuthToken = playerJwtAuthTokenInit;
        serverIdToken = serverIdTokenInit;

        Instance = this;
    }

    public void Run()
    {
        Console.CancelKeyPress += delegate
        {
            Stop();
        };

        Console.WriteLine("GameClient created");

        client = new Client();

        Message msg_temp = Message.Create();
        msg_temp.AddString(playerJwtAuthToken);
        msg_temp.AddInt(serverIdToken);

        client.Connect("127.0.0.1:5000", message: msg_temp);
        client.Send(msg_temp);

        clientThread = new Thread(new ThreadStart(Tick));
        clientThread.Start();
    }


    public void Tick()
    {
        while (isRunning)
        {
            client?.Update();
            Thread.Sleep(20);
        }
        client?.Disconnect();
    }

    public void Stop()
    {
        isRunning = false;

        clientThread?.Join();
    }

    [MessageHandler((ushort)MServer.GameUpdate)]
    private static void GameState(Message message)
    {
        Console.WriteLine("Message received");

        string body = message.GetString();

        Console.WriteLine(body);

        GrimOwlGame game2 = JsonSerializer.FromJson<GrimOwlGame>(body)!;

        Instance.OnNewGameUpdate?.Invoke(game2);
    }

    public static Message GenerateMessage(object[] messages, ushort messageKey)
    {
        Message msg_temp = Message.Create(MessageSendMode.Reliable, messageKey);
        foreach (object msg in messages)
        {
            if (msg is int intValue)
            {
                msg_temp.AddInt(intValue);
            }
            else if (msg is ushort ushortValue)
            {
                msg_temp.AddUShort(ushortValue);
            }
            else if (msg is string strValue)
            {
                msg_temp.AddString(strValue);
            }
            else if (msg is byte[])
            {
                msg_temp.AddBytes((byte[])msg);
            }
        }
        return msg_temp;
    }

    public void SendMessageToServer(Message message)
    {
        client.Send(message);
    }

    public void OnNewCommand(string command)
    {
        Message message = GenerateMessage(new object[] { command }, (ushort)MClient.ClientCommand);

        SendMessageToServer(message);
    }
}
