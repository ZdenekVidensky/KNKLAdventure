using CocosSharp;
using KNKL.Actor;
using KNKL.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes.Nadrazi.Items
{
    public class VataItem : GameItem
    {
        public VataItem(float x, float y, float width, float height, CCPoint goToPosition, Jenik.Directions direction) : base(x, y, width, height, goToPosition, direction, true)
        {
            this.Texture = new CCTexture2D("Graphics/Items/cukr_vata");
            this.Scale = 0.6f;
        }

        public override async Task TouchRoutine()
        {
            await Jenik.GoTo(this.GoToPoition);
            Jenik.ChangeDirection(Jenik.Directions.Left);
            await Jenik.Talk(16);
        }
        public override async Task UseItemRoutine(string itemName)
        {
            if(itemName == "rukavice")
            {
                await Jenik.GoTo(this.GoToPoition);
                Jenik.ChangeDirection(Jenik.Directions.Left);

                await Jenik.Talk(17);
                await Jenik.Take("cukr_vata_s_muchami", this);

                Game.SetConditionValue("vzal_vatu", true);
            }
            else
            {
                await Jenik.CantDo();
            }
        }
    }
}
