using GrimOwlGameEngine;
using Riptide;

namespace GrimOwlRiptideServer;

public static class MessageSenderHandler
{

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
        }
        return msg_temp;
    }


}