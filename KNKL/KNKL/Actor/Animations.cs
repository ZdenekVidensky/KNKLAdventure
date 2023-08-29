using CocosSharp;
using KNKL.Actor.Animations.Jenik.dd;
using KNKL.Actor.Animations.Jenik.ll;
using KNKL.Actor.Animations.Jenik.rr;
using KNKL.Actor.Animations.Jenik.uu;
using KNKL.Core.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor
{
    public partial class Jenik : CCNode
    {
        //Sprity v klidovem stavu
        public CCTexture2D IdleUp { get; set; }
        public CCTexture2D IdleDown { get; set; }
        public CCTexture2D IdleRight { get; set; }
        public CCTexture2D IdleLeft { get; set; }
        public GameScene ActiveScene { get; set; }
        public Dictionary<string, CCAnimation> AnimationsList { get; set; }
        public CCSprite Sprite { get; set; }

        /// <summary>
        /// Metoda zinicializuje animace
        /// </summary>
        public void InitAnimations()
        {
            this.Sprite = new CCSprite("Graphics/Actors/Jenik/rr/idle");
            this.AnimationsList = new Dictionary<string, CCAnimation>();
            this.Sprite.PositionX = 200;
            this.Sprite.PositionY = 200;

            //Naplnim slovnik animacemi pro chuzi
            this.AnimationsList.Add("GoDown", new GoDownAnimation());
            this.AnimationsList.Add("GoRight", new GoRightAnimation());
            this.AnimationsList.Add("GoLeft", new GoLeftAnimation());
            this.AnimationsList.Add("GoUp", new GoUpAnimation());

            //Naplnim slovnik animacemi pro mluveni
            this.AnimationsList.Add("TalkDown", new TalkDownAnimation());
            this.AnimationsList.Add("TalkRight", new TalkRightAnimation());
            this.AnimationsList.Add("TalkLeft", new TalkLeftAnimation());
            this.AnimationsList.Add("TalkUp", new TalkUpAnimation());

            //Naplnim slovnik animacemi pro brani predmetu
            this.AnimationsList.Add("TakeDown1", new TakeDown1Animation());
            this.AnimationsList.Add("TakeDown2", new TakeDown2Animation());
            this.AnimationsList.Add("TakeRight1", new TakeRight1Animation());
            this.AnimationsList.Add("TakeRight2", new TakeRight2Animation());
            this.AnimationsList.Add("TakeLeft1", new TakeLeft1Animation());
            this.AnimationsList.Add("TakeLeft2", new TakeLeft2Animation());
            this.AnimationsList.Add("TakeUp1", new TakeUp1Animation());
            this.AnimationsList.Add("TakeUp2", new TakeUp2Animation());

            //Animace bouchani do hnizda
            this.AnimationsList.Add("BeesDown", new BeesDownAnimation());

            //Priradim sprity kazdemu klidovemu smeru
            this.IdleDown = new CCTexture2D("Graphics/Actors/Jenik/dd/idle");
            this.IdleUp = new CCTexture2D("Graphics/Actors/Jenik/uu/idle");
            this.IdleRight = new CCTexture2D("Graphics/Actors/Jenik/rr/idle");
            this.IdleLeft = new CCTexture2D("Graphics/Actors/Jenik/ll/idle");
        }

        /// <summary>
        /// Metoda, ktera spusti animaci podle jmena ze slovniku animaci a porad ji opakuje.
        /// </summary>
        /// <param name="animationName"></param>
        public void RunAnimation(string animationName, bool repeat = true)
        {
            CCAnimation animationValue;

            if (AnimationsList.TryGetValue(animationName, out animationValue))
            {
                Sprite.RunAction(new CCRepeatForever(new CCAnimate(animationValue)));
            }
        }

        public async Task RunAnimationOnce(string animationName)
        {
            CCAnimation animationValue;
            if (AnimationsList.TryGetValue(animationName, out animationValue))
            {
                await Sprite.RunActionAsync(new CCAnimate(animationValue));
            }

            this.SetIdleSprite();
        }

        /// <summary>
        /// Vrati animaci chuze podle smeru, na ktery je postava otocena
        /// </summary>
        /// <returns></returns>
        CCAnimate GetAnimateWalk()
        {
            CCAnimation animation;

            if (this.Direction == Directions.Left)
            {
                this.AnimationsList.TryGetValue("GoLeft", out animation);
            }
            else if (this.Direction == Directions.Right)
            {
                this.AnimationsList.TryGetValue("GoRight", out animation);
            }
            else if (this.Direction == Directions.Up)
            {
                this.AnimationsList.TryGetValue("GoUp", out animation);
            }
            else
            {
                this.AnimationsList.TryGetValue("GoDown", out animation);
            }

            return new CCAnimate(animation);
        }

        /// <summary>
        /// Vrati instanci CCAnimate animaci vziti predmetu, prvni cast, podle smeru postavy.
        /// </summary>
        /// <returns></returns>
        CCAnimate GetAnimateTake1()
        {
            CCAnimation animation;

            if (this.Direction == Directions.Left)
            {
                this.AnimationsList.TryGetValue("TakeLeft1", out animation);
            }
            else if (this.Direction == Directions.Right)
            {
                this.AnimationsList.TryGetValue("TakeRight1", out animation);
            }
            else if (this.Direction == Directions.Up)
            {
                this.AnimationsList.TryGetValue("TakeUp1", out animation);
            }
            else
            {
                this.AnimationsList.TryGetValue("TakeDown1", out animation);
            }

            return new CCAnimate(animation);
        }

        /// <summary>
        /// Vrati instanci CCAnimate animaci vziti predmetu, druhou cast, podle smeru postavy.
        /// </summary>
        /// <returns></returns>
        CCAnimate GetAnimateTake2()
        {
            CCAnimation animation;

            if (this.Direction == Directions.Left)
            {
                this.AnimationsList.TryGetValue("TakeLeft2", out animation);
            }
            else if (this.Direction == Directions.Right)
            {
                this.AnimationsList.TryGetValue("TakeRight2", out animation);
            }
            else if (this.Direction == Directions.Up)
            {
                this.AnimationsList.TryGetValue("TakeUp2", out animation);
            }
            else
            {
                this.AnimationsList.TryGetValue("TakeDown2", out animation);
            }

            return new CCAnimate(animation);
        }

        /// <summary>
        /// Vrati instanci CCAnimate animaci mluveni podle smeru postavy.
        /// </summary>
        /// <returns></returns>
        CCAnimate GetAnimateTalk()
        {
            CCAnimation animation;

            if (this.Direction == Directions.Left)
            {
                this.AnimationsList.TryGetValue("TalkLeft", out animation);
            }
            else if (this.Direction == Directions.Right)
            {
                this.AnimationsList.TryGetValue("TalkRight", out animation);
            }
            else if (this.Direction == Directions.Up)
            {
                this.AnimationsList.TryGetValue("TalkUp", out animation);
            }
            else
            {
                this.AnimationsList.TryGetValue("TalkDown", out animation);
            }

            return new CCAnimate(animation);
        }

        /// <summary>
        /// Nastavi sprite v klidovem stavu
        /// </summary>
        void SetIdleSprite()
        {
            if (this.Direction == Directions.Up)
            {
                this.Sprite.Texture = IdleUp;
            }
            else if (this.Direction == Directions.Down)
            {
                this.Sprite.Texture = IdleDown;
            }
            else if (this.Direction == Directions.Left)
            {
                this.Sprite.Texture = IdleLeft;
            }
            else
            {
                this.Sprite.Texture = IdleRight;
            }
        }
    }
}
