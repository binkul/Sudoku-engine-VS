using Sudoku_engine.AppData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Drawing;
using System.Text;

namespace Sudoku_engine.Sudoku
{
    public class SudokuField
    {
        public OrderedDictionary Field { get; }

        public SudokuField()
        {
            Field = Generate();
        }

        public SudokuField(OrderedDictionary field)
        {
            Field = field;
        }

        private OrderedDictionary Generate()
        {
            OrderedDictionary result = new OrderedDictionary();
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
            var sudokuElement = (SudokuElement)Field[position];
            return sudokuElement;
        }

        public int GetNumber(int row, int column) => GetSudokuElement(row, column).Number;

        public void SetNumber(int row, int column, int number) => GetSudokuElement(row, column).Number = number;

        public Color GetFontColor(int row, int column) => GetSudokuElement(row, column).FontColor;

        public void SetFontColor(int row, int column, Color fontColor) => GetSudokuElement(row, column).FontColor = fontColor;

        public List<int> GetCandidates(int row, int column) => GetSudokuElement(row, column).Candidates;

        public bool IsOnlyOneCandidate(int row, int column) => GetSudokuElement(row, column).IsOnlyOneCandidate();

        public int GetFirstCandidate(int row, int column) => GetSudokuElement(row, column).GetFirstCandidate();

        public bool RemoveCandidte(int row, int column, int value) => GetSudokuElement(row, column).RemoveCandidate(value);

        public OrderedDictionary GetRow(int row)
        {
            var result = new OrderedDictionary();
            foreach(DictionaryEntry entry in Field)
            {
                var pos = (Position)entry.Key;
                if (pos.Row == row)
                {
                    result.Add(entry.Key, entry.Value);
                }
            }
            return result;
        }

        public OrderedDictionary GetColumn(int column)
        {
            var result = new OrderedDictionary();
            foreach (DictionaryEntry entry in Field)
            {
                var pos = (Position)entry.Key;
                if (pos.Column == column)
                {
                    result.Add(entry.Key, entry.Value);
                }
            }
            return result;
        }

        public OrderedDictionary GetSection(int row, int column)
        {
            OrderedDictionary result = new OrderedDictionary();
            var startRow = ((row - 1) / Data.Section) * Data.Section + 1;
            var startCol = ((column - 1) / Data.Section) * Data.Section + 1;

            foreach(DictionaryEntry entry in Field)
            {
                var pos = (Position)entry.Key;
                if (pos.Row >= startRow && pos.Row < (startRow + Data.Section)
                    && pos.Column >= startCol && pos.Column < (startCol + Data.Section))
                {
                    result.Add(entry.Key, entry.Value);
                }
            }
            return result;
        }

        public OrderedDictionary GetSudokuSection(int row, int column)
        {
            var result = GetRow(row);

            foreach (DictionaryEntry entry in GetColumn(column))
            {
                if (!result.Contains((Position)entry.Key))
                {
                    result.Add(entry.Key, entry.Value);
                }
            }

            foreach (DictionaryEntry entry in GetSection(row, column))
            {
                if (!result.Contains((Position)entry.Key))
                {
                    result.Add(entry.Key, entry.Value);
                }
            }
            return result;
        }

        public SudokuField DeepCopy()
        {
            var fieldCopy = new OrderedDictionary();
            foreach(DictionaryEntry entry in Field)
            {
                var positionOrg = (Position)entry.Key;
                var sudokuElementOrg = (SudokuElement)entry.Value;
               
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
            for (var i = 0; i < Data.MaxValue; i++)
            {
               result.Append("|");
               for (var j = 0; j < Data.MaxValue; j++)
                {
                    var sudokuElement = (SudokuElement)Field[i * Data.MaxValue + j];
                    if (sudokuElement.Number != Data.Empty)
                        result.Append(sudokuElement.Number);
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
