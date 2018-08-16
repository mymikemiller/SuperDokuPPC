using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace SuperDoku
{
    public class BufferedUserControl : UserControl
    {
        private Bitmap mOffscreenBitmap;
        private Graphics mOffscreenGraphics;


        public BufferedUserControl(int initialBufferWidth, int initialBufferHeight)
            : base()
        {
            ResizeOffscreenBuffer(initialBufferWidth, initialBufferHeight);
        }

        /// <summary>This constructor is only used by the designer. it complains if it doesn't have a parameterless constructor.</summary>
        private BufferedUserControl() : base()
        {
            ResizeOffscreenBuffer(2,2);
        }

        /// <summary>Resizes OffscreenBitmap to the specified width and height and reinitializes OffscreenGraphics.</summary>
        internal void ResizeOffscreenBuffer(int width, int height)
        {
            if (mOffscreenBitmap == null || mOffscreenBitmap.Width != width || mOffscreenBitmap.Height != height)
            {
                mOffscreenBitmap = new Bitmap(width, height);
               mOffscreenGraphics = Graphics.FromImage(mOffscreenBitmap);//todo: remove this line?
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e); //commented out to avoid flicker
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //blit the off-screen buffer onto the screen.
            Rectangle dest = new Rectangle(0, 0, Width, Height);
            Rectangle src = new Rectangle(0, 0, mOffscreenBitmap.Width, mOffscreenBitmap.Height);
            e.Graphics.DrawImage(mOffscreenBitmap, dest, src, GraphicsUnit.Pixel);
        }

        /// <summary>Resizes the BufferedUserControl to the size and location of the newBounds Rectangle. Only causes one resize event.</summary>
        public void ResizeTo(Rectangle newBounds)
        {
            Location = new Point(newBounds.X, newBounds.Y);
            Size = new Size(newBounds.Width, newBounds.Height);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }



        public Bitmap OffscreenBitmap
        {
            get { return mOffscreenBitmap; }
        }

        public Graphics OffscreenGraphics
        {
            get { return mOffscreenGraphics; }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BufferedUserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "BufferedUserControl";
            this.ResumeLayout(false);

        }
    }
}
