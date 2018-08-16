using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuperDoku
{
    public partial class SuperDokuWM : Form
    {
        Game mGame;

        public SuperDokuWM()
        {
            InitializeComponent();
            mGame = new Game(mField);
        }

        private void mnuNewGrid_Click(object sender, EventArgs e)
        {
            MenuItem clicked = (MenuItem)sender;
            int level = 0;
            for(int index = 0; index < mnuNew.MenuItems.Count; index++)
            {
                MenuItem item = mnuNew.MenuItems[index];
                if(item.Text != "-")
                {
                    item.Checked = (item == clicked);
                    if(item == clicked)
                        mGame.StartGeneration((Engine.Levels)level);
                    else
                        level++;
                }
            }
        }

        private void SuperDokuWM_KeyDown(object sender, KeyEventArgs e)
        {
            mGame.InputEvent(new Settings.Navigation.InputSetting(e.KeyCode));

            //if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            //{
            //    mField.Board.MoveSelectedSquare(Directions.Up);
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            //{
            //    mField.Board.MoveSelectedSquare(Directions.Down);
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            //{
            //    mField.Board.MoveSelectedSquare(Directions.Left);
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            //{
            //    mField.Board.MoveSelectedSquare(Directions.Right);
            //}
            //if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            //{
            //    // Enter
            //}
            //if(e.KeyCode == HardwareKeys

        }
    }
}