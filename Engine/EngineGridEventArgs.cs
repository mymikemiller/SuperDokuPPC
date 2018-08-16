using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class EngineGridEventArgs : EventArgs
    {
        public EngineGridEventArgs(EngineGrid grid)
        {
            Grid = grid;
        }

        public EngineGrid Grid { get; private set; }

    }
}
