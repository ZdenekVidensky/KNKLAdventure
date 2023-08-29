using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.rr
{
    public class TakeRight1Animation : CCAnimation
    {
        public TakeRight1Animation()
        {
            for (int i = 0; i < 21; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/rr/take/{0}", i)));
            }
            DelayPerUnit = 0.04f;
        }
    }
}
