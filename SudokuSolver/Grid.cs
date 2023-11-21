using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    internal class Grid
    {
        public Cell[,] Cells { get; private set; }
        public Row[] Rows { get; private set; }
        public Column[] Columns { get; private set; }
        public Box[] Boxes { get; private set; }
        // If a method determines a cell's value, the cell is added to this list, to be updated after the method
        public LinkedList<Cell> PendingCells { get; private set; }

        public Grid() 
        {
            this.Cells = new Cell[9, 9];
            this.Rows = new Row[9];
            this.Columns = new Column[9];
            this.Boxes = new Box[9];
            this.PendingCells = new LinkedList<Cell>();

            this.CreateCells();
            this.CreateRows();
            this.CreateColumns();
            this.CreateBoxes();
        }

        public void AddPendingCell(Cell cell, int value)
        {
            if (!PendingCells.Contains(cell))
            {
                cell.PendingValue = value;
                PendingCells.AddFirst(cell);
            }
        }

        /// <summary>
        /// Sets each pending cell's value to its pending value
        /// </summary>
        public void ResolvePendingCells()
        {
            while (PendingCells.Count > 0) 
            {
                Cell cell = PendingCells.First();
                if (cell.Value == 0)
                    cell.SetValue(cell.PendingValue);
                PendingCells.Remove(cell);
            }
        }

        public bool IsComplete()
        {
            foreach (Cell cell in this.Cells)
                if (cell.Value == 0) 
                    return false;
            
            return true;
        }

        public bool IsCorrect()
        {
            foreach (Row row in this.Rows)
                if (!row.IsValid())
                    return false;

            foreach (Column column in this.Columns)
                if (!column.IsValid()) 
                    return false;

            foreach (Box box in this.Boxes)
                if (!box.IsValid()) 
                    return false;

            return true;
        }

        private void CreateCells()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    this.Cells[i, j] = new Cell(this, (i, j));
        }

        private void CreateRows()
        {
            for (int i = 0; i < 9; i++)
                this.Rows[i] = new Row(this, (i, 0));
        }

        private void CreateColumns()
        {
            for (int i = 0; i < 9; i++)
                this.Columns[i] = new Column(this, (0, i));
        }

        private void CreateBoxes()
        {
            int boxCount = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    this.Boxes[boxCount] = new Box(this, (i * 3, j * 3));
                    boxCount++;
                }
        }

        public void Print()
        {
            string borderHoriz = " +-+-------+-------+-------+-+-------+-------+-------+-+-------+-------+-------+-+";
            Console.WriteLine(borderHoriz);
            Console.WriteLine(borderHoriz);
            for (int row = 0; row < 9; row++)
            {
                for (int noteRow = 0; noteRow < 3; noteRow++)
                {
                    Console.Write(" | | ");
                    for (int cell = 0; cell < 9; cell++)
                    {
                        int value = this.Rows[row].Cells[cell].Value;
                        if (value != 0)
                        {
                            if (noteRow == 0 || noteRow == 2) 
                                Console.Write("      ");
                            else 
                                Console.Write($"  {value}   ");
                        }
                        else
                        {
                            for (int noteCol = 0; noteCol < 3; noteCol++)
                            {
                                int note = (noteRow * 3) + noteCol + 1;
                                if (this.Rows[row].Cells[cell].Notes.Contains(note)) 
                                    Console.Write($"{note} ");
                                else
                                    Console.Write("· ");

                            }
                        }
                        if ((cell + 1) % 3 == 0)
                            Console.Write("| | ");
                        else
                            Console.Write("| ");
                    }
                    Console.Write("\n");
                }
                Console.WriteLine(borderHoriz);
                if ((row + 1) % 3 == 0)
                    Console.WriteLine(borderHoriz);
            }
        }
    }
}
