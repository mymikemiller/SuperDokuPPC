using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDoku
{
    public class SquareEventArgs : EventArgs
    {
        private Square mSquare;

        public SquareEventArgs(Square square)
        {
            mSquare = square;
        }

        public Square Square
        {
            get { return mSquare; }
        }
    }
}
