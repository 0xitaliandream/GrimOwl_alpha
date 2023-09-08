using Riptide;

namespace GrimOwlRiptideClient;

public static class GrimOwlClient
{
    internal static Client? client;
    internal static Thread? clientThread;
    internal static bool isRunning = true;

    internal static string playerJwtAuthToken = null!;
    internal static int serverIdToken;

    public static void Run(string playerJwtAuthTokenInit, int serverIdTokenInit)
    {
        Console.CancelKeyPress += delegate
        {
            Stop();
        };

        playerJwtAuthToken = playerJwtAuthTokenInit;
        serverIdToken = serverIdTokenInit;


        Console.WriteLine("GameClient created");

        client = new Client();

        Message msg_temp = Message.Create();
        msg_temp.AddString(playerJwtAuthTokenInit);
        msg_temp.AddInt(serverIdTokenInit);

        client.Connect("127.0.0.1:5000", message: msg_temp);
        client.Send(msg_temp);

        clientThread = new Thread(new ThreadStart(Tick));
        clientThread.Start();
    }


    public static void Tick()
    {
        while (isRunning)
        {
            client?.Update();
            Thread.Sleep(20);
        }
        client?.Disconnect();
    }

    public static void Stop()
    {
        isRunning = false;

        clientThread?.Join();
    }
}
