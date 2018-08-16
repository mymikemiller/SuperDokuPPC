using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace SuperDoku
{
    /// <summary>
    /// Represents a PencilMark on a Square
    /// </summary>
    public class PencilMark : VariableStyleDrawableObject
    {
        int mValue;
        bool mOn = false;

        /// <summary>The PencilMarks collection that this PencilMark belongs to.</summary>
        private PencilMarks mParentPencilMarksList;

        /// <summary>
        /// Creates an off PencilMark with the given value in the given parent Square.
        /// </summary>
        internal PencilMark(PencilMarks parentPencilMarksList, int value)
            : base(parentPencilMarksList.ParentSquare.ParentGrid.Parent)
        {
            mParentPencilMarksList = parentPencilMarksList;
            mValue = value;
        }

        #region Properties

        /// <summary>Gets and sets the number displayed in this Square. 0 implies blank.</summary>
        public int Value
        {
            get { return mValue; }
        }

        /// <summary>Gets the x,y Position of this PencilMark within the containing Square.</summary>
        public Position Position
        {
            get 
            {
                return new Position((mValue - 1) % 3, ((int)(mValue - 1) / 3));
            }
        }

        public bool On
        {
            get { return mOn; }
            set 
            {
                if (mOn != value)
                {
                    mOn = value;
                    if(mParentPencilMarksList.ParentSquare.IsBlank())
                        RePaint();
                }
            }
        }

        /// <summary>The PencilMarks collection that this PencilMark belongs to.</summary>
        public PencilMarks ParentPencilMarksList { get { return mParentPencilMarksList; } }

        internal override Rectangle DestRectUnscaled
        {
            get
            {
                Rectangle parentRect = mParentPencilMarksList.ParentSquare.DestRectUnscaled;
                Position p = Position;
                //calculate the destination rectangle based on the side length and position
                //the width and height is shorter if the PencilMark is in a middle row / column (respectively) of the parent Square.
                int width = p.X == 1 ? Settings.Skin.PencilMarkShortLength : Settings.Skin.PencilMarkLongLength;
                int height = p.Y == 1 ? Settings.Skin.PencilMarkShortLength : Settings.Skin.PencilMarkLongLength;
                int x = parentRect.X;
                int y = parentRect.Y;
                if (p.X > 0) x += p.X == 1 ? Settings.Skin.PencilMarkLongLength : Settings.Skin.PencilMarkLongLength + Settings.Skin.PencilMarkShortLength;
                if (p.Y > 0) y += p.Y == 1 ? Settings.Skin.PencilMarkLongLength : Settings.Skin.PencilMarkLongLength + Settings.Skin.PencilMarkShortLength;

                return new Rectangle(x, y, width, height);
            }
        }

        internal override Rectangle SrcRect
        {
            get
            {
                Rectangle sourceSquare;
                sourceSquare = mOn ? GetSrcSquare(10, (int)Style * 2 + (int)BackStyle) : GetSrcSquare(0, (int)BackStyle);
                Position p = Position;
                //calculate the source rectangle based on the side length and position
                //the width and height is shorter if the PencilMark is in a middle row / column (respectively) of the parent Square.
                int width = p.X == 1 ? Settings.Skin.PencilMarkShortLength : Settings.Skin.PencilMarkLongLength;
                int height = p.Y == 1 ? Settings.Skin.PencilMarkShortLength : Settings.Skin.PencilMarkLongLength;
                int x = sourceSquare.X;
                int y = sourceSquare.Y;
                if (p.X > 0) x += p.X == 1 ? Settings.Skin.PencilMarkLongLength : Settings.Skin.PencilMarkLongLength + Settings.Skin.PencilMarkShortLength;
                if (p.Y > 0) y += p.Y == 1 ? Settings.Skin.PencilMarkLongLength : Settings.Skin.PencilMarkLongLength + Settings.Skin.PencilMarkShortLength;

                return new Rectangle(x, y, width, height);

            }
        }

        internal override BackStyles BackStyle
        {
            get
            {
                return mParentPencilMarksList.ParentSquare.BackStyle;
            }
        }

        internal override int Block
        {
            get 
            {
                return mParentPencilMarksList.ParentSquare.Block;
            }
        }

        #endregion Properties

        #region Functions


        /// <summary>
        /// Draw onto the off-screen buffer and mark the area on the parent BufferedUserControl as Invalid.
        /// </summary>
        internal override void RePaint()
        {
            //only draw PencilMark objects onto Blank Squares.
            if (mParentPencilMarksList.ParentSquare.IsBlank())
            {
                base.RePaint();

                if (Settings.Navigation.SmartphoneMode)
                {
                    //the PencilMark has been drawn. now draw the selection rectangle around it if its parent square is the SelectedSquare.
                    if (mParentPencilMarksList.ParentSquare == mParentPencilMarksList.ParentSquare.ParentGrid.SelectedSquare)
                    {
                        SelectionSquareStyles style = SelectionSquareStyles.Border2;
                        SelectionSquare s = new SelectionSquare(mParentPencilMarksList.ParentSquare.ParentGrid.Parent, mParentPencilMarksList.ParentSquare, style);
                        s.RePaint();
                    }
                }
            }

        }

        #endregion Functions
    }
}