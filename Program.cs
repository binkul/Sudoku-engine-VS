using Sudoku_engine.Sudoku;
using System;

namespace Sudoku_engine
{
    public class Program
    {
        static void Main(string[] args)
        {
            SudokuField sudokuField = new SudokuField();
            sudokuField.SetNumber(1, 4, 1);
            sudokuField.SetNumber(5, 9, 2);
            sudokuField.SetNumber(3, 4, 3);
            sudokuField.SetNumber(9, 7, 4);
            sudokuField.SetNumber(2, 8, 5);
            sudokuField.SetNumber(6, 6, 6);
            sudokuField.SetNumber(4, 1, 7);
            sudokuField.SetNumber(7, 5, 8);
            sudokuField.SetNumber(8, 3, 9);
            Console.WriteLine(sudokuField.ToString());


        }
    }
}
