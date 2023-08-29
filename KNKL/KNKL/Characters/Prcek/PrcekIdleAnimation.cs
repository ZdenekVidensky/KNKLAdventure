using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Characters.Prcek
{
    public class PrcekIdleAnimation : CCAnimation
    {
        public PrcekIdleAnimation()
        {
            for (int i = 1; i < 71; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Characters/Prcek/Eating/eat ({0})", i)));
            }

            DelayPerUnit = 0.04f;
        }
    }
}
