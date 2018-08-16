using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDoku
{
    public class SquareValueChangedEventArgs : EventArgs
    {
        public SquareValueChangedEventArgs(Square square, int oldValue, int newValue)
        {
            Square = square;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public Square Square { get; private set; }
        public int OldValue { get; private set; }
        public int NewValue { get; private set; }

    }
}
