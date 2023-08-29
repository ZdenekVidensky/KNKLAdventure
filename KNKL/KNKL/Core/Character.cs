using CocosSharp;
using KNKL.Actor;
using KNKL.Core.Media;
using KNKL.Core.Subtitles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core
{
    public class Character : CCSprite
    {
        public string Description { get; set; }
        protected CCAnimation TalkAnimation { get; set; }
        protected CCAnimation IdleAnimation { get; set; }
        public CCPoint GoToPoint { get; set; }
        public Jenik.Directions Direction { get; set; }
        public VoicePlayer CharacterPlayer { get; set; }
        public bool Busy { get; set; }
        public bool Talking { get; set; }
        public GameAdventure Game { get; set; }
        public Jenik Jenik { get; set; }
        public CCTexture2D IdleTexture { get; set; }

        public Character(string name, string description, float x, float y, string fileName, Jenik.Directions direction) : base(fileName)
        {
            this.Name = name;
            this.Description = description;
            this.PositionX = x;
            this.PositionY = y;
            this.Direction = direction;

            this.Busy = false;
            this.Talking = false;

            this.Game = GameAdventure.Instance;
            this.Jenik = Jenik.Instance;
            this.CharacterPlayer = new VoicePlayer();

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouch;
            this.AddEventListener(touchListener);
        }

        public async Task Talk(int id)
        {
            if (!this.Talking)
            {
                //Vytahnu soubor
                Subtitle subtitle = this.Game.CurrentScene.GetSubtitles(id);

                await Task.Delay(50);

                this.Talking = true;

                if (Game.AllowSubtitles)
                {
                    Game.CurrentScene.TextLabel.Text = subtitle.Text;
                    Game.CurrentScene.TextSprite.Visible = true;
                }

                CharacterPlayer.Open(subtitle.Sound);
                CharacterPlayer.Play();

                int times = subtitle.Text.Length;

                await TalkAnimationRoutine(times);

                CharacterPlayer.Stop();

                this.Texture = this.IdleTexture;

                Game.CurrentScene.TextSprite.Visible = false;
                this.Talking = false;
            }
        }

        public Task TalkAnimationRoutine(int times)
        {
            return Task.Run(() => {
                this.RunAction(new CCRepeat(new CCAnimate(this.TalkAnimation), Convert.ToUInt16(times / 4)));

                while (this.Talking && this.CharacterPlayer.Playing) { }

                this.StopAllActions();
            });
        }

        void OnTouch(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0 && this.BoundingBox.ContainsPoint(touches[0].Location) && !Jenik.Busy)
            {
                if(Game.CurrentScene.ActiveItemSprite.Name == "noItem")
                {
                    TouchRoutine();
                }
                else
                {
                    UseItemRoutine(Game.CurrentScene.ActiveItemSprite.Name);
                }
            }

            if (this.Talking)
            {
                this.Talking = false;
            }
        }

        /// <summary>
        /// Metoda, ktera spusti chozeni hlavni postavy a az po tom rutinu na mluveni
        /// </summary>
        /// <returns></returns>
        private async Task TouchRoutine()
        {
            await this.Jenik.GoTo(this.GoToPoint);
            this.Jenik.ChangeDirection(this.Direction);

            await this.TalkRoutine();
        }

        public virtual async Task TalkRoutine() { }
        public virtual async Task UseItemRoutine(string itemName)
        {
            await Jenik.CantDo();
        }
    }
}
