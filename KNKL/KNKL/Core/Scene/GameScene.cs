using CocosDenshion;
using CocosSharp;
using KNKL.Actor;
using KNKL.Core.Subtitles;
using KNKL.Core.Visuals;
using KNKL.Scenes.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KNKL.Core.Scene
{
    public partial class GameScene : CCScene
    {
        private CCLayer BackgroundLayer { get; set; }
        private CCSprite BackgroundSprite { get; set; }
        public CCLayer ContentLayer { get; set; }
        public CCPolygon WalkableArea { get; set; }
        public Jenik Jenik { get; set; }
        public CCWindow MainWindow { get; set; }
        public string Description { get; set; }
        public GameAdventure Game { get; set; }

        private string MusicFileName { get; set; }

        public GameScene(CCWindow mainWindow, string backgroundFileName, string musicFileName) : base(mainWindow)
        {
            this.MainWindow = mainWindow;
            this.Game = GameAdventure.Instance;
            this.MusicFileName = musicFileName;

            Jenik = Jenik.Instance;

            BackgroundLayer = new CCLayer();
            Characters = new Dictionary<string, Character>();
    
            ContentLayer = new CCLayer();
            WalkableArea = new CCPolygon(true);

            BackgroundSprite = new CCSprite(backgroundFileName);
            BackgroundSprite.PositionX = CCScene.DefaultDesignResolutionSize.Width / 2;
            BackgroundSprite.PositionY = CCScene.DefaultDesignResolutionSize.Height / 2;
            BackgroundLayer.AddChild(BackgroundSprite);

            ContentLayer.AddChild(BackgroundLayer);

            AddChild(ContentLayer);

            //Zinicializuji Menu Layer
            this.InitMenuLayer();

            //Dialogove okenko s textem
            this.InitTextLayer();

            //Inventar
            this.InitInventoryLayer();

            //Inicializuji si slovnik pro titulky
            this.SceneSubtitles = new Dictionary<int, Subtitle>();
           
            //Hudba
            if (CCSimpleAudioEngine.SharedEngine.BackgroundMusicPlaying)
            {
                CCSimpleAudioEngine.SharedEngine.StopBackgroundMusic();
            }

            CCSimpleAudioEngine.SharedEngine.BackgroundMusicVolume = Game.MusicVolume;
        }

        public void PlayMusic()
        {
            CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic(string.Format("Music/{0}", this.MusicFileName), true);
        }

        /// <summary>
        /// Metoda, kterou kazda scena zavola pri inicializaci nebo pri zmene jazyka, aby se nacetla spravna jazykova mutace
        /// </summary>
        public virtual void InitializeSceneLanguage() { }
    }
}
