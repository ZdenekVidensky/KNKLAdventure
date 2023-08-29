using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.rr
{
    public class TalkRightAnimation : CCAnimation
    {
        public TalkRightAnimation()
        {
            for (int i = 1; i < 5; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/rr/talk/{0}", i)));
            }
            DelayPerUnit = 0.2f;
        }
    }
}
