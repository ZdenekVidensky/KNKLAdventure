using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.dd
{
    public class TakeDown2Animation : CCAnimation
    {
        public TakeDown2Animation()
        {
            for (int i = 20; i >= 0; i--)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/dd/take/{0}", i)));
            }
            DelayPerUnit = 0.04f;
        }
    }
}
