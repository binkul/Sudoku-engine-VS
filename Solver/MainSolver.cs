using Sudoku_engine.Exceptions;
using Sudoku_engine.Solver.Algorithm;
using Sudoku_engine.Sudoku;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Sudoku_engine.Solver
{
    public class MainSolver
    {
        private SolverUnique _solverUnique;
        private SolverBackTrack _solverBackTrack;

        public MainSolver()
        {
            _solverUnique = new SolverUnique();
            _solverBackTrack = new SolverBackTrack();
        }

        public SudokuField Process(SudokuField sudoku)
        {
            CheckCollision(sudoku);

            _ = _solverUnique.Process(sudoku);
            if (_solverUnique.Process(sudoku) != Result.FullFilled)
            {
                SolverResult solverResult = _solverBackTrack.Process(sudoku);
                if (solverResult.Result == Result.Error)
                    throw new SudokuUnsolvableException("There is no solution for this Sudoku.");
                else
                    sudoku = solverResult.Sudoku;
            }

            return sudoku;
        }

        private static void CheckCollision(SudokuField sudoku)
        {
            if (Validator.IsCollision(sudoku))
            {
                throw new SudokuCollisionNumberException("There are the same numbers in row/column/section");
            }
        }

        public SolverUnique GetSolverUnique() => _solverUnique;
    }
}
