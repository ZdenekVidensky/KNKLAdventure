using CocosSharp;
using KNKL.Core.SaveLoad;
using KNKL.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Visuals
{
    public class CCSaveSlot : CCLayer
    {
        const float WIDTH = 525;
        const float HEIGHT = 70;
        private CCWindow _mainWindow;

        private CCLabel _textLabel { get; set; }
        private CCRect _box { get; set; }
        public Action Click { get; set; }
        private bool _save { get; set; }
        private string _fileName { get; set; }
        public CCSaveSlot(string fileName, CCWindow mainWindow, bool save = false)
        {
            this._mainWindow = mainWindow;

            var sprite = new CCSprite("Graphics/Scenes/00_menu/SaveItems/field");
            this._save = save;
            this._fileName = fileName;
            this._textLabel = new CCLabel("--PRÁZDNÁ POZICE--", "Bebas", 32) {
                Color = new CCColor3B(161, 143, 111)
            };

            if (SaveLoadGame.SaveExists(fileName))
            {
                _textLabel.Text = SaveLoadGame.SaveName(fileName);
            }

            this.AddChild(sprite);
            this.AddChild(_textLabel);

            //Pokud existuje soubor odkazujici na tento save
            if(SaveLoadGame.SaveExists(fileName))
            {
                _textLabel.Text = SaveLoadGame.SaveName(fileName);
            }

            var saveSlotListener = new CCEventListenerTouchAllAtOnce();
            saveSlotListener.OnTouchesEnded = OnSaveSlotTouchesEnded;
            this.AddEventListener(saveSlotListener);
        }

        public void createBox()
        {
            _box = new CCRect(this.PositionX - (WIDTH/2), this.PositionY - (HEIGHT/2), WIDTH, HEIGHT);
        }

        async void OnSaveSlotTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0 && _box.ContainsPoint(touches[0].Location))
            {
                if (_save)
                {
                    _textLabel.Text = SaveLoadGame.SaveGame(_fileName);
                }
                else
                {
                    if(SaveLoadGame.SaveExists(_fileName))
                    {
                        var loadingScene = new LoadingScene(_mainWindow);
                        Window.DefaultDirector.PushScene(new CCTransitionFade(1, loadingScene));

                        //Restartuji hru (vymazu vsechny podminky, predmety)
                        var game = GameAdventure.Instance;
                        game.RestartGame();

                        var scene = await loadingScene.LoadGame(string.Format("{0}.xml", _fileName));

                        await Task.Delay(1000);
                        scene.PlayMusic();
                        Window.DefaultDirector.PushScene(new CCTransitionFade(1, scene));
                    }
                }
            }
        }
    }
}
