using Sudoku_engine.AppData;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Collections.Immutable;

namespace Sudoku_engine.Sudoku
{
    public class SudokuField
    {
        public SortedDictionary<Position, SudokuElement> Field { get; }

        public SudokuField()
        {
            Field = Generate();
        }

        public SudokuField(SortedDictionary<Position, SudokuElement> field)
        {
            Field = field;
        }

        private SortedDictionary<Position, SudokuElement> Generate()
        {
            SortedDictionary<Position, SudokuElement> result = new SortedDictionary<Position, SudokuElement>();
            for (var row = Data.MinValue; row <= Data.MaxValue; row++)
            {
                for (var column = Data.MinValue; column <= Data.MaxValue; column++)
                {
                    var position = new Position(row, column);
                    result.Add(position, new SudokuElement());
                }
            }
            return result;
        }

        public SudokuElement GetSudokuElement(int row, int column)
        {
            var position = new Position(row, column);
            var sudokuElement = Field[position];
            return sudokuElement;
        }

        public int GetNumber(int row, int column) => GetSudokuElement(row, column).Number;

        public void SetNumber(int row, int column, int number) => GetSudokuElement(row, column).Number = number;

        public Color GetFontColor(int row, int column) => GetSudokuElement(row, column).FontColor;

        public void SetFontColor(int row, int column, Color fontColor) => GetSudokuElement(row, column).FontColor = fontColor;

        public List<int> GetCandidates(int row, int column) => GetSudokuElement(row, column).Candidates;

        public bool IsOnlyOneCandidate(int row, int column) => GetSudokuElement(row, column).IsOnlyOneCandidate();

        public bool IsFilled(int row, int column) => GetNumber(row, column) != Data.Empty;
             
        public int GetFirstCandidate(int row, int column) => GetSudokuElement(row, column).GetFirstCandidate();

        public bool RemoveCandidte(int row, int column, int value) => GetSudokuElement(row, column).RemoveCandidate(value);

        public ImmutableSortedDictionary<Position, SudokuElement> GetRow(int row)
        {
            var result = Field
                .Select(n => n)
                .Where(k => k.Key.Row == row)
                .ToImmutableSortedDictionary(k => k.Key, v => v.Value);
            return result;
        }

        public ImmutableSortedDictionary<Position, SudokuElement> GetColumn(int column)
        {
            var result = Field
                .Select(n => n)
                .Where(k => k.Key.Column == column)
                .ToImmutableSortedDictionary(k => k.Key, v => v.Value);
            return result;
        }

        public ImmutableSortedDictionary<Position, SudokuElement> GetSection(int row, int column)
        {
            var startRow = ((row - 1) / Data.Section) * Data.Section + 1;
            var startCol = ((column - 1) / Data.Section) * Data.Section + 1;
 
            var result = Field
                .Select(n => n)
                .Where(k => k.Key.Row >= startRow && k.Key.Row < (startRow + Data.Section))
                .Where(k => k.Key.Column >= startCol && k.Key.Column < (startCol + Data.Section))
                .ToImmutableSortedDictionary(k => k.Key, v => v.Value);
            return result;
        }

        public ImmutableSortedDictionary<Position, SudokuElement> GetSudokuSection(int row, int column)
        {
            var result = GetRow(row)
                .Union(GetColumn(column))
                .Union(GetSection(row, column))
                .ToImmutableSortedDictionary(k => k.Key, v => v.Value);
            return result;
        }

        public SudokuField DeepCopy()
        {
            var fieldCopy = new SortedDictionary<Position, SudokuElement>();
            foreach(KeyValuePair<Position, SudokuElement> entry in Field)
            {
                var positionOrg = entry.Key;
                var sudokuElementOrg = entry.Value;
               
                var positionCopy = new Position(positionOrg.Row, positionOrg.Column);
                var sudokuElementCopy = new SudokuElement(sudokuElementOrg.Number, 
                    new List<int>(sudokuElementOrg.Candidates), 
                    sudokuElementOrg.FontColor);

                fieldCopy.Add(positionCopy, sudokuElementCopy);
            }
            return new SudokuField(fieldCopy);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (var i = 1; i <= Data.MaxValue; i++)
            {
                result.Append("|");
                var row = GetRow(i);
                foreach(SudokuElement element in row.Values)
                {
                    if (element.Number != Data.Empty)
                        result.Append(element.Number);
                    else
                        result.Append(" ");
                    result.Append("|");
                }
                result.Append("\n");
            }
            return result.ToString();
        }
    }
}
