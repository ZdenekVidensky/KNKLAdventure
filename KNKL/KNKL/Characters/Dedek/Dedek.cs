using CocosSharp;
using KNKL.Core;
using KNKL.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosDenshion;
using KNKL.Scenes.Nadrazi;

namespace KNKL.Characters.Dedek
{
    public class Dedek : Character
    {
        public CCAnimation RunningAnimation { get; set; }

        public Dedek(): base("dedek", "Promluvit s dědkem", 1220.145f, 440.5594f, "Graphics/Characters/Dedek/idle", Jenik.Directions.Right)
        {
            this.IdleAnimation = new DedekIdleAnimation();
            this.TalkAnimation = new DedekTalkAnimation();
            this.RunningAnimation = new DedekRunningAnimation();

            this.IdleTexture  = new CCTexture2D("Graphics/Characters/Dedek/idle");
            this.GoToPoint = new CCPoint(1103.193f, 390);

            this.Scale = 0.8f;
        }

        public override async Task TalkRoutine()
        {
            Jenik.Busy = true;

            if (Game.GetCondition("promluvil_s_dedkem"))
            {
                await Jenik.Talk(35);
                await this.Talk(36);
            }
            else
            {
                await Jenik.Talk(18);
                await this.Talk(19);
                await this.Talk(20);
                await Jenik.Talk(21);
                await this.Talk(22);
                await Jenik.Talk(23);
                await this.Talk(24);
                await Jenik.Talk(25);
                await this.Talk(26);
                await Jenik.Talk(27);
                await this.Talk(28);
                await Jenik.Talk(29);
                await Jenik.Talk(30);
                await this.Talk(31);
                await Jenik.Talk(32);
                await this.Talk(33);

                Jenik.ChangeDirection(Jenik.Directions.Down);
                await Jenik.Talk(34);
                Jenik.ChangeDirection(Jenik.Directions.Right);

                await Jenik.Talk(35);
                await this.Talk(36);

                Game.SetConditionValue("promluvil_s_dedkem", true);
            }

            Jenik.Busy = false;
        }

        public override async Task UseItemRoutine(string itemName)
        {
            if(itemName == "cukr_vata_s_muchami")
            {
                Jenik.Busy = true;

                await Jenik.GoTo(this.GoToPoint);
                Jenik.ChangeDirection(this.Direction);

                await Jenik.RunAnimationOnce("TakeRight1");
                Jenik.DropItem("cukr_vata_s_muchami");
                await Jenik.RunAnimationOnce("TakeRight2");

                //await Task.Delay(600);
                CCSimpleAudioEngine.SharedEngine.PlayEffect("Voice/dedek/aaaaaa");
                await Task.Delay(600);

                Jenik.Talk(37);
                await this.RunActionAsync(new CCAnimate(this.RunningAnimation));

                Game.CurrentScene.ContentLayer.RemoveChild(this);
                NadraziScene nadraziScene = (NadraziScene)Game.CurrentScene;
                nadraziScene.OknoItem.Active = true;

                Game.SetConditionValue("dedek_utekl", true);
                Jenik.Busy = false;
            }
        }
    }
}
