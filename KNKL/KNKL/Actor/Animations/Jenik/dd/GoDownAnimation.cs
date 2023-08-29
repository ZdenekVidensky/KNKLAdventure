using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.dd
{
    public class GoDownAnimation : CCAnimation
    {
        public GoDownAnimation()
        {
            for(int i = 0; i < 21; i++) {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/dd/walk/{0}", i)));
            }
            DelayPerUnit = 0.04f;
        }
    }
}
