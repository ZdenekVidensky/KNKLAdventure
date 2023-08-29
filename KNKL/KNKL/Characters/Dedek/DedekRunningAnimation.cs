using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Characters.Dedek
{
    public class DedekRunningAnimation : CCAnimation
    {
        public DedekRunningAnimation()
        {
            for (int i = 2; i < 56; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Characters/Dedek/Bees/bees ({0})", i)));
            }

            DelayPerUnit = 0.04f;
        }
    }
}
