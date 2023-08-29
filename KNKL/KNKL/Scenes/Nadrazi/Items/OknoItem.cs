using CocosSharp;
using KNKL.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Scenes.Nadrazi.Items
{
    public class OknoItem : GameItem
    {
        public OknoItem(float x, float y, float width, float height, CCPoint goToPosition, Actor.Jenik.Directions direction) : base(x, y, width, height, goToPosition, direction)
        {
            this.Visible = false;
        }

        public override async Task TouchRoutine()
        {
            await Jenik.GoTo(this.GoToPoition);
            Jenik.SetDirection(this.Direction);

            await Jenik.Talk(11);
            await Jenik.Take("strep");
            await Jenik.Talk(12);
            Jenik.ChangeDirection(Actor.Jenik.Directions.Down);
            await Jenik.Talk(13);
        }
    }
}
