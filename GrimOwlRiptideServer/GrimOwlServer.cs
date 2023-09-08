using GrimOwlGameEngine;

namespace GrimOwlRiptideServer;

public static class GrimOwlServer
{
    private static readonly ushort serverPort = 5000;
    public static int numberOfPlayer;
    public static int serverIdToken;

    internal static Riptide.Server server = new() { TimeoutTime = 5 * 1000 };
    internal static bool isServerRunning;
    private static Thread? serverThread;
    public static GrimOwlGame game = null!; 


    public static void Run(int serverIdTokenInit, int numberOfPlayerInit)
    {

        serverIdToken = serverIdTokenInit;
        numberOfPlayer = numberOfPlayerInit;

        Console.CancelKeyPress += delegate
        {
            Stop();
        };

        GrimOwlGameState grimOwlGameState = new GrimOwlGameState();
        game = new GrimOwlGame(grimOwlGameState);

        Console.WriteLine("ServerInstance created");

        server.ClientDisconnected += ConnectionHandler.OnClientDisconnected;
        server.HandleConnection += ConnectionHandler.HandleConnection;

        server.Start(serverPort, 2);

        isServerRunning = true;
        serverThread = new Thread(new ThreadStart(Tick));
        serverThread.Start();
    }

    private static void Tick()
    {
        if (server == null)
        {
            Console.WriteLine("Server is null");
            return;
        }
        while (isServerRunning)
        {
            server.Update();
            Thread.Sleep(10);
        }
        server.Stop();
    }

    public static void Stop()
    {

        isServerRunning = false;

        if (serverThread != null)
        {
            serverThread.Join();
        }
    }
}
