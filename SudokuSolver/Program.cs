using SudokuSolver;
using System;

namespace SudokuSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] sudokuEasy = { { 5,3,0,0,7,0,0,0,0 },
                                  { 6,0,0,1,9,5,0,0,0 },
                                  { 0,9,8,0,0,0,0,6,0 },
                                  { 8,0,0,0,6,0,0,0,3 },
                                  { 4,0,0,8,0,3,0,0,1 },
                                  { 7,0,0,0,2,0,0,0,6 },
                                  { 0,6,0,0,0,0,2,8,0 },
                                  { 0,0,0,4,1,9,0,0,5 },
                                  { 0,0,0,0,8,0,0,7,9 } };

            int[,] sudokuHard = { { 0,0,0,0,0,0,0,0,3 },
                                  { 7,4,0,3,0,0,2,0,0 },
                                  { 0,0,0,0,5,0,4,9,6 },
                                  { 0,0,0,0,6,0,0,8,0 },
                                  { 0,0,0,4,0,5,0,0,0 },
                                  { 5,0,1,0,0,0,0,7,0 },
                                  { 9,0,0,0,7,0,5,3,0 },
                                  { 0,0,0,0,0,0,6,0,0 },
                                  { 0,1,0,0,0,9,0,0,0 } };


            // -------------------- DEMONSTRATION ----------------------
            SudokuSolver sudokuSolver = new SudokuSolver();

            // Print sudoku1 Puzzle, then use SudokuSolver to annotate it, without solving
            Sudoku sudoku1 = new Sudoku(sudokuEasy);
            sudoku1.PrintPuzzle();
            sudokuSolver.Annotate(sudoku1);

            // SudokuSolver will successfully solve sudokuEasy, and store the solution in sudoku1
            sudokuSolver.Solve(sudoku1);
            sudoku1.PrintSolution();

            // SudokuSolver will fail to solve sudokuHard, and print its current grid progress, including annotated cells
            // SudokuSolver fails sudokuHard because advanced solving techniques have not yet been implemented
            Sudoku sudoku2 = new Sudoku(sudokuHard);
            sudoku2.PrintPuzzle();
            sudokuSolver.Solve(sudoku2);
            sudoku2.PrintSolution();

            // SudokuSolver can solve an array of Sudokus
            Sudoku[] sudokus = { sudoku1, sudoku2 };
            sudokuSolver.SolveAll(sudokus);


            Console.WriteLine("\nPress any key to exit . . .");
            Console.ReadKey();

            // -----------------------------------------------------------
        }
    }
}