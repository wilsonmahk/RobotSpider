using RobotSpider.Interface;
using RobotSpider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Commands
{
    public class TurnRightCommand : ICommand
    {
        public void Execute(Spider spider, Grid grid)
        {
            spider.TurnRight();
        }
    }
}
