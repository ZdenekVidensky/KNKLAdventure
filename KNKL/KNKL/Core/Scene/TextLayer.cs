using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Scene
{
    public partial class GameScene : CCScene
    {
        public CCSprite TextSprite { get; set; }
        public CCLabel TextLabel { get; set; }

        /// <summary>
        /// Metoda zinicializuje Text Layer
        /// </summary>
        public void InitTextLayer()
        {
            TextSprite = new CCSprite()
            {
                TextureRectInPixels = new CCRect(0, 0, 1280, 80),
                Color = new CCColor3B(54, 47, 45),
                Opacity = 200,
                PositionX = 0,
                PositionY = 0
            };

            TextLabel = new CCLabel("", "arial", 25)
            {
                Color = CCColor3B.White,
                PositionX = CCScene.DefaultDesignResolutionSize.Width / 2,
                PositionY = 40,
                HorizontalAlignment = CCTextAlignment.Center
            };

            //Zneviditelnim okenko s textem
            TextSprite.Visible = false;

            TextSprite.AddChild(TextLabel);
            MenuLayer.AddChild(TextSprite);
        }
    }
}
