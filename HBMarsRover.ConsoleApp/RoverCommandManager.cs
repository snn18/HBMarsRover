using HBMarsRover.ConsoleApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBMarsRover.ConsoleApp
{
    public class RoverCommandManager : IRoverCommandManager
    {
        public IRover Rover { get; set; }
        private Queue<ICommand> commands = new Queue<ICommand>();

        public RoverCommandManager(IRover rover)
        {
            this.Rover = rover;
        }

        public void AddCommand(ICommand command)
        {
            commands.Enqueue(command);
        }

        public void ProcessCommands()
        {
            while (commands.Count > 0)
            {
                ICommand command = commands.Dequeue();
                if (Rover.ActionResult)
                {
                    command.Process();
                }
            }
        }
    }
}