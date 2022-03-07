using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBMarsRover.ConsoleApp.Contracts
{
    public interface IPlateauGrid
    {
        int PosX { get; set; }
        int PosY { get; set; }
        bool IsCheck { get; set; }
        bool Initialize(string gridSizeInput);
        IList<IRover> Rovers { get; set; }
    }
}
