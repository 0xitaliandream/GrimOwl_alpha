﻿
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
    public CardCollectionView myInfoView = null!;

    public CardCollectionView enemyHandView = null!;
    public CardCollectionView enemyRitualsView = null!;
    public CardCollectionView enemyEnchantmentsView = null!;
    public CardCollectionView enemyGraveyardView = null!;
    public CardCollectionView enemyInfoView = null!;


    public CommandLineView commandLineView = null!;
    public GridView gridView = null!;


    private Thread mainThread = null!;
    private Thread updateThread = null!;
    private bool IsAppRunning;
    private bool IsUpdateRunning;

    public GrimOwlConsoleGui()
    {

        GenerateGui();

        this.mainThread = new Thread(new ThreadStart(this.MainLoop));
        this.mainThread.Start();

        this.updateThread = new Thread(new ThreadStart(this.Update));
        this.updateThread.Start();
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

        enemyInfoView = new CardCollectionView("Enemy Info")
        {
            X = Pos.Right(enemyGraveyardView),
            Y = 0,
            Width = Dim.Percent(15),
            Height = Dim.Percent(15)
        };

        gridView = new GridView("Grid")
        {
            X = 0,
            Y = Pos.Bottom(enemyInfoView),
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

        myInfoView = new CardCollectionView("My Info")
        {
            X = Pos.Right(myGraveyardView),
            Y = Pos.Bottom(gridView),
            Width = Dim.Percent(15),
            Height = Dim.Percent(22)
        };


        commandLineView = new CommandLineView()
        {
            X = 0,
            Y = Pos.Bottom(myInfoView),
            Width = Dim.Fill(),
            Height = 3
        };



        gameFrame.Add(enemyHandView, enemyRitualsView, enemyEnchantmentsView, enemyGraveyardView, enemyInfoView , gridView, myHandView, myRitualsView, myEnchantmentsView, myGraveyardView, myInfoView, commandLineView);

        this.mainWindow.Add(gameFrame);
    }

    public void Stop()
    {

        this.IsUpdateRunning = false;
        this.updateThread.Join();
        Application.RequestStop();

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
            });
            Thread.Sleep(1000); // Aggiorna ogni 0.5 secondo
        }
    }

    public void OnNewGameState(GrimOwlGame game)
    {
        Console.WriteLine("New game state received");
    }

}