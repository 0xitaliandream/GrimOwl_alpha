using GameEngine;

namespace GrimOwl;

public static class ConsoleUtil
{
    private const int columnWidth = 35;

    private const ConsoleColor BackgroundColorDefault = ConsoleColor.Black;
    private const ConsoleColor ColorDefault = ConsoleColor.White;
    private const ConsoleColor ColorMana = ConsoleColor.Blue;
    private const ConsoleColor ColorAttack = ConsoleColor.Red;
    private const ConsoleColor ColorLife = ConsoleColor.Green;
    private const ConsoleColor ColorEnergy = ConsoleColor.Yellow;
    private const ConsoleColor ColorRange = ConsoleColor.Gray;
    private const ConsoleColor ColorSelectable = ConsoleColor.Cyan;

    public static void PrintGame(GrimOwlGameState gameState)
    {
        Console.BackgroundColor = BackgroundColorDefault;
        PrintPlayer(gameState, gameState.NonActivePlayers.First(), 0);
        Console.Write("\n\n");
        PrintHand(gameState, gameState.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Hand), 1);
        Console.Write("\n\n");
        PrintBoard(gameState.NonActivePlayers.First().GetCardCollection(CardCollectionKeys.Board), 2);
        Console.Write("\n\n");
        PrintBoard(gameState.ActivePlayer.GetCardCollection(CardCollectionKeys.Board), 3);
        Console.Write("\n\n");
        PrintHand(gameState, gameState.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand), 4);
        Console.Write("\n\n");
        PrintPlayer(gameState, gameState.ActivePlayer, 5);
        Console.Write("\n\n");
    }

    public static void PrintPlayer(GrimOwlGameState gameState, IPlayer player, int id)
    {
        Console.ForegroundColor = ColorDefault;
        Console.Write(string.Format("Player #{0}\n", gameState.Players.ToList().IndexOf(player)));
        Console.ForegroundColor = ColorMana;
        Console.Write(string.Format("Mana: {0:D2}/{1:D2}\n", player.GetValue(StatKeys.Mana), player.GetBaseValue(StatKeys.Mana)));
        Console.ForegroundColor = ColorMana;
        Console.Write(string.Format("ManaSpecial: {0:D2}/{1:D2}\n", player.GetValue(StatKeys.ManaSpecial), player.GetBaseValue(StatKeys.ManaSpecial)));
        Console.ForegroundColor = ColorDefault;
        Console.Write(string.Format("Id: {0}", id));
    }

    public static void PrintHand(GrimOwlGameState gameState, ICardCollection hand, int idPrefix)
    {
        Console.ForegroundColor = ColorMana;
        for (int i = 0; i < hand.Size; ++i)
        {
            ICard card = hand[i];
            Console.Write(string.Format(
                "Mana: {0:D2}".PadRight(columnWidth),
                card.GetValue(StatKeys.Mana)
            ));
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorMana;
        for (int i = 0; i < hand.Size; ++i)
        {
            ICard card = hand[i];
            Console.Write(string.Format(
                "ManaSpecial: {0:D2}".PadRight(columnWidth),
                card.GetValue(StatKeys.ManaSpecial)
            ));
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorAttack;
        for (int i = 0; i < hand.Size; ++i)
        {
            ICard card = hand[i];
            if (card is GrimOwlCreatureCard monsterCard)
            {
                Console.Write(string.Format(
                    "Attack: {0:D2}".PadRight(columnWidth),
                    monsterCard.GetValue(StatKeys.Attack)
                ));
            }
            else
            {
                Console.Write("".PadRight(columnWidth - 4));
            }
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorLife;
        for (int i = 0; i < hand.Size; ++i)
        {
            ICard card = hand[i];
            if (card is GrimOwlCreatureCard monsterCard)
            {
                Console.Write(string.Format(
                    "Life: {0:D2}".PadRight(columnWidth),
                    monsterCard.GetValue(StatKeys.Life)
                ));
            }
            else
            {
                Console.Write("".PadRight(columnWidth - 4));
            }
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorEnergy;
        for (int i = 0; i < hand.Size; ++i)
        {
            ICard card = hand[i];
            if (card is GrimOwlCreatureCard monsterCard)
            {
                Console.Write(string.Format(
                    "Energy: {0:D2}".PadRight(columnWidth),
                    monsterCard.GetValue(StatKeys.Energy)
                ));
            }
            else
            {
                Console.Write("".PadRight(columnWidth - 4));
            }
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorRange;
        for (int i = 0; i < hand.Size; ++i)
        {
            ICard card = hand[i];
            if (card is GrimOwlCreatureCard monsterCard)
            {
                Console.Write(string.Format(
                    "Range: {0:D2}".PadRight(columnWidth),
                    monsterCard.GetValue(StatKeys.Range)
                ));
            }
            else
            {
                Console.Write("".PadRight(columnWidth - 4));
            }
        }
        Console.WriteLine();
        for (int i = 0; i < hand.Size; ++i)
        {
            ICard card = hand[i];
            Console.ForegroundColor = ColorDefault;
            if (card is GrimOwlCreatureCard c0 && c0.IsSummonable(gameState))
            {
                Console.ForegroundColor = ColorSelectable;
            }
            Console.Write(string.Format(
                "UniqueId: {0}{1}".PadRight(25),
                idPrefix,
                card.UniqueId.ToString()
            ));
        }
    }



    public static void PrintBoard(ICardCollection board, int idPrefix)
    {
        Console.ForegroundColor = ColorMana;
        for (int i = 0; i < board.Size; ++i)
        {
            ICard card = board[i];
            Console.Write(string.Format(
                "Mana: {0:D2}".PadRight(columnWidth),
                card.GetValue(StatKeys.Mana)
            ));
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorMana;
        for (int i = 0; i < board.Size; ++i)
        {
            ICard card = board[i];
            Console.Write(string.Format(
                "ManaSpecial: {0:D2}".PadRight(columnWidth),
                card.GetValue(StatKeys.ManaSpecial)
            ));
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorAttack;
        for (int i = 0; i < board.Size; ++i)
        {
            ICard card = board[i];
            if (card is GrimOwlCreatureCard monsterCard)
            {
                Console.Write(string.Format(
                    "Attack: {0:D2}".PadRight(columnWidth),
                    monsterCard.GetValue(StatKeys.Attack)
                ));
            }
            else
            {
                Console.Write("".PadRight(columnWidth - 4));
            }
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorLife;
        for (int i = 0; i < board.Size; ++i)
        {
            ICard card = board[i];
            if (card is GrimOwlCreatureCard monsterCard)
            {
                Console.Write(string.Format(
                    "Life: {0:D2}".PadRight(columnWidth),
                    monsterCard.GetValue(StatKeys.Life)
                ));
            }
            else
            {
                Console.Write("".PadRight(columnWidth - 4));
            }
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorEnergy;
        for (int i = 0; i < board.Size; ++i)
        {
            ICard card = board[i];
            if (card is GrimOwlCreatureCard monsterCard)
            {
                Console.Write(string.Format(
                    "Energy: {0:D2}".PadRight(columnWidth),
                    monsterCard.GetValue(StatKeys.Energy)
                ));
            }
            else
            {
                Console.Write("".PadRight(columnWidth - 4));
            }
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorRange;
        for (int i = 0; i < board.Size; ++i)
        {
            ICard card = board[i];
            if (card is GrimOwlCreatureCard monsterCard)
            {
                Console.Write(string.Format(
                    "Range: {0:D2}".PadRight(columnWidth),
                    monsterCard.GetValue(StatKeys.Range)
                ));
            }
            else
            {
                Console.Write("".PadRight(columnWidth - 4));
            }
        }
        Console.WriteLine();
        Console.ForegroundColor = ColorDefault;
        for (int i = 0; i < board.Size; ++i)
        {
            GrimOwlCreatureCard? monsterCard = (GrimOwlCreatureCard?)board[i];
            Console.ForegroundColor = monsterCard != null && monsterCard.IsReadyToAttack
                    ? ColorSelectable
                    : ColorDefault;
            if (monsterCard != null)
            {
                Console.Write(string.Format(
                    "UniqueId: {0}{1}".PadRight(columnWidth),
                    idPrefix,
                    monsterCard.UniqueId.ToString()
                ));
            }
            else
            {
                Console.Write(string.Format(
                    "".PadRight(columnWidth),
                    i
                ));
            }
        }
    }
}
