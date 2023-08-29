using CocosSharp;
using KNKL.Core;
using KNKL.Core.Media;
using KNKL.Core.Subtitles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor
{
    public partial class Jenik : CCNode
    {
        public bool Talking { get; set; }

        public VoicePlayer JenikPlayer { get; set; }

        public void InitTalking()
        {
            this.JenikPlayer = new VoicePlayer();
            this.Talking = false;
        }

        /// <summary>
        /// Spusti jeden z hlasek o tom, ze nejde provest kombinaci predmetu
        /// </summary>
        /// <returns></returns>
        public async Task CantDo()
        {
            await Talk("Nejde to.", "Voice/nejde_to");
        }

        public async Task Talk(string text, string soundName)
        {
            if (!this.Talking)
            {
                await Task.Delay(50);

                this.Talking = true;

                if (Game.AllowSubtitles)
                {
                    Game.CurrentScene.TextLabel.Text = text;
                    Game.CurrentScene.TextSprite.Visible = true;
                }

                JenikPlayer.Open(soundName);
                JenikPlayer.Play();

                int times = text.Length;

                await TalkAnimationRoutine(times);

                JenikPlayer.Stop();
                this.SetIdleSprite();

                Game.CurrentScene.TextSprite.Visible = false;
                this.Talking = false;
            }
        }

        public async Task Talk(int id)
        {
            if (!this.Talking)
            {
                await Task.Delay(50);

                this.Talking = true;

                //Vytahnu soubor
                Subtitle subtitle = this.Game.CurrentScene.GetSubtitles(id);

                if (Game.AllowSubtitles)
                {
                    Game.CurrentScene.TextLabel.Text = subtitle.Text;
                    Game.CurrentScene.TextSprite.Visible = true;
                }

                JenikPlayer.Open(subtitle.Sound);
                JenikPlayer.Play();

                int times = subtitle.Text.Length;

                await TalkAnimationRoutine(times);

                JenikPlayer.Stop();
                this.SetIdleSprite();

                Game.CurrentScene.TextSprite.Visible = false;
                this.Talking = false;
            }
        }

        public Task TalkAnimationRoutine(int times)
        {
            return Task.Run(() => {
                this.Sprite.RunAction(new CCRepeat(this.GetAnimateTalk(), Convert.ToUInt16(times / 6)));

                while (this.Talking && this.JenikPlayer.Playing) { }

                this.Sprite.StopAllActions();
            });
        }
    }
}
