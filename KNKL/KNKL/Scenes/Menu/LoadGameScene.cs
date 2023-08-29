using CocosSharp;
using KNKL.Core;
using KNKL.Core.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes.Menu
{
    public class LoadGameScene : CCScene
    {
        private CCWindow _mainWindow;
        private CCLayer _backgroundLayer;
        private CCLayer _menuLayer;
        private CCSprite _backgroundSprite;
        private CCButton _saveButton;
        private CCButton _backButton;

        public LoadGameScene(CCWindow mainWindow) : base(mainWindow)
        {
            var game = GameAdventure.Instance;

            _mainWindow = mainWindow;

            CCLayer _backgroundLayer = new CCLayer();
            CCLayer _menuLayer = new CCLayer();

            //Prvni slot
            CCSaveSlot slot1 = new CCSaveSlot("Save1", _mainWindow, false);
            slot1.PositionX = 783.5f;
            slot1.PositionY = 559.5f;
            slot1.createBox();
            _menuLayer.AddChild(slot1);

            //Druhy slot
            CCSaveSlot slot2 = new CCSaveSlot("Save2", _mainWindow, false);
            slot2.PositionX = 783.5f;
            slot2.PositionY = 469.5f;
            slot2.createBox();
            _menuLayer.AddChild(slot2);

            //Treti slot
            CCSaveSlot slot3 = new CCSaveSlot("Save3", _mainWindow, false);
            slot3.PositionX = 783.5f;
            slot3.PositionY = 379.5f;
            slot3.createBox();
            _menuLayer.AddChild(slot3);

            //Ctvrty slot
            CCSaveSlot slot4 = new CCSaveSlot("Save4", _mainWindow, false);
            slot4.PositionX = 783.5f;
            slot4.PositionY = 289.5f;
            slot4.createBox();
            _menuLayer.AddChild(slot4);

            //Paty slot
            CCSaveSlot slot5 = new CCSaveSlot("Save5", _mainWindow, false);
            slot5.PositionX = 783.5f;
            slot5.PositionY = 199.5f;
            slot5.createBox();
            _menuLayer.AddChild(slot5);

            //Nahraju si pozadi
            _backgroundSprite = new CCSprite("Graphics/Scenes/00_menu/save_game_background");
            _backgroundSprite.PositionX = CCScene.DefaultDesignResolutionSize.Width / 2;
            _backgroundSprite.PositionY = CCScene.DefaultDesignResolutionSize.Height / 2;
            _backgroundLayer.AddChild(_backgroundSprite);

            //Pridam tlacitko zpet
            _backButton = new CCButton(string.Format("Graphics/Scenes/00_menu/Buttons/back_{0}", game.Language),
                string.Format("Graphics/Scenes/00_menu/Buttons/back_1_{0}", game.Language));
            _backButton.PositionX = 185;
            _backButton.PositionY = 660;
            _backButton.Click = () => {
                Window.DefaultDirector.PopScene();
            };
            _menuLayer.AddChild(_backButton);

            AddChild(_backgroundLayer);
            AddChild(_menuLayer);
        }
    }
}
