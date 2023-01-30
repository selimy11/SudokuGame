using System.Windows.Forms;

namespace SudokuGame
{
    class Cell : Button
    {
        public int SelectedNumber { get; set; }

        public bool IsLocked { get; set; }

        public int XAxis { get; set; }

        public int YAxis { get; set; }

        public void Remove()
        {
            Text = string.Empty;
            IsLocked = false;
        }
    }
}
