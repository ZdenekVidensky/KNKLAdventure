using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.rr
{
    public class GoRightAnimation : CCAnimation
    {
        public GoRightAnimation()
        {
            for (int i = 0; i < 10; i++)
            {
                var sprite = new CCSprite(string.Format("Graphics/Actors/Jenik/rr/walk/{0}", i));
                this.AddSpriteFrame(sprite);
            }

            DelayPerUnit = 0.09f;
        }
    }
}
