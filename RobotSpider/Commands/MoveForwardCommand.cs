using RobotSpider.Interface;
using RobotSpider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Commands
{
    public class MoveForwardCommand : ICommand
    {
        public void Execute(Spider spider, Grid grid)
        {
            var originalX = spider.X;
            var originalY = spider.Y;

            spider.MoveForward();

            // boundary check
            if (!grid.IsInside(spider.X, spider.Y))
            {
                spider.X = originalX;
                spider.Y = originalY; 
            }
        }
    }
}
