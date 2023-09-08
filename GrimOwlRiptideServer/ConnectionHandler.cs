using GameEngine;
using GrimOwlGameEngine;
using Riptide;

namespace GrimOwlRiptideServer;

public static class ConnectionHandler
{
    internal static readonly Dictionary<int, Connection> playerIdToConnection = new();
    internal static readonly Dictionary<int, GrimOwlPlayer> connectionsToGrimOwlPlayer = new();

    public static List<int> ConnectionIds()
    {
        List<int> connectionIds = new();
        foreach (KeyValuePair<int, Connection> connection in playerIdToConnection)
        {
            connectionIds.Add(connection.Value.Id);
        }

        return connectionIds;
    }

    public static int ConnectionToPlayerId(Connection connection)
    {
        foreach (KeyValuePair<int, Connection> connectionToPlayer in playerIdToConnection)
        {
            if (connectionToPlayer.Value == connection)
            {
                return connectionToPlayer.Key;
            }
        }

        return -1;
    }

    public static int GrimOwlPlayerToPlayerId(GrimOwlPlayer player)
    {
        foreach (KeyValuePair<int, GrimOwlPlayer> playerToConnection in connectionsToGrimOwlPlayer)
        {
            if (playerToConnection.Value == player)
            {
                return playerToConnection.Key;
            }
        }

        return -1;
    }

    public static void HandleConnection(Connection pendingConnection, Message connectMessage)
    {

        //If messagge is null, reject connection
        if (connectMessage == null)
        {
            Console.WriteLine($"Connection from {pendingConnection.Id} rejected: no message");
            GrimOwlServer.server.Reject(pendingConnection);
            return;
        }

        //If message is not null
        string playerJwtAuthToken = connectMessage.GetString();
        int serverIdToken = connectMessage.GetInt();

        if (serverIdToken != GrimOwlServer.serverIdToken)
        {
            Console.WriteLine($"Connection from {pendingConnection.Id} rejected: serverIdToken {serverIdToken} does not match");
            GrimOwlServer.server.Reject(pendingConnection);
            return;
        }

        

        Console.WriteLine($"Connection from {pendingConnection.Id} accepted: playerJwtAuthToken {playerJwtAuthToken}");

        ClientHello(pendingConnection, playerJwtAuthToken);

        Message message = MessageSenderHandler.GenerateMessage(new object[] { "ServerHello" }, (ushort)MConnection.ServerHello);
        MessageSenderHandler.SendMessageToConnection(pendingConnection, message);

    }

    public static void OnClientDisconnected(object? sender, ServerDisconnectedEventArgs e)
    {

        //Check if connection is in connectionsToPlayer
        if (!ConnectionIds().Contains(e.Client.Id))
        {
            Console.WriteLine($"Client {e.Client.Id} disconnected but was not connected");
            return;
        }

        //Get playerUid from connectionsToPlayer
        int playerId = ConnectionToPlayerId(e.Client);
        if (playerId == -1)
        {
            Console.WriteLine($"Client {e.Client.Id} disconnected but was not connected");
            return;
        }

        playerIdToConnection.Remove(playerId);

        Console.WriteLine($"Client {e.Client.Id} disconnected!");

    }

    private static void ClientHello(Connection pendingConnection, string playerAuthToken)
    {

        int playerId = int.Parse(playerAuthToken);

        //Check if a connection of player is already connected
        if (playerIdToConnection.ContainsKey(playerId))
        {
            GrimOwlServer.server.Reject(pendingConnection);
        }

        //Check len of playerIdToConnection is less than 2
        if (playerIdToConnection.Count > GrimOwlServer.numberOfPlayer)
        {
            GrimOwlServer.server.Reject(pendingConnection);
        }


        playerIdToConnection[playerId] = pendingConnection;

        GrimOwlServer.server.Accept(pendingConnection);

        GrimOwlPlayerHello(playerId);

    }

    private static void GrimOwlPlayerHello(int playerId)
    {
        //Check if a connection of player is already connected
        if (connectionsToGrimOwlPlayer.ContainsKey(playerId))
        {
            return;
        }


        GrimOwlPlayer player = new GrimOwlPlayer();
        connectionsToGrimOwlPlayer[playerId] = player;

        //Retrieve deck from database
        RetrieveDeckFromDatabase(playerId, player);

        GrimOwlServer.game.State.AddPlayer(player);

    }

    private static void RetrieveDeckFromDatabase(int playerId, GrimOwlPlayer player)
    {
        GrimOwlKingCard creatureCard = new MalikII();
        player.SetKing(creatureCard);

        ICardCollection deck = player.GetCardCollection(CardCollectionKeys.Deck);
        for (int n = 0; n < 3; ++n)
        {
            deck.Add(new WallGrappler());
        }
        deck.Shuffle();
    }
}
