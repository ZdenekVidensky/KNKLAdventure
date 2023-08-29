using CocosSharp;
using KNKL.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Items
{
    public class InventoryItem : CCSprite
    {
        public string Description { get; set; }
        public DateTime TimeEnd { get; set; } 
        public DateTime TimeStart { get; set; }

        public static CCTexture2D NoItemTexture = new CCTexture2D("Graphics/Items/noItem");

        public InventoryItem (string name, string description) : base(string.Format("Graphics/Items/{0}", name))
        {
            this.TimeEnd = DateTime.Now;
            this.TimeStart = DateTime.Now;

            this.Name = name;
            this.Description = description;

            var listener = new CCEventListenerTouchAllAtOnce();
            listener.OnTouchesEnded = OnTouch;
            this.AddEventListener(listener);
        }

        public void OnTouch(List<CCTouch> touches, CCEvent touchEvent)
        {
            var jenik = Jenik.Instance;

            if (touches.Count > 0 && this.BoundingBoxTransformedToWorld.ContainsPoint(touches[0].Location))
            {
                this.TimeStart = this.TimeEnd;
                this.TimeEnd = DateTime.Now;

                //Jedno kliknuti
                jenik.Game.CurrentScene.ItemDescriptionLabel.Text = this.Description;
                jenik.Game.CurrentScene.ItemDescriptionLabel.PositionX = this.PositionX;
                jenik.Game.CurrentScene.ItemDescriptionLabel.PositionY = this.PositionY + 70;
                jenik.Game.CurrentScene.ItemDescriptionLabel.Visible = true;

                //Dvojklik
                if (this.TimeEnd.Subtract(this.TimeStart).Milliseconds < 200)
                {
                    jenik.Game.CurrentScene.ActiveItemSprite.Texture = this.Texture;
                    jenik.Game.CurrentScene.ActiveItemSprite.Scale = 0.5f;
                    jenik.Game.CurrentScene.ActiveItemSprite.Name = this.Name;
                    jenik.Game.CurrentScene.HideInventory();
                    jenik.Game.CurrentScene.ItemDescriptionLabel.Visible = false;
                }        
            }
        }
    }
}
