using GrimOwlGameEngine;
using GrimOwlGameServer;
using WebSocketSharp.Server;

class MainClass
{
    public static void Main(string[] args)
    {
        Server server = new Server();

        server.Start();



        System.Console.WriteLine("Press any key to exit...");
        System.Console.ReadKey(true);

        server.Stop();
    }
}