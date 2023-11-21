using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* TERMINOLOGY:
 * The Grid has 9 Rows, 9 Columns, and 9 Boxes, each having 9 Cells
 * A Group is a Row, Column, or Box
 * 3 horizontally adjacent Boxes are a Band; 3 vertically adjacent Boxes are a Stack
 * Initial defined values are Clues
 */

namespace SudokuSolver
{
    internal class Sudoku
    {
        private static int counter = 0;
        // Each Sudoku has a unique id in order of instatiation (tracked by counter above)
        public int ID { get; private set; }
        // Original puzzle grid
        public int[,] Puzzle { get; private set; }
        // Solution to the puzzle grid, with all cells filled
        public int[,] Solution { get; set; }

        public Sudoku(int[,] sudoku)
        {
            Sudoku.counter++;
            this.ID = Sudoku.counter;
            this.Puzzle = sudoku;
            Array.Copy(sudoku, this.Puzzle, sudoku.Length);
        }

        public void PrintPuzzle()
        {
            Console.WriteLine();
            Console.WriteLine($"Sudoku #{this.ID} Puzzle:");
            string borderHoriz = " +-------+-------+-------+";
            Console.WriteLine(borderHoriz);
            for (int i = 0; i < 9; i++)
            {
                Console.Write(" | ");
                for (int j = 0; j < 9; j++)
                {
                    int value = this.Puzzle[i, j];
                    if (value != 0)
                        Console.Write(value + " ");
                    else 
                        Console.Write("· ");

                    if ((j + 1) % 3 == 0) 
                        Console.Write("| ");
                }
                Console.WriteLine();
                if ((i + 1) % 3 == 0) 
                    Console.WriteLine(borderHoriz);
            }
        }

        public void PrintSolution()
        {
            Console.WriteLine();
            Console.WriteLine($"Sudoku #{this.ID} Solution:");
            if (this.Solution == null)
            {
                Console.WriteLine("\tERROR: This Sudoku has not been solved");
                return;
            }

            string borderHoriz = " +-------+-------+-------+";
            Console.WriteLine(borderHoriz);
            for (int i = 0; i < 9; i++)
            {
                Console.Write(" | ");
                for (int j = 0; j < 9; j++)
                {
                    int value = this.Solution[i, j];
                    if (value != 0)
                        Console.Write(value + " ");
                    else 
                        Console.Write("· ");
                    if ((j + 1) % 3 == 0) 
                        Console.Write("| ");
                }
                Console.WriteLine();
                if ((i + 1) % 3 == 0) 
                    Console.WriteLine(borderHoriz);
            }
        }
    }
}