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
    public class KlacekItem : GameItem
    {
        public KlacekItem(float x, float y, float width, float height, CCPoint goToPosition, Actor.Jenik.Directions direction) : base(x, y, width, height, goToPosition, direction, true)
        {
            this.Texture = new CCTexture2D("Graphics/Items/klacek");
            this.Scale = 0.6f;
        }

        public override async Task TouchRoutine()
        {
            await Jenik.GoTo(this.GoToPoition);
            Jenik.SetDirection(this.Direction);

            await Jenik.Talk(10);
            await Jenik.Take("klacek", this);

            Game.SetConditionValue("vzal_klacek", true);
        }
    }
}
