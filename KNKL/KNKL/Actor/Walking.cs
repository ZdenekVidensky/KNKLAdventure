using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor
{
    public partial class Jenik : CCNode
    {
        /// <summary>
        /// Metoda, ktera presune hrace na misto urceni
        /// </summary>
        /// <param name="destinationPoint"></param>
        public async Task GoTo(CCPoint destinationPoint)
        {
            if (Sprite.Position != destinationPoint)
            {
                //Nejdriv zastavim vsechny akce
                Sprite.StopAllActions();

                //Podle ciloveho bodu urcim smer chuze
                SetDirectionByTouch(destinationPoint);

                float distance = CCPoint.Distance(destinationPoint, Sprite.Position);
                float speed = Math.Abs(distance / 400);

                if (speed < 0.5f)
                {
                    speed = 0.5f;
                }

                if (this.Direction == Directions.Up)
                {
                    speed = speed * 2;
                }


                //Urcim animaci chuze a posunu postavu
                var moveSpriteAction = new CCMoveTo(speed, destinationPoint);
                var walkAnimate = GetAnimateWalk();

                //Spustim animaci chuze a pusunu postavy
                Sprite.RunAction(new CCRepeatForever(walkAnimate));
                await Sprite.RunActionAsync(moveSpriteAction);
                Sprite.StopAllActions();
                SetIdleSprite();
            }
        }


        /// <summary>
        /// Metoda, ktera reaguje na dotyk obrazovky
        /// </summary>
        /// <param name="touches"></param>
        /// <param name="touchEvent"></param>
        void OnTouchScreen(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                CCLog.Log(string.Format("X: {0}, Y: {1}", touches[0].Location.X, touches[0].Location.Y));


                if (!this.Talking && !this.Busy && !this.Game.CurrentScene.InventoryVisible)
                {
                    if (Game.CurrentScene.WalkableArea.Hit(touches[0].Location))
                    {
                        //Zjistim a nastavim cil chuze
                        var destinationPoint = new CCPoint(touches[0].Location.X, (touches[0].Location.Y + (SPRITEHEIGHT * this.Sprite.ScaleY)));
                        this.GoTo(destinationPoint);
                    }
                }
                else if (this.Talking)
                {
                    this.Talking = false;
                }
            }
        }
    }
}
