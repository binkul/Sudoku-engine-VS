using Sudoku_engine.Sudoku;
using Sudoku_engine.Solver;
using System;
using System.Collections.Generic;

namespace Sudoku_engine
{
    public class Program
    {
        static void Main(string[] args)
        {
            SudokuField sudoku = new SudokuField();
            MainSolver solver = new MainSolver(sudoku);
            sudoku = solver.Process();
            Console.WriteLine(sudoku);
        }
    }
}
