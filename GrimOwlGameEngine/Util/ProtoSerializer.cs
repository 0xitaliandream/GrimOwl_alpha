

using ProtoBuf;
using ProtoBuf.Meta;
using System;

namespace GrimOwlGameEngine;


public static class ProtoSerializer
{
    public static void WriteToFile(string filePath, byte[] bytes)
    {
        // Scrive l'array di byte in un file.
        File.WriteAllBytes(filePath, bytes);
    }

    public static byte[] SerializeProtoUpdateGrimOwlGameState(GrimOwlGame game)
    {

        ProtoUpdateGrimOwlGame protoUpdateGrimOwlGame = GrimOwlGameToProtoUpdateGrimOwlGame(game);

        using (MemoryStream ms = new MemoryStream())
        {
            Serializer.Serialize(ms, protoUpdateGrimOwlGame);
            return ms.ToArray();
        }
    }

    public static ProtoUpdateGrimOwlGame DeserializeProtoUpdateGrimOwlGameState(byte[] data)
    {
        using (MemoryStream ms = new MemoryStream(data))
        {
            return Serializer.Deserialize<ProtoUpdateGrimOwlGame>(ms);
        }
    }


    public static ProtoUpdateGrimOwlGame GrimOwlGameToProtoUpdateGrimOwlGame(GrimOwlGame game)
    {
        return new ProtoUpdateGrimOwlGame(game);
    }


}
