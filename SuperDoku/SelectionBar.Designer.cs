namespace SuperDoku
{
    partial class SelectionBar
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
            this.SuspendLayout();
            // 
            // SelectionBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "SelectionBar";
            this.Size = new System.Drawing.Size(158, 34);
            this.ParentChanged += new System.EventHandler(this.SelectionBar_ParentChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectionBar_MouseDown);
            this.Resize += new System.EventHandler(this.SelectionBar_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectionBar_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
