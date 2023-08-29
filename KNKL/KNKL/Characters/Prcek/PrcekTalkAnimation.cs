using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Characters.Prcek
{
    public class PrcekTalkAnimation : CCAnimation
    {
        public PrcekTalkAnimation()
        {
            for (int i = 1; i < 5; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Characters/Prcek/Talk/talk ({0})", i)));
            }

            DelayPerUnit = 0.23f;
        }
    }
}
