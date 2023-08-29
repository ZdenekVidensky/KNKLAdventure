using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Visuals
{
    public class CCSlider : CCLayer
    {
        const float SL_WIDTH = 273f;
        const float SL_HEIGHT = 50f;
        private CCSprite _valueSprite { get; set; }
        public float Value { get; set; }
        public Action ActionOnMove { get; set; }

        private CCRect _touchBox { get; set; }
        public CCSlider(float value)
        {
            var sliderSprite = new CCSprite("Graphics/Scenes/00_menu/OptionsItems/slider");
            _valueSprite = new CCSprite("Graphics/Scenes/00_menu/OptionsItems/circle");
            _valueSprite.PositionX = ValueToPositionX(value);
            _valueSprite.PositionY = 0;

            var sliderListener = new CCEventListenerTouchAllAtOnce();
            sliderListener.OnTouchesMoved = OnSliderMoved;
            this._valueSprite.AddEventListener(sliderListener);

            this.AddChild(sliderSprite);
            this.AddChild(_valueSprite);

            ValueToPositionX(value);
        }

        /// <summary>
        /// Metoda, ktera nastavi touchBox. Dava se az po konstruktoru kvuli pozici celeho slideru.
        /// </summary>
        public void CreateTouchBox()
        {
            //Nastavim udaje TouchBoxu
            this._touchBox = new CCRect(this.PositionX - (SL_WIDTH/2), this.PositionY - (SL_HEIGHT/2), SL_WIDTH, SL_HEIGHT);
            CCDrawNode draw = new CCDrawNode();
            this.AddChild(draw);
            draw.DrawRect(this._touchBox, CCColor4B.Red);
        }

        void OnSliderMoved(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0 && this._touchBox.ContainsPoint(touches[0].Location))
            {
                if(touches[0].Location.X > (this.PositionX - SL_WIDTH/2) &&
                    touches[0].Location.X < (this.PositionX + SL_WIDTH/2))
                {
                    var positionX = (touches[0].Location.X - this.PositionX);
                    this._valueSprite.PositionX = positionX;
                    this.Value = PositionToValue(positionX);
                    this.ActionOnMove();
                }
            }
        }

        /// <summary>
        /// Vrati 
        /// </summary>
        /// <param name="position"></param>
        /// <returns>float</returns>
        private float PositionToValue(float position)
        {
            float result;
            if(position > 0)
            {
                result = (position / (SL_WIDTH/100)) + 50;
            }
            else if(position < 0)
            {
                result = 50 - (Math.Abs(position) / (SL_WIDTH/100));
            }
            else
            {
                result = 50;
            }

            return result / 100;
        }

        /// <summary>
        /// Metoda, ktera vrati X souradnici podle zadane hodnoty hlasitosti
        /// </summary>
        /// <returns></returns>
        private float ValueToPositionX(float value)
        {
            if(value > 0.5f)
            {
                return (value - 0.5f) * SL_WIDTH;
            }
            else if(value < 0.5f)
            {
                return -(0.5f - value) * SL_WIDTH;
            }
            else
            {
                return 0;
            }
        }
    }
}
