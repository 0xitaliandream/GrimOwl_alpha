using GameEngine;
using Newtonsoft.Json;
using System;

namespace GrimOwl;

public class GrimOwlCard : Card
{

    protected GrimOwlCard()
    {
    }

    public GrimOwlCard(int mana, int manaSpecial, List<string> natures) : base(true)
    {
        AddComponent(new GrimOwlManasCardComponent(mana, manaSpecial));

        AddComponent(new GrimOwlNaturesCardComponent(natures));
    }

    [JsonIgnore]
    public List<string> Natures
    {
        get
        {
            List<string> natures = new List<string>();
            CardComponent cardComponent = GetComponent<GrimOwlNaturesCardComponent>() ?? null!;

            if (cardComponent != null)
            {
                foreach (KeyValuePair<string, IList<IStat>> nature in cardComponent.Stats)
                {
                    if (this.GetValue(nature.Key) > 0)
                    {
                        natures.Add(nature.Key);
                    }
                }
            }

            return natures;

        }
    }

    public T? GetComponent<T>() where T : CardComponent
    {
        return this.Components.FirstOrDefault(c => c is T) as T;
    }

}
