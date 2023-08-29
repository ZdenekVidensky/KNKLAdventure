using CocosDenshion;
using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Visuals
{
    public class CCCheckBox : CCSprite
    {
        public bool Checked { get; set; }
        private CCTexture2D _checkedTexture { get; set; }
        private CCTexture2D _uncheckedTexture { get; set; }
        public string ClickSound { get; set; }

        public Action Click;

        public CCCheckBox(string checkedTextureFileName, string uncheckedTextureFileName, bool isChecked)
            : base((isChecked ? checkedTextureFileName : uncheckedTextureFileName))
        {
            this.Scale = 1.2f;
            this.Checked = isChecked;
            this._checkedTexture = new CCTexture2D(checkedTextureFileName);
            this._uncheckedTexture = new CCTexture2D(uncheckedTextureFileName);

            this.ClickSound = "Sounds/clickSound";

            var checkBoxListener = new CCEventListenerTouchAllAtOnce();
            checkBoxListener.OnTouchesEnded = OnCheckBoxTouchesEnded;
            this.AddEventListener(checkBoxListener);
        }

        void OnCheckBoxTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0 && this.BoundingBox.ContainsPoint(touches[0].Location))
            {
                //Spustim zvuk kliknuti
                CCSimpleAudioEngine.SharedEngine.PlayEffect(ClickSound);

                //Spustim akci Click
                Click();

                if (this.Checked)
                {
                    this.Checked = false;
                    this.Texture = this._uncheckedTexture;
                }
                else
                {
                    this.Checked = true;
                    this.Texture = this._checkedTexture;
                }
            }
        }
    }
}
