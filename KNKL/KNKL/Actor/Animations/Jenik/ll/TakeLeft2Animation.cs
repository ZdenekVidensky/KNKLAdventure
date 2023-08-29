using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.ll
{
    public class TakeLeft2Animation : CCAnimation
    {
        public TakeLeft2Animation()
        {
            for (int i = 20; i >= 0; i--)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/ll/take/{0}", i)));
            }
            DelayPerUnit = 0.04f;
        }
    }
}
