using GameEngine;
using GrimOwlGameEngine;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class CardModalView : Dialog
{
    private ICard Card = null!;

    ListView listView = null!;

    public CardModalView(ICard card) : base()
    {

        Card = card;

        Title = "Card Infos";
        Width = 50;
        Height = 30;

        var closeBtn = new Button("Close")
        {
            X = Pos.Center(),
            Y = Pos.Bottom(this) - 1,
        };
        closeBtn.Clicked += () => Application.RequestStop();

        AddButton(closeBtn);


        this.listView = new ListView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill() - 2, // Lascia spazio per il pulsante
            AllowsMarking = false,
            CanFocus = false,
        };

        this.Add(this.listView);

        List<string> cardInfos = BuildCardListView(Card);
        listView.SetSource(cardInfos);
    }

    public static List<string> BuildCardListView(ICard card)
    {
        Clipboard.Contents = card.UniqueId.ToString();

        List<string> list = new List<string>()
        {
            $"Name: {card.GetType().Name}",
            $"Mana: {card.GetValue(StatKeys.Mana):D2}",
            $"ManaSpecial: {card.GetValue(StatKeys.ManaSpecial):D2}",
            $"Attack: {card.GetValue(StatKeys.Attack):D2}",
            $"Life: {card.GetValue(StatKeys.Life):D2}",
            $"Energy: {card.GetValue(StatKeys.Energy):D2}",
            $"Range: {card.GetValue(StatKeys.Range):D2}",
            $"Id: {card.UniqueId}",
        };


        return list;
    }

    public static string BuildCardShortView(ICard card)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"{card.GetType().Name}");
        sb.Append(" ");
        sb.Append($"M: {card.GetValue(StatKeys.Mana):D2}");
        sb.Append(" ");
        sb.Append($"MS: {card.GetValue(StatKeys.ManaSpecial):D2}");
        sb.Append(" ");
        sb.Append($"A: {card.GetValue(StatKeys.Attack):D2}");
        sb.Append(" ");
        sb.Append($"L: {card.GetValue(StatKeys.Life):D2}");

        return sb.ToString();
    }

}
