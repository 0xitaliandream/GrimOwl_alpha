using GameEngine;


namespace GrimOwlGameEngine;

public class SummonCreatureAction : GameEngine.Action<GrimOwlGameState>
{
    
    protected GrimOwlPlayer player = null!;

    
    protected GrimOwlCreatureCard creatureCard = null!;

    
    protected int x = 0;

     
    protected int y = 0;

    protected SummonCreatureAction() { }

    public SummonCreatureAction(GrimOwlPlayer player, GrimOwlCreatureCard creatureCard, int x, int y, bool isAborted = false
        ) : base(isAborted)
    {
        this.player = player;
        this.creatureCard = creatureCard;

        this.x = x;
        this.y = y;

    }

    
    public GrimOwlPlayer Player
    {
        get => player;
    }

    
    public GrimOwlCreatureCard CreatureCard
    {
        get => creatureCard;
    }

    
    public int X
    {
        get => x;
    }

    
    public int Y
    {
        get => y;
    }


    public override void Execute(IGame<GrimOwlGameState> game)
    {
        game.ExecuteSequentially(new List<IAction> {
            new ModifyManaStatAction(Player, -CreatureCard.GetValue(StatKeys.Mana), 0),
            new ModifyManaSpecialStatAction(Player, -CreatureCard.GetValue(StatKeys.ManaSpecial), 0),
            new RemoveCardFromCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Hand), CreatureCard),
            new AddCardToCardCollectionAction(Player.GetCardCollection(CardCollectionKeys.Permanent), CreatureCard),
            new AddCardToTerrainAction(game.State.Grid, CreatureCard, X, Y)
        });
    }

    public override bool IsExecutable(GrimOwlGameState gameState)
    {
        return CreatureCard.IsSummonable(gameState) && gameState.Grid.TerrainExists(X, Y) && gameState.Grid[X,Y]!.IsFree();
    }
}
