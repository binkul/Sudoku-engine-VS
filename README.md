# Simple sudoku solver.
Implementation:

            SudokuField sudoku = new SudokuField();
            MainSolver solver = new MainSolver();
            sudoku = solver.Process(sudoku);
            Console.WriteLine(sudoku);
            
 to set numbers in sudoku use sudoku.SetNumber(row, column, number), eg.
            sudoku.SetNumber(1, 2, 7);
            sudoku.SetNumber(5, 7, 2);
            ....
            sudoku = solver.Process(sudoku)
