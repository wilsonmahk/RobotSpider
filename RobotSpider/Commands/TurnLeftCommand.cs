using RobotSpider.Interface;
using RobotSpider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Commands
{
    public class TurnLeftCommand : ICommand
    {
        public void Execute(Spider spider, Grid grid)
        {
            spider.TurnLeft();
        }
    }
}
