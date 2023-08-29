using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNKL.Core.Visuals
{
    public class CCPolygon : CCNode
    {
        public CCPoint[] Points { get; set; }
        public List<CCRect> Rects { get; set; }
        public bool Walkable { get; private set; }

        public CCPolygon(bool walkable)
        {
            this.Walkable = walkable;
        }

        public void AddPoints(CCPoint[] points)
        {
            this.Points = points;
            this.Rects = new List<CCRect>();

            //Zjistim si pocet ctvercu
            int rectCount = this.Points.Count() / 4;

            int pointCount = 0;

            for (int i = 0; i < rectCount; i++)
            {
                var startPoint = Points[pointCount];
                var width = Points[pointCount + 1].X - Points[pointCount].X;
                var height = Points[pointCount + 2].Y - Points[pointCount].Y;

                this.Rects.Add(new CCRect(startPoint.X, startPoint.Y, width, height));

                pointCount += 4;
            }
        }

        public bool Hit(CCPoint touchPoint)
        {
            bool hitted = false;

            foreach(var rect in this.Rects) {
                hitted = rect.ContainsPoint(touchPoint);
            }

            return hitted;
        }
    }
}
