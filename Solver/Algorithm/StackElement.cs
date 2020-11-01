using Sudoku_engine.Sudoku;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_engine.Solver.Algorithm
{
    class StackElement
    {
        public SudokuField SudokuField { get; private set; }
        public SudokuElement SudokuElement { get; private set; }
        public int Index { get; private set; }

        public StackElement(SudokuField sudokuField, SudokuElement sudokuElement, int index)
        {
            SudokuField = sudokuField;
            SudokuElement = sudokuElement;
            Index = index;
        }

        public int GetCandidateSize()
        {
            return SudokuElement.GetCandidatesSize();
        }
    }
}
