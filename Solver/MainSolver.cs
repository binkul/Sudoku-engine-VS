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
        public SudokuField Sudoku { get; set; }
        private SolverUnique _solverUnique;
        private SolverBackTrack _solverBackTrack;

        public MainSolver(SudokuField sudoku)
        {
            Sudoku = sudoku;
            _solverUnique = new SolverUnique();
            _solverBackTrack = new SolverBackTrack(this);
        }

        public SudokuField Process()
        {
            if (Validator.IsCollision(Sudoku))
            {
                throw new SudokuCollisionNumberException("There are the same numbers in row/column/section");
            }

            _ = _solverUnique.Process(Sudoku);
            if (_solverUnique.Process(Sudoku) != Result.FullFilled)
            {
                if (_solverBackTrack.Process(Sudoku) == Result.Error)
                    throw new SudokuUnsolvableException("There is no solution for this Sudoku.");
            }

            return Sudoku;
        }

        public SolverUnique GetSolverUnique() => _solverUnique;
    }
}
