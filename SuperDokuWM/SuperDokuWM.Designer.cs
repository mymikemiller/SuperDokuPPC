namespace SuperDoku
{
    partial class SuperDokuWM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuperDokuWM));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuNew = new System.Windows.Forms.MenuItem();
            this.mnuBeginner = new System.Windows.Forms.MenuItem();
            this.mnuEasy = new System.Windows.Forms.MenuItem();
            this.mnuMild = new System.Windows.Forms.MenuItem();
            this.mnuModerate = new System.Windows.Forms.MenuItem();
            this.mnuDifficult = new System.Windows.Forms.MenuItem();
            this.mnuHard = new System.Windows.Forms.MenuItem();
            this.mnuHarder = new System.Windows.Forms.MenuItem();
            this.mnuHardest = new System.Windows.Forms.MenuItem();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mField = new SuperDoku.Field();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.mnuNew);
            this.menuItem1.Text = "&File";
            // 
            // mnuNew
            // 
            this.mnuNew.MenuItems.Add(this.mnuBeginner);
            this.mnuNew.MenuItems.Add(this.mnuEasy);
            this.mnuNew.MenuItems.Add(this.mnuMild);
            this.mnuNew.MenuItems.Add(this.mnuModerate);
            this.mnuNew.MenuItems.Add(this.mnuDifficult);
            this.mnuNew.MenuItems.Add(this.mnuHard);
            this.mnuNew.MenuItems.Add(this.mnuHarder);
            this.mnuNew.MenuItems.Add(this.menuItem2);
            this.mnuNew.MenuItems.Add(this.mnuHardest);
            this.mnuNew.Text = "&New";
            // 
            // mnuBeginner
            // 
            this.mnuBeginner.Text = "&Beginner";
            this.mnuBeginner.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuEasy
            // 
            this.mnuEasy.Text = "&Easy";
            this.mnuEasy.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuMild
            // 
            this.mnuMild.Text = "&Mild";
            this.mnuMild.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuModerate
            // 
            this.mnuModerate.Text = "M&oderate";
            this.mnuModerate.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuDifficult
            // 
            this.mnuDifficult.Text = "&Difficult";
            this.mnuDifficult.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuHard
            // 
            this.mnuHard.Text = "&Hard";
            this.mnuHard.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuHarder
            // 
            this.mnuHarder.Text = "Harde&r";
            this.mnuHarder.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // mnuHardest
            // 
            this.mnuHardest.Text = "Hardes&t";
            this.mnuHardest.Click += new System.EventHandler(this.mnuNewGrid_Click);
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.Add(this.toolBarButton1);
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Name = "toolBar1";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 0;
            this.imageList1.Images.Clear();
            this.imageList1.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource"))));
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // mField
            // 
            this.mField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mField.Location = new System.Drawing.Point(0, 0);
            this.mField.Name = "mField";
            this.mField.Size = new System.Drawing.Size(240, 268);
            this.mField.TabIndex = 0;
            // 
            // SuperDokuWM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.mField);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "SuperDokuWM";
            this.Text = "SuperDokuWM";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SuperDokuWM_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private SuperDoku.Field mField;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuItem mnuNew;
        private System.Windows.Forms.MenuItem mnuBeginner;
        private System.Windows.Forms.MenuItem mnuEasy;
        private System.Windows.Forms.MenuItem mnuMild;
        private System.Windows.Forms.MenuItem mnuModerate;
        private System.Windows.Forms.MenuItem mnuDifficult;
        private System.Windows.Forms.MenuItem mnuHard;
        private System.Windows.Forms.MenuItem mnuHarder;
        private System.Windows.Forms.MenuItem mnuHardest;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}

