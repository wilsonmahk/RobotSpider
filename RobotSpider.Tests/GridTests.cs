using RobotSpider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Tests
{
    public class GridTests
    {
        [Fact]
        public void Grid_ThrowsException_WhenMaxXIsNegative()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(-1, 5));
            Assert.Equal("MaxX cannot be negative (Parameter 'maxX')", ex.Message);
        }

        [Fact]
        public void Grid_ThrowsException_WhenMaxYIsNegative()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(5, -1));
            Assert.Equal("MaxY cannot be negative (Parameter 'maxY')", ex.Message);
        }

        [Fact]
        public void Grid_AllowsZeroDimensions()
        {
            var grid = new Grid(0, 0);
            Assert.Equal(0, grid.MaxX);
            Assert.Equal(0, grid.MaxY);
        }
    }
}
