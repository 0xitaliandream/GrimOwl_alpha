using GameEngine;
using GrimOwlGameEngine;
using System.Numerics;
using System.Text;
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class CardCollectionView : ListView
{

    private ICardCollection CardCollection = null!;

    public CardCollectionView() : base()
    {
        X = 0;
        Y = 0;
        Width = Dim.Fill();
        Height = Dim.Fill();
        AllowsMarking = false;
        CanFocus = true;

        OpenSelectedItem += OnSelectedChanged;
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

            string shortView = CardModalView.BuildCardShortView(card);

            list.Add($"{i:D2} " + shortView);
        }


        SetSource(list);

    }

}
