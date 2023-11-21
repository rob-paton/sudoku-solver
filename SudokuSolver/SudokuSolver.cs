using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    internal class SudokuSolver
    {
        /// <summary>
        /// Takes a Sudoku object, and annotates the Puzzle's empty cells, printing a fully annotated grid to console.<br/>
        /// <br/>
        /// For simplicity, assumes a 9x9 sudoku grid, with only 1 possible solution
        /// </summary>
        /// <param name="sudoku"></param>
        public void Annotate(Sudoku sudoku)
        {
            Console.WriteLine($"\nAnnotating Sudoku #{sudoku.ID} . . .");

            Grid grid = this.CreateAnnotatedGrid(sudoku);

            Console.WriteLine($"\nSudoku #{sudoku.ID} Annotated Puzzle:");
            grid.Print();
        }

        /// <summary>
        /// Takes a Sudoku object, and attempts to solve its Puzzle property.<br/>
        /// - If successful, the solution will be stored in the Sudoku object's Solution Property.<br/>
        /// - If unsuccessful, an annotated grid will be printed to console, showing the solver's progress.<br/>
        /// <br/>
        /// For simplicity, assumes a 9x9 sudoku grid, with only 1 possible solution
        /// </summary>
        /// <param name="sudoku"></param>
        public void Solve(Sudoku sudoku)
        {
            Console.WriteLine($"\nSolving Sudoku #{sudoku.ID} . . .");
            if (this.IsSolved(sudoku))
                return;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            Grid grid = this.CreateAnnotatedGrid(sudoku);
            this.SolveGrid(grid);

            timer.Stop();
            Console.WriteLine($"\t[Solved in {timer.Elapsed.TotalSeconds} seconds]");

            // If grid incomplete or incorrect, print current grid progress
            if (!this.CheckGrid(grid, sudoku))
            {
                Console.WriteLine($"\nSudoku #{sudoku.ID} Partial Solution:");
                grid.Print();
            } 
        }

        /// <summary>
        /// Takes an array of Sudoku objects, and attempts to solve each one's Puzzle property.<br/>
        /// - If successful, the solution will be stored in the Sudoku object's Solution property.<br/>
        /// - If unsuccessful, the Sudoku's Solution will be left null.<br/>
        /// <br/>
        /// For simplicity, assumes a 9x9 sudoku grid, with only 1 possible solution
        /// </summary>
        /// <param name="sudoku"></param>
        public void SolveAll(Sudoku[] sudokus)
        {
            int sudokuCount = sudokus.Length;
            int counter = 0;
            foreach (Sudoku sudoku in sudokus)
            {
                counter++;
                Console.WriteLine($"\nSolving Sudoku #{sudoku.ID} ({counter}/{sudokuCount}) . . .");
                if (this.IsSolved(sudoku))
                    continue;

                Stopwatch timer = new Stopwatch();
                timer.Start();

                Grid grid = this.CreateAnnotatedGrid(sudoku);
                this.SolveGrid(grid);

                timer.Stop();
                Console.WriteLine($"\t[Solved in {timer.Elapsed.TotalSeconds} seconds]");

                // Check if grid is incomplete or incorrect, but don't print failed grids to avoid spamming console
                this.CheckGrid(grid, sudoku);
            }
        }

        /// <summary>
        /// Checks if Sudoku already has a solution, reporting error in console if it does
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns>true if Sudoku already has a solution; false if it doesn't</returns>
        private bool IsSolved(Sudoku sudoku)
        {
            if (sudoku.Solution != null)
            {
                Console.WriteLine("\tERROR: Sudoku already has a solution.");
                return true;
            }
            return false;
        }

        private Grid CreateAnnotatedGrid(Sudoku sudoku)
        {
            Console.Write("\tCreating grid ...");
            Grid grid = new Grid();
            Console.WriteLine(" done.");

            // Insert clues into grid
            Console.Write("\tInserting clues ...");
            this.InsertClues(grid, sudoku);
            Console.WriteLine(" done.");

            return grid;
        }

        /// <summary>
        /// Insert clues into grid, causing empty cells to update their notes
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="sudoku"></param>
        private void InsertClues(Grid grid, Sudoku sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int value = sudoku.Puzzle[i, j];
                    if (value != 0)
                        grid.Cells[i, j].SetValue(value);
                }
            }
        }

        private void SolveGrid(Grid grid)
        {
            // Resolve grid's pending cells, setting their values to their pending values, which were determined by previous methods
            Console.Write("\tSolving ...");
            grid.ResolvePendingCells();
            Console.WriteLine(" done.");

            // TODO: Add more advanced solving methods
        }

        /// <summary>
        /// Checks completeness and correctness of grid, then saves solution if correct
        /// </summary>
        /// <param name="grid"></param>
        /// <returns>true if grid is complete and correct; false if grid is incomplete or incorrect</returns>
        private bool CheckGrid(Grid grid, Sudoku sudoku)
        {
            Console.Write("\tIs grid complete? ...");
            if (grid.IsComplete())
            {
                Console.WriteLine(" yes.");
                Console.Write("\tIs grid correct? ...");
                if (grid.IsCorrect())
                {
                    Console.WriteLine(" yes.");
                    this.SaveSolution(grid, sudoku);
                    return true;
                }
                else 
                    Console.WriteLine(" no.");
            }
            else 
                Console.WriteLine(" no.");
            return false;
        }

        /// <summary>
        /// Save solved grid values in sudoku object as a 2d int array
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="sudoku"></param>
        private void SaveSolution(Grid grid, Sudoku sudoku)
        {
            Console.Write("\tSaving solution ...");
            int[,] solution = new int[9,9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    solution[i, j] = grid.Cells[i, j].Value;
            sudoku.Solution = solution;
            Console.WriteLine(" done.");
        }
    }
}