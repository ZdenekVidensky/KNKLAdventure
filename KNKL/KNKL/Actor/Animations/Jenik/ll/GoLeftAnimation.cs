using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.ll
{
    public class GoLeftAnimation : CCAnimation
    {
        public GoLeftAnimation()
        {
            for (int i = 0; i < 10; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/ll/walk/{0}", i)));
            }
            DelayPerUnit = 0.09f;
        }
    }
}
