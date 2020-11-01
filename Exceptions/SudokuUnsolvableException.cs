using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku_engine.Exceptions
{
    class SudokuUnsolvableException : Exception
    {
        public SudokuUnsolvableException()
        { }

        public SudokuUnsolvableException(String message) : base(message)
        { }

        public SudokuUnsolvableException(String message, Exception inner) : base(message, inner)
        { }
    }
}
