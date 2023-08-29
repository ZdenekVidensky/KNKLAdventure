using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.uu
{
    public class TalkUpAnimation : CCAnimation
    {
        public TalkUpAnimation()
        {
            for (int i = 0; i < 3; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/uu/talk/{0}", i)));
            }
            DelayPerUnit = 0.2f;
        }
    }
}
