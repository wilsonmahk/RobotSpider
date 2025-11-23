using RobotSpider.Models;
using RobotSpider.Processor;
using RobotSpider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Tests
{
    public class InputValidatorTests
    {
        [Theory]
        [InlineData("7 15", 7, 15)]
        [InlineData("0 0", 0, 0)]  
        public void TryParseGrid_ValidInput_ReturnsTrue(string input, int expectedX, int expectedY)
        {
            bool result = InputValidator.TryParseGrid(input, out Grid? grid);

            Assert.True(result);
            Assert.NotNull(grid);
            Assert.Equal(expectedX, grid!.MaxX);
            Assert.Equal(expectedY, grid.MaxY);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("FXL")]
        [InlineData("123")]
        [InlineData("FL R")]
        public void IsValidInstructionString_Invalid_ReturnsFalse(string input)
        {
            bool result = InputValidator.IsValidInstructionString(input);

            Assert.False(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("4 10")]
        [InlineData("4 10 Leeft")]
        [InlineData("4 10 2 3")]
        public void TryParseSpider_InvalidInput_ReturnsFalse(string input)
        {
            bool result = InputValidator.TryParseSpider(input, out Spider? spider);

            Assert.False(result);
            Assert.Null(spider);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("7")]
        [InlineData("7 a")]
        [InlineData("7 10 12")]
        public void TryParseGrid_InvalidInput_ReturnsFalse(string input)
        {
            bool result = InputValidator.TryParseGrid(input, out Grid? grid);

            Assert.False(result);
            Assert.Null(grid);
        }

        // -------------------- Grid decimal input --------------------
        [Theory]
        [InlineData("7.5 15")]
        [InlineData("7 10.2")]
        [InlineData("3.14 2.71")]
        public void TryParseGrid_DecimalInput_ReturnsFalse(string input)
        {
            bool result = InputValidator.TryParseGrid(input, out Grid? grid);

            Assert.False(result);
            Assert.Null(grid);
        }

        // -------------------- Spider decimal input --------------------
        [Theory]
        [InlineData("4.5 10 Left")]
        [InlineData("4 10.7 Up")]
        [InlineData("3.1 2.9 Right")]
        public void TryParseSpider_DecimalInput_ReturnsFalse(string input)
        {
            bool result = InputValidator.TryParseSpider(input, out Spider? spider);

            Assert.False(result);
            Assert.Null(spider);
        }

        [Fact]
        public void TryParseGrid_MaxInt_ReturnsTrue()
        {
            string input = $"{int.MaxValue} {int.MaxValue}";
            bool result = InputValidator.TryParseGrid(input, out Grid? grid);

            Assert.True(result);
            Assert.NotNull(grid);
            Assert.Equal(int.MaxValue, grid!.MaxX);
            Assert.Equal(int.MaxValue, grid.MaxY);
        }

        [Fact]
        public void TryParseGrid_AboveMaxInt_ReturnsFalse()
        {
            string input = $"{(long)int.MaxValue + 1} {int.MaxValue}";
            bool result = InputValidator.TryParseGrid(input, out Grid? grid);

            Assert.False(result);
            Assert.Null(grid);
        }

        [Fact]
        public void TryParseSpider_MaxInt_ReturnsTrue()
        {
            string input = $"{int.MaxValue} {int.MaxValue} Up";
            bool result = InputValidator.TryParseSpider(input, out Spider? spider);

            Assert.True(result);
            Assert.NotNull(spider);
            Assert.Equal(int.MaxValue, spider!.X);
            Assert.Equal(int.MaxValue, spider.Y);
            Assert.Equal(Direction.Up, spider.Direction);
        }

        [Fact]
        public void TryParseSpider_AboveMaxInt_ReturnsFalse()
        {
            string input = $"{(long)int.MaxValue + 1} {int.MaxValue} Up";
            bool result = InputValidator.TryParseSpider(input, out Spider? spider);

            Assert.False(result);
            Assert.Null(spider);
        } 
    }
}
