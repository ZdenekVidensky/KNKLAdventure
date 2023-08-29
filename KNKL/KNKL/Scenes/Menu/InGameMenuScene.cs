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
    public class InGameMenuScene : CCScene
    {
        private CCWindow _mainWindow;
        private CCButton _continueButton;
        private CCButton _saveGameButton;
        private CCButton _loadGameButton;
        private CCButton _creditsButton;

        public InGameMenuScene(CCWindow mainWindow) : base(mainWindow)
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
            _continueButton = new CCButton(string.Format("Graphics/Scenes/00_menu/MenuItems/continue_{0}", game.Language), string.Format("Graphics/Scenes/00_menu/MenuItems/continue_1_{0}", game.Language));
            _continueButton.Click = () => Window.DefaultDirector.PopScene();
            _continueButton.PositionX = 370;
            _continueButton.PositionY = 469;

            //Tlacitko pro ulozeni hry
            _saveGameButton = new CCButton(string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_{0}", game.Language), string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_1_{0}", game.Language));
            _saveGameButton.Click = () => Window.DefaultDirector.PushScene(new SaveGameScene(_mainWindow));
            _saveGameButton.PositionX = 370;
            _saveGameButton.PositionY = 339;

            //Tlacitko pro nahrani hry
            _loadGameButton = new CCButton(string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_{0}", game.Language), string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_1_{0}", game.Language));
            _loadGameButton.Click = () => Window.DefaultDirector.PushScene(new LoadGameScene(_mainWindow));
            _loadGameButton.PositionX = 370;
            _loadGameButton.PositionY = 209;

            //Tlacitko pro autory
            _creditsButton = new CCButton(string.Format("Graphics/Scenes/00_menu/MenuItems/credits_{0}", game.Language), string.Format("Graphics/Scenes/00_menu/MenuItems/credits_1_{0}", game.Language));
            _creditsButton.Click = () => Window.DefaultDirector.ReplaceScene(new CCTransitionFade(1, new NadraziScene(_mainWindow)));
            _creditsButton.PositionX = 370;
            _creditsButton.PositionY = 79;

            //Tlacitko pro nastaveni
            CCButton optionsButton = new CCButton("Graphics/Scenes/00_menu/MenuItems/options", "Graphics/Scenes/00_menu/MenuItems/options_1");
            optionsButton.Click = () =>
            {
                Window.DefaultDirector.PushScene(new OptionsMenuScene(_mainWindow, null, this));
            };
            optionsButton.PositionX = 1114;
            optionsButton.PositionY = 79;

            menuLayer.AddChild(_continueButton);
            menuLayer.AddChild(_saveGameButton);
            menuLayer.AddChild(_loadGameButton);
            menuLayer.AddChild(_creditsButton);
            menuLayer.AddChild(optionsButton);

            AddChild(backgroundLayer);
            AddChild(menuLayer);
        }

        public void RefreshGraphics()
        {
            var game = GameAdventure.Instance;

            _continueButton.NormalImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/continue_{0}", game.Language));
            _continueButton.HoverImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/continue_1_{0}", game.Language));
            _continueButton.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/continue_{0}", game.Language));


            _saveGameButton.NormalImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_{0}", game.Language));
            _saveGameButton.HoverImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_1_{0}", game.Language));
            _saveGameButton.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/save_game_{0}", game.Language));

            _loadGameButton.NormalImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_{0}", game.Language));
            _loadGameButton.HoverImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_1_{0}", game.Language));
            _loadGameButton.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/load_game_{0}", game.Language));

            _creditsButton.NormalImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/credits_{0}", game.Language));
            _creditsButton.HoverImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/credits_1_{0}", game.Language));
            _creditsButton.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/MenuItems/credits_{0}", game.Language));
        }
    }
}
