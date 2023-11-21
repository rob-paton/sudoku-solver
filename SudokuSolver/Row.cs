using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    internal class Row : Group
    { 
        public Row(Grid grid, (int, int) location) : base(grid, location)
        {
            this.Cells = new Cell[9];
            for (int i = 0; i < 9; i++)
            {
                this.Cells[i] = this.Grid.Cells[this.Location.Item1, this.Location.Item2 + i];
                this.Cells[i].Row = this;
            }
        }
    }
}
