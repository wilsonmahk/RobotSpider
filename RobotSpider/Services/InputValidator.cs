using RobotSpider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Services
{
    public static class InputValidator
    {
        public static bool TryParseGrid(string? input, out Grid? grid)
        {
            grid = null;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            var parts = input.Trim().Split(' ');

            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int maxX) &&
                int.TryParse(parts[1], out int maxY))
            {
                grid = new Grid(maxX, maxY);
                return true;
            }

            return false;
        }

        public static bool TryParseSpider(string? input, out Spider? spider)
        {
            spider = null;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            var parts = input.Trim().Split(' ');

            if (parts.Length == 3 &&
                int.TryParse(parts[0], out int x) &&
                int.TryParse(parts[1], out int y) &&
                Enum.TryParse(parts[2], true, out Direction direction))
            {
                spider = new Spider(x, y, direction);
                return true;
            }

            return false;
        }

        public static bool IsValidInstructionString(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            foreach (char c in input)
            {
                if (c != 'F' && c != 'L' && c != 'R')
                    return false;
            }

            return true;
        }
    }
}
