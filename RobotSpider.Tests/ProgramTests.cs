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
    public class ProgramTests
    {
        [Theory]
        // gridX, gridY, spiderX, spiderY, spiderDirection, instruction string, expectedX, expectedY, expectedDirection
        [InlineData(7, 15, 4, 10, "Left", "FLFLFRFFLF", 5, 7, "Right")]
        [InlineData(5, 5, 0, 0, "Up", "FFRFF", 2, 2, "Right")]
        [InlineData(10, 10, 5, 5, "Down", "LFFRFFL", 7, 3, "Right")]
        [InlineData(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, "Down", "FFFFFFFFFFFFFF", int.MaxValue, int.MaxValue - 14, "Down")]
        public void FullFlow_ValidInputs_ProcessesCorrectly(
      int gridX, int gridY,
      int spiderX, int spiderY, string spiderDirection,
      string instructions,
      int expectedX, int expectedY, string expectedDirection)
        {
            // Arrange
            string gridInput = $"{gridX} {gridY}";
            string spiderInput = $"{spiderX} {spiderY} {spiderDirection}";
            string instructionInput = instructions;

            // Act
            bool gridResult = InputValidator.TryParseGrid(gridInput, out Grid? grid);
            bool spiderResult = InputValidator.TryParseSpider(spiderInput, out Spider? spider);
            bool instructionResult = InputValidator.IsValidInstructionString(instructionInput);

            Assert.True(gridResult);
            Assert.NotNull(grid);
            Assert.True(spiderResult);
            Assert.NotNull(spider);
            Assert.True(instructionResult);

            var processor = new InstructionProcessor();
            processor.Execute(instructionInput, spider!, grid!);

            // Assert final state
            Assert.Equal(expectedX, spider!.X);
            Assert.Equal(expectedY, spider.Y);
            Assert.Equal(Enum.Parse<Direction>(expectedDirection), spider.Direction);
        }

        [Theory]
        [InlineData("7 15", "4 10 Left", "FLXFRFFLF")] // invalid instruction
        [InlineData("7 15", "4 10 Up", "F L F")]       // space in instruction
        [InlineData("7 15", "4 10 Down", "FFR1FF")]    // number in instruction
        [InlineData("7 15", "11 10 Down", "FFR1FF")]    // spider is not in grid  
        public void FullFlow_InvalidInstruction_ReturnsFalse(
      string gridInput, string spiderInput, string instructionInput)
        {
            // Arrange & Act
            bool gridResult = InputValidator.TryParseGrid(gridInput, out Grid? grid);
            bool spiderResult = InputValidator.TryParseSpider(spiderInput, out Spider? spider);
            bool instructionResult = InputValidator.IsValidInstructionString(instructionInput);

            // Assert
            Assert.True(gridResult);
            Assert.NotNull(grid);
            Assert.True(spiderResult);
            Assert.NotNull(spider);
            Assert.False(instructionResult); // should reject invalid instruction
        }
    }
}
