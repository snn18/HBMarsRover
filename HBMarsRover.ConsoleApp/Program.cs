using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBMarsRover.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var addAnotherPlateau = true;

            while (addAnotherPlateau)
            {
                Console.WriteLine("Input :");
                var plateau = new PlateauGrid();
                while (!plateau.IsCheck)
                {
                    Console.WriteLine("Plateau area size : (5 5)");
                    Console.WriteLine(plateau.Initialize(Console.ReadLine()));
                }
                var addAnotherRover = true;

                while (addAnotherRover)
                {
                    var rover = new Rover();
                    rover.PlateauGrid = plateau;
                    while (!rover.IsCheckPosition)
                    {
                        Console.WriteLine("Rover position : (1 2 N)");
                        Console.WriteLine(rover.Initialize(Console.ReadLine()));
                    }
                    while (!rover.IsCheckCommand)
                    {
                        Console.WriteLine("Rover command : (LMRM)");
                        Console.WriteLine(rover.CommandParse(Console.ReadLine()));
                    }
                    plateau.Rovers.Add(rover);

                    Console.WriteLine("Do you want to deploy another rover ? (Y)");
                    var addAnotherRoverInput = Console.ReadLine();
                    if (addAnotherRoverInput.ToUpper() != "Y")
                    {
                        addAnotherRover = false;
                    }
                }
                Console.WriteLine("Output :");
                foreach (var rover in plateau.Rovers)
                {
                    var roverCommandManager = new RoverCommandManager(rover);
                    roverCommandManager.Rover = rover;

                    foreach (var roverCommand in rover.Commands)
                    {
                        roverCommandManager.AddCommand(roverCommand);
                    }

                    roverCommandManager.ProcessCommands();

                    Console.WriteLine(roverCommandManager.Rover.CurrentPosition.X+" " +
                      roverCommandManager.Rover.CurrentPosition.Y+" " +
                      roverCommandManager.Rover.CurrentPosition.Direction.ToString() + (roverCommandManager.Rover.ActionResult?"": " (this rover was stopped because it tried to move outside the plateau)"));
                }
                Console.WriteLine("Do you want to define another plateau? (Y)");
                if (Console.ReadLine().ToUpper() != "Y")
                {
                    addAnotherPlateau = false;
                }
            }
            Console.ReadKey();
        }
    }
}
