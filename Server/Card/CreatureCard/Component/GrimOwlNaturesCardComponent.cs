using GameEngine;

namespace GrimOwl;

public class GrimOwlNaturesCardComponent : CardComponent
{
    protected GrimOwlNaturesCardComponent() { }

    public GrimOwlNaturesCardComponent(List<string> natures)
        : base(true)
    {
        foreach (var nature in natures)
        {
            AddStat(nature, new Stat(1, 1));
        }
    }

}

public static class GrimOwlNatureGroups
{
    public static CardComponent GetNatureModificatorActions(GrimOwlGame game, GrimOwlCreatureCard creatureCard, GrimOwlTerrain terrain)
    {
        List<string> cardNatures = creatureCard.Natures;
        string terrainNature = terrain.Nature;

        int adiacentTerrainSameNatureCount = game.State.Grid.GetAdiacentTerrainSameNatureCount(terrain.X, terrain.Y, terrainNature);

        string terrainGroup = StatKeys.natureToGroup[terrainNature];

        CardComponent component = new CardComponent();
        foreach (string cardNature in cardNatures)
        {
            int sign = 0;
            if (cardNature == terrainNature)
            {
                sign = 1;
            }
            else if (StatKeys.weakAgainst.ContainsKey(cardNature) && StatKeys.weakAgainst[cardNature].Contains(terrainNature))
            {
                sign = -1;
            }

            if (sign != 0)
            {
                int valueDelta = 0;
                int baseValueDelta = 0;
                switch (terrainGroup)
                {
                    case StatKeys.Offensive:

                        switch (adiacentTerrainSameNatureCount)
                        {
                            case int n when (adiacentTerrainSameNatureCount >= 1 && adiacentTerrainSameNatureCount <= 2):
                                valueDelta = 1;
                                baseValueDelta = 1;
                                break;
                            case int n when (adiacentTerrainSameNatureCount >= 3 && adiacentTerrainSameNatureCount <= 5):
                                valueDelta = 2;
                                baseValueDelta = 2;
                                break;
                            case int n when (adiacentTerrainSameNatureCount >= 6):
                                valueDelta = 4;
                                baseValueDelta = 4;
                                break;
                        }
                        component.AddStat(StatKeys.Attack,new Stat(sign * valueDelta, sign * baseValueDelta));
                        break;
                    case StatKeys.Defensive:
                        switch (adiacentTerrainSameNatureCount)
                        {
                            case int n when (n >= 1 && n <= 2):
                                valueDelta = 1;
                                break;
                            case int n when (n >= 3 && n <= 5):
                                valueDelta = 2;
                                break;
                            case int n when (n >= 6):
                                valueDelta = 4;
                                break;
                        }
                        component.AddStat(StatKeys.Life, new Stat(sign * valueDelta, 0));
                        break;
                    case StatKeys.Inmaterial:
                        switch (adiacentTerrainSameNatureCount)
                        {
                            case int n when (adiacentTerrainSameNatureCount >= 1 && adiacentTerrainSameNatureCount <= 2):
                                valueDelta = 1;
                                baseValueDelta = 1;
                                break;
                            case int n when (adiacentTerrainSameNatureCount >= 3 && adiacentTerrainSameNatureCount <= 5):
                                valueDelta = 2;
                                baseValueDelta = 2;
                                break;
                            case int n when (adiacentTerrainSameNatureCount >= 6):
                                valueDelta = 4;
                                baseValueDelta = 4;
                                break;
                        }
                        component.AddStat(StatKeys.Life, new Stat(sign * valueDelta, sign * baseValueDelta));
                        break;
                    case StatKeys.Material:
                        switch (adiacentTerrainSameNatureCount)
                        {
                            case int n when (adiacentTerrainSameNatureCount >= 1 && adiacentTerrainSameNatureCount <= 2):
                                valueDelta = 1;
                                baseValueDelta = 1;
                                break;
                            case int n when (adiacentTerrainSameNatureCount >= 3 && adiacentTerrainSameNatureCount <= 6):
                                valueDelta = 2;
                                baseValueDelta = 2;
                                break;
                            case int n when (adiacentTerrainSameNatureCount >= 7):
                                valueDelta = 3;
                                baseValueDelta = 3;
                                break;
                        }
                        component.AddStat(StatKeys.Attack, new Stat(sign * valueDelta, sign * baseValueDelta));
                        component.AddStat(StatKeys.Life, new Stat(sign * valueDelta, sign * baseValueDelta));
                        break;
                    default:
                        break;
                }
            }

        }

        return component;
        
    }
}
