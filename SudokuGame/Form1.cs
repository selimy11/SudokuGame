using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SudokuGame
{
    public partial class Form1 : Form
    {
        private readonly CellHelper _cellHelper;
        private readonly Game _game;
        public Form1()
        {
            InitializeComponent();

            _cellHelper = new CellHelper();

            CreatePlayingBoard();

            _game = new Game(_cells);

            panel1.Enabled = false;
            checkBtn.Enabled = false;
            solveAllToolStripMenuItem.Enabled = false;
            stopwatchLbl.Text = @"00:00:00";
        }

        private readonly Cell[,] _cells = new Cell[9, 9];

        private readonly Levels _levels = new Levels(true, false, false);

        private Stopwatch _stopWatch;

        /// <summary>
        /// This method can create 9x9 cells with all properties according to index
        /// </summary>
        private void CreatePlayingBoard()
        {
            for (int xAxis = 0; xAxis < 9; xAxis++)
            {
                for (int yAxis = 0; yAxis < 9; yAxis++)
                {
                    _cells[xAxis, yAxis] = new Cell();

                    _cells[xAxis, yAxis].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20, FontStyle.Bold);
                    _cells[xAxis, yAxis].ForeColor = SystemColors.ActiveCaptionText;
                    _cells[xAxis, yAxis].Size = new Size(40, 40);
                    _cells[xAxis, yAxis].Location = new Point(xAxis * 40, yAxis * 40);
                    _cells[xAxis, yAxis].BackColor = ((xAxis / 3) + (yAxis / 3)) % 2 == 0 ? Color.LightBlue : Color.LightGreen;
                    _cells[xAxis, yAxis].FlatStyle = FlatStyle.Flat;
                    _cells[xAxis, yAxis].FlatAppearance.BorderColor = Color.Black;
                    _cells[xAxis, yAxis].XAxis = xAxis;
                    _cells[xAxis, yAxis].YAxis = yAxis;
                    _cells[xAxis, yAxis].Name = xAxis.ToString() + yAxis.ToString();

                    _cells[xAxis, yAxis].KeyPress += cell_keyPressed;
                    _cells[xAxis, yAxis].MouseHover += cell_mouseDown;
                    _cells[xAxis, yAxis].MouseLeave += cell_mouseUp;
                    
                    panel1.Controls.Add(_cells[xAxis, yAxis]);
                }
            }
        }

        /// <summary>
        /// This method can get game level from the menu strip
        /// </summary>
        /// <returns> Returns game level as integer</returns>
        private int GetGameLevel()
        {
            panel1.Enabled = true;
            checkBtn.Enabled = true;
            solveAllToolStripMenuItem.Enabled = true;

            return _levels.Easy ? 0 : _levels.Medium ? 1 : _levels.Hard ? 2 : 0;
        }

        #region Events

        private void cell_mouseDown(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            var a = _cellHelper.FindHorizontalCells(btn.Name, _cells);

            foreach (var button in a)
            {
                button.BackColor = Color.LightGray;
            }

            var b = _cellHelper.FindVerticalCells(btn.Name, _cells);

            foreach (var button in b)
            {
                button.BackColor = Color.LightGray;
            }

            var c = _cellHelper.FindNeighbors(btn.Name, _cells);

            foreach (var button in c)
            {
                button.BackColor = Color.LightGray;
            }
        }

        private void cell_mouseUp(object sender, EventArgs e)
        {
            for (int xAxis = 0; xAxis < 9; xAxis++)
            {
                for (int yAxis = 0; yAxis < 9; yAxis++)
                {
                    _cells[xAxis, yAxis].BackColor =
                        ((xAxis / 3) + (yAxis / 3)) % 2 == 0 ? Color.LightBlue : Color.LightGreen;
                }
            }
        }

        private void cell_keyPressed(object sender, KeyPressEventArgs e)
        {
            var cell = sender as Cell;

            if (cell != null && cell.IsLocked)
                return;

            if (!int.TryParse(e.KeyChar.ToString(), out var value)) return;

            if (value == 0)
                cell?.Remove();

            else if (cell != null) cell.Text = value.ToString();

            if (cell != null) cell.ForeColor = SystemColors.ControlDarkDark;
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _levels.Easy = true;
            _levels.Medium = false;
            _levels.Hard = false;

            easyToolStripMenuItem.Checked = true;
            mediumToolStripMenuItem.Checked = false;
            hardToolStripMenuItem.Checked = false;

            _game.StartGame(GetGameLevel());
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _levels.Easy = false;
            _levels.Medium = true;
            _levels.Hard = false;

            easyToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = true;
            hardToolStripMenuItem.Checked = false;

            _game.StartGame(GetGameLevel());
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _levels.Easy = false;
            _levels.Medium = false;
            _levels.Hard = true;

            easyToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            hardToolStripMenuItem.Checked = true;

            _game.StartGame(GetGameLevel());
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _game.StartGame(GetGameLevel());

            _stopWatch = Stopwatch.StartNew();

            _stopWatch.Start();

            playingTimer.Enabled = true;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBtn_Click(object sender, EventArgs e)
        {
            List<Cell> incorrectNumbers = new List<Cell>();

            foreach (var cell in _cells)
            {
                if (!string.Equals(cell.SelectedNumber.ToString(), cell.Text))
                {
                    incorrectNumbers.Add(cell);
                }
            }

            if (incorrectNumbers.Any())
            {
                incorrectNumbers.ForEach(x => x.ForeColor = Color.Red);

                MessageBox.Show("Sudoku is not completed!");
            }
            else
            {
                playingTimer.Stop();
                
                MessageBox.Show("Yes! You did!");

                _game.StartGame(GetGameLevel());

                _stopWatch.Restart();

                playingTimer.Start();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPage aboutPage = new AboutPage();
            aboutPage.Show();
        }

        private void solveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var cell in _cells)
            {
                cell.Text = cell.SelectedNumber.ToString();
            }
        }

        private void playingTimer_Tick(object sender, EventArgs e)
        {
            stopwatchLbl.Text = $@"{_stopWatch.Elapsed:hh\:mm\:ss}";
        }

        #endregion
        
    }
}
