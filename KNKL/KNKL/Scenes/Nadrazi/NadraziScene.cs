using Box2D.Dynamics;
using CocosSharp;
using KNKL.Actor;
using KNKL.Characters.Dedek;
using KNKL.Characters.Prcek;
using KNKL.Core;
using KNKL.Core.SaveLoad;
using KNKL.Core.Scene;
using KNKL.Core.Subtitles;
using KNKL.Scenes.Nadrazi.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes.Nadrazi
{
    public class NadraziScene : GameScene
    {
        //Predmety, ktere vyuzivaji jine scripty
        public CCSprite Bees { get; set; }
        public VataItem VataItem { get; set; }

        public OknoItem OknoItem { get; set; }

        public NadraziScene(CCWindow mainWindow) : base(mainWindow, "Graphics/Scenes/01_nadrazi/background", "klaster07")
        {
            this.Name = "01_nadrazi";
            this.InitializeSceneLanguage();

            if (!Game.GetCondition("nadrazi_poprve"))
            {
                //Startovni pozice postavy a jeji velikost
                Jenik.Sprite.Position = new CCPoint(55.64f, 388.59f);
                Jenik.ChangeDirection(Jenik.Directions.Right);
            }

            Jenik.Game.CurrentScene = this;
            Jenik.Sprite.Scale = 0.8f;

            //Urcim oblast, kde muze postava chodit
            CCPoint[] walkablePoints = new CCPoint[] {
                new CCPoint(5, 254.8f),
                new CCPoint(1278.62f, 290.488f),
                new CCPoint(1273.63f, 305.8f),
                new CCPoint(8.557f, 263.04f)
            };

            WalkableArea.AddPoints(walkablePoints);
            ContentLayer.AddChild(WalkableArea);

            //Pridani predmetu za Jenikem

            //Tlacitko
            var tlacitkoItem = new TlacitkoItem(577, 374, 30, 30, new CCPoint(594, 390), Jenik.Directions.Down);
            ContentLayer.AddChild(tlacitkoItem);

            var dvereItem = new DvereItem(688, 300, 155, 216, new CCPoint(766, 386), Jenik.Directions.Down);
            ContentLayer.AddChild(dvereItem);

            if (!Game.GetCondition("vzal_rukavice"))
            {
                var rukaviceItem = new RukaviceItem(133, 380, 70, 70, new CCPoint(224.5f, 380), Jenik.Directions.Left);
                ContentLayer.AddChild(rukaviceItem);
            }

            this.VataItem = new VataItem(454.9f, 270.12f, 70, 70, new CCPoint(570.57f, 390), Jenik.Directions.Left);
            if (!Game.GetCondition("prcek_utekl"))
            {
                this.VataItem.Visible = false;
                this.VataItem.Active = false;
            }

            if (Game.GetCondition("vzal_vatu"))
            {
                this.VataItem.Visible = false;
                this.VataItem.Active = false;
            }

            ContentLayer.AddChild(VataItem);

            this.OknoItem = new OknoItem(1168.7f, 400.1f, 110, 100, new CCPoint(1213.727f, 400), Jenik.Directions.Up);
            this.OknoItem.Active = false;
            ContentLayer.AddChild(OknoItem);

            //Pridam dedka
            var dedek = new Dedek();
            if (!this.Jenik.Game.GetCondition("dedek_utekl"))
            {
                ContentLayer.AddChild(dedek);
                this.AddCharacterToScene(dedek);
            }
            
            //Pridam prcka
            var prcek = new Prcek();
            if (!this.Jenik.Game.GetCondition("prcek_utekl"))
            {
                ContentLayer.AddChild(prcek);
                this.AddCharacterToScene(prcek);
            }

            //Pridani Jenika
            ContentLayer.AddChild(Jenik.Sprite);

            //Pridani hernich predmetu pred Jenikem
            if (!Game.GetCondition("vzal_klacek"))
            {
                var klacekItem = new KlacekItem(1060, 336, 70, 70, new CCPoint(1068.5f, 377.5f), Jenik.Directions.Down);
                ContentLayer.AddChild(klacekItem);
            }

            //Popredi
            var foreground = new CCSprite("Graphics/Scenes/01_nadrazi/foreground");
            foreground.PositionX = CCScene.DefaultDesignResolutionSize.Width / 2;
            foreground.PositionY = CCScene.DefaultDesignResolutionSize.Height / 2;
            ContentLayer.AddChild(foreground);

            //Animovane vcely
            this.Bees = new CCSprite("Graphics/Scenes/01_nadrazi/vcely/0");
            if (!Jenik.Game.GetCondition("prcek_utekl"))
            {
                Bees.PositionX = 910;
                Bees.PositionY = 330;
            }
            else
            {
                Bees.PositionX = 468.9f;
                Bees.PositionY = 285.12f;
            }

            Bees.RunAction(new CCRepeatForever(new CCAnimate(new VcelyAnimation())));
            ContentLayer.AddChild(Bees);

            //Vcely hnizdo
            var hnizdoItem = new HnizdoItem(898, 315, 70, 75, new CCPoint(919.5f, 379), Jenik.Directions.Down);
            ContentLayer.AddChild(hnizdoItem);

            if (!Game.GetCondition("nadrazi_poprve"))
            {
                //Pridam predmety
                Jenik.AddItem("50");
                Jenik.AddItem("mapa");

                TalkSequence();

                Game.SetConditionValue("nadrazi_poprve", true);
            }
        }

        public override void InitializeSceneLanguage()
        {
            this.Description = this.Game.GetSceneDescription(this.Name);
            this.SceneSubtitles.Clear();
            XmlParser.LoadText("01_nadrazi", this.SceneSubtitles);
        }

        public async void TalkSequence()
        {
            Jenik.Busy = true;
            await Task.Delay(1500);
            await Jenik.Talk(1);
            await Jenik.Talk(2);
            await Jenik.Talk(3);
            await Jenik.Talk(4);
            await Jenik.Talk(5);
            Jenik.Busy = false;
        }
    }
}
