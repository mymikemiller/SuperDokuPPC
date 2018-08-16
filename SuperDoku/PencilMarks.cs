using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SuperDoku
{
    /// <summary>A collection of the 9 PencilMark objects on the Squares on the Board. Can be accessed via [] with the value of the PencilMark, or with the 0-based x, y location within the Square.</summary>
    public class PencilMarks
    {
        private List<PencilMark> mPencilMarkList;
        Square mParentSquare;

        /// <summary>Set me via the property, not this variable because the property handles re-drawing the PencilMark when it's changed.</summary>
        private int mCurrentPencilMarkWhileDragging = 0; //if 0, implies that the user is not dragging, or has not yet dragged outside of the current Square

        /// <summary>Initializes a new collection of new, off PencilMark objects for the specified Square.</summary>
        /// <param name="parentSquare"></param>
        public PencilMarks(Square parentSquare)
        {
            mParentSquare = parentSquare;
            mPencilMarkList = new List<PencilMark>(9);
            for (int i = 1; i <= 9; i++)
            {
                mPencilMarkList.Add(new PencilMark(this, i));
            }
        }


        #region Properties

        /// <summary>Accessor for the PencilMark with the specified Value (1 to 9)</summary>
        public PencilMark this[int Value]
        {
            get
            {
                if (Value > 9 || Value < 1)
                {
                    throw new Exception("Cannot access a PencilMark with value " + Value + ".");
                }
                return mPencilMarkList[Value - 1];
            }
        }

        public PencilMark this[int X, int Y]
        {
            get
            {
                if (X > 2 || X < 0 || Y > 2 || Y < 0)
                {
                    throw new Exception("Cannot access a PencilMark with index " + X + ", " + Y + ". Valud values for X and Y are between 0 and 2.");
                }
                return mPencilMarkList[Y * 3 + X];
            }
        }

        public Square ParentSquare { get { return mParentSquare; } }

        public int CurrentPencilMarkWhileDragging
        {
            get { return mCurrentPencilMarkWhileDragging; }
            set 
            {
                if (mCurrentPencilMarkWhileDragging != value)
                {
                    if(value != 0 && mCurrentPencilMarkWhileDragging != 0) this[mCurrentPencilMarkWhileDragging].On = !this[mCurrentPencilMarkWhileDragging].On;
                    mCurrentPencilMarkWhileDragging = value;
                    if(value != 0) this[value].On = !this[value].On;
                }
            }
        }

        #endregion Properties

        #region Functions

        /// <summary>Repaints all the On PencilMark objects.</summary>
        internal virtual void RePaint()
        {
            foreach (PencilMark p in mPencilMarkList)
            {
                if(p.On)
                    p.RePaint();
            }
        }

        /// <summary>Turns all PencilMark objects Off</summary>
        public void Clear()
        {
            foreach (PencilMark p in mPencilMarkList)
            {
                p.On = false;
            }
        }

        #endregion Functions
    }
}
