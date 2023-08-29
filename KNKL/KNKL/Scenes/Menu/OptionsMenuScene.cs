using CocosDenshion;
using CocosSharp;
using KNKL.Core;
using KNKL.Core.SaveLoad;
using KNKL.Core.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes.Menu
{
    class OptionsMenuScene : CCScene
    {
        private CCWindow _mainWindow;
        private CCLayer _backgroundLayer;
        private CCLayer _menuLayer;
        private CCSprite _languageSprite;
        private CCSprite _backgroundSprite;
        private CCButton _saveButton;
        private CCButton _backButton;

        private MainMenuScene _mainMenuScene;
        private InGameMenuScene _ingameMenuScene;

        public OptionsMenuScene(CCWindow mainWindow, MainMenuScene mainMenuScene = null, InGameMenuScene inGameMenuScene = null) : base(mainWindow)
        {
            _mainWindow = mainWindow;
            _mainMenuScene = mainMenuScene;
            _ingameMenuScene = inGameMenuScene;

            var game = GameAdventure.Instance;

            CCLayer _backgroundLayer = new CCLayer();
            CCLayer _menuLayer = new CCLayer();

            //Nahraju si pozadi
            _backgroundSprite = new CCSprite(string.Format("Graphics/Scenes/00_menu/options_background_{0}", game.Language));
            _backgroundSprite.PositionX = CCScene.DefaultDesignResolutionSize.Width / 2;
            _backgroundSprite.PositionY = CCScene.DefaultDesignResolutionSize.Height / 2;
            _backgroundLayer.AddChild(_backgroundSprite);

            //Pridam tlacitko zpet
            _backButton = new CCButton(string.Format("Graphics/Scenes/00_menu/Buttons/back_{0}", game.Language),
                string.Format("Graphics/Scenes/00_menu/Buttons/back_1_{0}", game.Language));
            _backButton.PositionX = 185;
            _backButton.PositionY = 660;
            _backButton.Click = async () => {
                //Ulozim nastaveni do souboru xml
                Options.SaveOptions();
                //Znovu zinicializuji hru kvuli popiskum predmetu a scen
                await game.LanguageChanged();
                Window.DefaultDirector.PopScene();

                //Zmenim grafiku podle jazyka ve scene, ktera tuto scenu volala (ingame menu nebo main menu)
                if(_mainMenuScene != null)
                {
                    _mainMenuScene.RefreshGraphics();
                }

                if(_ingameMenuScene != null)
                {
                    _ingameMenuScene.RefreshGraphics();
                }
            };
            _menuLayer.AddChild(_backButton);

            //Vlozim checkbox
            CCCheckBox checkBox = new CCCheckBox("Graphics/Scenes/00_menu/OptionsItems/subtitles_on", "Graphics/Scenes/00_menu/OptionsItems/subtitles_off", game.AllowSubtitles);
            checkBox.PositionX = 1030f;
            checkBox.PositionY = 553.5f;
            checkBox.Click = () => {
                game.AllowSubtitles = !game.AllowSubtitles;
                RefreshBackground();
            };
            _menuLayer.AddChild(checkBox);

            //Vlozim sipku doleva
            CCArrow arrowLeft = new CCArrow("Graphics/Scenes/00_menu/OptionsItems/arrow_left");
            arrowLeft.PositionX = 804.5f;
            arrowLeft.PositionY = 458;
            arrowLeft.Click = () => {
                game.PreviousLanguage();
                RefreshBackground();
            };
            _menuLayer.AddChild(arrowLeft);

            //Vlozim sipku doprava
            CCArrow arrowRight = new CCArrow("Graphics/Scenes/00_menu/OptionsItems/arrow_right");
            arrowRight.PositionX = 1030f;
            arrowRight.PositionY = 458;
            arrowRight.Click = () => {
                game.NextLanguage();
                RefreshBackground();
            };
            _menuLayer.AddChild(arrowRight);

            //Vlozim sprite pro jazyk
            this._languageSprite = new CCSprite(string.Format("Graphics/Scenes/00_menu/OptionsItems/Languages/{0}", game.Language));
            this._languageSprite.PositionX = 917;
            this._languageSprite.PositionY = 460;
            _menuLayer.AddChild(this._languageSprite);

            //Vlozim slider pro zvuk
            var soundSlider = new CCSlider(game.SoundsVolume);
            soundSlider.PositionX = 925f;
            soundSlider.PositionY = 361.5f;
            soundSlider.CreateTouchBox();
            soundSlider.ActionOnMove = () => {
                game.SoundsVolume = soundSlider.Value;
            };
            _menuLayer.AddChild(soundSlider);

            //Vlozim slider pro hudbu
            var musicSlider = new CCSlider(game.MusicVolume);
            musicSlider.PositionX = 925f;
            musicSlider.PositionY = 271.5f;
            musicSlider.CreateTouchBox();
            musicSlider.ActionOnMove = () => {
                game.MusicVolume = musicSlider.Value;
                CCSimpleAudioEngine.SharedEngine.BackgroundMusicVolume = game.MusicVolume;
            };
            _menuLayer.AddChild(musicSlider);

            //Vlozim slider pro dialogy
            var dialogueSlider = new CCSlider(game.DialoguesVolume);
            dialogueSlider.PositionX = 925f;
            dialogueSlider.PositionY = 181.5f;
            dialogueSlider.CreateTouchBox();
            dialogueSlider.ActionOnMove = () => {
                game.DialoguesVolume = dialogueSlider.Value;
            };
            _menuLayer.AddChild(dialogueSlider);

            AddChild(_backgroundLayer);
            AddChild(_menuLayer);
        }

        public void RefreshBackground()
        {
            var game = GameAdventure.Instance;
            _backgroundSprite.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/options_background_{0}", game.Language));
            _languageSprite.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/OptionsItems/Languages/{0}", game.Language));

            _backButton.Texture = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/Buttons/back_{0}", game.Language));
            _backButton.NormalImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/Buttons/back_{0}", game.Language));
            _backButton.HoverImage = new CCTexture2D(string.Format("Graphics/Scenes/00_menu/Buttons/back_1_{0}", game.Language));
        }
    }
}
