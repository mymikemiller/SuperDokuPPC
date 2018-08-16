using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class EngineSquare
    {
        private int mValue;

        public int Value
        {
            get { return mValue; }
            set //0 implies blank.
            {
                if (value > 9 || value < 0)
                    throw new Exception("Can not set EngineSquare's value to " + value + ". Valid values are between 0 and 9");
                else
                    mValue = value;
            } 
        }

        public int SolutionIndex { get; set; } //0 implies a given value.

        public EngineSquare(int value)
        {
            Value = value;
        }
    }
}
