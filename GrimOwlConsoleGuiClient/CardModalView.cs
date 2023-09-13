using GameEngine;
using GrimOwlGameEngine;
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

        BuildCardListView(Card,listView);
    }

    public static void BuildCardListView(ICard card, ListView listView)
    {
        List<string> list = new List<string>()
        {
            $"Name: {card.GetType().Name}",
            $"Mana: {card.GetValue(StatKeys.Mana):D2}",
            $"ManaSpecial: {card.GetValue(StatKeys.ManaSpecial):D2}",
            $"Attack: {card.GetValue(StatKeys.Attack):D2}",
            $"Life: {card.GetValue(StatKeys.Life):D2}",
            $"Energy: {card.GetValue(StatKeys.Energy):D2}",
            $"Range: {card.GetValue(StatKeys.Range):D2}",
        };


        listView.SetSource(list);
    }

}
