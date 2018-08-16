using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace SuperDoku
{
    public delegate void ValueChangedHandler(object sender, SquareValueChangedEventArgs e);

    /// <summary>
    /// Represents one of the 81 Squares on the Board or the 9 Squares on the SelectionBar.
    /// </summary>
    public class Square : VariableStyleDrawableObject
    {
        //[System.ComponentModel.Description("Occurs when the Value of this Square has changed.")]
        public event ValueChangedHandler ValueChanged;

        int mValue = 0; //0 implies blank.
        int mSolutionIndex = 0; //0 implies a given value.
        bool mQuestionMark = false; //if true, display a question mark instead of the value.
        bool mPermanent = false; //permanent squares' values can not be changed; they were given at the beginning of the game

        PencilMarks mPencilMarks; //the collection of PencilMark objects for this Square. Only initialized if this Square is on the Board.

        /// <summary>The Grid that this Square belongs to.</summary>
        private Grid mParentGrid;
        private Position mPosition;

        /// <summary>
        /// Creates a Square at the given position in the given parent Grid.
        /// </summary>
        /// <param name="parentGrid">The Grid that contains this Square.</param>
        /// <param name="pos">The 0-based position that this Square ocupies in the parent Grid.</param>
        internal Square(Grid parentGrid, Position pos)
            : base(parentGrid.Parent)
        {
            mParentGrid = parentGrid;
            mPosition = pos;
            if (parentGrid.IsBoard())
            {
                mPencilMarks = new PencilMarks(this);
            }
        }

        /// <summary>
        /// Creates a Square at the given position in the given parent Grid based on the specified EngineSquare.
        /// </summary>
        /// <param name="parentGrid">The Grid that contains this Square.</param>
        /// <param name="pos">The 0-based position that this Square ocupies in the parent Grid.</param>
        /// <param name="val">The Value of this Square.</param>
        internal Square(Grid parentGrid, Position pos, int val)
            : this(parentGrid, pos)
        {
            mValue = val;
            mPermanent = (val != 0);
        }

        /// <summary>Sets the Square to the specified value with all other properties defaulted. The Square is made permanent if the value is 0.</summary>
        internal void InitWithValue(int val)
        {
            mPencilMarks.Clear();
            mPermanent = false;
            Value = val;
            mPermanent = (val != 0);
            mParentGrid.Parent.ApplyStyle(this);
        }

        #region Properties

        /// <summary>Gets and sets the number displayed in this Square. 0 implies blank.</summary>
        public int Value
        {
            get { return mValue; }
            set
            {
                if (mPermanent) throw new Exception("Can not change the value of a Permanent (Given) Square");
                if (mValue != value)
                {
                    if (value < 0 || value > 9) throw new Exception("Can not set the Value of this Square to " + value + ". Valid Values are 0-9");
                    int oldValue = mValue;
                    mValue = value;

                    mParentGrid.ThrowSquareValueChangedEvent(this, new SquareValueChangedEventArgs(this, oldValue, mValue));

                    mParentGrid.Parent.ApplyStyle(this);
                    RePaint();
                }
            }
        }

        /// <summary>Gets the x,y Position of this Square within the containing Grid. Alter the Position via the SetPosition method.</summary>
        public Position Position
        {
            get { return mPosition; }
        }

        public bool Permanent
        {
            get { return mPermanent; }
            set 
            {
                mPermanent = value;
                mParentGrid.Parent.ApplyStyle(this);
            }
        }

        /// <summary>Gets the collection of PencilMark objects for this Square. Only initialized if this Square is on the Board.</summary>
        public PencilMarks PencilMarks
        {
            get { return mPencilMarks; }
        }

        /// <summary>The Grid that this Square belongs to.</summary>
        public Grid ParentGrid { get { return mParentGrid; } }

        internal override Rectangle DestRectUnscaled
        {
            get
            {
                //calculate the destination rectangle based on the side length and position
                int nonOverlappingSideLength = Settings.Skin.SquareLength - Settings.Skin.OverlapLength;
                int overlapOffsetX = (int)(mPosition.X / 3) * Settings.Skin.OverlapLength; //the middle and right column of blocks need to be shifted right because block borders do not overlap
                int overlapOffsetY = (int)(mPosition.Y / 3) * Settings.Skin.OverlapLength; //the middle and bottom row of blocks need to be shifted down because block borders do not overlap

                int x = mPosition.X * nonOverlappingSideLength + overlapOffsetX;
                int y = mPosition.Y * nonOverlappingSideLength + overlapOffsetY;

                return new Rectangle(x, y, Settings.Skin.SquareLength, Settings.Skin.SquareLength);
            }
        }

        internal override Rectangle SrcRect
        {
            get
            {
                if (mQuestionMark)
                {
                    return GetSrcSquare(0, (int)BackStyle + 2);
                }
                else if (IsBlank())
                {
                    return GetSrcSquare(0, (int)BackStyle);
                }
                else
                {
                    return GetSrcSquare(mValue, (int)Style * 2 + (int)BackStyle);
                }
            }
        }

        /// <summary>Specifies whether or not to display a question mark instead of the Square's value.</summary>
        public bool QuestionMark
        {
            get { return mQuestionMark; }
            set
            {
                if (mQuestionMark != value)
                {
                    mQuestionMark = value;
                    RePaint();
                }
            }
        }

        internal override BackStyles BackStyle
        {
            get
            {
                if (mParentGrid.IsBoard())
                {
                    return (BackStyles)(Block % 2);
                }
                else
                {
                    //the SelectionBar only uses one BackStyle.
                    return BackStyles.Style1;
                }
            }
        }

        internal override int Block
        {
            get 
            {
                //calculate the block based on row and column.
                int horiz = (int)(mPosition.X / 3);
                int vert = (int)(mPosition.Y / 3);
                return vert + horiz * 3;
            }
        }

        #endregion Properties

        #region Functions

        

        public void SetPosition(Position pos)
        {
            if (!mPosition.Equals(pos))
            {
                mPosition = pos;
                RePaint();
            }
        }

        public bool IsBlank()
        {
            return mValue == 0;
        }

        /// <summary>
        /// Draw onto the off-screen buffer and mark the area on the parent BufferedUserControl as Invalid.
        /// </summary>
        internal override void RePaint()
        {
            base.RePaint();

            //repaint the On PencilMark objects
            if(mPencilMarks != null) mPencilMarks.RePaint();

            if (Settings.Navigation.SmartphoneMode || !mParentGrid.IsBoard())
            {
                    
                //the square has been drawn. now draw the selection rectangle around it if this square is the SelectedSquare.

                SelectionSquareStyles style = mParentGrid.IsBoard() ? SelectionSquareStyles.Border2 : SelectionSquareStyles.Border1;
                if (this == mParentGrid.SelectedSquare)
                {
                    SelectionSquare s = new SelectionSquare(mParentGrid.Parent, this, style);
                    s.RePaint();
                }
                //the SelectionSquare needs to be redrawn on top of the selected square if it borders this square, but only if they're in the same block.
                else if (this.Block == mParentGrid.SelectedSquare.Block && this.IsNextTo(mParentGrid.SelectedSquare))
                {
                    SelectionSquare s = new SelectionSquare(mParentGrid.Parent, mParentGrid.SelectedSquare, style);
                    s.RePaint();
                }
            }
        }

        public bool IsNextTo(Square squareToTest)
        {
            return (this != squareToTest) && (Math.Abs(this.Position.X - squareToTest.Position.X) <= 1 && Math.Abs(this.Position.Y - squareToTest.Position.Y) <= 1);
        }

        #endregion Functions
    }
}