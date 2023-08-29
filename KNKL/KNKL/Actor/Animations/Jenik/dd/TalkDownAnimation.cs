using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.dd
{
    public class TalkDownAnimation : CCAnimation
    {
        public TalkDownAnimation()
        {
            for (int i = 0; i < 5; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/dd/talk/{0}", i)));
            }
            DelayPerUnit = 0.2f;
        }
    }
}
