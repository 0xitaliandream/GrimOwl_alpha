using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GrimOwl;

public class StatKeys
{

    public const string Mana = "Mana";
    public const int ManaMax = 10;

    public const string ManaSpecial = "Mana Special";
    public const int ManaSpecialMax = 3;


    public const string Life = "Life";
    public const int LifeMax = 350;


    public const string Attack = "Attack";
    public const int AttackMax = 350;

    public const string Range = "Range";
    public const int RangeMax = 100;

    public const string Energy = "Energy";
    public const int EnergyMax = 100;

    public const string AvantGrade = "AvantGrade";
    public const string Strategist = "Strategist";

    public const string Wild = "Wild";
    public const string Arcana = "Arcana";
    public const string Blood = "Blood";
    public const string Feral = "Feral";
    public const string Fire = "Fire";
    public const string Holy = "Holy";
    public const string Necrotic = "Necrotic";
    public const string Plant = "Plant";
    public const string Spectral = "Spectral";
    public const string Steam = "Steam";
    public const string Void = "Void";
    public const string Water = "Water";
    public const string Wind = "Wind";

    public const string Defensive = "Defensive";
    public const string Offensive = "Offensive";
    public const string Inmaterial = "Inmaterial";
    public const string Material = "Material";
    public const string Wilderness = "Wilderness";

    public readonly static Dictionary<string, List<string>> strongAgainst = new Dictionary<string, List<string>> {
        { "Arcana", new List<string> { "Spectral", "Void" } },
        { "Blood", new List<string> { "Feral", "Spectral" } },
        { "Feral", new List<string> { "Plant", "Water" } },
        { "Fire", new List<string> { "Blood", "Plant" } },
        { "Holy", new List<string> { "Feral", "Necrotic" } },
        { "Necrotic", new List<string> { "Blood", "Arcana" } },
        { "Plant", new List<string> { "Water", "Wind" } },
        { "Spectral", new List<string> { "Holy", "Steam" } },
        { "Steam", new List<string> { "Void", "Holy" } },
        { "Void", new List<string> { "Arcana", "Holy", "Necrotic" } },
        { "Water", new List<string> { "Fire", "Wind" } },
        { "Wind", new List<string> { "Fire", "Steam" } },
        { "Wild", new List<string> { } }
    };
    // Dizionario delle nature che sono deboli contro altre nature
    public readonly static Dictionary<string, List<string>> weakAgainst = new Dictionary<string, List<string>> {
        { "Arcana", new List<string> { "Necrotic", "Void" } },
        { "Blood", new List<string> { "Fire", "Necrotic" } },
        { "Feral", new List<string> { "Holy", "Blood" } },
        { "Fire", new List<string> { "Water", "Wind" } },
        { "Holy", new List<string> { "Spectral", "Void" } },
        { "Necrotic", new List<string> { "Holy", "Void" } },
        { "Plant", new List<string> { "Feral", "Fire" } },
        { "Spectral", new List<string> { "Arcana", "Blood" } },
        { "Steam", new List<string> { "Spectral", "Wind" } },
        { "Void", new List<string> { "Arcana", "Steam" } },
        { "Water", new List<string> { "Feral", "Plant" } },
        { "Wind", new List<string> { "Plant", "Water" } },
        { "Wild", new List<string> { } }
    };

    // Dizionario per mappare le nature ai loro gruppi
    public readonly static Dictionary<string, string> natureToGroup = new Dictionary<string, string> {
        { "Arcana", "Material" },
        { "Blood", "Material" },
        { "Feral", "Offensive" },
        { "Fire", "Offensive" },
        { "Holy", "Defensive" },
        { "Necrotic", "Offensive" },
        { "Plant", "Defensive" },
        { "Spectral", "Inmaterial" },
        { "Steam", "Inmaterial" },
        { "Void", "Material" },
        { "Water", "Defensive" },
        { "Wind", "Inmaterial" },
        { "Wild", "Wilderness" }
    };
}
