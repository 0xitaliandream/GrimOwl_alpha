using ProtoBuf;
using System;

namespace GrimOwlGameEngine;

class MainClass
{
    public static void Main(string[] args)
    {
        GrimOwlGame game = TestScenarios.TestScenario2();

        byte[] serializedBytes = ProtoSerializer.SerializeProtoUpdateGrimOwlGameState(game);

        ProtoUpdateGrimOwlGame deserializedGame = ProtoSerializer.DeserializeProtoUpdateGrimOwlGameState(serializedBytes);

        Console.WriteLine(deserializedGame.test);
    }
}