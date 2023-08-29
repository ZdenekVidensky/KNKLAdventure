using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Characters.Dedek
{
    public class DedekIdleAnimation : CCAnimation
    {
        public DedekIdleAnimation()
        {
            this.AddSpriteFrame(new CCSprite("Graphics/Characters/Dedek/idle"));
            DelayPerUnit = 0.2f;
        }
    }
}
