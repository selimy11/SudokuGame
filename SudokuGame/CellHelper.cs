using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SudokuGame
{
    class CellHelper
    {
        public CellHelper()
        {
        }

        /// <summary>
        /// This method is to show horizontal cells that need to be checked when the mouse hovers over a cell.
        /// </summary>
        /// <param name="name">Given cell name</param>
        /// <param name="formCells">Entire board</param>
        /// <returns>Returns horizontal cells</returns>
        public List<Button> FindHorizontalCells(string name, Cell[,] formCells)
        {
            List<Button> horizontalCells = new List<Button>();

            foreach (var cell in formCells)
            {
                if (cell.Name.StartsWith(name.Substring(0, 1)))
                {
                    horizontalCells.Add(cell);
                }

            }

            return horizontalCells;
        }

        /// <summary>
        /// This method is to show vertical cells that need to be checked when the mouse hovers over a cell.
        /// </summary>
        /// <param name="name">Given cell name</param>
        /// <param name="formCells">Entire board</param>
        /// <returns>Returns vertical cells</returns>
        public List<Button> FindVerticalCells(string name, Cell[,] formCells)
        {
            List<Button> verticalCells = new List<Button>();

            foreach (var cell in formCells)
            {
                if (cell.Name.EndsWith(name.Substring(name.Length - 1)))
                {
                    verticalCells.Add(cell);
                }
            }
            return verticalCells;
        }

        /// <summary>
        /// This method is to show neighbor cells that need to be checked when the mouse hovers over a cell.
        /// </summary>
        /// <param name="name">Given cell name</param>
        /// <param name="formCells">Entire board</param>
        /// <returns>Returns neighbor cells</returns>
        public List<Button> FindNeighbors(string name, Cell[,] formCells)
        {
            List<Button> neighborCells = new List<Button>();

            int i = Convert.ToInt32(name.Substring(0, 1));
            int j = Convert.ToInt32(name.Substring(name.Length - 1));

            if (0 <= i && i <= 2)
            {
                if (0 <= j && j <= 2)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        neighborCells.Add(formCells[0, k]);
                        neighborCells.Add(formCells[1, k]);
                        neighborCells.Add(formCells[2, k]);
                    }
                }
                else if (3 <= j && j <= 5)
                {
                    for (int k = 3; k < 6; k++)
                    {
                        neighborCells.Add(formCells[0, k]);
                        neighborCells.Add(formCells[1, k]);
                        neighborCells.Add(formCells[2, k]);
                    }
                }
                else if (6 <= j && j <= 8)
                {
                    for (int k = 6; k < 9; k++)
                    {
                        neighborCells.Add(formCells[0, k]);
                        neighborCells.Add(formCells[1, k]);
                        neighborCells.Add(formCells[2, k]);
                    }
                }
            }
            else if (3 <= i && i <= 5)
            {
                if (0 <= j && j <= 2)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        neighborCells.Add(formCells[3, k]);
                        neighborCells.Add(formCells[4, k]);
                        neighborCells.Add(formCells[5, k]);
                    }
                }
                else if (3 <= j && j <= 5)
                {
                    for (int k = 3; k < 6; k++)
                    {
                        neighborCells.Add(formCells[3, k]);
                        neighborCells.Add(formCells[4, k]);
                        neighborCells.Add(formCells[5, k]);
                    }
                }
                else if (6 <= j && j <= 8)
                {
                    for (int k = 6; k < 9; k++)
                    {
                        neighborCells.Add(formCells[3, k]);
                        neighborCells.Add(formCells[4, k]);
                        neighborCells.Add(formCells[5, k]);
                    }
                }
            }
            else if (6 <= i && i <= 8)
            {
                if (0 <= j && j <= 2)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        neighborCells.Add(formCells[6, k]);
                        neighborCells.Add(formCells[7, k]);
                        neighborCells.Add(formCells[8, k]);
                    }
                }
                else if (3 <= j && j <= 5)
                {
                    for (int k = 3; k < 6; k++)
                    {
                        neighborCells.Add(formCells[6, k]);
                        neighborCells.Add(formCells[7, k]);
                        neighborCells.Add(formCells[8, k]);
                    }
                }
                else if (6 <= j && j <= 8)
                {
                    for (int k = 6; k < 9; k++)
                    {
                        neighborCells.Add(formCells[6, k]);
                        neighborCells.Add(formCells[7, k]);
                        neighborCells.Add(formCells[8, k]);
                    }
                }
            }

            return neighborCells;
        }
    }
}
