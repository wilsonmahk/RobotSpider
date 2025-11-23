using RobotSpider.Models;
using RobotSpider.Processor;
using RobotSpider.Services;

namespace RobotSpider
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Robot Spider Wall Exploration ===\n");
            Console.WriteLine("Instructions:");
            Console.WriteLine("1. Enter the wall size as two non-negative integers: MaxX MaxY (e.g., '7 15').");
            Console.WriteLine("   Negative numbers are not allowed.");
            Console.WriteLine("2. Enter the spider's starting location and direction:");
            Console.WriteLine("   X Y Direction (Up, Down, Left, Right).");
            Console.WriteLine("   The spider's starting location must be inside the grid and cannot be negative.");
            Console.WriteLine("3. Enter a sequence of instructions using only F, L, R:");
            Console.WriteLine("   F or f = move forward, L or l = turn left 90°, R or r = turn right 90°.\n");
            Console.WriteLine("The program will keep asking until valid inputs are provided.\n");
            while (true) // main loop to handle any exception
            {
                try
                {
                    // 1. Read grid
                    Grid grid = ReadGrid();

                    // 2. Read spider
                    Spider spider = ReadSpider();

                    // 3. Read instructions
                    string instructions = ReadInstructions();

                    // 4. Execute instructions
                    var processor = new InstructionProcessor();
                    processor.Execute(instructions, spider, grid);

                    // 5. Output final position
                    Console.WriteLine($"Spider final position: {spider.X} {spider.Y} {spider.Direction}");
                    break;  
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please try again from the beginning.");
                }
            }
        }

        private static Grid ReadGrid()
        {
            while (true)
            {
                Console.WriteLine("Enter wall size (e.g. '7 15'):");

                if (InputValidator.TryParseGrid(Console.ReadLine(), out var grid))
                    return grid;

                Console.WriteLine("Invalid wall size. Try again.\n");
            }
        }

        private static Spider ReadSpider()
        {
            while (true)
            {
                Console.WriteLine("Enter spider start (e.g. '4 10 Left'):");

                if (InputValidator.TryParseSpider(Console.ReadLine(), out var spider))
                    return spider;

                Console.WriteLine("Invalid spider start. Try again.\n");
            }
        }

        private static string ReadInstructions()
        {
            while (true)
            {
                Console.WriteLine("Enter instructions (e.g. 'FLFLFRFFLF'):");

                string? input = Console.ReadLine()?.Trim().ToUpper();

                if (InputValidator.IsValidInstructionString(input))
                    return input!;

                Console.WriteLine("Invalid instructions. Only F, L, R allowed.\n");
            }
        } 
    }
}
