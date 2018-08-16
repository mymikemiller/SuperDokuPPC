using System.Drawing;
using System;
using System.Diagnostics;
namespace SuperDoku
{
    //This file contains the code for resizing the Field. 
    partial class Field
    {



        private void Field_Resize(object sender, EventArgs e)
        {
            if (Parent != null) //only pay attention to the Resize events if the control has been fully initialized (at which point, the Parent has been set)
            {
                ResizeField();
            }
            
        }

        /// <summary>Fits the Board and SelectionBar appropriately in the Field.</summary>
        private void ResizeField()
        {
            ResizeBackgroundIfNecessary();

            int boardLength = GetMaxBoardLength();
            boardLength = IdealizeBoardLength(boardLength);

            double squareToBoardRatio = Settings.Skin.SquareLength / (double)Settings.Skin.BoardLength;
            int squareLength = (int)(boardLength * squareToBoardRatio);

            int margin = (int)(squareLength * Settings.Layout.MarginPercent);

            Rectangle boardRect = new Rectangle(0, 0, boardLength, boardLength);
            Rectangle selectionBarRect;

            SelectionBarPositions selectionBarPosition = SelectionBarPosition;

            Rectangle fitWithin = new Rectangle();

            if (selectionBarPosition == SelectionBarPositions.Top || selectionBarPosition == SelectionBarPositions.Bottom)
            {
                selectionBarRect = new Rectangle(0, 0, boardLength, squareLength);
                selectionBarRect = CenterX(selectionBarRect);
                boardRect.X = selectionBarRect.X;
                fitWithin.X = boardRect.X;
                fitWithin.Width = boardRect.Width;
                fitWithin.Height = boardRect.Height + margin + selectionBarRect.Height;
                fitWithin = CenterY(fitWithin);
            }
            else //left or right
            {
                selectionBarRect = new Rectangle(0, 0, squareLength, boardLength);
                selectionBarRect = CenterY(selectionBarRect);
                boardRect.Y = selectionBarRect.Y;
                fitWithin.Y = boardRect.Y;
                fitWithin.Height = boardRect.Height;
                fitWithin.Width = boardRect.Width + margin + selectionBarRect.Width;
                fitWithin = CenterX(fitWithin);
            }

            switch (SelectionBarPosition)
            {
                case SelectionBarPositions.Top:
                    selectionBarRect.Y = fitWithin.Y;
                    boardRect.Y = fitWithin.Bottom - boardRect.Height;
                    break;
                case SelectionBarPositions.Bottom:
                    selectionBarRect.Y = fitWithin.Bottom - selectionBarRect.Height;
                    boardRect.Y = fitWithin.Y;
                    break;
                case SelectionBarPositions.Left:
                    selectionBarRect.X = fitWithin.X;
                    boardRect.X = fitWithin.Right - boardRect.Width;
                    break;
                case SelectionBarPositions.Right:
                    selectionBarRect.X = fitWithin.Right - selectionBarRect.Width;
                    boardRect.X = fitWithin.X;
                    break;
            }

            mBoard.ResizeTo(boardRect);
            Debug.WriteLine("boardRect: " + boardRect.X + " " + boardRect.Y + " " + boardRect.Width + " " + boardRect.Height);
            mSelectionBar.ResizeTo(selectionBarRect);
        }


        /// <summary>Returns the maximum length of the Board based on the Field size and the SelectionBarPosition and Margin specified in Settings.LayoutSettings</summary>
        private int GetMaxBoardLength()
        {
            SelectionBarPositions selectionBarPosition = SelectionBarPosition;
            int verticalMax;
            int horizontalMax;
            if (selectionBarPosition == SelectionBarPositions.Top || selectionBarPosition == SelectionBarPositions.Bottom)
            {
                //the maximum Board length will be based either on the Height (while traveling through the SelectionBar)
                //or on the Width (not traveling through the SelectionBar)
                verticalMax = GetSpecificMaxBoardLength(Height, true);
                horizontalMax = GetSpecificMaxBoardLength(Width, false);
            }
            else
            {
                //the maximum Board length will be based either on the Width (while traveling through the SelectionBar)
                //or on the Height (not traveling through the SelectionBar)
                horizontalMax = GetSpecificMaxBoardLength(Width, true);
                verticalMax = GetSpecificMaxBoardLength(Height, false);
            }

            return Math.Min(verticalMax, horizontalMax); //whichever results in a smaller Board is the value we want, because having a bigger Board would cause overlap of the margins
        }

        /// <summary>Gets the maximum length (equals both width and height) of the Board for specific orientation of the screen, fit within a specified dimension length (i.e. the length of either the Width or the Height of the screen).</summary>
        private int GetSpecificMaxBoardLength(int fitWithin, bool selectionBarInPath)
        {
            double marginPercent = Settings.Layout.MarginPercent;
            double squareToBoardRatio = Settings.Skin.SquareLength / (double)Settings.Skin.BoardLength;
            int numMarginsInPath = selectionBarInPath ? 3 : 2;

            //these equations came from solving the following equations:
            //general form: BoardLength = fitWithin - (space taken up by margins) - (space taken up by SelectionBar if applicable)
            //define squareLength as BoardLength * squareToBoardRatio
            //if selectionBarInPath: BoardLength = fitWithin - numMarginsInPath * marginPercent * squareLength - squareLength
            //else: BoardLength = fitWithin - numMarginsInPath * marginPercent * squareLength

            if (selectionBarInPath)
            {
                return (int)(fitWithin / (1 + numMarginsInPath * marginPercent * squareToBoardRatio + squareToBoardRatio));
            }
            else
            {
                return (int)(fitWithin / (1 + numMarginsInPath * marginPercent * squareToBoardRatio));
            }
        }

        /// <summary>Some BoardLengths are better than others for resize issues, for example keeping the Board at the same size it is in the skin eliminates scaling.
        /// If the Board length is close enough to one of the ideal lengths, use that length instead of the calculated length.</summary>
        private int IdealizeBoardLength(int boardLength)
        {
            if (Settings.Skin.IdealBoardLengths.Contains(boardLength)) return boardLength;

            foreach (int idealLength in Settings.Skin.IdealBoardLengths)
            {
                double percentOff = (idealLength - boardLength) / (double)idealLength;
                if (Math.Abs(percentOff) < 0.03)
                {
                    return idealLength;
                }
            }
            //no ideal length found.
            return boardLength;
        }



        /// <summary>Returns the passed in Rectangle with modified X value to center it horizontally on the screen.</summary>
        private Rectangle CenterX(Rectangle r)
        {
            r.X = (int)((Width - r.Width) / 2);
            return r;
        }

        /// <summary>Returns the passed in Rectangle with modified Y value to center it vertically on the screen.</summary>
        private Rectangle CenterY(Rectangle r)
        {
            r.Y = (int)((Height - r.Height) / 2);
            return r;
        }



        /// <summary>Resizes the offscreen buffer of the background if the window has grown or shrunk enough to warrent a resize.</summary>
        private void ResizeBackgroundIfNecessary()
        {
            bool resize = false;
            if (Width > mBackground.Width || Height > mBackground.Height)
            {
                //mBackground need to be enlarged
                resize = true;

            }
            if (Width < mBackground.Width || Height < mBackground.Height)
            {
                //mBackground may need to be shrunk, but only if an entire row or column of the background tile can be removed.

                if ((mBackground.Width - Width) >= Settings.Skin.BackgroundRect.Width || (mBackground.Height - Height) >= Settings.Skin.BackgroundRect.Height)
                {
                    resize = true;
                }
            }

            if (resize) ResizeBackground();
        }

        private void ResizeBackground()
        {
            int w = Settings.Skin.BackgroundRect.Width;
            int h = Settings.Skin.BackgroundRect.Height;

            //we're going to size the background as a multiple of the size of the background tile, so we're actually blitting a bit of mBackground
            //off screen in OnPaintBackground, but that's ok, because we'll have to do less resizing of the bitmap when changing size of the Field
            int numX = (int)(Width / w) + 1;
            int numY = (int)(Height / h) + 1;

            mBackground = new Bitmap(numX * w, numY * h);

            using (Graphics g = Graphics.FromImage(mBackground))
            {
                for (int x = 0; x < mBackground.Width; x += w)
                {
                    for (int y = 0; y < mBackground.Height; y += h)
                    {
                        g.DrawImage(Settings.Skin.Bitmap, new Rectangle(x, y, w, h), Settings.Skin.BackgroundRect, GraphicsUnit.Pixel);
                    }
                }
            }
        }

    }
}
