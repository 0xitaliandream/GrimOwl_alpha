using GameEngine;
using GrimOwlGameEngine;
using Riptide;
using System.Diagnostics;
using GrimOwlCommon;


namespace GrimOwlRiptideServer;

public static class MessageSenderHandler
{


    public static void SendGameUpdate()
    {
        Console.WriteLine("SendGameUpdate");

        Dictionary<GrimOwlPlayer, string> playerToBytes = JsonSerializer.SerializeGame(GrimOwlServer.game);
        
        foreach (KeyValuePair<GrimOwlPlayer, string> playerToByte in playerToBytes)
        {
            Message message = GenerateMessage(new object[] { playerToByte.Value }, (ushort)MServer.GameUpdate);
            SendMessageToPlayer(playerToByte.Key, message);
        }
    }


    public static void SendMessageToConnection(Connection connection, Message message)
    {
        GrimOwlServer.server.Send(message, connection.Id);
    }

    public static void BroadcastMessage(Message message)
    {
        foreach (KeyValuePair<int, Connection> connection in ConnectionHandler.playerIdToConnection)
        {
            SendMessageToConnection(connection.Value, message);
        }
    }

    public static void SendMessageToPlayer(GrimOwlPlayer player, Message message)
    {


        int playerId = ConnectionHandler.GrimOwlPlayerToPlayerId(player);
        if (playerId == -1)
        {
            Console.WriteLine($"SendMessageToPlayer: Player {playerId} is not in connectionsToGrimOwlPlayer");
            return;
        }

        //Check if playerUid is in playersToLobby
        if (!ConnectionHandler.playerIdToConnection.ContainsKey(playerId))
        {
            Console.WriteLine($"SendMessageToPlayer: Player {playerId} is not in playersToConnection");
            return;
        }

        Console.WriteLine($"Sending message to player {playerId}");

        Connection connection = ConnectionHandler.playerIdToConnection[playerId];
        SendMessageToConnection(connection, message);
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


}


public static class MessageReceiverHandler
{

    [MessageHandler((ushort)MClient.ClientCommand)]
    private static void ClientCommand(ushort fromClientId, Message message)
    {

        Console.WriteLine("ClientCommand received");

        bool status = GrimOwlServer.server.TryGetClient(fromClientId, out Connection client);
        if (!status)
        {
            Console.WriteLine($"Client {fromClientId} sent ClientCommand but was not connected");
            return;
        }

        //Check if connection is in connectionsToPlayer
        if (!ConnectionHandler.ConnectionIds().Contains(client.Id))
        {
            Console.WriteLine($"Client {client.Id} not valid");
            return;
        }

        //Get playerUid from connectionsToPlayer
        int playerId = ConnectionHandler.ConnectionToPlayerId(client);
        if (playerId == -1)
        {
            Console.WriteLine($"Client {client.Id} not valid");
            return;
        }

        GrimOwlPlayer player = ConnectionHandler.connectionsToGrimOwlPlayer[playerId];

        // deserialize message

        bool newGameState = player.CommandController.HandleCommand(GrimOwlServer.game, "deserializedMassage");
        if (!newGameState)
        {
            Console.WriteLine($"Command Error");

            // send message to player
            Message messageTemp = MessageSenderHandler.GenerateMessage(new object[] { "CommandError" }, (ushort)MServer.CommandError);
            MessageSenderHandler.SendMessageToPlayer(player, messageTemp);

            return;
        }

    }

}
