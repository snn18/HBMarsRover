using HBMarsRover.ConsoleApp.Contracts;
using HBMarsRover.ConsoleApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBMarsRover.ConsoleApp
{
    public class RoverAction
    {
        public static IRoverPosition Move(IRover rover)
        {
            IRoverPosition currentRoverPosition = rover.CurrentPosition;
            switch (rover.CurrentPosition.Direction)
            {
                case Direction.N:
                    if(rover.PlateauGrid.PosY < rover.CurrentPosition.Y + 1)
                    {
                        rover.ActionResult = false;
                    }
                    else
                    {
                        currentRoverPosition = new RoverPosition(rover.CurrentPosition.Direction, rover.CurrentPosition.X, rover.CurrentPosition.Y + 1 );
                    }

                    break;
                case Direction.S:
                    if(0 > rover.CurrentPosition.Y - 1)
                    {
                        rover.ActionResult = false;
                    }
                    else
                    {
                        currentRoverPosition = new RoverPosition(rover.CurrentPosition.Direction, rover.CurrentPosition.X, rover.CurrentPosition.Y-1);
                    }
                    break;
                case Direction.W:
                    if(0 > rover.CurrentPosition.X - 1)
                    {
                        rover.ActionResult = false;
                    }
                    else
                    {
                        currentRoverPosition = new RoverPosition(rover.CurrentPosition.Direction,rover.CurrentPosition.X - 1, rover.CurrentPosition.Y);
                    }
                    break;
                case Direction.E:
                    if(rover.PlateauGrid.PosX < rover.CurrentPosition.X + 1)
                    {
                        rover.ActionResult = false; 
                    }
                    else
                    {
                        currentRoverPosition = new RoverPosition(rover.CurrentPosition.Direction, rover.CurrentPosition.X + 1, rover.CurrentPosition.Y);
                    }
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return currentRoverPosition;
        }

        public static Direction TurnRight(Direction roverDirection)
        {
            Direction currentRoverDirection = roverDirection;

            switch (roverDirection)
            {
                case Direction.N:
                    currentRoverDirection = Direction.E;
                    break;
                case Direction.E:
                    currentRoverDirection = Direction.S;
                    break;
                case Direction.S:
                    currentRoverDirection = Direction.W;
                    break;
                case Direction.W:
                    currentRoverDirection = Direction.N;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            return currentRoverDirection;
        }

        public static Direction TurnLeft(Direction roverDirection)
        {
            Direction currentRoverDirection = roverDirection;

            switch (roverDirection)
            {
                case Direction.N:
                    currentRoverDirection = Direction.W;
                    break;
                case Direction.E:
                    currentRoverDirection = Direction.N;
                    break;
                case Direction.S:
                    currentRoverDirection = Direction.E;
                    break;
                case Direction.W:
                    currentRoverDirection = Direction.S;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            return currentRoverDirection;
        }
    }
}
