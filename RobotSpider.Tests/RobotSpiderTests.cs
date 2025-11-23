using RobotSpider.Models;

namespace RobotSpider.Tests
{
    public class RobotSpiderTests
    {
        [Fact]
        public void Spider_TurnLeft_FromUp_ShouldBeLeft()
        {
            var spider = new Spider(0, 0, Direction.Up);
            spider.TurnLeft();
            Assert.Equal(Direction.Left, spider.Direction);
        } 

        [Fact]
        public void Spider_TurnRight_FromUp_ShouldBeRight()
        {
            var spider = new Spider(0, 0, Direction.Up);
            spider.TurnRight();
            Assert.Equal(Direction.Right, spider.Direction);
        }

        [Fact]
        public void Spider_MoveForward_Up_ShouldIncreaseY()
        {
            var spider = new Spider(1, 1, Direction.Up);
            spider.MoveForward();
            Assert.Equal(2, spider.Y);
            Assert.Equal(1, spider.X);
            Assert.Equal(Direction.Up, spider.Direction);
        }

        [Fact]
        public void Spider_ThrowsException_WhenXIsNegative()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Spider(-1, 0, Direction.Up));
            Assert.Equal("X coordinate cannot be negative (Parameter 'x')", ex.Message);
        }

        [Fact]
        public void Spider_ThrowsException_WhenYIsNegative()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Spider(0, -1, Direction.Up));
            Assert.Equal("Y coordinate cannot be negative (Parameter 'y')", ex.Message);
        } 
    }
}