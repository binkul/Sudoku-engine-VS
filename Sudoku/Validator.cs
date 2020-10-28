using Sudoku_engine.AppData;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace Sudoku_engine.Sudoku
{
    public class Validator
    {
        public static bool IsFilled(SudokuField sudokuField) => sudokuField.Field
                .Select(n => n.Value.Number)
                .Where(n => n == Data.Empty)
                .Count() == 0;

        public static bool IsCollision(SudokuField sudokuField)
        {
            for (var i = 1; i <= Data.MaxValue; i++)
            {
                if (IsDuplicate(sudokuField.GetRow(i))) return true;
                if (IsDuplicate(sudokuField.GetColumn(i))) return true;
                if (IsDuplicate(sudokuField.GetSection(i))) return true;
            }
            return false;
        }

        public static bool IsDuplicate(ImmutableSortedDictionary<Position, SudokuElement> elements) => elements
                .GroupBy(n => n.Value.Number)
                .Where(n => n.Key != Data.Empty)
                .Select(num => new { Value = num.Key, Total = num.Count() })
                .Select(n => n.Total)
                .Where(n => n > 1)
                .Count() > 0;
    }
}
