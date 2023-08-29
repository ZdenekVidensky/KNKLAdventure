using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Characters.Dedek
{
    public class DedekTalkAnimation : CCAnimation
    {
        public DedekTalkAnimation()
        {
            for (int i = 0; i < 4; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Characters/Dedek/Talk/{0}", i)));
            }

            DelayPerUnit = 0.23f;
        }
    }
}
