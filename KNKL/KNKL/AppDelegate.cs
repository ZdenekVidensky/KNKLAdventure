using System.Reflection;
using Microsoft.Xna.Framework;
using CocosSharp;
using CocosDenshion;
using KNKL.Scenes;
using KNKL.Scenes.Menu;
using KNKL.Actor;
using KNKL.Scenes.Nadrazi;
using KNKL.Core;
using Ninject;
using System.Threading.Tasks;
using KNKL.Core.SaveLoad;
using System;

namespace KNKL
{
    public class AppDelegate : CCApplicationDelegate
    {
        public static StandardKernel Container { get; set; }

        public override async void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            application.ContentRootDirectory = "Content";
            var windowSize = mainWindow.WindowSizeInPixels;

            var desiredWidth = 1280.0f;
            var desiredHeight = 720.0f;

            CCScene.SetDefaultDesignResolution(desiredWidth, desiredHeight, CCSceneResolutionPolicy.ShowAll);
            await InitializeGame(mainWindow);

            //var scene = new NadraziScene(mainWindow);
            var scene = new MainMenuScene(mainWindow);

            CCLog.Logger = (format, args) =>
            {
                System.Diagnostics.Debug.WriteLine(format, args);
            };

            mainWindow.RunWithScene(new CCTransitionFade(1, scene));
        }

        public async Task InitializeGame(CCWindow mainWindow)
        {

            //Inicializuji hru
            var game = GameAdventure.Instance;
            await game.InitializeGame();
            game.MainWindow = mainWindow;

            //Nactu Jenika
            var jenik = Jenik.Instance;
            await jenik.InitializeJenik();
        }

        public override void ApplicationDidEnterBackground(CCApplication application)
        {
            application.Paused = true;
        }

        public override void ApplicationWillEnterForeground(CCApplication application)
        {
            application.Paused = false;
        }
    }
}