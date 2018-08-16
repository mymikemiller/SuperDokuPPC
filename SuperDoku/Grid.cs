using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace SuperDoku
{
    /// <summary>Handles events caused when the Selected Square for this Grid is changed.</summary>
    public delegate void SelectedSquareChangedHandler(object sender, SquareChangedEventArgs e);
    /// <summary>Handles events caused when the value of a Square in this Grid has been changed.</summary>
    public delegate void SquareValueChangedHandler(object sender, SquareValueChangedEventArgs e);

    /// <summary>
    /// A collection of Square objects.
    /// </summary>
    public class Grid
    {
        /// <summary>Occurs when the SelectedSquare for this Grid has been changed.</summary>
        public event SelectedSquareChangedHandler SelectedSquareChanged;
        /// <summary>Occurs when the the Value of a Square on this Grid has been changed.</summary>
        public event SquareValueChangedHandler SquareValueChanged;

        /// <summary>The GridUserControl that contains this Grid.</summary>
        private GridUserControl mParent;

        /// <summary>The collection of Squares making up this Grid.</summary>
        private Square[,] mSquares;

        Position mSelectedSquarePosition;


        /// <summary>
        /// Creates a new Grid with the specified number of rows and columns.
        /// </summary>
        /// <param name="parent">The GridUserControl that contains the Grid.</param>
        /// <param name="numRows">The number of Rows of Squares in the Grid.</param>
        /// <param name="numCols">The number of Columns of Squares in the Grid.</param>
        public Grid(GridUserControl parent, int numRows, int numCols)
        {
            mParent = parent;
            mSquares = new Square[numCols, numRows];

            for (int x = 0; x < numCols; x++)
            {
                for (int y = 0; y < numRows; y++)
                {
                    mSquares[x,y] = new Square(this, new Position(x, y));
                }
            }

            mSelectedSquarePosition = new Position(0, 0);
        }

        /// <summary>
        /// Square accessor. Returns the Square at the specified X and Y index.
        /// </summary>
        /// <param name="X">The 0-based X index for the returned Square.</param>
        /// <param name="Y">The 0-based Y index for the returned Square.</param>
        /// <returns>The Square at the specified X and Y index.</returns>
        public Square this[int X, int Y]
        {
            get
            {
                if (X >= NumCols || Y >= NumRows)
                {
                    throw new Exception("Cannot access a Square with index " + X + ", " + Y + ". Max index is " + (NumCols - 1) + " by " + (NumRows - 1));
                }
                return mSquares[X,Y];
            }
            set
            {
                if (X >= NumCols || Y >= NumRows)
                {
                    throw new Exception("Cannot access a Square with index " + X + ", " + Y + ". Max index is " + (NumCols - 1) + " by " + (NumRows - 1));
                }
                mSquares[X, Y] = value;
                mSquares[X, Y].RePaint();
            }
        }

        /// <summary>
        /// Square accessor. Returns the Square at the specified position.
        /// </summary>
        /// <param name="p">The 0-based position index for the returned Square.</param>
        /// <returns>The Square at the specified position.</returns>
        public Square this[Position p]
        {
            get
            {
                return this[p.X, p.Y];
            }
            set
            {
                this[p.X, p.Y] = value;
            }
        }


        #region Properties

        /// <summary>The GridUserControl that this Grid belongs to.</summary>
        public GridUserControl Parent { get { return mParent; } }

        /// <summary>Gets the number of columns in this Grid.</summary>
        public int NumCols
        {
            get { return mSquares.GetLength(0); }
        }

        /// <summary>Gets the number of rows in this Grid.</summary>
        public int NumRows
        {
            get { return mSquares.GetLength(1); }
        }

        /// <summary>Gets the 2D array of Squares represented by this Grid.</summary>
        public Square[,] Squares { get { return mSquares; } }

        public Position SelectedSquarePosition
        {
            get { return mSelectedSquarePosition; }
            set
            {
                if (value.X >= NumCols || value.Y >= NumRows || value.X < 0 || value.Y < 0)
                    throw new Exception("Can not assign SelectedSquarePosition to " + value.ToString() + ". This Grid is only " + NumRows + " by " + NumCols + ".");

                Square oldSelected = SelectedSquare;
                Square newSelected = this[value];

                if (oldSelected == null || oldSelected.Position != value)
                {
                    //if (oldSelected != null)
                    //{
                    //    Square temp = oldSelected;
                    //    mSelectedSquare = null; //set first to null so the old SelectedSquare will be drawn as non-selected.
                    //    temp.RePaint(); //redraw the old selected square
                    //}

                    mSelectedSquarePosition = value;
                    if (oldSelected != null) oldSelected.RePaint();
                    newSelected.RePaint();

                    if (SelectedSquareChanged != null)
                    {
                        SelectedSquareChanged(this, new SquareChangedEventArgs(oldSelected, newSelected));
                    }
                }

            }
        }

        /// <summary>Gets and sets the Selected Square for this Grid. For the Board, this is the square that will be altered when using SmartPhone mode. For the SelectionBar, this is the currently selected Square.</summary>
        public Square SelectedSquare
        {
            get { return this[mSelectedSquarePosition]; }
            set
            {
                if (this.Parent != value.ParentGrid.Parent)
                    throw new Exception("The SelectedSquare property of this Grid can not be set to a Square that is not a part of this Grid's GridUserControl");

                SelectedSquarePosition = value.Position;
            }
        }

        /// <summary>Gets and (private) sets the list of values for which there are exactly 9 Squares on the board.</summary>
        public List<int> CompletedValues { get; private set; }
        /// <summary>Gets and (private) sets the list of 0-based rows whose Squares all have a value.</summary>
        public List<int> CompletedRows { get; private set; }
        /// <summary>Gets and (private) sets the list of 0-based columns whose Squares all have a value.</summary>
        public List<int> CompletedColumns{ get; private set; }

        #endregion Properties

        #region Functions

        /// <summary>Repaints the entire Grid.</summary>
        public void RePaint()
        {
            foreach (Square s in mSquares)
            {
                //if (mSelectedSquare == null || mSelectedSquare != s)
                //{
                    s.RePaint();
                //}
            }
            //if (mSelectedSquare != null) mSelectedSquare.RePaint();
        }

        /// <summary>Invalidates the entire Grid, causing it to repaint itself.</summary>
        public void Invalidate()
        {
            mParent.Invalidate();
        }

        /// <summary>Flips the Grid about the y=x axis. Alters the index in the Squares array and alters each Square's Position value to reflect the change. Used for flipping the SelectionBar from horizontal to vertical and vice versa.</summary>
        internal void Flip()
        {
            Square[,] newSquares = new Square[NumRows, NumCols];
            //loop through the old Squares array adding to the new Squares array.
            for (int x = 0; x < NumRows; x++)
            {
                for (int y = 0; y < NumCols; y++)
                {
                    newSquares[x, y] = mSquares[y,x];
                    newSquares[x, y].SetPosition(new Position(x, y));
                }
            }
            mSquares = newSquares;
            mSelectedSquarePosition = new Position(mSelectedSquarePosition.Y, mSelectedSquarePosition.X);
            if(mSquares[0,0] != null) mSquares[0,0].RePaint(); //the only one needing special repanting is 0,0 because it wasn't altered.
        }

        /// <summary>Returns true if this Grid is the game Board and not the SelectionBar.</summary>
        public bool IsBoard()
        {
            return NumRows == 9 && NumCols == 9;
        }

        /// <summary>Returns true if the Grid contains exaclty nine Squares with the specified Value.</summary>
        public bool ContainsNine(int value)
        {
            int count = 0;
            foreach (Square s in mSquares)
            {
                if (s.Value == value) count++;
                if (count > 9) break;
            }
            return count == 9;
        }

        /// <summary>Returns the number of Squares the Grid contains with the specified value.</summary>
        public int NumberOf(int value)
        {
            int count = 0;
            foreach (Square s in mSquares)
            {
                if (s.Value == value) count++;
            }
            return count;
        }

        /// <summary>Removes all the PencilMarks on each of this Grid's Square objects</summary>
        internal void ClearPencilMarks()
        {
            foreach (Square s in mSquares)
            {
                s.PencilMarks.Clear();
            }
        }

        /// <summary>Returns a String with all the Values in the Grid from left to right, top to bottom. "0" is used for empty Squares.</summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            for (int y = 0; y < NumRows; y++)
                for (int x = 0; x < NumCols; x++)
                    sb.Append(mSquares[x, y].Value);
            return sb.ToString();
        }

        #endregion Functions


        #region Event Throws

        internal void ThrowSquareValueChangedEvent(object sender, SquareValueChangedEventArgs e)
        {
            if (SquareValueChanged != null)
            {
                SquareValueChanged(sender, e);
            }
        }

        #endregion Event Throws
    }
}
