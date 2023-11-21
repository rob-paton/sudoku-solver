using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    internal class Column : Group
    { 
        public Column(Grid grid, (int, int) location) : base(grid, location)
        {
            this.Cells = new Cell[9];
            for (int i = 0; i < 9; i++)
            {
                this.Cells[i] = this.Grid.Cells[this.Location.Item1 + i, this.Location.Item2];
                this.Cells[i].Column = this;
            }
        }
    }
}
