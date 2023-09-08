using GrimOwlGameEngine;

namespace GrimOwlRiptideServer;

class MainClass
{
    public static void Main(string[] args)
    {
        Console.WriteLine("GrimOwlServer");

        GrimOwlServer.Run(1,2);
    }
}