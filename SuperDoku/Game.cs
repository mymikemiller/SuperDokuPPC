using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Engine;

namespace SuperDoku
{
    public class Game
    {
        Field mField;
        Thread mWorkerThread;
        Generator mGenerator;

        public Game(Field field)
        {
            mField = field;

            StartGeneration(Levels.Hard);
        }


        #region Functions

        public void StartGeneration(Levels level)
        {
            Generator g = new Generator();
            IntPtr handle = g.Handle; // Forces creation of the handle so OnGeneratorFinish will work

            g.GeneratorFinished += new Engine.GeneratorFinishedHandler(OnGeneratorFinish);

            mGenerator = g;
            mWorkerThread = g.StartGenerating(level);

            mField.Board.Grid.ClearPencilMarks();
        }


        private void OnGeneratorFinish(object sender, Engine.EngineGridEventArgs e)
        {
            
            mField.Board.InitFromEngineGrid(e.Grid);
            ApplyStyleToAll();
        }

        public void InputEvent(Settings.Navigation.InputSetting input)
        {
            if (input.Equals(Settings.Navigation.BoardSelectionUp))
                mField.Board.MoveSelectedSquare(Directions.Up);
            else if (input.Equals(Settings.Navigation.BoardSelectionDown))
                mField.Board.MoveSelectedSquare(Directions.Down);
            else if (input.Equals(Settings.Navigation.BoardSelectionLeft))
                mField.Board.MoveSelectedSquare(Directions.Left);
            else if (input.Equals(Settings.Navigation.BoardSelectionRight))
                mField.Board.MoveSelectedSquare(Directions.Right);
            else if (input.Equals(Settings.Navigation.SelectionBarUp))
                mField.SelectionBar.MoveSelectedSquare(Directions.Up);
            else if (input.Equals(Settings.Navigation.SelectionBarDown))
                mField.SelectionBar.MoveSelectedSquare(Directions.Down);
            else if (input.Equals(Settings.Navigation.SelectionBarLeft))
                mField.SelectionBar.MoveSelectedSquare(Directions.Left);
            else if (input.Equals(Settings.Navigation.SelectionBarRight))
                mField.SelectionBar.MoveSelectedSquare(Directions.Right);
            else if (input.Equals(Settings.Navigation.Enter))
                mField.Board.ApplySelectedSquare();
            else if (input.Equals(new Settings.Navigation.InputSetting(Keys.V | Keys.Control)))
                PasteGrid();
            else if (input.Equals(new Settings.Navigation.InputSetting(Keys.C | Keys.Control)))
                CopyGrid();


            /*
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                IDataObject d = Clipboard.GetDataObject();
                if (d.GetDataPresent(DataFormats.StringFormat))
                {
                    string s = (string)(d.GetData(DataFormats.StringFormat));
                    Board.InitFromString(s);
                }
            }
            else if (e.KeyCode == Keys.P)
            {
                Settings.Current.PencilMarkMode = !Settings.Current.PencilMarkMode;
            }
             * */
        }

        public void PasteGrid()
        {
            IDataObject d = Clipboard.GetDataObject();
            if (d.GetDataPresent(DataFormats.StringFormat))
            {
                string s = (string)(d.GetData(DataFormats.StringFormat));
                mField.Board.InitFromString(s);
            }
        }

        public void CopyGrid()
        {
            string s = mField.Board.Grid.ToString();

            IDataObject d = new DataObject();
            d.SetData(DataFormats.Text, true, s);
            Clipboard.SetDataObject(d);
        }



        private void ApplyStyleToAll()
        {
            mField.Board.ApplyStyle();
            mField.SelectionBar.ApplyStyle();
        }

        #endregion Functions



        #region Events


        #endregion Events
    }
}
