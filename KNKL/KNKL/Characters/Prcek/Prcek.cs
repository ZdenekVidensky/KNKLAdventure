using KNKL.Core;
using KNKL.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KNKL.Characters.Prcek
{
    public class Prcek : Character
    {
        public CCAnimation RunningAnimation { get; set; }
        public Prcek() : base("prcek", "Promluvit s prckem", 665.36f, 357.42f, "Graphics/Characters/Prcek/idle", Jenik.Directions.Left)
        {
            this.GoToPoint = new CCPoint(745.68f, 392.8f);
            this.TalkAnimation = new PrcekTalkAnimation();
            this.IdleAnimation = new PrcekIdleAnimation();
            this.RunningAnimation = new PrcekRunningAnimation();

            this.IdleTexture = new CCTexture2D("Graphics/Characters/Prcek/idle");
            this.Scale = 1.3f;

            EatingRoutine();
        }

        /// <summary>
        /// Metoda, ktera bude vykonavat animaci jezeni cukrove vaty
        /// </summary>
        /// <returns></returns>
        public Task EatingRoutine()
        {
            return Task.Run(new Action(async () =>
            {
                while (true)
                {
                    await Task.Delay(5000);
                    if (!this.Busy)
                    {
                        await this.RunActionAsync(new CCAnimate(this.IdleAnimation));
                        this.StopAllActions();
                    }
                }
            }));
        }

        public override async Task TalkRoutine()
        {
            Jenik.Busy = true;
            this.Busy = true;

            //Pokud Prcek zrovna ji, pockame a zrusime mu vsechny akce
            this.StopAllActions();
            this.Texture = this.IdleTexture;

            if (!Game.GetCondition("promluvil_s_dedkem"))
            {
                await Jenik.Talk(38);
            }
            else if(Game.GetCondition("promluvil_s_dedkem") && !Game.GetCondition("promluvil_s_prckem"))
            {
                await Jenik.Talk(39);
                await this.Talk(40);
                await Jenik.Talk(41);
                await Jenik.Talk(42);
                await this.Talk(43);
                await Jenik.Talk(44);
                await this.Talk(45);
                await Jenik.Talk(46);
                await this.Talk(47);
                await Jenik.Talk(48);
                await this.Talk(49);
                await Jenik.Talk(50);
                await this.Talk(51);
                await Jenik.Talk(52);
                await this.Talk(53);

                Game.SetConditionValue("promluvil_s_prckem", true);
            }
            else
            {
                await Jenik.Talk(54);
            }

            Jenik.Busy = false;
            this.Busy = false;

            //Prcek muze znovu zacit jist
            EatingRoutine();
        }
    }
}
