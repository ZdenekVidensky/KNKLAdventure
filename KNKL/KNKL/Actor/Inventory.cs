using CocosSharp;
using KNKL.Core;
using KNKL.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor
{
    public partial class Jenik : CCNode
    {
        public List<InventoryItem> Inventory { get; set; }

        public void InitItems()
        {
            this.Inventory = new List<InventoryItem>();
        }

        /// <summary>
        /// Metoda, ktera provede animaci vziti predmetu a vlozi predmet do inventare
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task Take(string itemName, GameItem invoker = null)
        {
            this.Busy = true;
            await Sprite.RunActionAsync(this.GetAnimateTake1());

            if (invoker != null)
            {
                invoker.RemoveFromParent();
            }

            this.Inventory.Add(this.Game.GetItem(itemName));
            await Sprite.RunActionAsync(this.GetAnimateTake2());
            SetIdleSprite();
            this.Busy = false;
        }

        /// <summary>
        /// Metoda, ktera vlozi predmet do inventare podle jmena ve slovniku
        /// </summary>
        /// <param name="itemName"></param>
        public void AddItem(string itemName)
        {
            this.Inventory.Add(this.Game.GetItem(itemName));
        }

        /// <summary>
        /// Metoda, ktera vymaze predmet z inventare
        /// </summary>
        /// <param name="itemName"></param>
        public void DropItem(string itemName)
        {
            var item = this.Inventory.Where(m => m.Name == itemName).FirstOrDefault();
            this.Inventory.Remove(item);
            Game.CurrentScene.ActiveItemSprite.Texture = InventoryItem.NoItemTexture;
            Game.CurrentScene.ActiveItemSprite.Name = "noItem";
        }
    }
}
