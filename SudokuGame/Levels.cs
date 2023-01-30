namespace SudokuGame
{
    class Levels
    {
        public Levels(bool easy, bool medium, bool hard)
        {
            Easy = easy;
            Medium = medium;
            Hard = hard;
        }

        public bool Easy { get; set; }
        public bool Medium { get; set; }
        public bool Hard { get; set; }

    }
}
