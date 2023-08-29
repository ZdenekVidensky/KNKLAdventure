using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.ll
{
    public class TalkLeftAnimation : CCAnimation
    {
        public TalkLeftAnimation()
        {
            for (int i = 1; i < 6; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/ll/talk/{0}", i)));
            }
            DelayPerUnit = 0.2f;
        }
    }
}
