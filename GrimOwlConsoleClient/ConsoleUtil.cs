using GameEngine;
using GrimOwlGameEngine;


namespace GrimOwlConsoleClient;

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
        PrintBoard(gameState, 2);
        Console.Write("\n\n");
        PrintHand(gameState, gameState.ActivePlayer.GetCardCollection(CardCollectionKeys.Hand), 3);
        Console.Write("\n\n");
        PrintPlayer(gameState, gameState.ActivePlayer, 4);
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

    public static void PrintBoard(GrimOwlGameState gameState, int idPrefix)
    {
        var grid = gameState.Grid.Terrains;
        int rowCount = gameState.Grid.Rows;
        int colCount = gameState.Grid.Columns;
        int cellHeight = 9; // altezza di ogni cella "quadrata"
        int cellWidth = 20; // larghezza di ogni cella "quadrata"

        for (int row = 0; row < rowCount; ++row)
        {
            for (int h = 0; h < cellHeight; ++h)
            {
                for (int col = 0; col < colCount; ++col)
                {
                    GrimOwlPermanentCard? card = grid[col, row]!.PermanentCard;

                    string line = "";

                    if (h == 0)
                    {
                        line = $"|Idx: {col},{row}".PadRight(cellWidth - 1) + "|";
                    }
                    else if (h == cellHeight - 1)
                    {
                        line = "|" + new string('-', cellWidth - 2) + "|";
                    }
                    else if (card != null)
                    {
                        if (h == 1)
                        {
                            line = $"|Mana: {card.GetValue(StatKeys.Mana):D2}".PadRight(cellWidth - 1) + "|";
                            Console.ForegroundColor = ColorMana;
                        }
                        else if (h == 2)
                        {
                            line = $"|MnSpc: {card.GetValue(StatKeys.ManaSpecial):D2}".PadRight(cellWidth - 1) + "|";
                            Console.ForegroundColor = ColorMana;
                        }
                        else if (h == 3 && card is GrimOwlCreatureCard)
                        {
                            CardComponent buffComponent = card.GetComponent<GrimOwlCreatureBuffStatsStatsCardComponent>()!;

                            line = $"|Attk: {card.GetValue(StatKeys.Attack):D2} - ({buffComponent.GetValue(StatKeys.Attack)})".PadRight(cellWidth - 1) + "|";

                            Console.ForegroundColor = ColorAttack;
                        }
                        else if (h == 4 && card is GrimOwlCreatureCard)
                        {
                            CardComponent buffComponent = card.GetComponent<GrimOwlCreatureBuffStatsStatsCardComponent>()!;

                            line = $"|Life: {card.GetValue(StatKeys.Life):D2} - ({buffComponent.GetValue(StatKeys.Life)})".PadRight(cellWidth - 1) + "|";
                            Console.ForegroundColor = ColorLife;
                        }
                        else if (h == 5 && card is GrimOwlCreatureCard)
                        {
                            CardComponent buffComponent = card.GetComponent<GrimOwlCreatureBuffStatsStatsCardComponent>()!;

                            line = $"|Enrg: {card.GetValue(StatKeys.Energy):D2} - ({buffComponent.GetValue(StatKeys.Energy)}) ".PadRight(cellWidth - 1) + "|";
                            Console.ForegroundColor = ColorEnergy;
                        }
                        else if (h == 6 && card is GrimOwlCreatureCard)
                        {
                            CardComponent buffComponent = card.GetComponent<GrimOwlCreatureBuffStatsStatsCardComponent>()!;

                            line = $"|Rang: {card.GetValue(StatKeys.Range):D2} - ({buffComponent.GetValue(StatKeys.Range)})".PadRight(cellWidth - 1) + "|";
                            Console.ForegroundColor = ColorRange;
                        }
                        else if (h == 7 && card is GrimOwlCreatureCard)
                        {
                            line = $"|UID: {idPrefix}{card.UniqueId}".PadRight(cellWidth - 1) + "|";
                            Console.ForegroundColor = ColorDefault;
                        }
                        else
                        {
                            line = "|".PadRight(cellWidth - 1) + "|";
                            Console.ForegroundColor = ColorDefault;
                        }
                    }
                    else
                    {
                        line = "|".PadRight(cellWidth - 1) + "|";
                        Console.ForegroundColor = ColorDefault;
                    }

                    Console.Write(line);
                }

                Console.WriteLine();
            }
        }
    }
}
