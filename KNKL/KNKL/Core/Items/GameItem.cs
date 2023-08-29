using CocosSharp;
using KNKL.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Items
{
    public class GameItem : CCSprite
    {
        public CCPoint GoToPoition { get; set; }
        public Jenik.Directions Direction { get; set; }
        public bool Active { get; set; }
        public Jenik Jenik { get; set; }
        public GameAdventure Game { get; set; }
        public GameItem(float x, float y, float width, float height, CCPoint goToPosition, Actor.Jenik.Directions direction, bool hasTexture = false) : base()
        {
            if (!hasTexture) {
                this.TextureRectInPixels = new CCRect(x, y, width, height);
            }

            PositionX = x;
            PositionY = y;

            Direction = direction;

            var listener = new CCEventListenerTouchAllAtOnce();
            listener.OnTouchesEnded = OnTouch;
            this.AddEventListener(listener);

            this.GoToPoition = goToPosition;
            this.Active = true;

            this.Jenik = Jenik.Instance;
            this.Game = GameAdventure.Instance;
        }

        private void OnTouch(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0 && this.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location) && Jenik.Ready() && this.Active)
            {
                var activeItemName = this.Jenik.Game.CurrentScene.ActiveItemSprite.Name;

                if (activeItemName == "noItem")
                {
                    TouchRoutine();
                }
                else
                {
                    UseItemRoutine(activeItemName);
                }
            }
        }

        public virtual async Task TouchRoutine(){ }
        public virtual async Task UseItemRoutine(string itemName) {
            await Jenik.CantDo();
        }
    }
}
