using CocosDenshion;
using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Visuals
{
    public class CCButton : CCSprite
    {
        public bool Active { get; set; }
        public CCTexture2D HoverImage { get; set; }
        public CCTexture2D NormalImage { get; set; }
        public string ClickSound { get; set; }

        public Action Click;

        public CCButton(string imageName, string hoverImageName) : base(imageName)
        {
            Active = true;
            HoverImage = new CCTexture2D(hoverImageName);
            NormalImage = new CCTexture2D(imageName);

            ClickSound = "Sounds/clickSound";

            var buttonListener = new CCEventListenerTouchAllAtOnce();
            buttonListener.OnTouchesBegan = OnButtonTouchesBegan;
            buttonListener.OnTouchesEnded = OnButtonTouchesEnded;
            this.AddEventListener(buttonListener);
        }

        void OnButtonTouchesBegan(List<CCTouch> touches, CCEvent touchEvent) {
            if (touches.Count > 0 && this.BoundingBox.ContainsPoint(touches[0].Location)) {
                CCSimpleAudioEngine.SharedEngine.PlayEffect(ClickSound);
                this.Texture = HoverImage;
            }
                
        }
        void OnButtonTouchesEnded(List<CCTouch> touches, CCEvent touchEvent) {
            if (touches.Count > 0 && this.BoundingBox.ContainsPoint(touches[0].Location)) {
                Click();
                this.Texture = this.NormalImage;
            }
        }
    }
}
