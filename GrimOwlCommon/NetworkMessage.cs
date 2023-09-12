namespace GrimOwlCommon;

public enum MClient : int
{
    GameStateUpdate = 0,
    PlayerCommand,
}

public enum MServer : int
{
    GameStateUpdate = 0,
}


public class NetworkMessage
{
    public int Id { get; set; }
    public string Payload { get; set; } = null!;
}