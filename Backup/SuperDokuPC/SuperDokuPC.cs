using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuperDoku
{
    public partial class SuperDokuPC : Form
    {
        Game mGame;

        public SuperDokuPC()
        {
            InitializeComponent();
            mGame = new Game(mField);
        }

        /// <summary>Creates the appropriate grid and places a check-mark on the clicked ToolStripMenuItem to let the user know the previous grid's level.</summary>
        /// <param name="sender">The ToolStripMenuItem whose click caused this event. Its Tag property must be an integer corresponding to the SuperDoku.Levels enum (0-based) of the level to be created.</param>
        /// <remarks>Occurs when any of the File > New ToolStripMenuItems are clicked.</remarks>
        private void mnuNewGrid_Click(object sender, EventArgs e)
        {


            ToolStripMenuItem clicked = (ToolStripMenuItem)sender;

            foreach (ToolStripItem i in mnuNew.DropDownItems)
            {
                if (i.GetType() == typeof(ToolStripMenuItem)) ((ToolStripMenuItem)i).Checked = (i == clicked);
            }

            mGame.StartGeneration((Engine.Levels)int.Parse((clicked.Tag.ToString())));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;

            if ((msg.Msg == WM_KEYDOWN))
            {
                mGame.InputEvent(new Settings.Navigation.InputSetting(keyData));
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
