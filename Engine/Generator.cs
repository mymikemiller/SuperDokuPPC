using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Engine
{
    public enum Levels { Beginner = 0, Easy, Mild, Moderate, Difficult, Hard, Harder, Hardest }

    /// <summary>Handles events caused when the Generator has finished generating a new Grid.</summary>
    public delegate void GeneratorFinishedHandler(object sender, EngineGridEventArgs e);

    public class Generator : Control //(being a Control provides Invoke methods. that's all. we're not really a control)
    {
        //![System.ComponentModel.Description("Triggered when the background generator has finished generating")]
        public event GeneratorFinishedHandler GeneratorFinished;


        static Random rand = new Random();

        private Levels currentLevel;
        /// <summary>
        /// a background thread for creating the puzzle that doesn't
        /// lock the main thread while busy
        /// </summary>
        private Thread workerThread = null;


#region stuff for solver

        private int GridSize = 3;
        private int GWidth = 9;
        private int GWmin1 = 8;
        private int GNum = 81;
        private int[, ,] Cell = new int[9,9,2];
        private string[] Grid;//work grid
        private string[] StartGrid;//Start grid
        private const string BString = "123456789";

#endregion





        public Generator()
        {

        }

        public Thread StartGenerating(Levels level)
        {
            currentLevel = level;

            workerThread = new System.Threading.Thread(
                new System.Threading.ThreadStart(StartGenerating));
            workerThread.IsBackground = true;
            workerThread.Name = "Generator";
            workerThread.Start();
            return workerThread;
        }

        private void StartGenerating()
        {
            EngineGrid.BadKeyNums.Clear();
            //for (int i = 0; i <= 30; i++) //todo: get rid of this line
            //{
                EngineGrid grid = new EngineGrid();
                try
                {
                    grid = CreateNewGrid(currentLevel);
                }
                catch (Exception Err)
                {
                    if (Err.GetType() != typeof(System.Threading.ThreadAbortException))
                    {
                        //ReportErr(Err);
                    }
                }
                finally
                {
                    if (GeneratorFinished != null)
                    {
                        Solver.Solve(grid);
                        //why not just do this: GeneratorFinished(this, new EngineGridEventArgs(grid));
                        //because we need the handler to run in the thread that started the generation (the main thread), not in the worker thread from where this method is called.
                        this.Invoke(new GeneratorFinishedHandler(GeneratorFinished), new object[] { this, new EngineGridEventArgs(grid) });
                    }
                }
            //}
            System.Diagnostics.Debug.Write("Bad nums: ");
            foreach (int num in EngineGrid.BadKeyNums)
                System.Diagnostics.Debug.Write(num + " ");
        }

        public void StopBackgroundWork()
        {
            if (null != workerThread)
            {
                workerThread.Abort();
            }
        }
                

        private EngineGrid CreateNewGrid(Levels level)
        {
            EngineGrid tempGrid;
            bool fullySolved = false;
            do {
                int x;
                int y;
                int Pos;
                string Value;
                string Reg;

                Init_Sudoku();
                Reg = GetField(level);
                Reg = Scramble_Field(Reg);
                //Init_Sudoku(); again?
                //Call Init_Solver()      'to initialize the reset function

                tempGrid = new EngineGrid(level, Reg);

                fullySolved = true;// solveGrid();
            }
            while (!((fullySolved || level == Levels.Hardest)));
            //txtCreating.Visible = False

            return tempGrid;
        }


        //init this module and its variables
        public void Init_Sudoku()
        {
            int x;
            int y;
            int A;
            int b;
            int ST1;
            int St2;
            int SP;
            GWidth = GridSize * GridSize;
            GWmin1 = GWidth - 1;

            Cell = new int[9, 9, 2];
            Grid = new string[GNum];//work grid
            StartGrid = new string[GNum];//Start grid
            //ExtSolved = new int[GNum];//Buffer for solved positions (0)=number of solved positions
            //BackGrid = new string[GNum];//Backup grid for guessing

            
            for (x = 0; x <= GWmin1; x++) {
                for (y = 0; y <= GWmin1; y++) {
                    Cell[x, y, 0] = x * GWidth + y; //+1?

                }
            }
            ST1 = -1;
            for (x = 0; x < GridSize; x++) {
                for (y = 0; y < GridSize; y++) {
                    SP = x * GWidth * GridSize + y * GridSize; //+1?
                    ST1 = ST1 + 1;
                    St2 = -1;
                    for (A = 0; A <= GridSize - 1; A++) {
                        for (b = 0; b <= GridSize - 1; b++) {
                            St2 = St2 + 1;
                            Cell[ST1, St2, 1] = SP + b + A * GWidth;
                        }
                    }
                }
            }
            
            
            //For x = 0 To 8
            //    For y = 0 To 8
            //        myGrid.Board(x, y) = 0
            //        myGrid.SolvedBoard(x, y) = 0
            //        myGrid.BoardPosIsEditable(x, y) = True
            //        For z = 1 To 9
            //            myGrid.PencilMarks(x, y, z) = False
            //        Next
            //    Next y
            //Next x
            
        }



        //Get a puzzle from the book
        public string GetField(Levels Lev)
        {
            int x;
            int y;
            //x is the number of keys in the level in the book (always 15)
            int[] Nums = new int[9];
            int Pos;
            bool IsNew;

            Pos = rand.Next(1,15+1);
            EngineGrid.KeyNum = Pos;

            string Sud = GetKeyVal(Lev, Pos);
            //get 1-9 in random order
            //Set_Values:
            for (x = 0; x < Nums.Length; x++) {
                do {
                    IsNew = true;
                    Pos = rand.Next(1, GWidth + 1);
                    if (Pos == GWidth + 1)
                        IsNew = false;
                    for (y = 0; y <= x; y++) {
                        if (Nums[y] == Pos)
                            IsNew = false;
                    }
                    Nums[x] = Pos;
                }
                while (IsNew == false);
            }
            //Place 1-9 in positions
            for (x = 0; x <= GWmin1; x++) {
                Sud = Sud.Replace(Convert.ToChar(72 + x), BString[Nums[x]-1]);
            }
            Sud = Sud.Replace("x", "0");
            return Sud;
        }

        public string GetKeyVal(Levels Level, int KeyIndex)
        {
            //Returns a string from the book file
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            System.IO.Stream keysStream = myAssembly.GetManifestResourceStream("Engine.Resources.Keys.txt");

            System.IO.StreamReader bookReader = new System.IO.StreamReader(keysStream);
            int numCharsInBlock = 1284;
            string seed = "";
            
            try {
                string bookText = bookReader.ReadToEnd();
                string blockText = bookText.Substring(bookText.IndexOf("[" + (int)Level + "]"), numCharsInBlock);
                string searchFor = KeyIndex + "=";
                seed = blockText.Substring(blockText.IndexOf(searchFor) + searchFor.Length, 81);
            }
            catch {
                throw new Exception("Error reading Keys.txt. Level: " + Level + ", KeyIndex: " + KeyIndex + ".");
            }
            finally {
                bookReader.Close();
            }
            
            return seed;
        }

        //scramble a puzzle so it looks like a brand new one
        public string Scramble_Field(string Sud)
        {
            StringBuilder TSud = new StringBuilder(Sud);
            char T;
            int U;
            int x;
            int y;
            int X1;
            int Y1;
            int A;
            int b;
            int GS;
            bool DoBlock;
            bool DoRow;
            int GridSize = 3;
            GS = GridSize;
            for (U = 1; U <= 40; U++)
            {
                //do 40 transformations

                DoBlock = rand.Next(0, 2) == 0;
                DoRow = rand.Next(0, 2) == 0;

                //A = (int)Conversion.Int(VBMath.Rnd() * GS);
                A = rand.Next(GS);
                do
                {
                    b = rand.Next(GS);
                }
                while (b == A);
                if (DoBlock)
                {
                    //X1 = (int)Conversion.Int(VBMath.Rnd() * GS);
                    X1 = rand.Next(GS);

                    X1 = X1 * GS;
                    if (DoRow)
                    {
                        for (y = 0; y <= GS * GS - 1; y++)
                        {
                            //T = Strings.Mid(TSud, Cell(X1 + A, y, 0), 1);
                            T = TSud[Cell[X1 + A, y, 0]];

                            //Strings.Mid(TSud, Cell(X1 + A, y, 0), 1) = Strings.Mid(TSud, Cell(X1 + b, y, 0), 1);
                            TSud[Cell[X1 + A, y, 0]] = TSud[Cell[X1 + b, y, 0]];
                            //Strings.Mid(TSud, Cell(X1 + b, y, 0), 1) = T;
                            TSud[Cell[X1 + b, y, 0]] = T;
                        }
                    }
                    else
                    {
                        for (y = 0; y <= GS * GS - 1; y++)
                        {
                            T = TSud[Cell[y, X1 + A, 0]];

                            TSud[Cell[y, X1 + A, 0]] = TSud[Cell[y, X1 + b, 0]];
                            TSud[Cell[y, X1 + b, 0]] = T;
                        }
                    }
                }
                else
                {
                    for (x = 0; x <= GS - 1; x++)
                    {
                        if (DoRow)
                        {
                            X1 = A * GS + x;
                            Y1 = b * GS + x;
                        }
                        else
                        {
                            X1 = A + x * GS;
                            Y1 = b + x * GS;
                        }
                        for (y = 0; y <= (GS * GS) - 1; y++)
                        {
                            T = TSud[Cell[X1, y, 1]];
                            //this
                            TSud[Cell[X1, y, 1]] = TSud[Cell[Y1, y, 1]];
                            TSud[Cell[Y1, y, 1]] = T;
                        }
                    }
                }
            }
            return TSud.ToString();
        }

    }
}


