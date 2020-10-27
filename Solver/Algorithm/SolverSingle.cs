using Sudoku_engine.AppData;
using Sudoku_engine.Sudoku;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Sudoku_engine.Solver.Algorithm
{
    public class SolverSingle
    {
        protected Result Process(SudokuField sudokuField, int row, int column)
        {
            SudokuElement sudokuElement = sudokuField.GetSudokuElement(row, column);
            HashSet<int> existingNumbers = GetNumberFromSudokuSection(sudokuField, row, column);

            foreach (int number in existingNumbers)
            {
                _ = sudokuElement.RemoveCandidate(number);
            }
            return AddNumber(sudokuElement);
        }

        public HashSet<int> GetNumberFromSudokuSection(SudokuField sudokuField, int row, int column)
        {
            return sudokuField.GetSudokuSection(row, column)
                .Select(n => n.Value)
                .Select(n => n.Number)
                .Where(n => n != Data.Empty)
                .ToHashSet();
        }

        private Result AddNumber(SudokuElement sudokuElement)
        {
            var size = sudokuElement.GetCandidatesSize();

            switch (size)
            {
                case 0:
                    return Result.Error;
                case 1:
                    sudokuElement.Number = sudokuElement.GetFirstCandidate();
                    sudokuElement.FontColor = Data.SingleColor;
                    return Result.Added;
                default:
                    return Result.None;
            }
        }
    }
}
