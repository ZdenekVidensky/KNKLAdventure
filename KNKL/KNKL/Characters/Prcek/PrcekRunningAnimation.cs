﻿using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Characters.Prcek
{
    public class PrcekRunningAnimation : CCAnimation
    {
        public PrcekRunningAnimation()
        {
            for (int i = 1; i < 93; i++)
            {
                this.AddSpriteFrame(new CCSprite(string.Format("Graphics/Characters/Prcek/Running/running ({0})", i)));
            }

            DelayPerUnit = 0.04f;
        }
    }
}
