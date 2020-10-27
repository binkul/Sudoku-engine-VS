using Sudoku_engine.AppData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Sudoku_engine.Sudoku
{
    public class SudokuElement
    {
        public int Number { get; set; } = Data.Empty;
        public List<int> Candidates { get; set; } = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};
        public Color FontColor { get; set; } = Data.NormalColor;

        public SudokuElement()
        { }

        public SudokuElement(int number, List<int> candidates, Color fontColor)
        {
            Number = number;
            Candidates = candidates;
            FontColor = fontColor;
        }

        public bool RemoveCandidate(int number) => Candidates.Remove(number);

        public int GetCandidate(int index)
        {
            if (index >= 0 && index < Candidates.Count())
                return Candidates[index];
            else
                return -1;
        }

        public int GetFirstCandidate()
        {
            if (Candidates.Count() > 0)
                return Candidates[0];
            else
                return -1;
        }

        public int GetCandidatesSize() => Candidates.Count();

        public bool IsOnlyOneCandidate() => Candidates.Count == 1;

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("SudokuElement : [Number=");
            result.Append(Number);
            result.Append("; Candidates={");
            if (Candidates.Count() > 0)
            {
                foreach (int num in Candidates)
                {
                    result.Append(num);
                    result.Append(", ");
                }
                result.Remove(result.Length - 2, 2);
            }
            result.Append("}; FontColor=");
            result.Append(FontColor.ToString());
            result.Append("]");

            return result.ToString();
        }
    }
}
