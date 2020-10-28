using Sudoku_engine.AppData;
using Sudoku_engine.Sudoku;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_engine.Solver.Algorithm
{
    class SolverUnique : SolverSingle
    {
        public Result Process(SudokuField sudokuField)
        {
            bool loop;

            do
            {
                loop = false;
                foreach (var element in sudokuField.Field.Values)
                {
                    Result result = SolveElement(sudokuField, element);
                    switch (result)
                    {
                        case Result.Error:
                            return Result.Error;
                        case Result.Added:
                            loop = true;
                            break;
                    }
                }
            } while (loop);

            return Validator.IsFilled(sudokuField) ? Result.FullFilled : Result.None;
        }

        private Result SolveElement(SudokuField sudokuField, SudokuElement sudokuElement)
        {
            if (sudokuElement.Number == Data.Empty)
            {
                return Analyze(sudokuField, sudokuElement);
            }

            return Result.None;
        }

        private Result Analyze(SudokuField sudokuField, SudokuElement sudokuElement)
        {

            return Result.None;
        }
    }
}
