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
    public class DvereItem : GameItem
    {
        public DvereItem(float x, float y, float width, float height, CCPoint goToPosition, Actor.Jenik.Directions direction) : base(x, y, width, height, goToPosition, direction)
        {
            this.Visible = false;
        }

        public override async Task TouchRoutine()
        {
            await Jenik.GoTo(this.GoToPoition);
            Jenik.ChangeDirection(this.Direction);

            await Jenik.Talk(6);
        }
    }
}
