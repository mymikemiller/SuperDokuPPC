namespace SuperDoku
{
    partial class Field
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mSelectionBar = new SuperDoku.SelectionBar();
            this.mBoard = new SuperDoku.Board();
            this.SuspendLayout();
            // 
            // mSelectionBar
            // 
            this.mSelectionBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mSelectionBar.IgnoreSelectedStyle = false;
            this.mSelectionBar.Location = new System.Drawing.Point(20, 373);
            this.mSelectionBar.Name = "mSelectionBar";
            this.mSelectionBar.Size = new System.Drawing.Size(338, 42);
            this.mSelectionBar.TabIndex = 1;
            // 
            // mBoard
            // 
            this.mBoard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mBoard.IgnoreSelectedStyle = false;
            this.mBoard.Location = new System.Drawing.Point(20, 27);
            this.mBoard.Name = "mBoard";
            this.mBoard.Size = new System.Drawing.Size(338, 313);
            this.mBoard.TabIndex = 0;
            // 
            // Field
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.mSelectionBar);
            this.Controls.Add(this.mBoard);
            this.Name = "Field";
            this.Size = new System.Drawing.Size(378, 443);
            this.ParentChanged += new System.EventHandler(this.Field_ParentChanged);
            this.Resize += new System.EventHandler(this.Field_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Field_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Board mBoard;
        private SelectionBar mSelectionBar;
    }
}
