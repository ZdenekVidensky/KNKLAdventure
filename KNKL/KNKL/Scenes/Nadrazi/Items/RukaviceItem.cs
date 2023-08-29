using CocosSharp;
using KNKL.Actor;
using KNKL.Core;
using KNKL.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes.Nadrazi.Items
{
    public class RukaviceItem : GameItem
    {
        public RukaviceItem(float x, float y, float width, float height, CCPoint goToPosition, Jenik.Directions direction) : base(x, y, width, height, goToPosition, direction, true)
        {
            this.Texture = new CCTexture2D("Graphics/Items/rukavice");
            this.Scale = 0.4f;
        }

        public override async Task TouchRoutine()
        {
            await Jenik.GoTo(this.GoToPoition);
            Jenik.ChangeDirection(this.Direction);

            await Jenik.Talk(14);
            await Jenik.Take("rukavice", this);

            Game.SetConditionValue("vzal_rukavice", true);
        }
    }
}
