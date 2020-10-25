using Sudoku_engine.AppData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Text;

namespace Sudoku_engine.Sudoku
{
    public class SudokuField
    {
        private readonly OrderedDictionary Field;

        public SudokuField()
        {
            Field = Generate();
        }

        private OrderedDictionary Generate()
        {
            OrderedDictionary result = new OrderedDictionary();
            for (var row = Data.MinValue; row <= Data.MaxValue; row++)
            {
                for (var column = Data.MinValue; column <= Data.MaxValue; column++)
                {
                    Position position = new Position(row, column);
                    result.Add(position, new SudokuElement());
                }
            }
            return result;
        }

        public SudokuElement GetSudokuElement(int row, int column)
        {
            Position position = new Position(row, column);
            return (SudokuElement)Field[position];
        }

        public int GetNumber(int row, int column) => GetSudokuElement(row, column).Number;

        public void SetNumber(int row, int column, int number) => GetSudokuElement(row, column).Number = number;

        public Color GetFontColor(int row, int column) => GetSudokuElement(row, column).FontColor;

        public void SetFontColor(int row, int column, Color fontColor) => GetSudokuElement(row, column).FontColor = fontColor;

        public List<int> GetCandidates(int row, int column) => GetSudokuElement(row, column).Candidates;

        public bool RemoveCandidte(int row, int column, int value) => GetSudokuElement(row, column).RemoveCandidate(value);

        public OrderedDictionary GetRow(int row)
        {
            OrderedDictionary result = new OrderedDictionary();
            foreach(DictionaryEntry entry in Field)
            {
                Position pos = (Position)entry.Key;
                if (pos.Row == row)
                {
                    result.Add(entry.Key, entry.Value);
                }
            }
            return result;
        }
        //public List<SudokuElement> GetRow(int row)
        //{
        //    List<SudokuElement> result = new List<SudokuElement>();
        //    for (var column = Data.MinValue; column <= Data.MaxValue; column++)
        //    {
        //        Position position = new Position(row, column);
        //        result.Add((SudokuElement)Field[position]);
        //    }
        //    return result;
        //}

        public OrderedDictionary GetColumn(int column)
        {
            OrderedDictionary result = new OrderedDictionary();
            foreach (DictionaryEntry entry in Field)
            {
                Position pos = (Position)entry.Key;
                if (pos.Column == column)
                {
                    result.Add(entry.Key, entry.Value);
                }
            }
            return result;
        }
        //public List<SudokuElement> GetColumn(int column)
        //{
        //    List<SudokuElement> result = new List<SudokuElement>();
        //    for (var row = Data.MinValue; row <= Data.MaxValue; row++)
        //    {
        //        Position position = new Position(row, column);
        //        result.Add((SudokuElement)Field[position]);
        //    }
        //    return result;
        //}

        public OrderedDictionary GetSection(int row, int column)
        {
            OrderedDictionary result = new OrderedDictionary();
            int startRow = ((row - 1) / Data.Section) * Data.Section + 1;
            int startCol = ((column - 1) / Data.Section) * Data.Section + 1;

            foreach(DictionaryEntry entry in Field)
            {
                Position pos = (Position)entry.Key;
                if (pos.Row >= startRow && pos.Row < (startRow + Data.Section)
                    && pos.Column >= startCol && pos.Column < (startCol + Data.Section))
                {
                    result.Add(entry.Key, entry.Value);
                }
            }
            return result;
        }
        //public List<SudokuElement> GetSection(int row, int column)
        //{
        //    List<SudokuElement> result = new List<SudokuElement>();
        //    int startRow = ((row - 1) / Data.Section) * Data.Section + 1;
        //    int startCol = ((column - 1) / Data.Section) * Data.Section + 1;

        //    for (var i = startRow; i < startRow + Data.Section; i++)
        //    {
        //        for (var j = startCol; j < startCol + Data.Section; j++)
        //        {
        //            Position position = new Position(i, j);
        //            result.Add((SudokuElement)Field[position]);
        //        }
        //    }
        //    return result;
        //}

        public OrderedDictionary GetSudokuSection(int row, int column)
        {
            OrderedDictionary result = GetRow(row);

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
    }
}
