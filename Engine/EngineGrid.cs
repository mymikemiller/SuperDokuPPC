using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class EngineGrid
    {
        public static int KeyNum = 0;
        public static List<int> BadKeyNums = new List<int>();
        Levels mLevel;

        /// <summary>The collection of EngineSquares making up this EngineGrid.</summary>
        private EngineSquare[,] mSquares;

        public EngineGrid()
        {
            mLevel = Levels.Beginner;
            mSquares = new EngineSquare[9, 9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    mSquares[x, y] = new EngineSquare(0);
                }
            }
        }

        public EngineGrid(Levels level, string values)
        {
            mLevel = level;

            mSquares = new EngineSquare[9, 9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    mSquares[x, y] = new EngineSquare(Convert.ToInt32(values[y*9 + x] - '0'));
                }
            }
        }

        /// <summary>
        /// EngineSquare accessor. Returns the EngineSquare at the specified X and Y index.
        /// </summary>
        /// <param name="X">The 0-based X index for the returned EngineSquare.</param>
        /// <param name="Y">The 0-based Y index for the returned EngineSquare.</param>
        /// <returns>The EngineSquare at the specified X and Y index.</returns>
        public EngineSquare this[int X, int Y]
        {
            get
            {
                if (X >= 9 || Y >= 9)
                {
                    throw new Exception("Cannot access an EngineSquare with index " + X + ", " + Y + ". Max index is 8, 8.");
                }
                return mSquares[X, Y];
            }
            set
            {
                if (X >= 9 || Y >= 9)
                {
                    throw new Exception("Cannot access an EngineSquare with index " + X + ", " + Y + ". Max index is 8, 8.");
                }
                mSquares[X, Y] = value;
            }
        }

        /// <summary>
        /// EngineSquare accessor. Returns the EngineSquare at the specified position.
        /// </summary>
        /// <param name="p">The 0-based position index for the returned EngineSquare.</param>
        /// <returns>The EngineSquare at the specified position.</returns>
        //public EngineSquare this[Position p]
        //{
        //    get
        //    {
        //        return this[p.X, p.Y];
        //    }
        //    set
        //    {
        //        this[p.X, p.Y] = value;
        //    }
        //}

        /// <summary>Returns a String with all the Values in the EngineGrid from left to right, top to bottom. "0" is used for empty EngineSquares.</summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            for (int y = 0; y < 9; y++)
                for (int x = 0; x < 9; x++)
                    sb.Append(mSquares[x, y].Value);
            return sb.ToString();
        }
    }
}
