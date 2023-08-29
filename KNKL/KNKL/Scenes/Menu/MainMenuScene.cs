using CocosDenshion;
using CocosSharp;
using KNKL.Core;
using KNKL.Core.Visuals;
using KNKL.Scenes.Nadrazi;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes.Menu
{
    public class MainMenuScene : CCScene
    {
        private CCWindow _mainWindow;
        private CCButton _newGameButton;
        private CCButton _saveGameButton;
        private CCButton _loadGameButton;
        private CCButton _creditsButton;

        public MainMenuScene(CCWindow mainWindow) : base(mainWindow)
        {
            var game = GameAdventure.Instance;
            _mainWindow = mainWindow;

            CCLayer backgroundLayer = new CCLayer();
            CCLayer menuLayer = new CCLayer();

            CCSprite backgroundSprite = new CCSprite("Graphics/Scenes/00_menu/background");
            backgroundSprite.PositionX = CCScene.DefaultDesignResolutionSize.Width / 2;
            backgroundSprite.PositionY = CCScene.DefaultDesignResolutionSize.Height / 2;
            backgroundLayer.AddChild(backgroundSprite);

            //Tlacitko pro novou hru
            _newGameButton = new CCButton(string.Format("Graphics/Scenes/00_menu/MenuItems/new_game_{0}", game.Language), string.Format("Graphics/Scenes/00_menu/MenuItems/new_game_1_{0}", game.Language));
            _newGameButton.Click = async() => {
                var loadingScene = new LoadingScene(_mainWindow);
                Window.DefaultDirector.ReplaceScene(new CCTransitionFade(1, loadingScene));

                var newGameScene = await loadingScene.StartNewGame();
                newGameScene.PlayMusic();
                Window.DefaultDirector.ReplaceScene(new CCTransitionFade(1, newGameScene));
            };
            _newGameButton.PositionX = 370;
            _newGameButton.PositionY = 469;

            //Tlacitko pro ulozeni hry
            _saveGameButton = new CCButton(string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_2_{0}", game.Language), string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_2_{0}", game.Language));
            _saveGameButton.Click = () => {};
            _saveGameButton.PositionX = 370;
            _saveGameButton.PositionY = 339;

            //Tlacitko pro nahrani hry
            _loadGameButton = new CCButton(string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_{0}", game.Language), string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_1_{0}", game.Language));
            _loadGameButton.Click = () => Window.DefaultDirector.PushScene(new LoadGameScene(_mainWindow));
            _loadGameButton.PositionX = 370;
            _loadGameButton.PositionY = 209;

            //Tlacitko pro autory
            _creditsButton = new CCButton(string.Format("Graphics/Scenes/00_menu/MenuItems/credits_{0}", game.Language), string.Format("Graphics/Scenes/00_menu/MenuItems/credits_1_{0}", game.Language));
            _creditsButton.Click = () => { };
            _creditsButton.PositionX = 370;
            _creditsButton.PositionY = 79;

            //Tlacitko pro nastaveni
            CCButton optionsButton = new CCButton("Graphics/Scenes/00_menu/MenuItems/options", "Graphics/Scenes/00_menu/MenuItems/options_1");
            optionsButton.Click = () =>
            {
                Window.DefaultDirector.PushScene(new OptionsMenuScene(_mainWindow, this));
            };
            optionsButton.PositionX = 1114;
            optionsButton.PositionY = 79;

            menuLayer.AddChild(_newGameButton);
            menuLayer.AddChild(_saveGameButton);
            menuLayer.AddChild(_loadGameButton);
            menuLayer.AddChild(_creditsButton);
            menuLayer.AddChild(optionsButton);

            AddChild(backgroundLayer);
            AddChild(menuLayer);

            CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("Music/klaster05", true);
        }

        public void RefreshGraphics()
        {
            var game = GameAdventure.Instance;

            _newGameButton.NormalImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/new_game_{0}", game.Language));
            _newGameButton.HoverImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/new_game_1_{0}", game.Language));
            _newGameButton.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/new_game_{0}", game.Language));


            _saveGameButton.NormalImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_2_{0}", game.Language));
            _saveGameButton.HoverImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_2_{0}", game.Language));
            _saveGameButton.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_2_{0}", game.Language));

            _loadGameButton.NormalImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_{0}", game.Language));
            _loadGameButton.HoverImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_1_{0}", game.Language));
            _loadGameButton.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_{0}", game.Language));

            _creditsButton.NormalImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/credits_{0}", game.Language));
            _creditsButton.HoverImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/credits_1_{0}", game.Language));
            _creditsButton.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/credits_{0}", game.Language));
        }
    }
}
