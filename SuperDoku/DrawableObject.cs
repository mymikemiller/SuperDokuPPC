using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace SuperDoku
{

    
    /// <summary>
    /// Represents an object that can be drawn to a BufferedUserControl by extracting an image from the skin file. (i.e. Squares, PencilMarks, SkinBanner, Background)
    /// </summary>
    public abstract class DrawableObject
    {
        private BufferedUserControl mParent;
       
        static DrawableObject()
        {


        }

        /// <summary>
        /// Creates a DrawableObject belonging to the specified UserControl.
        /// </summary>
        /// <param name="parent"></param>
        public DrawableObject(BufferedUserControl parent)
        {
            mParent = parent;
        }

        #region Properties

        /// <summary>Gets the rectangle on the skin file from which to extract the image for drawing.</summary>
        internal abstract Rectangle SrcRect { get; }
        /// <summary>Gets the rectangle on the off-screen buffer onto which to draw the unscaled image.</summary>
        internal abstract Rectangle DestRectUnscaled { get; }
        /// <summary>Gets the rectangle on the parent BufferedUserControl that is occupied by this DrawableObject, inflated to leave room for an outer border.</summary>
        internal Rectangle DestRectFinal
        {
            get
            {
                Rectangle destRect = DestRectUnscaled;
                double horizScale = mParent.Width / (double)mParent.OffscreenBitmap.Width;
                double vertScale = mParent.Height / (double)mParent.OffscreenBitmap.Height;

                destRect.X = (int)(destRect.X * horizScale) - 1;
                destRect.Y = (int)(destRect.Y * vertScale) - 1;
                destRect.Width = (int)(destRect.Width * horizScale) + 2;
                destRect.Height = (int)(destRect.Height * vertScale) + 2;

                return destRect;
            }
        }


        
        #endregion Properties

        #region Functions

        /// <summary>
        /// Draw onto the off-screen buffer and mark the area on the parent BufferedUserControl as Invalid.
        /// </summary>
        internal virtual void RePaint()
        {
            mParent.OffscreenGraphics.DrawImage(Settings.Skin.Bitmap, DestRectUnscaled, SrcRect, GraphicsUnit.Pixel);
            Invalidate();
        }

        /// <summary>
        /// RePaints while ignoring Magenta pixels.
        /// </summary>
        internal virtual void RePaintTransparent()
        {
            using (ImageAttributes attr = new ImageAttributes())
            {
                attr.SetColorKey(Color.Magenta, Color.Magenta);
                Rectangle srcRect = SrcRect;

                mParent.OffscreenGraphics.DrawImage(Settings.Skin.Bitmap, DestRectUnscaled, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, attr);
                Invalidate();
            }
        }

        /// <summary>Returns the SrcRect for the Square at the given 0-based x,y coordinate in the skin file (0,0 for blank with BackStyle=0, 3,5 for "5" with BackStyle=2. See skin file for more info). </summary>
        internal Rectangle GetSrcSquare(int x, int y)
        {
            int totalGutterX = Settings.Skin.GutterLength * (x + 1);
            int totalGutterY = Settings.Skin.GutterLength * (y + 1);

            int totalSquareX = Settings.Skin.SquareLength * x;
            int totalSquareY = Settings.Skin.SquareLength * y;

            return new Rectangle(totalGutterX + totalSquareX, totalGutterY + totalSquareY, Settings.Skin.SquareLength, Settings.Skin.SquareLength);
        }

        /// <summary>
        /// Invalidates the region on the parent BufferedUserControl that is occupied by this object. Causes that area to be redrawn the next time the parent is updated.
        /// </summary>
        internal void Invalidate()
        {
            mParent.Invalidate(DestRectFinal);
        }

        #endregion Functions


    }
}
