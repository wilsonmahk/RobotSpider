using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Models
{
    public class Spider
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        public Spider(int x, int y, Direction direction)
        {
            if (x < 0) throw new ArgumentOutOfRangeException(nameof(x), "X coordinate cannot be negative");
            if (y < 0) throw new ArgumentOutOfRangeException(nameof(y), "Y coordinate cannot be negative");
             
            X = x;
            Y = y;
            Direction = direction;
        }
        public void TurnLeft()
        {
            Direction = Direction switch
            {
                Direction.Up => Direction.Left,
                Direction.Left => Direction.Down,
                Direction.Down => Direction.Right,
                Direction.Right => Direction.Up,
                _ => Direction
            };
        }

        public void TurnRight()
        {
            Direction = Direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => Direction
            };
        }

        public void MoveForward()
        {
            switch (Direction)
            {
                case Direction.Up: Y += 1; break;
                case Direction.Down: Y -= 1; break;
                case Direction.Left: X -= 1; break;
                case Direction.Right: X += 1; break;
            }
        }

    }
}
