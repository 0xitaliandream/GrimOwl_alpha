using Newtonsoft.Json;

namespace  GrimOwlGameEngine;

public static class JsonSerializer
{
    private static JsonSerializerSettings serializaterSettings = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        ObjectCreationHandling = ObjectCreationHandling.Auto
    };

    public static string ToJson(object? obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented, serializaterSettings);
    }

    public static T? FromJson<T>(string json) where T : class
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json, serializaterSettings);
        }
        catch (JsonException)
        {
            // Log dell'errore, se necessario
            return null;
        }
    }

    public static Dictionary<GrimOwlPlayer,string> SerializeGame(GrimOwlGame game)
    {
        Dictionary<GrimOwlPlayer, string> serializedGame = new Dictionary<GrimOwlPlayer, string>();

        foreach (GrimOwlPlayer player in game.State.Players)
        {
            serializedGame.Add(player, ToJson(game));
        }

        return serializedGame;
    }
}
