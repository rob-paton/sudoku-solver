using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    internal class Box : Group
    { 
        public Box(Grid grid, (int, int) location) : base(grid, location)
        {
            this.Cells = new Cell[9];
            int cellCount = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.Cells[cellCount] = this.Grid.Cells[this.Location.Item1 + i, this.Location.Item2 + j];
                    this.Cells[cellCount].Box = this;
                    cellCount++;
                }
            }
        }
    }
}
