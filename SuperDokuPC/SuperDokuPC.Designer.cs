namespace SuperDoku
{
    partial class SuperDokuPC
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuperDokuPC));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBeginner = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEasy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMild = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModerate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDifficult = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHarder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHardest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.mField = new SuperDoku.Field();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mainMenu.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.toolsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(6, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(91, 24);
            this.mainMenu.Stretch = false;
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "File";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuNew
            // 
            this.mnuNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBeginner,
            this.mnuEasy,
            this.mnuMild,
            this.mnuModerate,
            this.mnuDifficult,
            this.mnuHard,
            this.mnuHarder,
            this.toolStripSeparator1,
            this.mnuHardest});
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(106, 22);
            this.mnuNew.Text = "New";
            // 
            // mnuBeginner
            // 
            this.mnuBeginner.Name = "mnuBeginner";
            this.mnuBeginner.Size = new System.Drawing.Size(131, 22);
            this.mnuBeginner.Tag = "0";
            this.mnuBeginner.Text = "Beginner";
            this.mnuBeginner.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuEasy
            // 
            this.mnuEasy.Name = "mnuEasy";
            this.mnuEasy.Size = new System.Drawing.Size(131, 22);
            this.mnuEasy.Tag = "1";
            this.mnuEasy.Text = "Easy";
            this.mnuEasy.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuMild
            // 
            this.mnuMild.Name = "mnuMild";
            this.mnuMild.Size = new System.Drawing.Size(131, 22);
            this.mnuMild.Tag = "2";
            this.mnuMild.Text = "Mild";
            this.mnuMild.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuModerate
            // 
            this.mnuModerate.Name = "mnuModerate";
            this.mnuModerate.Size = new System.Drawing.Size(131, 22);
            this.mnuModerate.Tag = "3";
            this.mnuModerate.Text = "Moderate";
            this.mnuModerate.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuDifficult
            // 
            this.mnuDifficult.Name = "mnuDifficult";
            this.mnuDifficult.Size = new System.Drawing.Size(131, 22);
            this.mnuDifficult.Tag = "4";
            this.mnuDifficult.Text = "Difficult";
            this.mnuDifficult.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuHard
            // 
            this.mnuHard.Name = "mnuHard";
            this.mnuHard.Size = new System.Drawing.Size(131, 22);
            this.mnuHard.Tag = "5";
            this.mnuHard.Text = "Hard";
            this.mnuHard.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuHarder
            // 
            this.mnuHarder.Name = "mnuHarder";
            this.mnuHarder.Size = new System.Drawing.Size(131, 22);
            this.mnuHarder.Tag = "6";
            this.mnuHarder.Text = "Harder";
            this.mnuHarder.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // mnuHardest
            // 
            this.mnuHardest.Name = "mnuHardest";
            this.mnuHardest.Size = new System.Drawing.Size(131, 22);
            this.mnuHardest.Tag = "7";
            this.mnuHardest.Text = "Hardest";
            this.mnuHardest.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.mField);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(478, 531);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(478, 556);
            this.toolStripContainer.TabIndex = 2;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.mainMenu);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // mField
            // 
            this.mField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mField.Location = new System.Drawing.Point(0, 0);
            this.mField.Name = "mField";
            this.mField.Size = new System.Drawing.Size(478, 531);
            this.mField.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(97, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(35, 25);
            this.toolStrip1.TabIndex = 2;
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            // 
            // SuperDokuPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 556);
            this.Controls.Add(this.toolStripContainer);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "SuperDokuPC";
            this.Text = "SuperDokuPC";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SuperDoku.Field mField;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuBeginner;
        private System.Windows.Forms.ToolStripMenuItem mnuEasy;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem mnuMild;
        private System.Windows.Forms.ToolStripMenuItem mnuModerate;
        private System.Windows.Forms.ToolStripMenuItem mnuDifficult;
        private System.Windows.Forms.ToolStripMenuItem mnuHard;
        private System.Windows.Forms.ToolStripMenuItem mnuHarder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuHardest;
    }
}

