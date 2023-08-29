using CocosDenshion;
using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Visuals
{
    public class CCArrow : CCSprite
    {
        public Action Click { get; set; }
        public string ClickSound { get; set; }

        public CCArrow(string arrowFileName) : base(arrowFileName)
        {
            this.ClickSound = "Sounds/clickSound";

            var arrowListener = new CCEventListenerTouchAllAtOnce();
            arrowListener.OnTouchesEnded = OnArrowTouchesEnded;
            this.AddEventListener(arrowListener);
        }

        void OnArrowTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0 && this.BoundingBox.ContainsPoint(touches[0].Location))
            {
                //Spustim zvuk kliknuti
                CCSimpleAudioEngine.SharedEngine.PlayEffect(ClickSound);

                //Spustim akci Click
                Click();
            }
        }
    }
}
