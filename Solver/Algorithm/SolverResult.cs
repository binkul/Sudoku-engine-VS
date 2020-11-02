using Sudoku_engine.Sudoku;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_engine.Solver.Algorithm
{
    public class SolverResult
    {
        public Result Result { get; set; }
        public SudokuField Sudoku { get; set; }

        public SolverResult(Result result) : this (result, null)
        {
        }

        public SolverResult(Result result, SudokuField sudokuField)
        {
            Result = result;
            Sudoku = sudokuField;
        }
    }
}
