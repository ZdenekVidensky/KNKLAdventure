using CocosDenshion;
using CocosSharp;
using KNKL.Actor;
using KNKL.Characters.Prcek;
using KNKL.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes.Nadrazi.Items
{
    public class HnizdoItem : GameItem
    {
        public HnizdoItem(float x, float y, float width, float height, CCPoint goToPosition, Actor.Jenik.Directions direction) : base(x, y, width, height, goToPosition, direction)
        {
            this.Visible = false;
        }

        public override async Task TouchRoutine()
        {
            await Jenik.GoTo(this.GoToPoition);
            Jenik.SetDirection(this.Direction);

            await Jenik.Talk(7);
        }

        public override async Task UseItemRoutine(string itemName)
        {
            if(itemName == "klacek")
            {
                await Jenik.GoTo(this.GoToPoition);
                Jenik.ChangeDirection(this.Direction);

                if (!Game.GetCondition("promluvil_s_prckem"))
                {
                    await Jenik.Talk(8);
                }
                else if(Game.GetCondition("promluvil_s_prckem"))
                {
                    Jenik.Busy = true;
                    await Jenik.RunAnimationOnce("BeesDown");

                    var character = Game.CurrentScene.GetCharacterFromScene("prcek");
                    Prcek prcek = ((Prcek)character);

                    NadraziScene nadraziScene = (NadraziScene)Game.CurrentScene;
                    
                    prcek.Busy = true;
                    prcek.StopAllActions();
                    prcek.RunAction(new CCAnimate(prcek.RunningAnimation));
                    prcek.PositionY -= 20;
                    prcek.PositionX -= 235;

                    CCSimpleAudioEngine.SharedEngine.PlayEffect("Voice/prcek/008");
                    await Task.Delay(500);
                    Jenik.Talk(9);
                    await Task.Delay(1700);

                    nadraziScene.VataItem.Visible = true;
                    nadraziScene.VataItem.Active = true;

                    nadraziScene.Bees.PositionX = 468.9f;
                    nadraziScene.Bees.PositionY = 285.12f;
                    await Task.Delay(1500);
                    nadraziScene.ContentLayer.RemoveChild(prcek);

                    Game.SetConditionValue("promluvil_s_prckem", false);
                    Game.SetConditionValue("prcek_utekl", true);
                    Jenik.Busy = false;
                }
            }
            else
            {
                await Jenik.CantDo();
            }
        }
    }
}
