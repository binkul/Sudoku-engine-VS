using Sudoku_engine.AppData;
using Sudoku_engine.Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku_engine.Solver.Algorithm
{
    public class SolverBackTrack : SolverUnique
    {
        private SudokuField _sudokuFieldCopy;
        private SudokuElement _sudokuElement;

        public new SolverResult Process(SudokuField sudokuField)
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
                        return new SolverResult(Result.FullFilled, _sudokuFieldCopy);

                    case Result.Error:
                        bool loop = true;
                        do
                        {
                            if (stackElements.Count == 0)
                                return new SolverResult(Result.Error);

                            stackElement = stackElements.Pop();
                            index = stackElement.Index + 1;
                            if (index < stackElement.GetCandidateSize())
                            {
                                _sudokuFieldCopy = stackElement.SudokuField;
                                _sudokuElement = stackElement.SudokuElement;
                                if (PutOnStack(stackElements, index) == Result.Error)
                                    return new SolverResult(Result.Error);
                                loop = false;
                            }
                            else
                            {
                                loop = true;
                            }

                        } while (loop);
                        break;

                    default:
                        _sudokuElement = FindFirstEmpty(_sudokuFieldCopy);
                        if (PutOnStack(stackElements, 0) == Result.Error)
                            return new SolverResult(Result.Error);
                        break;
                }
                result = base.Process(_sudokuFieldCopy);
            }
        }

        private Result PutOnStack(Stack<StackElement> stack, int index)
        {
            StackElement stackElement = new StackElement(_sudokuFieldCopy, _sudokuElement, index);
            stack.Push(stackElement);

            _sudokuFieldCopy = _sudokuFieldCopy.DeepCopy();
            _sudokuElement = FindFirstEmpty(_sudokuFieldCopy);
            if (_sudokuElement == null)
                return Result.Error;

            int number = _sudokuElement.GetCandidate(index);
            _sudokuElement.Number = number;
            _sudokuElement.FontColor = Data.BackTrackColor;

            return Result.None;
        }

        private SudokuElement FindFirstEmpty(SudokuField sudokuField)
        {
            return sudokuField.Field
                .Select(n => n.Value)
                .Where(n => n.Number == Data.Empty)
                .DefaultIfEmpty(null)
                .FirstOrDefault();
        }
    }
}
