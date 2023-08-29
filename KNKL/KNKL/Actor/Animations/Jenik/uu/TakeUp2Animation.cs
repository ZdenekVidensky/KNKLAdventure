using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.uu
{
    public class TakeUp2Animation : CCAnimation
    {
        public TakeUp2Animation()
        {
            for (int i = 19; i >= 0; i--)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/uu/take/{0}", i)));
            }
            DelayPerUnit = 0.04f;
        }
    }
}
