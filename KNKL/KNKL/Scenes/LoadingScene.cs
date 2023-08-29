using CocosDenshion;
using CocosSharp;
using KNKL.Core;
using KNKL.Core.SaveLoad;
using KNKL.Core.Scene;
using KNKL.Scenes.Menu;
using KNKL.Scenes.Nadrazi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes
{
    public class LoadingScene : CCScene
    {

        private CCWindow _mainWindow;
        public LoadingScene(CCWindow mainWindow) : base(mainWindow)
        {
            this._mainWindow = mainWindow;

            //Pokud hraje nejaka hudba, pausnu ji.
            if (CCSimpleAudioEngine.SharedEngine.BackgroundMusicPlaying)
            {
                CCSimpleAudioEngine.SharedEngine.StopBackgroundMusic();
            }

            var mainLayer = new CCLayer();

            var loadingIndicator = new CCSprite("Graphics/Loading/0");
            loadingIndicator.PositionX = 1162;
            loadingIndicator.PositionY = 157;

            loadingIndicator.RunAction(new CCRepeatForever(new CCAnimate(new LoadingAnimation())));

            mainLayer.AddChild(loadingIndicator);
            AddChild(mainLayer);
        }

        /// <summary>
        /// Metoda, ktera odstartuje novou hru.
        /// </summary>
        /// <returns></returns>
        public async Task<GameScene> StartNewGame()
        {
            GameScene scene = null;

            await Task.Run(() => {
                var game = GameAdventure.Instance;
                game.RestartGame();
                scene = new NadraziScene(_mainWindow);
            });

            return scene;
        }

        public async Task<GameScene> LoadGame(string fileName)
        {
            GameScene scene = null;
            await Task.Run(() => {
                //Nahraju vsechny dulezite udaje ze souboru a vratim nazev sceny
                var sceneName = SaveLoadGame.LoadGame(fileName);
                //Na zaklade nazvu sceny se do ni presunu
                switch (sceneName)
                {
                    case "01_nadrazi":
                        scene = new NadraziScene(_mainWindow);
                        break;
                }
            });
            return scene;
        }
    }
}
