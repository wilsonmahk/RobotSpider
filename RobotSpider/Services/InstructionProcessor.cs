using RobotSpider.Commands;
using RobotSpider.Interface;
using RobotSpider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSpider.Processor
{
    public class InstructionProcessor
    {
        private readonly Dictionary<char, ICommand> _commands;

        public InstructionProcessor()
        {
            _commands = new Dictionary<char, ICommand>
        {
            { 'L', new TurnLeftCommand() },
            { 'R', new TurnRightCommand() },
            { 'F', new MoveForwardCommand() }
        };
        }

        public void Execute(string instructions, Spider spider, Grid grid)
        {
            if (!ValidateInstructions(instructions))
                throw new ArgumentException("Instructions can only contain F, L, R");

            // 2. Validate starting position
            if (!grid.IsInside(spider.X, spider.Y))
                throw new InvalidOperationException("Spider is outside grid boundaries");

            foreach (var c in instructions.ToUpper())
            {
                if (_commands.ContainsKey(c))
                {
                    _commands[c].Execute(spider, grid);
                }
            }
        } 

        private bool ValidateInstructions(string instructions)
        {
            instructions = instructions.ToUpper();
            foreach (char c in instructions)
            {
                if (c != 'F' && c != 'L' && c != 'R')
                    return false;
            }
            return true;
        }

    }
}
