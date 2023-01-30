using System;
using System.Drawing;

namespace SudokuGame
{
    class Game
    {
        private readonly Cell[,] _cells;
        public Game(Cell[,] cells)
        {
            _cells = cells;
        }

        private readonly Random _random = new Random();

        /// <summary>
        /// This method triggers the game to start.
        /// </summary>
        /// <param name="level">0-Easy, 1-Medium, 2-Hard</param>
        public void StartGame(int level)
        {
            Numbers numbers = new Numbers(_cells);

            numbers.InsertNumbers();

            int startCount;

            switch (level)
            {
                case 0:
                    startCount = 45;
                    break;
                case 1:
                    startCount = 30;
                    break;
                default:
                    startCount = 15;
                    break;
            }

            ShowNumbersOnBoard(startCount);
        }

        /// <summary>
        /// This method shows numbers to display to the user based on Level.
        /// </summary>
        /// <param name="startCount"></param>
        private void ShowNumbersOnBoard(int startCount)
        {
            for (int i = 0; i < startCount; i++)
            {
                var rX = _random.Next(9);
                var rY = _random.Next(9);

                _cells[rX, rY].Text = _cells[rX, rY].SelectedNumber.ToString();
                _cells[rX, rY].ForeColor = Color.Black;
                _cells[rX, rY].IsLocked = true;
            }
        }
    }
}
