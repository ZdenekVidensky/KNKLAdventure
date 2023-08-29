using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes.Nadrazi
{
    public class VcelyAnimation : CCAnimation
    {
        public VcelyAnimation()
        {
            for (int i = 0; i < 90; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Scenes/01_nadrazi/vcely/{0}", i)));
            }
            DelayPerUnit = 0.08f;
        }
    }
}
