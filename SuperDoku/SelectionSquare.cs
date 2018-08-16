using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.Diagnostics;

namespace SuperDoku
{
    enum SelectionSquareStyles : int { Border1 = 0, Border2 };
        
    /// <summary>Represents the border that is drawn on top of the Grid's SelectedSquare.</summary>
    class SelectionSquare : DrawableObject
    {
        Square mOwnerSquare;
        SelectionSquareStyles mSelectionSquareStyle;

        /// <summary>
        /// Creates a new SelectionSquare
        /// </summary>
        public SelectionSquare(BufferedUserControl parent, Square ownerSquare, SelectionSquareStyles style)
            : base(parent)
        {
            mOwnerSquare = ownerSquare;
            mSelectionSquareStyle = style;
        }

        #region Properties

        internal override System.Drawing.Rectangle SrcRect
        {
            get  { return GetSrcSquare(0, 4 + (int)mSelectionSquareStyle); }
        }

        internal override System.Drawing.Rectangle DestRectUnscaled
        {
            get { return mOwnerSquare.DestRectUnscaled; }
        }

        #endregion Properties

        #region Functions

        internal override void RePaint()
        {
            base.RePaintTransparent();
        }

        #endregion Functions
    }
}
