using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SuperDoku
{
    public enum Styles : int { Regular = 0, Bold, Dim };

    /// <summary>
    /// A DrawableObject designed to describe elements that can be either Regular, Bold or Dim (i.e. Squares and PencilMarks).
    /// </summary>
    public abstract class VariableStyleDrawableObject : DrawableObject
    {

        internal Styles mStyle = Styles.Regular;

        /// <summary>The background style.</summary>
        internal enum BackStyles : int
        {
            /// <summary>The background style for the blocks of Squares at the corners and middle of the Board.</summary>
            Style1 = 1,
            /// <summary>The background style for the blocks of Squares in the middle at the top, left, right and bottom sides of the Board.</summary>
            Style2
        };


        /// <summary>
        /// Creates a new VariableStyleDrawableObject
        /// </summary>
        /// <param name="parent"></param>
        public VariableStyleDrawableObject(GridUserControl parent)
            : base(parent)
        {
        }

        /// <summary>Specifies whether the BoardDrawableObject is regular, bold or dim.</summary>
        internal Styles Style
        {
            get { return mStyle; }
            set
            {
                if (mStyle != value)
                {
                    mStyle = value;
                    RePaint();
                }
            }
        }
        /// <summary>Specifies the Background Style of the BoardDrawableObject (depends on the block).</summary>
        internal abstract BackStyles BackStyle { get; }
        /// <summary>Gets the block that this DrawableObject is in. 0 for top-left, 1 for top-middle, 2 for top-right, 3 for middle-left, etc.</summary>
        internal abstract int Block { get; }

    }
}
