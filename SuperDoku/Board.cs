using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SuperDoku
{
    /// <summary>
    /// The Board containing the 81 Square objects (via the grid object).
    /// </summary>
    public partial class Board : GridUserControl
    {
        /// <summary>
        /// Creates a new Board.
        /// </summary>
        public Board()
            : base(9, 9, Settings.Skin.BoardLength, Settings.Skin.BoardLength) //set at the standard board size for vga skins.
        {
            InitializeComponent();
            Grid.RePaint();
        }


        #region Properties
        


        #endregion Properties

        #region Functions

        
        /// <summary>
        /// Initializes this Grid with the values from the specified EngineGrid.
        /// </summary>
        /// <param name="eg">The EngineGrid whose values are to be copied.</param>
        public void InitFromEngineGrid(Engine.EngineGrid eg)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Grid[x, y] = new Square(Grid, new Position(x, y), eg[x,y].Value);
                }
            }
        }

        public void InitFromString(string values)
        {
            if (values.Length != 81)
            {
                MessageBox.Show("Can not initialize the Grid with the specified string. String must contain exactly 81 numbers. Specified string: " + values);
            }
            else
            {
                int i = 0;
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        try
                        {
                            int val = int.Parse(values[i++].ToString());
                            Grid[x, y].InitWithValue(val);
                        }
                        catch
                        {
                            throw new Exception("Invalid character in string: " + values[i].ToString());
                        }
                    }
                }
                ApplyStyle();
            }
        }


        public Engine.EngineGrid ToEngineGrid()
        {
            Engine.EngineGrid eg = new Engine.EngineGrid();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if(Grid[x,y].Permanent)
                        eg[x, y] = new Engine.EngineSquare(Grid[x, y].Value);
                }
            }
            return eg;
        }

        /// <summary>
        /// Applies the Value of the ParentField's SelectionBar's SelectedSquare to this Board's SelectedSquare if this Board's SelectedSquare is not Permanent.
        /// </summary>
        /// <returns>True if the SelectedSquare was applied.</returns>
        public bool ApplySelectedSquare()
        {
            bool canApply = !SelectedSquare.Permanent;
            if (canApply)
            {
                int selectedValue = ParentField.SelectionBar.SelectedSquare.Value;
                if (Settings.Current.PencilMarkMode)
                {
                    SelectedSquare.PencilMarks[selectedValue].On = !SelectedSquare.PencilMarks[selectedValue].On;
                }
                else
                {
                    SelectedSquare.Value = SelectedSquare.Value == selectedValue ? 0 : selectedValue; //change to 0 if already at the number, otherwise change to the number
                }
            }
            return canApply;
        }

        #endregion Functions

        #region Events

        internal void selectionBar_SelectedSquareChanged(object sender, SquareChangedEventArgs e)
        {
            foreach (Square s in Grid.Squares)
            {
                if (s.Value == e.NewSquare.Value || s.Value == e.OldSquare.Value)
                {
                    ApplyStyle(s);
                }
            }
        }

        private void Board_KeyDown(object sender, KeyEventArgs e)
        {
            ParentField.Field_OnKeyDown(sender, e);
        }

        #region Mouse Events

        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            SelectedSquare = GetSquareAt(new Point(e.X, e.Y));
            ApplySelectedSquare();
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            if (!SelectedSquare.Permanent) //ignore mousemoves if the SelectedSquare is permanent.
            {
                if (e.Button == MouseButtons.Left)
                {
                    Square s = GetSquareAt(new Point(e.X, e.Y));
                    if (s != SelectedSquare) //the user has dragged off the starting square
                    {
                        SelectedSquare.Value = 0; //the user is trying to alter a pencil mark, so make sure the Square is blank

                        //find out what octant the mouse is now in in relation to the middle of the starting square.
                        Rectangle startRect = SelectedSquare.DestRectFinal;
                        Point center = new Point(startRect.X + (int)(startRect.Width / 2), startRect.Y + (int)(startRect.Height / 2));
                        int dx = e.X - center.X;
                        if (dx == 0) dx = 1; //don't allow divide by 0
                        int dy = center.Y - e.Y; //flip dy because the origin is at the top. from now on, octant 1 is at the top of the screen.
                        if (dy == 0) dy = 1; //don't allow divide by 0

                        int pencilMarkInOctant;
                        double slope = dy / (double)dx;

                        if (Math.Abs(slope) > 2)
                        {
                            pencilMarkInOctant = (dy > 0) ? 2 : 8;
                        }
                        else if (Math.Abs(slope) < .5)
                        {
                            pencilMarkInOctant = (dx > 0) ? 6 : 4;
                        }
                        else if (slope > 0)
                        {
                            pencilMarkInOctant = (dx > 0) ? 3 : 7;
                        }
                        else
                        {
                            pencilMarkInOctant = (dx > 0) ? 9 : 1;
                        }

                        SelectedSquare.PencilMarks.CurrentPencilMarkWhileDragging = pencilMarkInOctant;
                    }
                    else //the user is dragging within the starting square.
                    {
                        //actually, the user may be dragging, for example, above the SelectedSquare if it's on the top row, because GetSquareAt() would have still returned the SelectedSquare.
                        int pencilMark = 0;
                        if (e.X < 0)
                        {
                            if (e.Y < 0) pencilMark = 1;
                            else if (e.Y > this.Height) pencilMark = 7;
                            else pencilMark = 4;
                        }
                        else if (e.X > this.Width)
                        {
                            if (e.Y < 0) pencilMark = 3;
                            else if (e.Y > this.Height) pencilMark = 9;
                            else pencilMark = 6;
                        }
                        else if (e.Y < 0) pencilMark = 2;
                        else if (e.Y > this.Height) pencilMark = 8;

                        if (pencilMark != 0) SelectedSquare.PencilMarks.CurrentPencilMarkWhileDragging = pencilMark;
                        else
                        {

                            if (SelectedSquare.PencilMarks.CurrentPencilMarkWhileDragging != 0) //the user has been out of the current square and is now back in
                            {
                                SelectedSquare.PencilMarks.CurrentPencilMarkWhileDragging = 5;
                            }
                        }
                    }
                }
            }
        }

        private void Board_MouseUp(object sender, MouseEventArgs e)
        {
            SelectedSquare.PencilMarks.CurrentPencilMarkWhileDragging = 0; //let the PencilMarks collection know that we're no longer dragging
        }

        #endregion Mouse Events


        #endregion Events



    }
}

