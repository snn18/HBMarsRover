using Microsoft.VisualStudio.TestTools.UnitTesting;
using HBMarsRover.ConsoleApp;
using System;
using HBMarsRover.ConsoleApp.Enums;

namespace HBMarsRover.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestPlateauInitialize()
        {
            var plateau = new PlateauGrid();
            Assert.AreEqual(true, plateau.Initialize("5 7"));
            Assert.AreEqual(5,plateau.PosX);
            Assert.AreEqual(7,plateau.PosY);
            Assert.AreEqual(false,plateau.Initialize("5 A"));
            Assert.AreEqual(false, plateau.Initialize("55"));
            Assert.AreEqual(false, plateau.Initialize("55 "));
        }
        [TestMethod]
        public void TestRoverInitialize()
        {
            var plateau = new PlateauGrid(5,7); 
            var rover = new Rover();
            rover.PlateauGrid = plateau;
            Assert.AreEqual(true, rover.Initialize("2 1 N"));
            Assert.AreEqual(true, rover.Initialize("2 1 w"));
            Assert.AreEqual(true, rover.Initialize("3 2 E"));
            Assert.AreEqual(true, rover.Initialize("3 2 s"));
            Assert.AreEqual(3, rover.CurrentPosition.X);
            Assert.AreEqual(2, rover.CurrentPosition.Y);
            Assert.AreEqual(Direction.S, rover.CurrentPosition.Direction);
            Assert.AreEqual(false, rover.Initialize("5 5 A"));
            Assert.AreEqual(false, rover.Initialize("5 5 E "));
            Assert.AreEqual(false, rover.Initialize("55 E"));
            Assert.AreEqual(false, rover.Initialize("5 10 E"));
        }
        [TestMethod]
        public void TestRoverCommandParse()
        {
            var rover = new Rover(new RoverPosition(Direction.N, 1, 2), new PlateauGrid(5, 5));
            Assert.AreEqual(false, rover.CommandParse(""));
            Assert.AreEqual(false, rover.CommandParse("RLMrlm "));
            Assert.AreEqual(false, rover.CommandParse("RLMrlma"));
            Assert.AreEqual(true, rover.CommandParse("RLMrlm"));
            Assert.AreEqual(6, rover.Commands.Count);
        }
        [TestMethod]
        public void TestRoverActionMove()
        {
            var currentPosition=RoverAction.Move(new Rover(new RoverPosition(Direction.N, 1, 1), new PlateauGrid(5, 5)));
            Assert.AreEqual(Direction.N, currentPosition.Direction);
            Assert.AreEqual(1, currentPosition.X);
            Assert.AreEqual(2, currentPosition.Y); 
            currentPosition=RoverAction.Move(new Rover(new RoverPosition(Direction.E, 1, 1), new PlateauGrid(5, 5)));
            Assert.AreEqual(Direction.E, currentPosition.Direction);
            Assert.AreEqual(2, currentPosition.X);
            Assert.AreEqual(1, currentPosition.Y);
            currentPosition=RoverAction.Move(new Rover(new RoverPosition(Direction.S, 1, 1), new PlateauGrid(5, 5)));
            Assert.AreEqual(Direction.S, currentPosition.Direction);
            Assert.AreEqual(1, currentPosition.X);
            Assert.AreEqual(0, currentPosition.Y);
            currentPosition=RoverAction.Move(new Rover(new RoverPosition(Direction.W, 1, 1), new PlateauGrid(5, 5)));
            Assert.AreEqual(Direction.W, currentPosition.Direction);
            Assert.AreEqual(0, currentPosition.X);
            Assert.AreEqual(1, currentPosition.Y);

            var rover = new Rover(new RoverPosition(Direction.N, 5, 5), new PlateauGrid(5, 5));
            currentPosition = RoverAction.Move(rover);
            Assert.AreEqual(Direction.N, currentPosition.Direction);
            Assert.AreEqual(5, currentPosition.X);
            Assert.AreEqual(5, currentPosition.Y);
            Assert.AreEqual(false, rover.ActionResult);
            rover = new Rover(new RoverPosition(Direction.E, 5, 5), new PlateauGrid(5, 5));
            currentPosition = RoverAction.Move(rover);
            Assert.AreEqual(Direction.E, currentPosition.Direction);
            Assert.AreEqual(5, currentPosition.X);
            Assert.AreEqual(5, currentPosition.Y);
            Assert.AreEqual(false, rover.ActionResult);
            rover = new Rover(new RoverPosition(Direction.S, 0, 0), new PlateauGrid(5, 5));
            currentPosition = RoverAction.Move(rover);
            Assert.AreEqual(Direction.S, currentPosition.Direction);
            Assert.AreEqual(0, currentPosition.X);
            Assert.AreEqual(0, currentPosition.Y);
            Assert.AreEqual(false, rover.ActionResult);
            rover = new Rover(new RoverPosition(Direction.W, 0, 0), new PlateauGrid(5, 5));
            currentPosition = RoverAction.Move(rover);
            Assert.AreEqual(Direction.W, currentPosition.Direction);
            Assert.AreEqual(0, currentPosition.X);
            Assert.AreEqual(0, currentPosition.Y);
            Assert.AreEqual(false, rover.ActionResult);
        }
        [TestMethod]
        public void TestRoverActionTurnRight()
        {
            Assert.AreEqual(Direction.E, RoverAction.TurnRight(Direction.N));
            Assert.AreEqual(Direction.W, RoverAction.TurnRight(Direction.S));
            Assert.AreEqual(Direction.N, RoverAction.TurnRight(Direction.W));
            Assert.AreEqual(Direction.S, RoverAction.TurnRight(Direction.E));
        }
        [TestMethod]
        public void TestRoverActionTurnLeft()
        {
            Assert.AreEqual(Direction.N, RoverAction.TurnLeft(Direction.E));
            Assert.AreEqual(Direction.S, RoverAction.TurnLeft(Direction.W));
            Assert.AreEqual(Direction.W, RoverAction.TurnLeft(Direction.N));
            Assert.AreEqual(Direction.E, RoverAction.TurnLeft(Direction.S));
        }
        [TestMethod]
        public void TestRoverCommandManagerProcessCommands()
        {
            var rover = new Rover(new RoverPosition(Direction.N,1,2), new PlateauGrid(5, 5));
            rover.CommandParse("LMLMLMLMM");
            var roverCommandManager = new RoverCommandManager(rover);
            foreach (var roverCommand in rover.Commands)
            {
                roverCommandManager.AddCommand(roverCommand);
            }
            roverCommandManager.ProcessCommands();
            Assert.AreEqual(1,roverCommandManager.Rover.CurrentPosition.X);
            Assert.AreEqual(3,roverCommandManager.Rover.CurrentPosition.Y);
            Assert.AreEqual(Direction.N,roverCommandManager.Rover.CurrentPosition.Direction);
            var rover2 = new Rover(new RoverPosition(Direction.E,3,3), new PlateauGrid(5, 5));
            rover2.CommandParse("MMRMMRMRRMM");
            var roverCommandManager2 = new RoverCommandManager(rover2);
            foreach (var roverCommand in rover2.Commands)
            {
                roverCommandManager2.AddCommand(roverCommand);
            }
            roverCommandManager2.ProcessCommands();
            Assert.AreEqual(5, roverCommandManager2.Rover.CurrentPosition.X);
            Assert.AreEqual(1, roverCommandManager2.Rover.CurrentPosition.Y);
            Assert.AreEqual(Direction.E, roverCommandManager2.Rover.CurrentPosition.Direction);
            Assert.AreEqual(false, roverCommandManager2.Rover.ActionResult);
        }
    }
}
