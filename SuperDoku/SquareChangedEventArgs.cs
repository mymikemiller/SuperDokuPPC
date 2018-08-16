using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDoku
{
    public class SquareChangedEventArgs : EventArgs
    {
        public SquareChangedEventArgs(Square oldSquare, Square newSquare)
        {
            OldSquare = oldSquare;
            NewSquare = newSquare;
        }

        public Square OldSquare { get; private set; }
        public Square NewSquare { get; private set; }

    }
}
