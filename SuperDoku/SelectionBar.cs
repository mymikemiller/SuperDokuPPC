using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SuperDoku
{
    
    public partial class SelectionBar : GridUserControl
    {
        public enum Orientations : int { Horizontal, Vertical }
        Orientations mOrientation = Orientations.Horizontal;

        /// <summary>Creates a new SelectionBar.</summary>
        public SelectionBar()
            : base(1, 9, Settings.Skin.BoardLength, Settings.Skin.SquareLength) //set to the standard horizontal SelectionBar size for vga skins. on Field resize, a call to ResizeOffscreenBuffer might change the size of the buffer to make it vertical.
        {
            InitializeComponent();
        }

        //oddly, we lack a Load function, so we'll use ParentChanged. This makes sure that this initialization doesn't occur until after the Parent 
        //has been set, so the events that fire when Squares change can refer to the parent.
        private void SelectionBar_ParentChanged(object sender, EventArgs e)
        {

            for (int i = 1; i <= 9; i++)
            {
                GetSquare(i).Value = i;
            }

            Grid.RePaint();
            Grid.SelectedSquare = GetSquare(1);
        }

        #region Properties


        public Orientations Orientation
        {
            get
            {
                return mOrientation;
            }
        }

        #endregion Properties


        #region Functions

        private void SelectionBar_Resize(object sender, EventArgs e)
        {
            FlipIfNecessary();
        }

        /// <summary>
        /// If the SelectionBar is taller than it is wide, the squares needs to be vertical, vice versa for horizontal.
        /// </summary>
        private void FlipIfNecessary()
        {
            if (Width > Height) //it should be horizontal
            {
                if (mOrientation == Orientations.Vertical)
                {
                    mOrientation = Orientations.Horizontal;
                    ResizeOffscreenBuffer(Settings.Skin.BoardLength, Settings.Skin.SquareLength);
                    Grid.Flip();
                }
            }
            else //it should be vertical
            {
                if (mOrientation == Orientations.Horizontal)
                {
                    mOrientation = Orientations.Vertical;
                    ResizeOffscreenBuffer(Settings.Skin.SquareLength, Settings.Skin.BoardLength);
                    Grid.Flip();
                }
            }
        }

        /// <summary>Get the Square representing the specified number.</summary>
        /// <param name="number">The selection number (1 through 9) whose square is to be returned.</param>
        /// <returns>The Square representing the specified number.</returns>
        public Square GetSquare(int number)
        {
            if (number < 1 || number > 9) throw new Exception("Can not get number " + number + " on the SelectionBar. Valid values are 1-9.");
            if (mOrientation == Orientations.Horizontal)
            {
                //the squares are indexed by x value.
                return Grid[number - 1, 0];
            }
            else
            {
                //the squares are indexed by the y value.
                return Grid[0, number - 1];
            }
        }

        #endregion Functions

        #region Events

        private void SelectionBar_MouseDown(object sender, MouseEventArgs e)
        {
            Square newSelected;

            if (mOrientation == Orientations.Horizontal)
            {
                newSelected = GetSquareAt(new Point(e.X, 0));
            }
            else
            {
                newSelected = GetSquareAt(new Point(0, e.Y));
            }

            Square oldSelected = SelectedSquare;

            if (oldSelected == newSelected)
            {
                if (Settings.Style.ToggleSelectedStyleOnRepeatedTaps)
                {
                    ParentField.Board.IgnoreSelectedStyle = !ParentField.Board.IgnoreSelectedStyle;
                    foreach (Square s in ParentField.Board.Grid.Squares)
                    {
                        if (s.Value == newSelected.Value) ParentField.Board.ApplyStyle(s);
                    }
                }
            }
            else
            {
                ParentField.Board.IgnoreSelectedStyle = false;
                SelectedSquare = newSelected;
            }
            
        }

        private void SelectionBar_KeyDown(object sender, KeyEventArgs e)
        {
            ParentField.Field_OnKeyDown(sender, e);
        }

        #endregion Events
    }
}
