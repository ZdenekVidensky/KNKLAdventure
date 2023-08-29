using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Actor
{
    public partial class Jenik : CCNode
    {
        public Directions Direction { get; set; }
        public enum Directions { Left, Right, Up, Down }

        public Directions StringToDirection(string direction)
        {
            switch (direction)
            {
                case "Left":
                    return Directions.Left;
                case "Right":
                    return Directions.Right;
                case "Up":
                    return Directions.Up;
                default:
                    return Directions.Down;
            }
        }

        public void InitDirection()
        {
            this.Direction = Directions.Right;
        }

        /// <summary>
        /// Metoda, ktera na zaklade ciloveho bodu urci smer chuze postavy
        /// </summary>
        /// <param name="destinationPoint"></param>
        void SetDirectionByTouch(CCPoint destinationPoint)
        {
            CCPoint direction = destinationPoint - Sprite.Position;

            if (Math.Round(direction.X) < EPSILON && Math.Round(direction.X) > -EPSILON)
            {
                if (direction.Y > 0)
                {
                    this.Direction = Directions.Up;
                }
                else if (direction.Y < 0)
                {
                    this.Direction = Directions.Down;
                }
            }
            else if (Math.Round(direction.Y) == 0)
            {
                if (direction.X > 0)
                {
                    this.Direction = Directions.Right;
                }
                else if (direction.X < 0)
                {
                    this.Direction = Directions.Left;
                }
            }
            else if (direction.Y > 0)
            {
                if (direction.X > 0)
                {
                    this.Direction = Directions.Right;
                }
                else if (direction.X < 0)
                {
                    this.Direction = Directions.Left;
                }
            }
            else if (direction.Y < 0)
            {
                if (direction.X > 0)
                {
                    this.Direction = Directions.Right;
                }
                else if (direction.X < 0)
                {
                    this.Direction = Directions.Left;
                }
            }
        }

        /// <summary>
        /// Metoda, ktera zmeni dosavadni smer a nastavi Idle Sprite podle nej.
        /// </summary>
        /// <param name="direction"></param>
        public void ChangeDirection(Directions direction)
        {
            this.Direction = direction;
            SetIdleSprite();
        }

        /// <summary>
        /// Metoda, kterea zmeni dosavani smer.
        /// </summary>
        /// <param name="direction"></param>
        public void SetDirection(Directions direction)
        {
            this.Direction = direction;
        }
    }
}
