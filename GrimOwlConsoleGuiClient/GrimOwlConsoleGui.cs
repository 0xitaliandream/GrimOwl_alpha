
using GrimOwlGameClient;
using GrimOwlGameEngine;
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class GrimOwlConsoleGui
{

    public Window mainWindow = null!;
    public MenuBar menu = null!;
    public CardCollectionView myHandView = null!;
    public CardCollectionView myRitualsView = null!;
    public CardCollectionView myEnchantmentsView = null!;
    public CardCollectionView myGraveyardView = null!;
    public GameInfoView myGameInfoView = null!;

    public CardCollectionView enemyHandView = null!;
    public CardCollectionView enemyRitualsView = null!;
    public CardCollectionView enemyEnchantmentsView = null!;
    public CardCollectionView enemyGraveyardView = null!;
    public GameInfoView enemyGameInfoView = null!;


    public CommandLineView commandLineView = null!;
    public GridView gridView = null!;


    public static GrimOwlGameUpdatePlayerContext currentGameState = null!;

    private Client client = null!;
    private Thread mainThread = null!;
    private Thread updateThread = null!;
    private bool IsAppRunning;
    private bool IsUpdateRunning;

    public GrimOwlConsoleGui(int arg1)
    {

        GenerateGui();

        this.mainThread = new Thread(new ThreadStart(this.MainLoop));
        this.mainThread.Start();

        this.updateThread = new Thread(new ThreadStart(this.Update));
        this.updateThread.Start();


        client = new Client("ws://127.0.0.1:8080/GameService", arg1);
        client.GameService.OnGrimOwlGameStateUpdate += OnNewGameState;
        commandLineView.OnNewCommand += client.GameService.SendCommand;
        client.ConnectAll();
    }

    private void GameService_OnGrimOwlGameStateUpdate(GrimOwlGame obj)
    {
        throw new NotImplementedException();
    }

    public void MainLoop()
    {
        Application.Init();
        Application.Top.Add(this.menu, this.mainWindow);

        this.IsAppRunning = true;

        Application.Run();

        Application.Shutdown();

        this.IsAppRunning = false;
    }

    public void GenerateGui()
    {

        

        this.mainWindow = new Window($"GAME BOARD")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        this.menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("Action", new MenuItem [] {
                new MenuItem ("_Quit", "", () => {
                    this.Stop();
                })
            }),
        });

        var gameFrame = new FrameView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),

        };


        


        enemyHandView = new CardCollectionView("Enemy Hand")
        {
            X = 0,
            Y = 0,
            Width = Dim.Percent(30),
            Height = Dim.Percent(15)
        };

        enemyRitualsView = new CardCollectionView("Enemy Rituals")
        {
            X = Pos.Right(enemyHandView),
            Y = 0,
            Width = Dim.Percent(15),
            Height = Dim.Percent(15)
        };

        enemyEnchantmentsView = new CardCollectionView("Enemy Enchantments")
        {
            X = Pos.Right(enemyRitualsView),
            Y = 0,
            Width = Dim.Percent(15),
            Height = Dim.Percent(15)
        };

        enemyGraveyardView = new CardCollectionView("Enemy Graveyard")
        {
            X = Pos.Right(enemyEnchantmentsView),
            Y = 0,
            Width = Dim.Percent(25),
            Height = Dim.Percent(15)
        };

        enemyGameInfoView = new GameInfoView("Enemy Info")
        {
            X = Pos.Right(enemyGraveyardView),
            Y = 0,
            Width = Dim.Percent(15),
            Height = Dim.Percent(15)
        };

        gridView = new GridView("Grid")
        {
            X = 0,
            Y = Pos.Bottom(enemyGameInfoView),
            Width = Dim.Fill(),
            Height = Dim.Percent(58)
        };

        myHandView = new CardCollectionView("My Hand")
        {
            X = 0,
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(30),
            Height = Dim.Percent(22)
        };

        myRitualsView = new CardCollectionView("My Rituals")
        {
            X = Pos.Right(myHandView),
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(15),
            Height = Dim.Percent(22)
        };

        myEnchantmentsView = new CardCollectionView("My Enchantments")
        {
            X = Pos.Right(myRitualsView),
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(15),
            Height = Dim.Percent(22)
        };

        myGraveyardView = new CardCollectionView("My Graveyard")
        {
            X = Pos.Right(myEnchantmentsView),
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(25),
            Height = Dim.Percent(22)
        };

        myGameInfoView = new GameInfoView("Game Info")
        {
            X = Pos.Right(myGraveyardView),
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(15),
            Height = Dim.Percent(22)
        };


        commandLineView = new CommandLineView()
        {
            X = 0,
            Y = Pos.Bottom(myGameInfoView),
            Width = Dim.Fill(),
            Height = 3
        };



        gameFrame.Add(enemyHandView, enemyRitualsView, enemyEnchantmentsView, enemyGraveyardView, enemyGameInfoView , gridView, myHandView, myRitualsView, myEnchantmentsView, myGraveyardView, myGameInfoView, commandLineView);

        this.mainWindow.Add(gameFrame);
    }

    public void Stop()
    {

        this.IsUpdateRunning = false;
        this.updateThread.Join();
        Application.RequestStop();

        client.DisconnectAll();

    }


    public void Update()
    {
        this.IsUpdateRunning = true;

        while (this.IsUpdateRunning)
        {

            //Check if Application is still running

            if (Application.MainLoop == null)
            {
                continue;
            }

            Application.MainLoop.Invoke(() =>
            {
                gridView.SetNeedsDisplay();
                myHandView.SetNeedsDisplay();
                myRitualsView.SetNeedsDisplay();
                myEnchantmentsView.SetNeedsDisplay();
                myGraveyardView.SetNeedsDisplay();
                myGameInfoView.SetNeedsDisplay();
                commandLineView.SetNeedsDisplay();
                enemyHandView.SetNeedsDisplay();
                enemyRitualsView.SetNeedsDisplay();
                enemyEnchantmentsView.SetNeedsDisplay();
                enemyGraveyardView.SetNeedsDisplay();
                enemyGameInfoView.SetNeedsDisplay();
            });
            Thread.Sleep(1000); // Aggiorna ogni 0.5 secondo
        }
    }


    public void OnNewGameState(GrimOwlGameUpdatePlayerContext game)
    {
        Console.WriteLine("New game state received");

        currentGameState = null!;
        currentGameState = game;

        myGameInfoView.Update(game.Player);
        enemyGameInfoView.Update(game.EnemyPlayer);

        myHandView.Update(game.Player.GetCardCollection(CardCollectionKeys.Hand));
        enemyHandView.Update(game.EnemyPlayer.GetCardCollection(CardCollectionKeys.Hand));
    }

}
