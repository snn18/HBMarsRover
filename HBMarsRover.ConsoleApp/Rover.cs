using HBMarsRover.ConsoleApp.Command;
using HBMarsRover.ConsoleApp.Contracts;
using HBMarsRover.ConsoleApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBMarsRover.ConsoleApp
{
    public class Rover : IRover
    {
        public IRoverPosition CurrentPosition { get; private set; }
        public IPlateauGrid PlateauGrid { get; set; }
        public IList<ICommand> Commands { get; set; }
        public bool IsCheckPosition { get; set; }
        public bool IsCheckCommand { get; set; }
        public bool ActionResult { get; set; }

        public Rover()
        {
            this.Commands = new List<ICommand>();
            this.CurrentPosition = new RoverPosition();
            this.PlateauGrid = new PlateauGrid();
            this.ActionResult = true;
        }

        public Rover(IRoverPosition roverPosition, IPlateauGrid plateauGrid)
        {
            this.CurrentPosition = roverPosition;
            this.PlateauGrid = plateauGrid;
            this.Commands = new List<ICommand>();
            this.ActionResult = true;
        }

        public void Move()
        {
            this.CurrentPosition = RoverAction.Move(this);
        }

        public void TurnLeft()
        {
            this.CurrentPosition.Direction = RoverAction.TurnLeft(this.CurrentPosition.Direction);
        }

        public void TurnRight()
        {
            this.CurrentPosition.Direction = RoverAction.TurnRight(this.CurrentPosition.Direction);
        }

        public bool Initialize(string roverPositionInput)
        {
            this.IsCheckPosition = false;
            var roverPosition = roverPositionInput.Split(' ');
            if (roverPosition.Length == 3)
            {
                try
                {
                    var x = int.Parse(roverPosition[0]);
                    var y = int.Parse(roverPosition[1]);

                    if (PlateauGrid.PosX >= x && x >= 0 && PlateauGrid.PosY >= y && y >= 0)
                    {
                        var direction = roverPosition[2].ToUpper();
                        if (direction == "N" || direction == "S" || direction == "E" || direction == "W")
                        {
                            this.CurrentPosition.Direction = (Direction)Enum.Parse(typeof(Direction), direction);
                            this.CurrentPosition.X = x;
                            this.CurrentPosition.Y = y;
                            this.IsCheckPosition = true;
                            return true;
                        }
                    }
                }
                catch (Exception)
                {
                    this.IsCheckPosition = false;
                }
            }
            return this.IsCheckPosition;
        }

        public bool CommandParse(string roverCommandInput)
        {
            this.Commands = new List<ICommand>();
            foreach (var letter in roverCommandInput.ToCharArray())
            {
                switch (char.ToUpper(letter))
                {
                    case 'L':
                        this.Commands.Add(new TurnLeftCommand(this));
                        break;
                    case 'R':
                        this.Commands.Add(new TurnRightCommand(this));
                        break;
                    case 'M':
                        this.Commands.Add(new MoveCommand(this));
                        break;
                    default:
                        this.IsCheckCommand = false;
                        return this.IsCheckCommand;
                }
            }
            if (this.Commands.Count > 0)
            {
                this.IsCheckCommand = true;
            }
            else
            {
                this.IsCheckCommand = false;
            }
            return this.IsCheckCommand;
        }

    }
}
