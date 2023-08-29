using CocosSharp;
using KNKL.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Scene
{
    public partial class GameScene : CCScene
    {
        //Inventar
        private CCLayer InventoryLayer { get; set; }
        private CCSprite InventoryFrame { get; set; }
        private CCLayer InventoryItemsLayer { get; set; }
        public bool InventoryVisible { get; set; }
        public CCPoint[] InventorySlots { get; set; }
        public CCSprite ActiveItemSprite { get; set; }
        public CCLabel ItemDescriptionLabel { get; set; }

        /// <summary>
        /// Zinicializuje Inventory Layer
        /// </summary>
        public void InitInventoryLayer()
        {
            //Inicializuji  jednotlive souradnice slotu inventare
            InventorySlots = new CCPoint[15];
            InitInventorySlots();

            //Sestavim inventar
            this.InventoryLayer = new CCLayer();
            this.InventoryFrame = new CCSprite("Graphics/Interface/InventoryFrame");
            this.InventoryItemsLayer = new CCLayer();
            this.InventoryFrame.PositionX = CCScene.DefaultDesignResolutionSize.Width / 2;
            this.InventoryFrame.PositionY = CCScene.DefaultDesignResolutionSize.Height / 2;

            this.InventoryLayer.AddChild(this.InventoryFrame);
            this.InventoryLayer.AddChild(this.InventoryItemsLayer);

            this.InventoryLayer.Visible = false;
            this.InventoryVisible = false;

            this.ItemDescriptionLabel = new CCLabel("", "arial", 35)
            {
                Color = CCColor3B.White,
                HorizontalAlignment = CCTextAlignment.Center
            };
            this.ItemDescriptionLabel.Visible = false;

            //Ovladani vybraneho predmetu
            var listener = new CCEventListenerTouchAllAtOnce();
            listener.OnTouchesEnded = OnActiveItemTouch;
            this.ActiveItemSprite.AddEventListener(listener);

            AddChild(this.InventoryLayer);
            InventoryLayer.AddChild(this.ItemDescriptionLabel);
            AddChild(this.MenuLayer);
        }

        /// <summary>
        /// Nastavi aktivni predmet podle nazvu.
        /// </summary>
        /// <param name="itemName"></param>
        public void SetActiveItem(string itemName)
        {
            if(itemName != "noItem")
            {
                var game = GameAdventure.Instance;
                var item = game.GetItem(itemName);

                this.ActiveItemSprite.Texture = item.Texture;
                this.ActiveItemSprite.Name = item.Name;
            }
            else
            {
                this.ActiveItemSprite.Texture = InventoryItem.NoItemTexture;
                this.ActiveItemSprite.Name = "noItem";
            }
        }

        /// <summary>
        /// Reakce na kliknuti na aktivni predmet v ramecku aktivniho predmetu.
        /// </summary>
        /// <param name="touches"></param>
        /// <param name="touchEvent"></param>
        public void OnActiveItemTouch(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0 && this.ActiveItemSprite.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location))
            {
                this.ActiveItemSprite.Name = "noItem";
                this.ActiveItemSprite.Texture = InventoryItem.NoItemTexture;
            }
        }

        /// <summary>
        /// Po kliknuti na tlacitko inventare
        /// </summary>
        public void OnInventoryButtonClick()
        {
            if (this.InventoryVisible)
            {
                this.InventoryLayer.Visible = false;
                this.InventoryVisible = false;
                this.ItemDescriptionLabel.Visible = false;
            }
            else
            {
                RefreshInventoryItems();
                this.InventoryLayer.Visible = true;
                this.InventoryVisible = true;
                this.ItemDescriptionLabel.Visible = false;
            }
        }

        /// <summary>
        /// Skryje inventarove okenko
        /// </summary>
        public void HideInventory()
        {
            this.InventoryLayer.Visible = false;
            this.InventoryVisible = false;
        }

        /// <summary>
        /// Znovu projde predmety v inventari a sestavi ho graficky.
        /// </summary>
        public void RefreshInventoryItems()
        {
            this.InventoryItemsLayer.RemoveAllChildren();
            //Vymazu vsechny predmety z teto vrstvy
            for(int i = 0; i < this.Jenik.Inventory.Count; i++)
            {
                if(i < 15)
                {
                    var item = this.Jenik.Inventory[i];

                    this.InventoryItemsLayer.AddChild(item);
                    item.Position = this.InventorySlots[i];
                }
            }
        }

        /// <summary>
        /// Naplni pole souradnicemi jednotilvych slotu v inventari
        /// </summary>
        public void InitInventorySlots()
        {
            //Prvni radek
            InventorySlots[0] = new CCPoint(308, 536);
            InventorySlots[1] = new CCPoint(477, 536);
            InventorySlots[2] = new CCPoint(646, 536);
            InventorySlots[3] = new CCPoint(815, 536);
            InventorySlots[4] = new CCPoint(984, 536);

            //Druhy radek
            InventorySlots[5] = new CCPoint(308, 367);
            InventorySlots[6] = new CCPoint(477, 367);
            InventorySlots[7] = new CCPoint(646, 367);
            InventorySlots[8] = new CCPoint(815, 367);
            InventorySlots[9] = new CCPoint(984, 367);

            //Treti radek
            InventorySlots[10] = new CCPoint(308, 198);
            InventorySlots[11] = new CCPoint(477, 198);
            InventorySlots[12] = new CCPoint(646, 198);
            InventorySlots[13] = new CCPoint(815, 198);
            InventorySlots[14] = new CCPoint(984, 198);
        }
    }
}
