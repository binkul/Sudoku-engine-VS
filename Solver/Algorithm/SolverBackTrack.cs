using Sudoku_engine.AppData;
using Sudoku_engine.Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_engine.Solver.Algorithm
{
    public class SolverBackTrack
    {
        private MainSolver _solver;
        private SudokuField _sudokuFieldCopy;
        private SudokuElement _sudokuElement;

        public SolverBackTrack(MainSolver solver)
        {
            _solver = solver;
        }

        public Result Process(SudokuField sudokuField)
        {
            _sudokuFieldCopy = sudokuField;
            Stack<StackElement> stackElements = new Stack<StackElement>();
            StackElement stackElement;
            Result result = Result.None;
            int index;

            while (true)
            {
                switch (result)
                {
                    case Result.FullFilled:
                        _solver.Sudoku = _sudokuFieldCopy;
                        return Result.FullFilled;

                    case Result.Error:
                        bool loop = true;
                        do
                        {
                            if (stackElements.Count == 0)
                                return Result.Error;

                            stackElement = stackElements.Pop();
                            index = stackElement.Index + 1;
                            if (index < stackElement.GetCandidateSize())
                            {
                                _sudokuFieldCopy = stackElement.SudokuField;
                                _sudokuElement = stackElement.SudokuElement;
                                if (PutOnStack(stackElements, index) == Result.Error)
                                    return Result.Error;
                                loop = false;
                            }
                            else
                            {
                                loop = true;
                            }

                        } while (loop);
                        break;

                    default:
                        _sudokuElement = FindFirsEmpty(_sudokuFieldCopy);
                        if (PutOnStack(stackElements, 0) == Result.Error)
                            return Result.Error;
                        break;
                }
                result = _solver.GetSolverUnique().Process(_sudokuFieldCopy);
            }
        }

        private Result PutOnStack(Stack<StackElement> stack, int index)
        {
            StackElement stackElement = new StackElement(_sudokuFieldCopy, _sudokuElement, index);
            stack.Push(stackElement);

            _sudokuFieldCopy = _sudokuFieldCopy.DeepCopy();
            _sudokuElement = FindFirsEmpty(_sudokuFieldCopy);
            if (_sudokuElement == null)
                return Result.Error;

            int number = _sudokuElement.GetCandidate(index);
            _sudokuElement.Number = number;
            _sudokuElement.FontColor = Data.BackTrackColor;

            return Result.None;
        }

        private SudokuElement FindFirsEmpty(SudokuField sudokuField)
        {
            return sudokuField.Field
                .Select(n => n.Value)
                .Where(n => n.Number == Data.Empty)
                .DefaultIfEmpty(null)
                .FirstOrDefault();
        }
    }
}
