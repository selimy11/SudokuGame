using System;
using System.Collections.Generic;

namespace SudokuGame
{
    class Numbers
    {
        private readonly Cell[,] _cells;
        public Numbers(Cell[,] cells)
        {
            _cells = cells;
        }

        private readonly Random _random = new Random();

        /// <summary>
        /// Starts assigning numbers for each cell in the background.
        /// </summary>
        public void InsertNumbers()
        {
            foreach (var cell in _cells)
            {
                cell.SelectedNumber = 0;
                cell.Remove();
            }

            FindNumberForNextCell(0, -1);
        }

        /// <summary>
        /// This method fills all the cells in turn.
        /// </summary>
        /// <param name="i"> X-Axis </param>
        /// <param name="j"> Y-Axis </param>
        /// <returns></returns>
        private bool FindNumberForNextCell(int i, int j)
        {

            if (++j > 8)
            {
                j = 0;

                if (++i > 8)
                    return true;
            }

            int value;

            var numbersLeft = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            do
            {
                if (numbersLeft.Count < 1)
                {
                    _cells[i, j].SelectedNumber = 0;
                    return false;
                }

                value = numbersLeft[_random.Next(0, numbersLeft.Count)];

                _cells[i, j].SelectedNumber = value;

                numbersLeft.Remove(value);
            }
            while (!RuleChecker(value, i, j) || !FindNumberForNextCell(i, j));

            return true;
        }

        /// <summary>
        /// This method checks the compliance of the placed numbers with the rule.
        /// </summary>
        /// <param name="value">Selected number</param>
        /// <param name="x"> X-Axis </param>
        /// <param name="y"> Y-Axis </param>
        /// <returns>If it's correct returns true</returns>
        private bool RuleChecker(int value, int x, int y)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i != y && _cells[x, i].SelectedNumber == value)
                    return false;
                
                if (i != x && _cells[i, y].SelectedNumber == value)
                    return false;
            }
            
            for (int i = x - (x % 3); i < x - (x % 3) + 3; i++)
            {
                for (int j = y - (y % 3); j < y - (y % 3) + 3; j++)
                {
                    if (i != x && j != y && _cells[i, j].SelectedNumber == value)
                        return false;
                }
            }

            return true;
        }
    }
}
