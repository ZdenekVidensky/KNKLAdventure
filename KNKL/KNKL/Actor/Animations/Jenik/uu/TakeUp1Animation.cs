using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.uu
{
    public class TakeUp1Animation : CCAnimation
    {
        public TakeUp1Animation()
        {
            for (int i = 0; i < 20; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/uu/take/{0}", i)));
            }
            DelayPerUnit = 0.04f;
        }
    }
}
