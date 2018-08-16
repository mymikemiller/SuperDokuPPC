namespace SuperDoku
{
    /// <summary>
    /// Represents an x,y position on the board.
    /// </summary>
    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Gets and sets the 0-based Row.
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Gets and sets the 0-based Column.
        /// </summary>
        public int Y { get; private set; }

        public bool Equals(Position obj)
        {
            return (X == obj.X && Y == obj.Y);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}
