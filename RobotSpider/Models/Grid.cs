using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Models
{
    public class Grid
    {
        public int MaxX { get; }
        public int MaxY { get; }

        public Grid(int maxX, int maxY)
        {
            if (maxX < 0) throw new ArgumentOutOfRangeException(nameof(maxX), "MaxX cannot be negative");
            if (maxY < 0) throw new ArgumentOutOfRangeException(nameof(maxY), "MaxY cannot be negative");

            MaxX = maxX;
            MaxY = maxY;
        }

        public bool IsInside(int x, int y)
        {
            return x >= 0 && x <= MaxX && y >= 0 && y <= MaxY;
        }
    }
}
