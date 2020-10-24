using Sudoku_engine.Element;
using System;

namespace Sudoku_engine
{
    class Program
    {
        static void Main(string[] args)
        {
            Position pos1 = new Position(1, 3);
            Position pos2 = new Position(1, 3);
            Console.WriteLine(pos1.ToString());
            Console.WriteLine(pos2.ToString());
            Console.WriteLine(pos1.CompareTo(pos2));

            SudokuElement elem = new SudokuElement();
            Console.WriteLine(elem.ToString());
            Console.WriteLine(elem.RemoveCandidate(5));
            Console.WriteLine(elem.RemoveCandidate(2));
            Console.WriteLine(elem.RemoveCandidate(2));
            Console.WriteLine(elem.ToString());
            Console.WriteLine(elem.GetCandidatesSize());
            Console.WriteLine(elem.GetCandidate(1));
            Console.WriteLine(elem.GetFirstCandidate());
            Console.WriteLine(elem.IsOnlyOneCandidate());
            Console.WriteLine(elem.RemoveCandidate(1));
            Console.WriteLine(elem.RemoveCandidate(3));
            Console.WriteLine(elem.RemoveCandidate(4));
            Console.WriteLine(elem.RemoveCandidate(6));
            Console.WriteLine(elem.RemoveCandidate(7));
            Console.WriteLine(elem.RemoveCandidate(8));
            Console.WriteLine(elem.IsOnlyOneCandidate());
            Console.WriteLine(elem.ToString());


        }
    }
}
