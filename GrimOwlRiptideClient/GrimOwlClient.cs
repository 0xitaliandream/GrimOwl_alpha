using GrimOwlGameEngine;
using Riptide;

namespace GrimOwlRiptideClient;

public class GrimOwlClient
{
    public static GrimOwlClient Instance { get; private set; } = null!;

    internal Client? client;
    internal Thread? clientThread;
    internal bool isRunning = true;

    internal string playerJwtAuthToken = null!;
    internal int serverIdToken;

    public event Action<string> OnNewGameUpdate = delegate { };

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

    [MessageHandler((ushort)MServer.ServerHello)]
    private static void GameState(Message message)
    {
        Console.WriteLine("Message received");

        string test = message.GetString();

        Instance.OnNewGameUpdate?.Invoke(test);
    }
}
