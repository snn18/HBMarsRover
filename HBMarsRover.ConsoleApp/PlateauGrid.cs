using HBMarsRover.ConsoleApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBMarsRover.ConsoleApp
{
    public class PlateauGrid : IPlateauGrid
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public bool IsCheck { get; set; }
        public IList<IRover> Rovers { get; set; }
        public PlateauGrid()
        {
            this.Rovers = new List<IRover>();
        }
        public PlateauGrid(int X, int Y)
        {
            PosX = X;
            PosY = Y;
        }

        public bool Initialize(string sizeInput)
        {
            this.IsCheck = false;
            if (!string.IsNullOrEmpty(sizeInput))
            {
                var size = sizeInput.Split(' ');
                if(size.Length == 2)
                {
                    int gridX;
                    if (int.TryParse(size[0], out gridX))
                    {
                        int gridY;
                        if (int.TryParse(size[1], out gridY))
                        {
                            this.PosX = gridX;
                            this.PosY = gridY;
                            this.IsCheck = true;
                        }
                    }
                }
            }
            return this.IsCheck;
        }
    }
}
