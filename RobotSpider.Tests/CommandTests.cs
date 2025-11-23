using RobotSpider.Commands;
using RobotSpider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Tests
{
    public class CommandTests
    {
        [Fact]
        public void MoveForwardCommand_MoveInsideGrid_ShouldUpdatePosition()
        {
            var grid = new Grid(5, 5);
            var spider = new Spider(2, 2, Direction.Up);
            var command = new MoveForwardCommand();

            command.Execute(spider, grid);

            Assert.Equal(3, spider.Y);  
            Assert.Equal(2, spider.X);  
        }

        [Fact]
        public void MoveForwardCommand_MoveOutsideGrid_ShouldNotChangePosition()
        {
            var grid = new Grid(5, 5);
            var spider = new Spider(5, 5, Direction.Up);
            var command = new MoveForwardCommand();

            command.Execute(spider, grid);

            Assert.Equal(5, spider.Y);
            Assert.Equal(5, spider.X);
        }

        [Fact]
        public void TurnLeftCommand_ShouldRotateCorrectly()
        {
            var spider = new Spider(0, 0, Direction.Up);
            var command = new TurnLeftCommand();
            command.Execute(spider, new Grid(5, 5));
            Assert.Equal(Direction.Left, spider.Direction);
        }
         
    }
}
