using Sudoku_engine.Sudoku;
using System;

namespace Sudoku_engine
{
    public class Program
    {
        static void Main(string[] args)
        {
            SudokuField sudokuField = new SudokuField();
            SudokuElement sudokuElement = sudokuField.GetSudokuElement(2, 3);
            Console.WriteLine(sudokuElement.ToString());


        }
    }
}
