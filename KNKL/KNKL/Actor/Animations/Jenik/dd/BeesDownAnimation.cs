using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor.Animations.Jenik.dd
{
    public class BeesDownAnimation : CCAnimation
    {
        public BeesDownAnimation()
        {
            for (int i = 1; i < 41; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Actors/Jenik/dd/bees/bees ({0})", i)));
            }
            DelayPerUnit = 0.08f;
        }
    }
}
