using RobotSpider.Models;
using RobotSpider.Processor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Tests
{
    public class InstructionProcessorTests
    {
        [Fact]
        public void Processor_ExecutesInstructionString_CorrectFinalPosition()
        {
            var grid = new Grid(7, 15);
            var spider = new Spider(4, 10, Direction.Left);
            var processor = new InstructionProcessor();

            processor.Execute("FLFLFRFFLF", spider, grid);

            Assert.Equal(5, spider.X);
            Assert.Equal(7, spider.Y);
            Assert.Equal(Direction.Right, spider.Direction);
        }

        [Fact]
        public void Processor_ExecutesInstructionString_CorrectFinalPositionInEdges()
        {
            var grid = new Grid(5, 5);
            var spider = new Spider(0, 5, Direction.Left);
            var processor = new InstructionProcessor();

            processor.Execute("FFFF", spider, grid);

            Assert.Equal(0, spider.X);
            Assert.Equal(5, spider.Y);
            Assert.Equal(Direction.Left, spider.Direction);
        }


        [Fact]
        public void Processor_ThrowsException_WhenInstructionStringIsInvalid()
        { 
            var grid = new Grid(5, 5);
            var spider = new Spider(1, 1, Direction.Up);
            var processor = new InstructionProcessor();

            string invalidInstructions = "FXLZ"; // X and Z are invalid
             
            var exception = Assert.Throws<ArgumentException>(() =>
                processor.Execute(invalidInstructions, spider, grid)
            );

            Assert.Equal("Instructions can only contain F, L, R", exception.Message);
        }

        [Fact]
        public void Processor_Executes_FFL_WithoutSpaces_Correctly()
        {
            // Arrange
            var grid = new Grid(5, 5);
            var spider = new Spider(0, 0, Direction.Up);
            var processor = new InstructionProcessor();

            string instructions = "FFL"; // valid string

            // Act
            processor.Execute(instructions, spider, grid);

            // Assert final state
            Assert.Equal(0, spider.X);          // X unchanged
            Assert.Equal(2, spider.Y);          // moved forward 2 steps
            Assert.Equal(Direction.Left, spider.Direction); // turned left
        }

        [Fact]
        public void Processor_Executes_MixedCaseInstructions_Correctly()
        {
            var grid = new Grid(5, 5);
            var spider = new Spider(0, 0, Direction.Up);
            var processor = new InstructionProcessor();

            string instructions = "fFlLrR"; // mixed case

            processor.Execute(instructions, spider, grid);

            Assert.Equal(0, spider.X);          
            Assert.Equal(2, spider.Y);           
            Assert.Equal(Direction.Up, spider.Direction); 
        }

        [Fact]
        public void Processor_ThrowsException_WhenInstructionStringContainsSpaces()
        {
            // Arrange
            var grid = new Grid(5, 5);
            var spider = new Spider(0, 0, Direction.Up);
            var processor = new InstructionProcessor();

            string instructions = "F F L"; // contains spaces, invalid

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                processor.Execute(instructions, spider, grid)
            );

            Assert.Equal("Instructions can only contain F, L, R", exception.Message);
        }

        [Fact]
        public void Processor_ThrowsException_IfSpiderStartsOutsideGrid()
        {
            var grid = new Grid(5, 5);
            var spider = new Spider(6, 0, Direction.Up); // X > MaxX
            var processor = new InstructionProcessor();

            var ex = Assert.Throws<InvalidOperationException>(() =>
                processor.Execute("FFL", spider, grid)
            );

            Assert.Equal("Spider is outside grid boundaries", ex.Message);
        }

        [Fact]
        public void Processor_TurnCommandsAtCorner_DoNotMoveSpider()
        {
            // Arrange
            var grid = new Grid(5, 5);
            var spider = new Spider(0, 0, Direction.Up); // bottom-left corner
            var processor = new InstructionProcessor();

            string instructions = "LRLR"; // alternating Left and Right turns

            // Act
            processor.Execute(instructions, spider, grid);

            // Assert position remains unchanged
            Assert.Equal(0, spider.X);
            Assert.Equal(0, spider.Y);
             
            Assert.Equal(Direction.Up, spider.Direction);
        }

        [Fact]
        public void Processor_Performance_WithLargeInstructionString()
        {
            // Arrange
            var grid = new Grid(1000, 1000);  
            var spider = new Spider(500, 500, Direction.Up); 
            var processor = new InstructionProcessor();

            // Create a large instruction string: 1 million commands
            string instructions = new string('F', 1_000_000);

            // Act
            var stopwatch = Stopwatch.StartNew();
            processor.Execute(instructions, spider, grid);
            stopwatch.Stop();

  
            // Check if it can be completed with 1 seconds
            Assert.True(stopwatch.ElapsedMilliseconds < 1000,
                $"Execution took too long: {stopwatch.ElapsedMilliseconds}ms");
             
            Assert.Equal(1000, spider.Y);  
        }

        [Fact]
        public void Processor_Performance_WithLargeMixedInstructions()
        {
            // Arrange
            var grid = new Grid(1000, 1000);          
            var spider = new Spider(500, 500, Direction.Up);  
            var processor = new InstructionProcessor();
             
            var instructionsBuilder = new System.Text.StringBuilder();
            string pattern = "FFLFRRFLF"; // repeated pattern
            for (int i = 0; i < 100_000; i++)  // total 900,000 commands
            {
                instructionsBuilder.Append(pattern);
            }
            string instructions = instructionsBuilder.ToString();
             
            var stopwatch = Stopwatch.StartNew();
            processor.Execute(instructions, spider, grid);
            stopwatch.Stop();

            // Check if it can be completed with 2 seconds
            Assert.True(stopwatch.ElapsedMilliseconds < 2000,
                $"Execution took too long: {stopwatch.ElapsedMilliseconds}ms");
             
            Assert.InRange(spider.X, 0, grid.MaxX);
            Assert.InRange(spider.Y, 0, grid.MaxY);
        }
    }
}
