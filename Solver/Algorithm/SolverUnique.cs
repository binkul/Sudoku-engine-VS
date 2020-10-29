using Sudoku_engine.AppData;
using Sudoku_engine.Sudoku;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Sudoku_engine.Solver.Algorithm
{
    public class SolverUnique : SolverSingle
    {
        public Result Process(SudokuField sudokuField)
        {
            bool loop;

            do
            {
                loop = false;
                foreach (KeyValuePair<Position, SudokuElement> entry in sudokuField.Field) //.Values)
                {
                    Result result = SolveElement(sudokuField, entry);
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

        private Result SolveElement(SudokuField sudokuField, KeyValuePair<Position, SudokuElement> entry) // SudokuElement sudokuElement)
        {
            if (entry.Value.Number == Data.Empty)
            {
                return Analyze(sudokuField, entry);
            }

            return Result.None;
        }

        private Result Analyze(SudokuField sudokuField, KeyValuePair<Position, SudokuElement> entry)
        {
            var row = entry.Key.Row;
            var column = entry.Key.Column;
            var result = base.Process(sudokuField, row, column);
            if (result != Result.None) return result;

            HashSet<int> existngNumbers = GetNumbersFromSudokuSection(sudokuField, row, column);
            if (AddUniqueFromExisting(sudokuField.GetRow(row), entry.Value, existngNumbers)) return Result.Added;
            if (AddUniqueFromExisting(sudokuField.GetColumn(column), entry.Value, existngNumbers)) return Result.Added;
            if (AddUniqueFromExisting(sudokuField.GetSection(row, column), entry.Value, existngNumbers)) return Result.Added;

            return Result.None;
        }

        protected bool AddUniqueFromExisting(ImmutableSortedDictionary<Position, SudokuElement> elements, SudokuElement sudokuElement, HashSet<int> existngNumbers)
        {
            var result = false;
            var number = FindUniqueCandidates(elements)
                .Where(n => sudokuElement.ContainCandidate(n))
                .Where(n => !existngNumbers.Contains(n))
                .DefaultIfEmpty(0)
                .First();

            if (number != 0)
            {
                sudokuElement.Number = number;
                sudokuElement.FontColor = Data.UniqueColor;
                result = true;
            }
            return result;
        }

        protected List<int> FindUniqueCandidates(ImmutableSortedDictionary<Position, SudokuElement> elements) => elements
                .Select(n => n.Value)
                .Where(n => n.Number == Data.Empty)
                .SelectMany(n => n.Candidates)
                .GroupBy(n => n)
                .Select(num => new { num.Key, Total = num.Count() })
                .Where(n => n.Total <= 1)
                .Select(n => n.Key)
                .ToList();
    }
}
