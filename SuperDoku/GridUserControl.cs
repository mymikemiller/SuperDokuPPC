using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace SuperDoku
{
    public enum Directions : int { Left, Right, Up, Down }

    /// <summary>
    /// Represents a BufferedUserControl that is drawn according to a Grid that it contains (i.e. the Board and SelectionBar)
    /// </summary>
    public class GridUserControl : BufferedUserControl
    {
        private Grid mGrid;

        public GridUserControl(int rows, int cols, int initialBufferWidth, int initialBufferHeight)
            : base(initialBufferWidth, initialBufferHeight)
        {
            mGrid = new Grid(this, rows, cols);
        }

        /// <summary>This constructor is only used by the designer. it complains if it doesn't have a parameterless constructor.</summary>
        private GridUserControl() : base(2,2)
        {
        }

        #region Properties

        /// <summary>Gets the Grid encapsulated by this GridUserControl. If this is the SelectionBar, Grid can be either a row or column, so use SelectionBar.GetSquare(int index) to get the Square representing the number intended.</summary>
        public Grid Grid
        {
            get { return mGrid; }
        }

        /// <summary>Gets the Field that this GridUserControl belongs to.</summary>
        public Field ParentField
        {
            get { return (Field)Parent; }
        }

        /// <summary>Gets and sets the Square that is selected on this GridUserControl (only applies to the Board when using SmartphoneMode).</summary>
        public Square SelectedSquare
        {
            get { return mGrid.SelectedSquare; }
            set { mGrid.SelectedSquare = value; }
        }

        /// <summary>Gets and sets a value indicating whether or not to apply the SelectedStyle to the Squares on the Grid. Used to toggle SelectedStyle on the Board when the user repeatedly taps the SelectionBar.</summary>
        public bool IgnoreSelectedStyle { get; set; }


        #endregion Properties



        #region Style Logic


        /// <summary>Applies the correct Style to all Squares in this GridUserControl's Grid based on the options in Settings.StyleSettings.</summary>
        public void ApplyStyle()
        {
            foreach (Square s in mGrid.Squares) ApplyStyle(s);
        }

        /// <summary>Applies the correct Style to a single Square (on either Board or SelectionBar) based on the options in Settings.StyleSettings.</summary>
        public void ApplyStyle(Square square)
        {
            /*
            EverythingElseOnSelectionBar = Styles.Regular;
            EverythingElse = Styles.Regular;
            OriginalGame = new StyleSetting(Styles.Regular, false);
            CompletedBlock = new StyleSetting(Styles.Dim, false);
            CompletedRowOrColumn = new StyleSetting(Styles.Dim, false);
            CompletedOnSelectionRow = new StyleSetting(Styles.Dim, true);
            Completed = new StyleSetting(Styles.Dim, true);
            SelectedPencilMark = new StyleSetting(Styles.Bold, true);
            Selected = new StyleSetting(Styles.Bold, true);
             * */

            //the following are in order of priority
            if (!ApplySelectedStyle(square))
                if (!ApplyCompletedStyle(square))
                    if (!ApplyGivenStyle(square))
                        ApplyEverythingElseStyle(square);

        }

        /// <summary>Applies the correct style to the Square on the Board if its value is the same as the SelectionBar's SelectedSquare's value.</summary>
        /// <returns>True if the Style was applied.</returns>
        private bool ApplySelectedStyle(Square square)
        {
            bool applied = false;
            if (Settings.Style.Selected.Active && !IgnoreSelectedStyle && square.ParentGrid.IsBoard()) //Selected style only applies to the Board - Selected squares on the SelectionBar are bordered.
            {
                applied = (square.Value == ParentField.SelectionBar.SelectedSquare.Value);
                if (applied) square.Style = Settings.Style.Selected.Style;
            }
            return applied;
        }

        /// <summary>Applies the correct style to the Square if exactly nine Squares with the same value exist on the Board.</summary>
        /// <returns>True if the Style was applied.</returns>
        private bool ApplyCompletedStyle(Square square)
        {
            bool applied = false;
            if (square.ParentGrid.IsBoard())
            {
                if (Settings.Style.Completed.Active)
                {
                    applied = (ParentField.Board.Grid.ContainsNine(square.Value));
                    if (applied) square.Style = Settings.Style.Completed.Style;
                }
            }
            else //SelectionBar
            {
                if (Settings.Style.CompletedOnSelectionBar.Active)
                {
                    applied = (ParentField.Board.Grid.ContainsNine(square.Value));
                    if (applied) square.Style = Settings.Style.CompletedOnSelectionBar.Style;
                }
            }
            return applied;
        }

        /// <summary>Applies the correct style to the Square if it is a Given square on the Board.</summary>
        /// <returns>True if the Style was applied.</returns>
        private bool ApplyGivenStyle(Square square)
        {
            bool applied = false;
            if (square.ParentGrid.IsBoard() && Settings.Style.Given.Active)
            {
                applied = (square.Permanent);
                if (applied) square.Style = Settings.Style.Given.Style;
            }
            return applied;
        }





        /// <summary>Applies the correct style to the Square if it fits no other categories.</summary>
        private void ApplyEverythingElseStyle(Square square)
        {
            if (square.ParentGrid.IsBoard())
            {
                square.Style = Settings.Style.EverythingElse;
            }
            else //SelectionBar
            {
                square.Style = Settings.Style.EverythingElseOnSelectionBar;
            }
        }

        #endregion Style Logic

        #region Functions

        public Square GetSquareAt(Point boardScaledPixelPosition)
        {
            int x = Math.Max(0, (int)(9 * (boardScaledPixelPosition.X / (double)Width)));
            int y = Math.Max(0, (int)(9 * (boardScaledPixelPosition.Y / (double)Height)));
            x = Math.Min(x, 8);
            y = Math.Min(y, 8);

            return Grid[x, y];
        }

        public void MoveSelectedSquare(Directions direction)
        {
            Position p = SelectedSquare.Position;
            switch (direction)
            {
                case Directions.Left:
                    if (p.X > 0) SelectedSquare = mGrid[p.X - 1, p.Y];
                    break;
                case Directions.Right:
                    if (p.X < mGrid.NumCols - 1) SelectedSquare = mGrid[p.X + 1, p.Y];
                    break;
                case Directions.Up:
                    if (p.Y > 0) SelectedSquare = mGrid[p.X, p.Y - 1];
                    break;
                case Directions.Down:
                    if (p.Y < mGrid.NumRows - 1) SelectedSquare = mGrid[p.X, p.Y + 1];
                    break;
                default:
                    break;
            }
        }

        #endregion Functions

        #region Events

        internal void board_SquareValueChanged(object sender, SquareValueChangedEventArgs e)
        {
            bool containsNineNew = mGrid.ContainsNine(e.NewValue) && e.NewValue != 0;
            bool containsNineOld = mGrid.ContainsNine(e.OldValue) && e.OldValue != 0;
            IgnoreSelectedStyle = (containsNineNew || containsNineOld) ? true : false;

            foreach (Square s in mGrid.Squares)
            {
                //changing the Value of a Square on the Board means tha the Style of all Squares with the same Value may need to be updated
                if (s.Value == e.NewValue || s.Value == e.OldValue)
                {
                    ApplyStyle(s);
                }
            }
        }

        #endregion Events


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GridUserControl
            // 
            this.Name = "GridUserControl";
            this.ResumeLayout(false);

        }
    }
}
