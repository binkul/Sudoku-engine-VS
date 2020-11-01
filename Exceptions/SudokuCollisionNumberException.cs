using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_engine.Exceptions
{
    public class SudokuCollisionNumberException : Exception
    {
        public SudokuCollisionNumberException()
        { }

        public SudokuCollisionNumberException(String message) : base(message)
        { }

        public SudokuCollisionNumberException(String message, Exception inner) : base(message, inner)
        { }
    }
}
