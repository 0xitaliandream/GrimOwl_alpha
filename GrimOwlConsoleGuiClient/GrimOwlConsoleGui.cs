
using GrimOwlGameClient;
using GrimOwlGameEngine;
using Terminal.Gui;

namespace GrimOwlConsoleGuiClient;

public class GrimOwlConsoleGui
{

    public Window mainWindow = null!;
    public MenuBar menu = null!;

    public FrameView gameFrame = null!;

    public FrameView myHandFrame = null!;
    public FrameView myRitualsFrame = null!;
    public FrameView myEnchantmentsFrame = null!;
    public FrameView myGraveyardFrame = null!;

    public FrameView enemyHandFrame = null!;
    public FrameView enemyRitualsFrame = null!;
    public FrameView enemyEnchantmentsFrame = null!;
    public FrameView enemyGraveyardFrame = null!;


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

        this.gameFrame = new FrameView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),

        };

        

        enemyHandFrame = new FrameView("Enemy Hand")
        {
            X = 0,
            Y = 0,
            Width = Dim.Percent(30),
            Height = Dim.Percent(15)
        };

        enemyHandView = new CardCollectionView();

        enemyHandFrame.Add(enemyHandView);

        

        enemyRitualsFrame = new FrameView("Enemy Rituals")
        {
            X = Pos.Right(enemyHandFrame),
            Y = 0,
            Width = Dim.Percent(15),
            Height = Dim.Percent(15)
        };

        enemyRitualsView = new CardCollectionView();

        enemyRitualsFrame.Add(enemyRitualsView);

        

        enemyEnchantmentsFrame = new FrameView("Enemy Enchantments")
        {
            X = Pos.Right(enemyRitualsFrame),
            Y = 0,
            Width = Dim.Percent(15),
            Height = Dim.Percent(15)
        };

        enemyEnchantmentsView = new CardCollectionView();

        enemyEnchantmentsFrame.Add(enemyEnchantmentsView);

        

        enemyGraveyardFrame = new FrameView("Enemy Graveyard")
        {
            X = Pos.Right(enemyEnchantmentsFrame),
            Y = 0,
            Width = Dim.Percent(25),
            Height = Dim.Percent(15)
        };

        enemyGraveyardView = new CardCollectionView();

        enemyGraveyardFrame.Add(enemyGraveyardView);

        

        enemyGameInfoView = new GameInfoView("Enemy Info")
        {
            X = Pos.Right(enemyGraveyardFrame),
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


        myHandFrame = new FrameView("My Hand")
        {
            X = 0,
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(30),
            Height = Dim.Percent(22)
        };

        myHandView = new CardCollectionView();

        myHandFrame.Add(myHandView);

        myRitualsFrame = new FrameView("My Rituals")
        {
            X = Pos.Right(myHandFrame),
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(15),
            Height = Dim.Percent(22)
        };

        myRitualsView = new CardCollectionView();

        myRitualsFrame.Add(myRitualsView);

        myEnchantmentsFrame = new FrameView("My Enchantments")
        {
            X = Pos.Right(myRitualsFrame),
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(15),
            Height = Dim.Percent(22)
        };

        myEnchantmentsView = new CardCollectionView();

        myEnchantmentsFrame.Add(myEnchantmentsView);

        myGraveyardFrame = new FrameView("My Graveyard")
        {
            X = Pos.Right(myEnchantmentsFrame),
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(25),
            Height = Dim.Percent(22)
        };

        myGraveyardView = new CardCollectionView();

        myGraveyardFrame.Add(myGraveyardView);

        myGameInfoView = new GameInfoView("Game Info")
        {
            X = Pos.Right(myGraveyardFrame),
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


        gameFrame.Add(enemyHandFrame, enemyHandFrame, enemyRitualsFrame, enemyEnchantmentsFrame, enemyGraveyardFrame, enemyGameInfoView, gridView, myHandFrame, myRitualsFrame, myEnchantmentsFrame, myGraveyardFrame, myGameInfoView, commandLineView);
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

                gameFrame.SetChildNeedsDisplay();
                enemyHandFrame.SetChildNeedsDisplay();
                enemyRitualsFrame.SetChildNeedsDisplay();
                enemyEnchantmentsFrame.SetChildNeedsDisplay();
                enemyGraveyardFrame.SetChildNeedsDisplay();
                enemyGameInfoView.SetNeedsDisplay();
                gridView.SetNeedsDisplay();
                myHandFrame.SetChildNeedsDisplay();
                myRitualsFrame.SetChildNeedsDisplay();
                myEnchantmentsFrame.SetChildNeedsDisplay();
                myGraveyardFrame.SetChildNeedsDisplay();
                myGameInfoView.SetNeedsDisplay();
                commandLineView.SetNeedsDisplay();


                
                
                



            });
            Thread.Sleep(1000); // Aggiorna ogni 0.5 secondo
        }
    }


    public void OnNewGameState(GrimOwlGameUpdatePlayerContext game)
    {
        Console.WriteLine("New game state received");

        currentGameState = null!;
        currentGameState = game;

        gridView.Update(game.GameInfo.State.Grid);
        
        myGameInfoView.Update(game.Player);
        enemyGameInfoView.Update(game.EnemyPlayer);

        myHandView.Update(game.Player.GetCardCollection(CardCollectionKeys.Hand));
        enemyHandView.Update(game.EnemyPlayer.GetCardCollection(CardCollectionKeys.Hand));

        myGraveyardView.Update(game.Player.GetCardCollection(CardCollectionKeys.Graveyard));
        enemyGraveyardView.Update(game.EnemyPlayer.GetCardCollection(CardCollectionKeys.Graveyard));


    }

}
