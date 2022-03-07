using HBMarsRover.ConsoleApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBMarsRover.ConsoleApp.Contracts
{
    public interface IRoverPosition
    {
        int X { get; set; }
        int Y { get; set; }
        Direction Direction { get; set; }
    }
}
