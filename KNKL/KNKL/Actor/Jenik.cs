using CocosDenshion;
using CocosSharp;
using KNKL.Actor.Animations.Jenik.dd;
using KNKL.Actor.Animations.Jenik.ll;
using KNKL.Actor.Animations.Jenik.rr;
using KNKL.Actor.Animations.Jenik.uu;
using KNKL.Core;
using KNKL.Core.Scene;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor
{
    public partial class Jenik : CCNode
    {
        const int EPSILON = 95;
        const int SPRITEHEIGHT = 140;

        public bool Busy { get; set; }

        public GameAdventure Game { get; set; }

        private static Jenik instance;

        public static Jenik Instance
        {
            get
            {
                if(instance == null) {
                    instance = new Jenik();
                }

                return instance;
            }
        }

        public Task InitializeJenik()
        {
            return Task.Run(() => {
                this.Game = GameAdventure.Instance;

                //Inicializuji Items
                this.InitItems();
                //Inicializuji Talking
                this.InitTalking();
                //Inicializuji Animations
                this.InitAnimations();
                //Inicializuji Direction
                this.InitDirection();

                //Nastavim naslouchani na dotyk obrazovky
                var jenikTouchListener = new CCEventListenerTouchAllAtOnce();
                jenikTouchListener.OnTouchesEnded = OnTouchScreen;
                this.Sprite.AddEventListener(jenikTouchListener); 
            });
        }

        /// <summary>
        /// Vrati pravdivostni hodnotu o tom, jestli zrovna hlavni postava nevykonava nejakou cinnost
        /// </summary>
        /// <returns></returns>
        public bool Ready()
        {
            return !this.Busy && !this.Game.CurrentScene.InventoryVisible && !this.Talking;
        }
    }
}
