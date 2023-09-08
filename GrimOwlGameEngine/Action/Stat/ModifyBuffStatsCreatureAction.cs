using Newtonsoft.Json;
using GameEngine;
using System.Collections.Generic;

namespace GrimOwlGameEngine;

public class ModifyBuffStatsCreatureAction : GameEngine.Action<GrimOwlGameState>
{
    [JsonProperty]
    protected GrimOwlCreatureCard creatureCard = null!;

    [JsonProperty]
    protected IStatContainer statContainer = null!;

    protected ModifyBuffStatsCreatureAction() { }

    public ModifyBuffStatsCreatureAction(
        GrimOwlCreatureCard creatureCard,
        IStatContainer statContainer,
        bool isAborted = false
        ) : base(isAborted)
    {
        this.creatureCard = creatureCard;
        this.statContainer = statContainer;
    }

    [JsonIgnore]
    public IStatContainer StatContainer
    {
        get => statContainer;
    }

    [JsonIgnore]
    public GrimOwlCreatureCard CreatureCard
    {
        get => creatureCard;
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return true;
    }

    public override void Execute(IGame<GrimOwlGameState> game)
    {

        CardComponent buffComponent = CreatureCard.GetComponent<GrimOwlCreatureBuffStatsStatsCardComponent>()!;

        List<IAction> actions = new List<IAction>();

        foreach(KeyValuePair<string, IList<IStat>> stat in StatContainer.Stats)
        {
            switch (stat.Key)
            {
                case StatKeys.Attack:
                    foreach (IStat s in stat.Value)
                    {
                        actions.Add(new ModifyAttackStatAction(buffComponent, s.Value, s.BaseValue));
                    }
                    break;
                case StatKeys.Life:
                    foreach (IStat s in stat.Value)
                    {
                        actions.Add(new ModifyLifeStatAction(buffComponent, s.Value, s.BaseValue));
                    }
                    break;
                case StatKeys.Mana:
                    foreach (IStat s in stat.Value)
                    {
                        actions.Add(new ModifyManaStatAction(buffComponent, s.Value, s.BaseValue));
                    }
                    break;
                case StatKeys.ManaSpecial:
                    foreach (IStat s in stat.Value)
                    {
                        actions.Add(new ModifyManaSpecialStatAction(buffComponent, s.Value, s.BaseValue));
                    }
                    break;
                default:
                    break;
            }
        }

        game.ExecuteSimultaneously(actions);
    }
}
