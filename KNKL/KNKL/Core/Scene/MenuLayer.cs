using CocosSharp;
using KNKL.Core.Items;
using KNKL.Core.Visuals;
using KNKL.Scenes.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Scene
{
    public partial class GameScene : CCScene
    {
        private CCLayer MenuLayer { get; set; }
        private CCButton InventoryButton { get; set; }
        private CCButton MenuButton { get; set; }

        /// <summary>
        /// Metoda na inicializaci Menu Layeru
        /// </summary>
        public void InitMenuLayer()
        {
            MenuLayer = new CCLayer();

            InventoryButton = new CCButton("Graphics/Interface/InventoryButton", "Graphics/Interface/InventoryButton");
            InventoryButton.PositionX = (CCScene.DefaultDesignResolutionSize.Width - 170);
            InventoryButton.PositionY = (CCScene.DefaultDesignResolutionSize.Height - 75);
            InventoryButton.ClickSound = "Sounds/inventory_open";
            InventoryButton.Scale = 0.5f;
            InventoryButton.Click = () => {
                if (!Jenik.Busy) {
                    OnInventoryButtonClick();
                }
            };

            MenuButton = new CCButton("Graphics/Interface/MenuButton", "Graphics/Interface/MenuButton");
            MenuButton.PositionX = (CCScene.DefaultDesignResolutionSize.Width - 50);
            MenuButton.PositionY = (CCScene.DefaultDesignResolutionSize.Height - 75);
            MenuButton.Click = () => {
                if (!Jenik.Busy)
                {
                    Window.DefaultDirector.PushScene(new InGameMenuScene(this.MainWindow));
                }
            };

            var activeItemFrame = new CCSprite("Graphics/Interface/ActiveItemFrame");
            activeItemFrame.PositionX = 100;
            activeItemFrame.PositionY = (CCScene.DefaultDesignResolutionSize.Height - 75);

            ActiveItemSprite = new CCSprite("Graphics/Items/noItem");
            ActiveItemSprite.Name = "noItem";
            ActiveItemSprite.PositionX = 100;
            ActiveItemSprite.PositionY = (CCScene.DefaultDesignResolutionSize.Height - 72);

            //Vytvorim listener pro aktivni sprite
            var ActiveItemSpriteListener = new CCEventListenerTouchAllAtOnce();
            ActiveItemSpriteListener.OnTouchesEnded = OnActiveItemSpriteTouch;
            ActiveItemSprite.AddEventListener(ActiveItemSpriteListener);

            //Pridam do vrstvy
            MenuLayer.AddChild(InventoryButton);
            MenuLayer.AddChild(MenuButton);

            var activeItemLayer = new CCLayer();
            activeItemLayer.AddChild(activeItemFrame);
            activeItemLayer.AddChild(ActiveItemSprite);

            AddChild(activeItemLayer);
        }

        public void OnActiveItemSpriteTouch(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0 && ActiveItemSprite.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location) && !Jenik.Busy)
            {
                //Pokud je vybrany nejaky predmet, zrusim ho
                if (Jenik.Game.CurrentScene.ActiveItemSprite.Texture != InventoryItem.NoItemTexture)
                {
                    Jenik.Game.CurrentScene.ActiveItemSprite.Texture = InventoryItem.NoItemTexture;
                    Jenik.Game.CurrentScene.ActiveItemSprite.Name = "noItem";
                }
            }
        }
    }
}
