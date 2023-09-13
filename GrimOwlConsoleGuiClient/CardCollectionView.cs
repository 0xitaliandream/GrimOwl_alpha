using GameEngine;
using GrimOwlGameEngine;
using System.Numerics;
using System.Text;
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class CardCollectionView : FrameView
{

    private ICardCollection CardCollection = null!;
    ListView listView = null!;

    public CardCollectionView(string v) : base(v)
    {
        this.listView = new ListView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            AllowsMarking = false,
            CanFocus = true,
        };

        listView.OpenSelectedItem += OnSelectedChanged;

        this.Add(this.listView);
    }

    private void OnSelectedChanged(ListViewItemEventArgs args)
    {
        // Ottieni l'elemento selezionato
        ICard selectedItem = CardCollection[args.Item];

        Application.Run(new CardModalView(selectedItem));
    }

    public void Update(ICardCollection cardCollection)
    {

        CardCollection = cardCollection;

        List<string> list = new List<string>();

        for (int i = 0; i < cardCollection.Size; ++i)
        {
            ICard card = cardCollection[i];

            StringBuilder sb = new StringBuilder();

            sb.Append($"{i:D2} {card.GetType().Name}");
            sb.Append(" ");
            sb.Append($"M: {card.GetValue(StatKeys.Mana):D2}");
            sb.Append(" ");
            sb.Append($"MS: {card.GetValue(StatKeys.ManaSpecial):D2}");
            sb.Append(" ");
            sb.Append($"A: {card.GetValue(StatKeys.Attack):D2}");
            sb.Append(" ");
            sb.Append($"L: {card.GetValue(StatKeys.Life):D2}");
            list.Add(sb.ToString());
        }


        this.listView.SetSource(list);

    }

}
