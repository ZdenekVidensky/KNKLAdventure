using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes
{
    public class LoadingAnimation : CCAnimation
    {
        public LoadingAnimation()
        {
            for (int i = 0; i < 13; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Loading/{0}", i)));
            }
            DelayPerUnit = 0.08f;
        }
    }
}
