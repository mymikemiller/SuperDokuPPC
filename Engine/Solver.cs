using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public static class Solver
    {
        public static void Solve(EngineGrid gridToSolve)
        {
        }
    }
}



//using System;
//using System.Linq;
//using System.Collections.Generic;
//using System.Text;

//namespace Engine
//{
//    public class Solver
//    {
        
        
//        #region "Methods involved in solving a Grid and getting hints"

//        EngineGrid solvedGrid = new EngineGrid();

        

//        private const int GridSize = 3;
//        private const int GWidth = 9;
//        private const int GWmin1 = 8;
//        private const int GNum = 81;
//        private int[, ,] Cell = new int[9, 9, 2];
//        private string[] Grid;//work grid
//        private string[] StartGrid;//Start grid
//        private const string BString = "123456789";

//        private int[] ExtSolved;//Buffer for solved positions (0)=number of solved positions
//        private string[] BackGrid;//Backup grid for guessing
//        private bool UseGuessing = false; //Use guessing if logic can't solve puzzle
//        private int[] MethUsed = new int[2];
//        private string UsedMeth;
//        private long TPoints;
//        private bool HintOn;//try to find a hint
//        private bool GotHint;//set as hint is found
//        private int ToGo;
//        private string ResetGrid;
//        private bool UsedUseGues;
//        private long MinPossibilities;
//        private int SolveMethod;//Method of solving
//        private int LastMethod;//Last used method
//        private int[] RC = new int[4];
//        private string RV;
//        private long BuTPoints;
//        private int BuToGo;
//        private long BUMinPos;
//        private int blinkCount;
//        private int hintX;
//        private int hintY;


//        public void solve(EngineGrid toSolve)
//        {
//            solveGrid(toSolve);
//            //if (myGrid.boardIsSolved == false)
//            //{
//            //    if (myGrid.isBlank() == true)
//            //    {
//            //        Interaction.MsgBox(myLang.blankGrid);
//            //        return; // TODO: might not be correct. Was : Exit Sub
//            //    }
//            //    burnGrid();
//            //}
//            //if (solveGrid() == false)
//            //{
//            //    //board was not fully solved
//            //    showSolvedGrid();
//            //    if (Interaction.MsgBox(myLang.noFullSolution, MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
//            //    {
//            //        bool backupUseGuessing = UseGuessing;
//            //        UseGuessing = true;
//            //        if (solveGrid() == false)
//            //        {
//            //            Interaction.MsgBox(myLang.stillNoFullSolution);
//            //        }
//            //        UseGuessing = backupUseGuessing;
//            //    }
//            //}
//            showSolvedGrid();
//        }

//        private void showSolvedGrid()
//        {
//            //Grid tempGrid = new Grid(myGrid());
//            //int x;
//            //int y;
//            //for (x = 0; x <= 8; x++)
//            //{
//            //    for (y = 0; y <= 8; y++)
//            //    {
//            //        tempGrid.Board(x, y) = tempGrid.SolvedBoard(x, y);
//            //        if (mySettings.autoPencilMarks & !tempGrid.isFull())
//            //            fillPencilmarks(tempGrid);
//            //    }
//            //}
//            //addToGridHistory(tempGrid);
//            //redraw(true);

//            //System.Windows.Forms.MessageBox.Show("Solved! " + solvedGrid.ToString());
//        }

//        //returns true if grid was solved fully
//        private bool solveGrid(EngineGrid gridToSolve)
//        {
//            //txtSolving.Visible = True
//            int x;
//            int y;
//            //Dim SolveD As Boolean

//            StringBuilder sb1 = new StringBuilder();
//            for (y = 0; y <= 8; y++)
//            {
//                for (x = 0; x <= 8; x++)
//                {
//                    System.Diagnostics.Debug.Write(gridToSolve[x, y].Value.ToString());
//                }
//                System.Diagnostics.Debug.WriteLine("");
//            }


//            Init_Solver();
//            //set start values of sudoku-puzzle

//            ResetGrid = gridToSolve.ToString();
//            char[] chars = ResetGrid.ToCharArray();
//            for (x = 0; x < chars.Length; x++)
//                StartGrid[x] = chars[x] == '0' ? "" : chars[x].ToString();

//            if (SolveSudoku() == false)
//            {
//                //txtSolving.Visible = False
//                //TXT = myLang.cantSolve & vbCrLf
//                //MsgBox(TXT)
//                //return false;
//            }

//            int xInString = 0;
//            StringBuilder sb = new StringBuilder();
//            StringBuilder sb2 = new StringBuilder();
//            bool bad = false;
//            for (y = 0; y <= 8; y++)
//            {
//                for (x = 0; x <= 8; x++)
//                {
//                    sb.Append(Grid[xInString]).Append("\t");
//                    sb2.Append(Grid[xInString] == "" ? "0" : Grid[xInString]);
//                    if (Grid[xInString].Length > 1 || Grid[xInString].Length == 0) bad = true;
                    
//                    xInString++;
//                }
//                sb.AppendLine();
//            }
//            if (bad) { 
//                EngineGrid.BadKeyNums.Add(EngineGrid.KeyNum); }
//            System.Diagnostics.Debug.WriteLine(sb.ToString());
//            System.Diagnostics.Debug.WriteLine("");
//            System.Diagnostics.Debug.WriteLine(sb2.ToString());

//            //for (x = 0; x <= 8; x++)
//            //{
//            //    for (y = 0; y <= 8; y++)
//            //    {
//            //        xInString = (x) + (y * 9); //was: (x + 1) + (y * 9);
//            //        if (Grid[xInString].Length == 1) //if the Square is solved (i.e. only has one possible value)
//            //            solvedGrid[x, y].Value = int.Parse(Grid[xInString]);
//            //    }
//            //}

//            return true;
//        }

//        //init this module and its variables
//        public void Init_Sudoku()
//        {

            
            
//        }

//        //init the solver and its variables
//        public void Init_Solver()
//        {
//            int x;
//            int y;
//            int ST1;
//            int St2;
//            int A;
//            int b;
//            int SP;

//            Cell = new int[9, 9, 2];
//            Grid = new string[GNum];//work grid
//            StartGrid = new string[GNum];//Start grid
//            ExtSolved = new int[GNum];//Buffer for solved positions (0)=number of solved positions
//            BackGrid = new string[GNum];//Backup grid for guessing
//            for (x = 0; x <= GWmin1; x++)
//            {
//                for (y = 0; y <= GWmin1; y++)
//                {
//                    Cell[x, y, 0] = x * GWidth + y; //+1?

//                }
//            }

//            ST1 = -1;
//            for (x = 0; x < GridSize; x++)
//            {
//                for (y = 0; y < GridSize; y++)
//                {
//                    SP = x * GWidth * GridSize + y * GridSize; //+1?
//                    ST1 = ST1 + 1;
//                    St2 = -1;
//                    for (A = 0; A <= GridSize - 1; A++)
//                    {
//                        for (b = 0; b <= GridSize - 1; b++)
//                        {
//                            St2 = St2 + 1;
//                            Cell[ST1, St2, 1] = SP + b + A * GWidth;
//                        }
//                    }
//                }
//            }

//            for (x = 0; x < GNum; x++)
//            {
//                StartGrid[x] = "";
//                Grid[x] = BString;
//            }
//            MethUsed[0] = 0;
//            MethUsed[1] = 0;
//            UsedMeth = "";
//            TPoints = 0;
//            HintOn = false;
//            ToGo = GNum;
//        }

//        //!!DefGrid was replaced a few lines up. search for DefGrid in comments.
//        //define the grid for the solver and set the reset positions
//        //public void DefGrid(int Field, string Value)
//        //{
//        //    StartGrid[Field] = Value;
//        //    Strings.Mid(ResetGrid, Field, 1) = Value;  //Strings.Mid(ResetGrid, Field, 1) = Value;
//        //}


//        //try to solve the puzzle
//        public bool SolveSudoku()
//        {
//            bool functionReturnValue = false;
//            int TestPos;
//            int T1;
//            int TS;
//            int x;
//            int Guess;
//            System.Windows.Forms.Application.DoEvents();
//            ExtSolved[0] = 0;
//            Guess = 0;
//            T1 = 0;
//            TestPos = 0;
//            UsedUseGues = false;
//            MinPossibilities = 1;
//            SolveA();
//            //set start grid
//            do
//            {
//                if (HintOn == true & GotHint == true)
//                {
//                    return true;
//                }
//                if (IsSolved() == true)
//                {
//                    functionReturnValue = true;
//                    break;
//                }

                 
//                if (SolveB() == false)
//                {
//                    if (SolveC() == false)
//                    {
//                        if (SolveD() == false)
//                        {
//                            if (SolveE() == false)
//                            {
//                                break; //remove me!!
//                                /*
//                                if (SolveF() == false)
//                                {
//                                    if (SolveG() == false)
//                                    {
//                                        if (SolveH() == false)
//                                        {

//                                            if (UseGuessing == false)
//                                                break; // TODO: might not be correct. Was : Exit Do

//                                            UsedUseGues = true;
//                                            if (TestPos == GNum + 1)
//                                                break; // TODO: might not be correct. Was : Exit Do

//                                            SolveMethod = 99;
//                                            if (Guess != 0)
//                                                CopyFromBackup();
//                                            else
//                                                CopyToBackup();

//                                            T1 = 0;
//                                            Guess = 1;
//                                            //try putting one in and see if it can be solved now
//                                            do
//                                            {
//                                                TS = Strings.Len(Grid(TestPos));
//                                                if (TS > 1)
//                                                {
//                                                    x = TestPos;
//                                                    MakeLog(99, "", x, Strings.Mid(Grid(TestPos), T1 + 1, 1));
//                                                    Grid(TestPos) = Strings.Mid(Grid(TestPos), T1 + 1, 1);
//                                                    T1 = T1 + 1;
//                                                    if (T1 == TS)
//                                                    {
//                                                        T1 = 1;
//                                                        TestPos = TestPos + 1;
//                                                    }
//                                                    SetGrid(x);
//                                                    break; // TODO: might not be correct. Was : Exit Do
//                                                }
//                                                else
//                                                {
//                                                    TestPos = TestPos + 1;
//                                                }
//                                            }
//                                            while (TestPos < GNum + 1);
//                                        }
//                                    }
//                                }
//                                */
//                            }
//                        }
//                    }
//                }
                 
//            }
//            while (true);
//            SolveMethod = 0;
//            MakeLog(0, "", 1, "1");
//            if (TestPos == GNum + 1)
//            {
//                TPoints = BuTPoints;
//            }

//            return functionReturnValue;
//        }

//        #region "SolveA through SolveH"

//        //Store the startgrid into the workgrid
//        private void SolveA()
//        {
//            int x;
//            SolveMethod = 1;
//            for (x = 0; x < GNum; x++)
//            {
//                if (StartGrid[x].Length == 1 && Grid[x].Length > 1)
//                {
//                    Grid[x] = StartGrid[x];
//                    SetGrid(x);
//                }
//            }
//        }

//        //Find a number wich appearce ones in a block,row or column
//        //this number me be on that place
//        private bool SolveB()
//        {
//            bool functionReturnValue = false;
//            int x;
//            int y;
//            int C;
//            int D;
//            int Pos;
//            string SNum;
//            int R1;
//            int C1;
//            string BL1;
//            int[,] NumUsed = new int[GWidth, 2];
//            bool F1;

//            SolveMethod = 2;
//            //find a single possebillity in block 3x3
//            for (x = 0; x <= GWmin1; x++)
//            {
//                do
//                {
//                    for (y = 0; y <= GWmin1; y++)
//                    {
//                        Pos = Cell[x, y, 1]; //todo: 0?
//                        //GoSub Check_Pos
//                        if (Grid[Pos].Length != 1)
//                        {
//                            for (C = 0; C < Grid[Pos].Length; C++)
//                            {
//                                D = BString.IndexOf(Grid[Pos][C]); //was: Strings.InStr(BString, Strings.Mid(Grid(Pos), C, 1));
//                                NumUsed[D, 0]++;
//                                NumUsed[D, 1] = Pos;
//                            }
//                        }
//                    }
//                    //check if there is a number with 1 appearence
//                    F1 = false;
//                    for (C = 0; C < GWidth; C++) //was: C = 1
//                    {
//                        if (NumUsed[C, 0] == 1)
//                        {
//                            if (MethUsed[1] != SolveMethod)
//                            {
//                                MethUsed[0]++;
//                                MethUsed[1] = SolveMethod;
//                                AddUsedMeth();
//                            }
//                            SNum = BString[C].ToString(); //was: Strings.Mid(BString, C, 1); //todo: offset wrong? C-1? start C at 0 above?
//                            //Put this number in place and start over
//                            Pos = NumUsed[C, 1]; //offset: C, 0?
//                            D = Cell[x, 0, 1];
//                            R1 = (int)((D - 1) / GWidth); //was: (int)Conversion.Int((D - 1) / GWidth);
//                            C1 = D - 1 - R1 * GWidth;
//                            BL1 = BString[R1] + (C1 + 1).ToString(); //was: Strings.Chr(65 + R1) + Strings.Trim(Conversion.Str(C1 + 1));
//                            MakeLog(2, "B", Pos, SNum, BL1);
//                            functionReturnValue = true;
//                            if (GotHint)
//                                return true; // TODO: might not be correct. Was : Exit Function

//                            Grid[Pos] = SNum;
//                            SetGrid(Pos);
//                            F1 = true;
//                        }
//                        NumUsed[C, 0] = 0;
//                    }
//                }
//                while (F1);
//            }
//            //find a single possebillity in rows
//            for (x = 0; x <= GWmin1; x++)
//            {
//                do
//                {
//                    for (y = 0; y <= GWmin1; y++)
//                    {
//                        Pos = Cell[x, y, 0];
//                        //GoSub Check_Pos
//                        if (Grid[Pos].Length != 1)
//                        {
//                            for (C = 0; C < Grid[Pos].Length; C++)
//                            {
//                                D = BString.IndexOf(Grid[Pos][C]); //was: Strings.InStr(BString, Strings.Mid(Grid(Pos), C, 1));
//                                NumUsed[D, 0] ++;
//                                NumUsed[D, 1] = Pos;
//                            }
//                        }
//                    }
//                    //check if there is a number with 1 appearence
//                    F1 = false;
//                    for (C = 0; C < GWidth; C++)
//                    {
//                        if (NumUsed[C, 0] == 1)
//                        {
//                            if (MethUsed[1] != SolveMethod)
//                            {
//                                MethUsed[0] ++;
//                                MethUsed[1] = SolveMethod;
//                                AddUsedMeth();
//                            }
//                            SNum = BString[C].ToString(); //was: Strings.Mid(BString, C, 1); //offset: C+1?
//                            //Put this number in place and start over
//                            Pos = NumUsed[C, 1];
//                            MakeLog(2, "R", Pos, SNum, "");
//                            functionReturnValue = true;
//                            if (GotHint)
//                                return true; // TODO: might not be correct. Was : Exit Function

//                            Grid[Pos] = SNum;
//                            SetGrid(Pos);
//                            F1 = true;
//                        }
//                        NumUsed[C, 0] = 0;
//                    }
//                }
//                while (F1);
//            }
//            //find a single possebillity in columns
//            for (x = 0; x <= GWmin1; x++)
//            {
//                do
//                {
//                    for (y = 0; y <= GWmin1; y++)
//                    {
//                        Pos = Cell[y, x, 0];
//                        //GoSub Check_Pos
//                        if (Grid[Pos].Length != 1)
//                        {
//                            for (C = 0; C < Grid[Pos].Length; C++)
//                            {
//                                D = BString.IndexOf(Grid[Pos][C]); //was: Strings.InStr(BString, Strings.Mid(Grid(Pos), C, 1));
//                                NumUsed[D, 0]++;
//                                NumUsed[D, 1] = Pos;
//                            }
//                        }
//                    }
//                    //check if there is a number with 1 appearence
//                    F1 = false;
//                    for (C = 0; C < GWidth; C++)
//                    {
//                        if (NumUsed[C, 0] == 1)
//                        {
//                            if (MethUsed[1] != SolveMethod)
//                            {
//                                MethUsed[0]++;
//                                MethUsed[1] = SolveMethod;
//                                AddUsedMeth();
//                            }
//                            SNum = BString[C].ToString(); //was: Strings.Mid(BString, C, 1); //offset: C+1?
//                            //Put this number in place and start over
//                            Pos = NumUsed[C, 1];
//                            MakeLog(2, "C", Pos, SNum, "");
//                            functionReturnValue = true;
//                            if (GotHint)
//                                return true; // TODO: might not be correct. Was : Exit Function

//                            Grid[Pos] = SNum;
//                            SetGrid(Pos);
//                            F1 = true;
//                        }
//                        NumUsed[C, 0] = 0;
//                    }
//                }
//                while (F1);
//            }

//            //return; // TODO: might not be correct. Was : Exit Function
//            return functionReturnValue;

//        }

//        //Look in a row or column for a value wich could only appear inside a
//        //certain block
//        //if found remove that number from the rest of the rows and columns
//        //inside that block
//        private bool SolveC()
//        {
//            bool functionReturnValue = false;
//            int x;
//            int y;
//            int A;
//            int Pos;
//            int P2;
//            string PV;
//            int PW;
//            int Block;
//            string[] V;
//            bool DL;
//            SolveMethod = 3;
//            //check rows for number wich could only appear in a certain block
//            for (x = 0; x <= GWmin1; x++)
//            {
//                V = new string[GWidth]{"", "", "", "", "", "", "", "", "" };

//                for (y = 0; y <= GWmin1; y++)
//                {
//                    Pos = Cell[x, y, 0];
//                    if (Grid[Pos].Length > 1)
//                    {
//                        for (A = 0; A < Grid[Pos].Length; A++)
//                        {
//                            PV = Grid[Pos][A].ToString(); //was: Strings.Mid(Grid(Pos), A, 1);
//                            PW = BString.IndexOf(PV); //was: Strings.InStr(BString, PV);
//                            string search = ((int)(y / GridSize)).ToString();
//                            if (!V[PW].Contains(search)) //was: if (Strings.InStr(V(PW), Strings.Trim(Conversion.Str(Conversion.Int(y / GridSize)))) == 0)
//                            {
//                                V[PW] += search;
//                            }
//                        }
//                    }
//                }
//                for (A = 0; A < GWidth; A++)
//                {
//                    if (V[A].Length == 1)
//                    {
//                        //found number A wich could appear only in row x from block(v(A))
//                        //define start of block
//                        Block = ((int)(x / GridSize)) * GridSize + int.Parse(V[A]);
//                        DL = false;
//                        PV = BString[A].ToString(); //was: Strings.Mid(BString, A, 1);
//                        for (y = 0; y <= GWmin1; y++)
//                        {
//                            Pos = Cell[Block, y, 1];
//                            if ((int)((Pos) / GWidth) != x) //was: ((int)((Pos - 1) / GWidth) != x)
//                            {
//                                //do not check the same row
//                                if (Grid[Pos].Contains(PV)) //was: (Strings.InStr(Grid(Pos), PV) > 0)
//                                {
//                                    if (MethUsed[1] != SolveMethod)
//                                    {
//                                        MethUsed[0]++;
//                                        MethUsed[1] = SolveMethod;
//                                        AddUsedMeth();
//                                    }
//                                    if (DL == false)
//                                    {
//                                        MakeLog(3, "R", Cell[Block, 0, 1], PV, Convert.ToChar(65 + x).ToString());
//                                        DL = true;
//                                        functionReturnValue = true;
//                                    }
//                                    P2 = Grid[Pos].IndexOf(PV); //was: Strings.InStr(Grid(Pos), PV);
//                                    Grid[Pos] = Grid[Pos].Remove(P2, 1); //was: LeftChar(Grid(Pos), P2 - 1) + Strings.Mid(Grid(Pos), P2 + 1);
//                                    if (Grid[Pos].Length == 1)
//                                    {
//                                        MakeLog(3, "", Pos, PV);
//                                        if (GotHint)
//                                            return true;

//                                        SetGrid(Pos);
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }

//            //check columns for number wich could only appear in a certain block
//            for (x = 0; x <= GWmin1; x++)
//            {
//                V = new string[GWidth] { "", "", "", "", "", "", "", "", "" };

//                for (y = 0; y <= GWmin1; y++)
//                {
//                    Pos = Cell[y, x, 0];
//                    if (Grid[Pos].Length > 1)
//                    {
//                        for (A = 0; A < Grid[Pos].Length; A++)
//                        {
//                            PV = Grid[Pos][A].ToString(); //was: Strings.Mid(Grid(Pos), A, 1);
//                            PW = BString.IndexOf(PV);
//                            string search = ((int)(y / GridSize)).ToString();
//                            if (!V[PW].Contains(search)) //was: if (Strings.InStr(V(PW), Strings.Trim(Conversion.Str(Conversion.Int(y / GridSize)))) == 0)
//                            {
//                                V[PW] += search;
//                            }
//                            //was:
//                            //if (Strings.InStr(V(PW), Strings.Trim(Conversion.Str(Conversion.Int(y / GridSize)))) == 0)
//                            //{
//                            //    V(PW) = V(PW) + Strings.Trim(Conversion.Str(Conversion.Int(y / GridSize)));
//                            //}
//                        }
//                    }
//                }
//                for (A = 0; A < GWidth; A++)
//                {
//                    if (V[A].Length == 1)
//                    {
//                        //found number A wich could appear only in column x from block(v(A))
//                        //define start of block
//                        Block = ((int)(x / GridSize)) + int.Parse(V[A]) * GridSize; //was: (int)Conversion.Int(x / GridSize) + (int)(int)Conversion.Int(V(A)) * GridSize;
//                        //if errors, change back to Block = Int(x / GridSize) + Int(V(A)) * GridSize
//                        DL = false;
//                        PV = BString[A].ToString();
//                        for (y = 0; y <= GWmin1; y++)
//                        {
//                            Pos = Cell[Block, y, 1];
//                            if ((Pos) % GridSize != x % GridSize) //was: ((Pos - 1) % GridSize != x % GridSize)
//                            {
//                                //do not check the same column
//                                if (Grid[Pos].Contains(PV)) //was: (Strings.InStr(Grid(Pos), PV) > 0)
//                                {
//                                    if (MethUsed[1] != SolveMethod)
//                                    {
//                                        MethUsed[0]++;
//                                        MethUsed[1] = SolveMethod;
//                                        AddUsedMeth();
//                                    }
//                                    if (DL == false)
//                                    {
//                                        MakeLog(3, "C", Cell[Block, 0, 1], PV, (x + 1).ToString());
//                                        DL = true;
//                                        functionReturnValue = true;
//                                    }
//                                    P2 = Grid[Pos].IndexOf(PV);
//                                    Grid[Pos] = Grid[Pos].Remove(P2, 1); //was: LeftChar(Grid(Pos), P2 - 1) + Strings.Mid(Grid(Pos), P2 + 1);
//                                    if (Grid[Pos].Length == 1)
//                                    {
//                                        MakeLog(3, "", Pos, PV);
//                                        if (GotHint)
//                                            return true; // TODO: might not be correct. Was : Exit Function

//                                        SetGrid(Pos);
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            return functionReturnValue;
//        }

//        //look in a block for a number wich appear only in a certain row or column
//        //if found than that number can be savely removed from the rest of the
//        //row or column outside that block
//        private bool SolveD()
//        {
//            bool functionReturnValue = false;
//            int x;
//            int y;
//            int A;
//            int Z;
//            int Sc;
//            int Pos;
//            string PV;
//            int P1;
//            int P2;
//            int Row;
//            int Col;
//            string[] R;
//            string[] C;
//            bool DL;
//            SolveMethod = 4;
//            for (x = 0; x <= GWmin1; x++)
//            {
//                R = new string[GWidth] { "", "", "", "", "", "", "", "", "" };
//                C = new string[GWidth] { "", "", "", "", "", "", "", "", "" };

//                for (y = 0; y <= GWmin1; y++)
//                {
//                    Pos = Cell[x, y, 1];
//                    if (Grid[Pos].Length > 1)
//                    {
//                        Row = (int)((Pos) / GWidth); //was: (int)((Pos - 1) / GWidth);
//                        Col = Pos - Row * GWidth; //was: Pos - 1 - Row * GWidth;
//                        for (Z = 0; Z < Grid[Pos].Length; Z++)
//                        {
//                            PV = Grid[Pos][Z].ToString();
//                            P2 = BString.IndexOf(PV); //was: Strings.InStr(BString, PV);
//                            if (!R[P2].Contains(Row.ToString())) //was: (Strings.InStr(R(P2), Strings.Trim(Conversion.Str(Row))) == 0)
//                                R[P2] += Row.ToString();
//                            if (!C[P2].Contains(Col.ToString()))
//                                C[P2] += Col.ToString();
//                        }
//                    }
//                }
//                //check if a number appears only in one row
//                for (Z = 0; Z < GWidth; Z++)
//                {
//                    DL = false;
//                    if (R[Z].Length == 1)
//                    {
//                        Row = int.Parse(R[Z]);
//                        //row number
//                        Sc = (x % GridSize);
//                        PV = BString[Z].ToString();
//                        for (A = 0; A <= GWmin1; A++)
//                        {
//                            if ((int)(A / GridSize) != Sc)
//                            {
//                                Pos = Cell[Row, A, 0];
//                                if (Grid[Pos].Length > 1)
//                                {
//                                    if (Grid[Pos].Contains(PV))
//                                    {
//                                        if (MethUsed[1] != SolveMethod)
//                                        {
//                                            MethUsed[0] = MethUsed[0] + 1;
//                                            MethUsed[1] = SolveMethod;
//                                            AddUsedMeth();
//                                        }
//                                        if (DL == false)
//                                        {
//                                            MakeLog(4, "R", Cell[x, 0, 1], PV, Convert.ToChar(65 + Row).ToString());
//                                            DL = true;
//                                            functionReturnValue = true;
//                                        }
//                                        P1 = Grid[Pos].IndexOf(PV);
//                                        Grid[Pos] = Grid[Pos].Remove(P1, 1); // LeftChar(Grid(Pos), P1 - 1) + Strings.Mid(Grid(Pos), P1 + 1); //was:  LeftChar(Grid(Pos), P1 - 1) + Strings.Mid(Grid(Pos), P1 + 1);
//                                        MakeLog(4, "", Pos, PV);
//                                        if (Grid[Pos].Length == 1)
//                                        {
//                                            if (GotHint)
//                                                return true;

//                                            SetGrid(Pos);
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }


//                //check if a number appears only in one column
//                for (Z = 0; Z < GWidth; Z++)
//                {
//                    DL = false;
//                    if (C[Z].Length == 1)
//                    {
//                        Col = int.Parse(C[Z]);
//                        //column number
//                        Sc = (int)(x / GridSize);
//                        PV = BString[Z].ToString();
//                        for (A = 0; A <= GWmin1; A++)
//                        {
//                            if ((int)(A / GridSize) != Sc)
//                            {
//                                Pos = Cell[A, Col, 0];
//                                if (Grid[Pos].Length > 1)
//                                {
//                                    if (Grid[Pos].Contains(PV))
//                                    {
//                                        if (MethUsed[1] != SolveMethod)
//                                        {
//                                            MethUsed[0] = MethUsed[0] + 1;
//                                            MethUsed[1] = SolveMethod;
//                                            AddUsedMeth();
//                                        }
//                                        if (DL == false)
//                                        {
//                                            MakeLog(4, "C", Cell[x, 0, 1], PV, (Col+1).ToString()); //was: 4, "C", Cell[x, 0, 1], PV, (Col + 1).ToString()); //offset: Col+1?
//                                            DL = true;
//                                            functionReturnValue = true;
//                                        }
//                                        P1 = Grid[Pos].IndexOf(PV);
//                                        Grid[Pos] = Grid[Pos].Remove(P1, 1); //was: LeftChar(Grid(Pos), P1 - 1) + Strings.Mid(Grid(Pos), P1 + 1);
//                                        MakeLog(4, "", Pos, PV);
//                                        if (Grid[Pos].Length == 1)
//                                        {
//                                            if (GotHint)
//                                                return true; // TODO: might not be correct. Was : Exit Function

//                                            SetGrid(Pos);
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            return functionReturnValue;
//        }


//        //If two values appears twice in a row,column or block and these values
//        //appears also in the same fields than the
//        //remaining numbers in those field can be savely removed
//        //the same goes for 3 field bij values of 3 bytes
//        //etc.etc.
//        private bool SolveE()
//        {
//            bool functionReturnValue = false;
//            int x;
//            int y;
//            int A;
//            int b;
//            int C;
//            int Pos;
//            int[] B1;
//            int P2;
//            string Lg; //Logs            
//            int Cnt;
//            int[,] V; //5 is out the question
//            int[,] P;
//            int P1;
//            string PV;
//            bool Fail;
//            bool Test;
//            string SetTo;
//            string[] BL = new string[GWidth] { "", "", "", "", "", "", "", "", "" };
//            string[] ST = new string[GWidth] { "", "", "", "", "", "", "", "", "" };
//            int[] Ch = new int[8];
//            int R1;
//            int C1;
//            SolveMethod = 5;
//            //check rows
//            for (x = 0; x <= GWmin1; x++)
//            {
//                //Rows |
//                V = new int[GWidth, 7];

//                for (y = 0; y <= GWmin1; y++)
//                {
//                    //columns -
//                    Pos = Cell[y, x, 0];
//                    for (A = 0; A < Grid[Pos].Length; A++)
//                    {
//                        PV = Grid[Pos][A].ToString();
//                        P2 = BString.IndexOf(PV);
//                        V[P2, 0] += 1;
//                        if (V[P2, 0] < 5)
//                            V[P2, V[P2, 0]] = Pos;
//                    }
//                }
//                //GoSub CheckSet
//                Cnt = 2;
//                while (Cnt < 5)
//                {
//                    P = new int[GWidth, GWidth];

//                    P1 = 0;
//                    Fail = false;
//                    B1 = new int[GWidth];

//                    for (b = 0; b < GWidth; b++)
//                    {
//                        //Value
//                        if (V[b, 0] == Cnt)
//                        {
//                            if (P1 == 0)
//                            {
//                                P1 = 1;
//                                P[P1, 0]++;
//                                P[P1, P[P1, 0]] = b;
//                            }
//                            else
//                            {
//                                Test = false;
//                                for (C = 0; C < P1; C++)
//                                {
//                                    if (V[b, 1] == V[P[C, P[C, 0]], 1]) //offset: 0 instead of 1?
//                                    {
//                                        P[C, 0]++;
//                                        P[C, P[C, 0]] = b;
//                                        Test = true;
//                                    }
//                                }
//                                if (Test == false)
//                                {
//                                    P1++;
//                                    P[P1, 0]++;
//                                    P[P1, P[P1, 0]] = b;
//                                }
//                            }
//                        }
//                    }
//                    for (y = 0; y < P1; y++)
//                    {
//                        if (P[y, 0] == Cnt)
//                        {
//                            for (C = 0; C < Cnt; C++)
//                            {
//                                B1[0] = Cnt;
//                                B1[C] = P[y, C];
//                            }
//                            P2 = B1[1];
//                            for (b = 1; b < Cnt; b++)
//                            {
//                                C = B1[b];
//                                for (A = 1; A <= Cnt; A++)
//                                {
//                                    if (V[P2, A] != V[C, A])
//                                        Fail = true;
//                                    //not the same
//                                }
//                                if (Fail)
//                                    break;

//                            }
//                            SetTo = "";
//                            for (A = 0; A < Cnt; A++)
//                            {
//                                SetTo += BString[B1[A]].ToString();
//                            }
//                            b = 0;
//                            for (A = 1; A <= Cnt; A++)
//                            {
//                                if (Grid[V[P2, A]] == SetTo)
//                                    b++;
//                            }
//                            if (b == Cnt)
//                                Fail = true;
//                            if (!Fail)
//                            {
//                                //succes
//                                SetTo = "";
//                                for (A = 1; A <= Cnt; A++)
//                                {
//                                    R1 = (int)((V[P2, A] - 1) / GWidth) + 1; //offset: -1? +1?      !!
//                                    C1 = V[P2, A] - (R1 - 1) * GWidth; //offset: -1?                !!
//                                    BL[A] = Convert.ToChar(64 + R1) + C1.ToString();
//                                    ST[A] = BString[B1[A]].ToString();
//                                    SetTo += BString[B1[A]];
//                                }
//                                Lg = "The values " + ST[1];
//                                for (A = 1; A < Cnt; A++)
//                                {
//                                    Lg += " and " + ST[A];
//                                }
//                                Lg += " are set to field [" + BL[1] + "]";
//                                for (A = 1; A < Cnt; A++)
//                                {
//                                    Lg += " and [" + BL[A] + "]";
//                                }
//                                MakeLog(5, "", 1, Lg);
//                                functionReturnValue = true;
//                                for (A = 1; A <= Cnt; A++)
//                                {
//                                    Grid[V[P2, A]] = SetTo;
//                                }
//                                if (MethUsed[1] != SolveMethod)
//                                {
//                                    MethUsed[0]++;
//                                    MethUsed[1] = SolveMethod;
//                                    AddUsedMeth();
//                                }
//                            }
//                        }
//                    }
//                    Cnt++;
//                }
//            }
//            //check Columns
//            for (x = 0; x <= GWmin1; x++)
//            {
//                //Rows |
//                V = new int[GWidth, 4];

//                for (y = 0; y <= GWmin1; y++)
//                {
//                    //columns -
//                    Pos = Cell[x, y, 0];
//                    for (A = 0; A < Grid[Pos].Length; A++)
//                    {
//                        PV = Grid[Pos][A].ToString();
//                        P2 = BString.IndexOf(PV);
//                        V[P2, 0]++;
//                        if (V[P2, 0] < 5) //offset: 4?
//                            V[P2, V[P2, 0]] = Pos;
//                    }
//                }
//                //GoSub CheckSet
//                Cnt = 2;
//                while (Cnt < 5)
//                {
//                    // ERROR: Not supported in C#: ReDimStatement

//                    P1 = 0;
//                    Fail = false;
//                    // ERROR: Not supported in C#: ReDimStatement

//                    for (b = 1; b <= GWidth; b++)
//                    {
//                        //Value
//                        if (V(b, 0) == Cnt)
//                        {
//                            if (P1 == 0)
//                            {
//                                P1 = 1;
//                                P(P1, 0) = P(P1, 0) + 1;
//                                P(P1, P(P1, 0)) = b;
//                            }
//                            else
//                            {
//                                Test = false;
//                                for (C = 1; C <= P1; C++)
//                                {
//                                    if (V(b, 1) == V(P(C, P(C, 0)), 1))
//                                    {
//                                        P(C, 0) = P(C, 0) + 1;
//                                        P(C, P(C, 0)) = b;
//                                        Test = true;
//                                    }
//                                }
//                                if (Test == false)
//                                {
//                                    P1 = P1 + 1;
//                                    P(P1, 0) = P(P1, 0) + 1;
//                                    P(P1, P(P1, 0)) = b;
//                                }
//                            }
//                        }
//                    }
//                    for (y = 1; y <= P1; y++)
//                    {
//                        if (P(y, 0) == Cnt)
//                        {
//                            for (C = 1; C <= Cnt; C++)
//                            {
//                                B1(0) = Cnt;
//                                B1(C) = P(y, C);
//                            }
//                            P2 = B1(1);
//                            for (b = 2; b <= Cnt; b++)
//                            {
//                                C = B1(b);
//                                for (A = 1; A <= Cnt; A++)
//                                {
//                                    if (V(P2, A) != V(C, A))
//                                        Fail = true;
//                                    //not the same
//                                }
//                                if (Fail)
//                                    break; // TODO: might not be correct. Was : Exit For

//                            }
//                            SetTo = "";
//                            for (A = 1; A <= Cnt; A++)
//                            {
//                                SetTo = SetTo + Strings.Mid(BString, B1(A), 1);
//                            }
//                            b = 0;
//                            for (A = 1; A <= Cnt; A++)
//                            {
//                                if (Grid(V(P2, A)) == SetTo)
//                                    b = b + 1;
//                            }
//                            if (b == Cnt)
//                                Fail = true;
//                            if (!Fail)
//                            {
//                                //succes
//                                SetTo = "";
//                                for (A = 1; A <= Cnt; A++)
//                                {
//                                    R1 = (int)Conversion.Int((V(P2, A) - 1) / GWidth) + 1;
//                                    C1 = V(P2, A) - (R1 - 1) * GWidth;
//                                    BL(A) = Strings.Chr(64 + R1) + Strings.Trim(Conversion.Str(C1));
//                                    ST(A) = Strings.Mid(BString, B1(A), 1);
//                                    SetTo = SetTo + Strings.Mid(BString, B1(A), 1);
//                                }
//                                Lg = "The values " + ST(1);
//                                for (A = 2; A <= Cnt; A++)
//                                {
//                                    Lg = Lg + " and " + ST(A);
//                                }
//                                Lg = Lg + " are set to field [" + BL(1) + "]";
//                                for (A = 2; A <= Cnt; A++)
//                                {
//                                    Lg = Lg + " and [" + BL(A) + "]";
//                                }
//                                MakeLog(5, "", 1, Lg);
//                                functionReturnValue = true;
//                                for (A = 1; A <= Cnt; A++)
//                                {
//                                    Grid(V(P2, A)) = SetTo;
//                                }
//                                if (MethUsed[1] != SolveMethod)
//                                {
//                                    MethUsed[0] = MethUsed[0] + 1;
//                                    MethUsed[1] = SolveMethod;
//                                    AddUsedMeth();
//                                }
//                            }
//                        }
//                    }
//                    Cnt = Cnt + 1;
//                }
//            }
//            //check blocks
//            for (x = 0; x <= GWmin1; x++)
//            {
//                // ERROR: Not supported in C#: ReDimStatement

//                for (y = 0; y <= GWmin1; y++)
//                {
//                    Pos = Cell(x, y, 1);
//                    for (A = 1; A <= Grid[Pos].Length; A++)
//                    {
//                        PV = Strings.Mid(Grid(Pos), A, 1);
//                        P2 = Strings.InStr(BString, PV);
//                        V(P2, 0) = V(P2, 0) + 1;
//                        if (V(P2, 0) < 5)
//                            V(P2, V(P2, 0)) = Pos;
//                    }
//                }
//                //GoSub CheckSet
//                Cnt = 2;
//                while (Cnt < 5)
//                {
//                    // ERROR: Not supported in C#: ReDimStatement

//                    P1 = 0;
//                    Fail = false;
//                    // ERROR: Not supported in C#: ReDimStatement

//                    for (b = 1; b <= GWidth; b++)
//                    {
//                        //Value
//                        if (V(b, 0) == Cnt)
//                        {
//                            if (P1 == 0)
//                            {
//                                P1 = 1;
//                                P(P1, 0) = P(P1, 0) + 1;
//                                P(P1, P(P1, 0)) = b;
//                            }
//                            else
//                            {
//                                Test = false;
//                                for (C = 1; C <= P1; C++)
//                                {
//                                    if (V(b, 1) == V(P(C, P(C, 0)), 1))
//                                    {
//                                        P(C, 0) = P(C, 0) + 1;
//                                        P(C, P(C, 0)) = b;
//                                        Test = true;
//                                    }
//                                }
//                                if (Test == false)
//                                {
//                                    P1 = P1 + 1;
//                                    P(P1, 0) = P(P1, 0) + 1;
//                                    P(P1, P(P1, 0)) = b;
//                                }
//                            }
//                        }
//                    }
//                    for (y = 1; y <= P1; y++)
//                    {
//                        if (P(y, 0) == Cnt)
//                        {
//                            for (C = 1; C <= Cnt; C++)
//                            {
//                                B1(0) = Cnt;
//                                B1(C) = P(y, C);
//                            }
//                            P2 = B1(1);
//                            for (b = 2; b <= Cnt; b++)
//                            {
//                                C = B1(b);
//                                for (A = 1; A <= Cnt; A++)
//                                {
//                                    if (V(P2, A) != V(C, A))
//                                        Fail = true;
//                                    //not the same
//                                }
//                                if (Fail)
//                                    break; // TODO: might not be correct. Was : Exit For

//                            }
//                            SetTo = "";
//                            for (A = 1; A <= Cnt; A++)
//                            {
//                                SetTo = SetTo + Strings.Mid(BString, B1(A), 1);
//                            }
//                            b = 0;
//                            for (A = 1; A <= Cnt; A++)
//                            {
//                                if (Grid(V(P2, A)) == SetTo)
//                                    b = b + 1;
//                            }
//                            if (b == Cnt)
//                                Fail = true;
//                            if (!Fail)
//                            {
//                                //succes
//                                SetTo = "";
//                                for (A = 1; A <= Cnt; A++)
//                                {
//                                    R1 = (int)Conversion.Int((V(P2, A) - 1) / GWidth) + 1;
//                                    C1 = V(P2, A) - (R1 - 1) * GWidth;
//                                    BL(A) = Strings.Chr(64 + R1) + Strings.Trim(Conversion.Str(C1));
//                                    ST(A) = Strings.Mid(BString, B1(A), 1);
//                                    SetTo = SetTo + Strings.Mid(BString, B1(A), 1);
//                                }
//                                Lg = "The values " + ST(1);
//                                for (A = 2; A <= Cnt; A++)
//                                {
//                                    Lg = Lg + " and " + ST(A);
//                                }
//                                Lg = Lg + " are set to field [" + BL(1) + "]";
//                                for (A = 2; A <= Cnt; A++)
//                                {
//                                    Lg = Lg + " and [" + BL(A) + "]";
//                                }
//                                MakeLog(5, "", 1, Lg);
//                                functionReturnValue = true;
//                                for (A = 1; A <= Cnt; A++)
//                                {
//                                    Grid(V(P2, A)) = SetTo;
//                                }
//                                if (MethUsed[1] != SolveMethod)
//                                {
//                                    MethUsed[0] = MethUsed[0] + 1;
//                                    MethUsed[1] = SolveMethod;
//                                    AddUsedMeth();
//                                }
//                            }
//                        }
//                    }
//                    Cnt = Cnt + 1;
//                }
//            }
//            return; // TODO: might not be correct. Was : Exit Function
//            return functionReturnValue;
//        }

//        /*
//        //If a value of two bytes can be found in a square block across two
//        //3x3 blocks than one of the two values must be in that fields
//        //just put one value into a field en the other 3 will be solved automaticly
//        //this is a sort of guessing but there is no other way to do this
//        private bool SolveF()
//        {
//            bool functionReturnValue = false;
//            if (UseGuessing == true)
//            {
//                int x;
//                int y;
//                int A;
//                int b;
//                int R1;
//                int C1;
//                string BLU;
//                string BRD;
//                int P1;
//                int P2;
//                int P3;
//                int P4;
//                SolveMethod = 6;
//                for (x = 0; x <= GWmin1; x++)
//                {
//                    if (((x + 1) % GridSize) > 0)
//                    {
//                        for (y = 0; y <= GWmin1 - 1; y++)
//                        {
//                            P1 = x * GWidth + 1 + y;
//                            if (Strings.Len(Grid(P1)) == 2)
//                            {
//                                for (A = 1; A <= GWmin1 - y; A++)
//                                {
//                                    P2 = P1 + A;
//                                    if (Grid(P1) == Grid(P2))
//                                    {
//                                        for (b = 1; b <= GridSize - ((x + 1) % GridSize); b++)
//                                        {
//                                            P3 = P1 + b * GWidth;
//                                            if (Grid(P1) == Grid(P3))
//                                            {
//                                                P4 = P3 + A;
//                                                if (Grid(P1) == Grid(P4))
//                                                {
//                                                    //found a square
//                                                    if (MethUsed[1] != SolveMethod)
//                                                    {
//                                                        MethUsed[0] = MethUsed[0] + 1;
//                                                        MethUsed[1] = SolveMethod;
//                                                        AddUsedMeth();
//                                                    }
//                                                    R1 = (int)Conversion.Int((P1 - 1) / GWidth) + 1;
//                                                    C1 = P1 - (R1 - 1) * GWidth;
//                                                    BLU = Strings.Chr(64 + R1) + Strings.Trim(Conversion.Str(C1));
//                                                    R1 = (int)Conversion.Int((P4 - 1) / GWidth) + 1;
//                                                    C1 = P4 - (R1 - 1) * GWidth;
//                                                    BRD = Strings.Chr(64 + R1) + Strings.Trim(Conversion.Str(C1));
//                                                    MakeLog(6, "", 1, BLU, BRD);
//                                                    Grid(P1) = LeftChar(Grid(P1), 1);
//                                                    functionReturnValue = true;
//                                                    if (GotHint == true)
//                                                        return; // TODO: might not be correct. Was : Exit Function

//                                                    SetGrid(P1);
//                                                }
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }

//                for (x = 0; x <= GWmin1; x++)
//                {
//                    if (((x + 1) % GridSize) > 0)
//                    {
//                        for (y = 0; y <= GWmin1 - 1; y++)
//                        {
//                            P1 = x + 1 + y * GWidth;
//                            if (Strings.Len(Grid(P1)) == 2)
//                            {
//                                for (A = 1; A <= GWmin1 - y; A++)
//                                {
//                                    P2 = P1 + A * GWidth;
//                                    if (Grid(P1) == Grid(P2))
//                                    {
//                                        for (b = 1; b <= GridSize - ((x + 1) % GridSize); b++)
//                                        {
//                                            P3 = P1 + b;
//                                            if (Grid(P1) == Grid(P3))
//                                            {
//                                                P4 = P3 + A * GWidth;
//                                                if (Grid(P1) == Grid(P4))
//                                                {
//                                                    //found a square
//                                                    if (MethUsed[1] != SolveMethod)
//                                                    {
//                                                        MethUsed[0] = MethUsed[0] + 1;
//                                                        MethUsed[1] = SolveMethod;
//                                                        AddUsedMeth();
//                                                    }
//                                                    R1 = (int)Conversion.Int((P1 - 1) / GWidth) + 1;
//                                                    C1 = P1 - (R1 - 1) * GWidth;
//                                                    BLU = Strings.Chr(64 + R1) + Strings.Trim(Conversion.Str(C1));
//                                                    R1 = (int)Conversion.Int((P4 - 1) / GWidth) + 1;
//                                                    C1 = P4 - (R1 - 1) * GWidth;
//                                                    BRD = Strings.Chr(64 + R1) + Strings.Trim(Conversion.Str(C1));
//                                                    MakeLog(6, "", 1, BLU, BRD);
//                                                    Grid(P1) = LeftChar(Grid(P1), 1);
//                                                    functionReturnValue = true;
//                                                    if (GotHint == true)
//                                                        return; // TODO: might not be correct. Was : Exit Function

//                                                    SetGrid(P1);
//                                                }
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            return false;
//            return functionReturnValue;
//        }

//        //find two numbers of two bytes wich are the same in a row, column or block
//        //These numbers then can be removed from the rest of the row, column or block
//        private bool SolveG()
//        {
//            bool functionReturnValue = false;
//            int x;
//            int X1;
//            int Y1;
//            string BL1;
//            string BL2;
//            string BL3;
//            int A;
//            int b;
//            int C;
//            int D;
//            int Pos;
//            int PO;
//            string PV1;
//            string PV2;
//            string[] P = new string[2];
//            bool DL;
//            SolveMethod = 7;
//            //search the rows
//            for (x = 0; x <= GWmin1; x++)
//            {
//                //rows
//                for (A = 0; A <= GWmin1 - 1; A++)
//                {
//                    PV1 = Grid(Cell(x, A, 0));
//                    if (Strings.Len(PV1) == 2)
//                    {
//                        for (b = A + 1; b <= GWmin1; b++)
//                        {
//                            PV2 = Grid(Cell(x, b, 0));
//                            if (PV2 == PV1)
//                            {
//                                //found a double
//                                DL = false;
//                                P(0) = LeftChar(PV1, 1);
//                                P(1) = RightChar(PV1, 1);
//                                for (C = 0; C <= GWmin1; C++)
//                                {
//                                    //search the row
//                                    if (C != A & C != b)
//                                    {
//                                        Pos = Cell(x, C, 0);
//                                        for (D = 0; D <= 1; D++)
//                                        {
//                                            if (Strings.InStr(Grid(Pos), P(D)) > 0)
//                                            {
//                                                if (MethUsed[1] != SolveMethod)
//                                                {
//                                                    MethUsed[0] = MethUsed[0] + 1;
//                                                    MethUsed[1] = SolveMethod;
//                                                    AddUsedMeth();
//                                                }
//                                                if (DL == false)
//                                                {
//                                                    X1 = (int)Conversion.Int((Cell(x, A, 0) - 1) / GWidth) + 1;
//                                                    Y1 = Cell(x, A, 0) - (X1 - 1) * GWidth;
//                                                    BL1 = Strings.Chr(64 + X1) + Strings.Trim(Conversion.Str(Y1));
//                                                    X1 = (int)Conversion.Int((Cell(x, b, 0) - 1) / GWidth) + 1;
//                                                    Y1 = Cell(x, b, 0) - (X1 - 1) * GWidth;
//                                                    BL2 = Strings.Chr(64 + X1) + Strings.Trim(Conversion.Str(Y1));
//                                                    MakeLog(7, "R", 0, PV1, BL1, BL2);
//                                                    DL = true;
//                                                }
//                                                PO = Strings.InStr(Grid(Pos), P(D));
//                                                Grid(Pos) = LeftChar(Grid(Pos), PO - 1) + Strings.Mid(Grid(Pos), PO + 1);
//                                                functionReturnValue = true;
//                                                MakeLog(7, "", Pos, P(D));
//                                                if (Grid[Pos].Length == 1)
//                                                {
//                                                    if (GotHint)
//                                                        return; // TODO: might not be correct. Was : Exit Function

//                                                    SetGrid(Pos);
//                                                }
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }

//            //search the columns
//            for (x = 0; x <= GWmin1; x++)
//            {
//                //rows
//                for (A = 0; A <= GWmin1 - 1; A++)
//                {
//                    PV1 = Grid(Cell(A, x, 0));
//                    if (Strings.Len(PV1) == 2)
//                    {
//                        for (b = A + 1; b <= GWmin1; b++)
//                        {
//                            PV2 = Grid(Cell(b, x, 0));
//                            if (PV2 == PV1)
//                            {
//                                //found a double
//                                DL = false;
//                                P(0) = LeftChar(PV1, 1);
//                                P(1) = RightChar(PV1, 1);
//                                for (C = 0; C <= GWmin1; C++)
//                                {
//                                    //search the row
//                                    if (C != A & C != b)
//                                    {
//                                        Pos = Cell(C, x, 0);
//                                        for (D = 0; D <= 1; D++)
//                                        {
//                                            if (Strings.InStr(Grid(Pos), P(D)) > 0)
//                                            {
//                                                if (MethUsed[1] != SolveMethod)
//                                                {
//                                                    MethUsed[0] = MethUsed[0] + 1;
//                                                    MethUsed[1] = SolveMethod;
//                                                    AddUsedMeth();
//                                                }
//                                                if (DL == false)
//                                                {
//                                                    X1 = (int)Conversion.Int((Cell(A, x, 0) - 1) / GWidth) + 1;
//                                                    Y1 = Cell(A, x, 0) - (X1 - 1) * GWidth;
//                                                    BL1 = Strings.Chr(64 + X1) + Strings.Trim(Conversion.Str(Y1));
//                                                    X1 = (int)Conversion.Int((Cell(b, x, 0) - 1) / GWidth) + 1;
//                                                    Y1 = Cell(b, x, 0) - (X1 - 1) * GWidth;
//                                                    BL2 = Strings.Chr(64 + X1) + Strings.Trim(Conversion.Str(Y1));
//                                                    MakeLog(7, "C", 0, PV1, BL1, BL2);
//                                                    DL = true;
//                                                }
//                                                PO = Strings.InStr(Grid(Pos), P(D));
//                                                Grid(Pos) = LeftChar(Grid(Pos), PO - 1) + Strings.Mid(Grid(Pos), PO + 1);
//                                                functionReturnValue = true;
//                                                MakeLog(7, "", Pos, P(D));
//                                                if (Grid[Pos].Length == 1)
//                                                {
//                                                    if (GotHint)
//                                                        return; // TODO: might not be correct. Was : Exit Function

//                                                    SetGrid(Pos);
//                                                }
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }

//            //search the blocks
//            for (x = 0; x <= GWmin1; x++)
//            {
//                //rows
//                for (A = 0; A <= GWmin1 - 1; A++)
//                {
//                    PV1 = Grid(Cell(x, A, 1));
//                    if (Strings.Len(PV1) == 2)
//                    {
//                        for (b = A + 1; b <= GWmin1; b++)
//                        {
//                            PV2 = Grid(Cell(x, b, 1));
//                            if (PV2 == PV1)
//                            {
//                                //found a double
//                                DL = false;
//                                P(0) = LeftChar(PV1, 1);
//                                P(1) = RightChar(PV1, 1);
//                                for (C = 0; C <= GWmin1; C++)
//                                {
//                                    //search the row
//                                    if (C != A & C != b)
//                                    {
//                                        Pos = Cell(x, C, 1);
//                                        for (D = 0; D <= 1; D++)
//                                        {
//                                            if (Strings.InStr(Grid(Pos), P(D)) > 0)
//                                            {
//                                                if (MethUsed[1] != SolveMethod)
//                                                {
//                                                    MethUsed[0] = MethUsed[0] + 1;
//                                                    MethUsed[1] = SolveMethod;
//                                                    AddUsedMeth();
//                                                }
//                                                if (DL == false)
//                                                {
//                                                    X1 = (int)Conversion.Int((Cell(x, A, 1) - 1) / GWidth) + 1;
//                                                    Y1 = Cell(x, A, 1) - (X1 - 1) * GWidth;
//                                                    BL1 = Strings.Chr(64 + X1) + Strings.Trim(Conversion.Str(Y1));
//                                                    X1 = (int)Conversion.Int((Cell(x, b, 1) - 1) / GWidth) + 1;
//                                                    Y1 = Cell(x, b, 1) - (X1 - 1) * GWidth;
//                                                    BL2 = Strings.Chr(64 + X1) + Strings.Trim(Conversion.Str(Y1));
//                                                    X1 = (int)Conversion.Int((Cell(x, 0, 1) - 1) / GWidth) + 1;
//                                                    Y1 = Cell(x, 0, 1) - (X1 - 1) * GWidth;
//                                                    BL3 = Strings.Chr(64 + X1) + Strings.Trim(Conversion.Str(Y1));
//                                                    MakeLog(7, "B", 0, PV1, BL1, BL2, BL3);
//                                                    DL = true;
//                                                }
//                                                PO = Strings.InStr(Grid(Pos), P(D));
//                                                Grid(Pos) = LeftChar(Grid(Pos), PO - 1) + Strings.Mid(Grid(Pos), PO + 1);
//                                                functionReturnValue = true;
//                                                MakeLog(7, "", Pos, P(D));
//                                                if (Grid[Pos].Length == 1)
//                                                {
//                                                    if (GotHint)
//                                                        return; // TODO: might not be correct. Was : Exit Function

//                                                    SetGrid(Pos);
//                                                }
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            return functionReturnValue;

//        }

//        //Trying the remaining numbers of a cell and check if a cell anywhere else
//        //on the grid shows the same value after this test
//        //this cell must then have this value
//        public bool SolveH()
//        {
//            bool functionReturnValue = false;
//            string[,] BU;
//            int x;
//            int y;
//            int Lop;
//            int LopT;
//            int StPos;
//            int Pos;
//            string Value;
//            bool Donext;
//            bool Found;
//            long BUP;
//            long BUG;
//            string[] f = new string[2];
//            // ERROR: Not supported in C#: ReDimStatement

//            for (x = 1; x <= GNum; x++)
//            {
//                BU(0, x) = Grid(x);
//            }
//            BUP = TPoints;
//            BUG = ToGo;
//            StPos = 1;
//        DoNextPos:
//            Lop = 1;
//            do
//            {
//                Donext = false;
//                while (Strings.Len(Grid(StPos)) == 1)
//                {
//                    StPos = StPos + 1;
//                    if (StPos > GNum)
//                        goto StopFunc;
//                }
//                LopT = Strings.Len(Grid(StPos));
//                Grid(StPos) = Strings.Mid(Grid(StPos), Lop, 1);
//                SetGrid(StPos);
//                if (Lop == LopT)
//                {
//                    StPos = StPos + 1;
//                    Lop = 0;
//                }
//                if (Lop != 0)
//                {
//                    for (x = 1; x <= GNum; x++)
//                    {
//                        try
//                        {
//                            BU(Lop, x) = Grid(x);
//                        }
//                        catch
//                        {
//                            return false;
//                        }

//                        Grid(x) = BU(0, x);
//                    }
//                    TPoints = BUP;
//                    ToGo = (int)Conversion.Int(BUG);
//                    Lop = Lop + 1;
//                }
//                else
//                {
//                    break; // TODO: might not be correct. Was : Exit Do
//                }
//            }
//            while (true);
//            Value = "";
//            GotHint = false;
//            //hint could be set but shouldn't be
//            for (x = 1; x <= GNum; x++)
//            {
//                if (Strings.Len(BU(0, x)) > 1)
//                {
//                    Found = true;
//                    for (y = 1; y <= LopT; y++)
//                    {
//                        if (Strings.Len(BU(y, x)) > 1)
//                        {
//                            Found = false;
//                            break; // TODO: might not be correct. Was : Exit For
//                        }
//                        if (Grid(x) != BU(y, x))
//                        {
//                            Found = false;
//                            break; // TODO: might not be correct. Was : Exit For
//                        }
//                    }
//                    if (Found == true)
//                    {
//                        Value = Grid(x);
//                        Pos = x;
//                        break; // TODO: might not be correct. Was : Exit For
//                    }
//                }
//            }
//            for (x = 1; x <= GNum; x++)
//            {
//                Grid(x) = BU(0, x);
//            }
//            TPoints = BUP;
//            ToGo = (int)Conversion.Int(BUG);
//            if (Value != "")
//            {
//                SolveMethod = 8;
//                if (MethUsed[1] != SolveMethod)
//                {
//                    MethUsed[0] = MethUsed[0] + 1;
//                    MethUsed[1] = SolveMethod;
//                    AddUsedMeth();
//                }
//                x = (int)Conversion.Int((StPos - 2) / GWidth) + 1;
//                y = StPos - 1 - (x - 1) * GWidth;
//                f(1) = Strings.Chr(64 + x) + Strings.Trim(Conversion.Str(y));
//                MakeLog(8, "", Pos, Value, f(1), Grid(StPos - 1));
//                functionReturnValue = true;
//                if (GotHint == true)
//                    return; // TODO: might not be correct. Was : Exit Function

//                Grid(Pos) = Value;
//                SetGrid(Pos);
//                return; // TODO: might not be correct. Was : Exit Function
//            }
//            if (StPos <= GNum)
//                goto DoNextPos;
//            return; // TODO: might not be correct. Was : Exit Function
//        StopFunc:

//            TPoints = BUP;
//            ToGo = (int)Conversion.Int(BUG);
//            return functionReturnValue;
//        }
//        */
//        #endregion

//        //Remove the known position from related row, column and block
//        private void SetGrid(int Position)
//        {
//            int x;
//            int Row;
//            int Col;
//            int BL;
//            int Pos;
//            if (MethUsed[1] != 99)
//            {
//                MethUsed[0]++;
//                MethUsed[1] = 99;
//                AddUsedMeth();
//            }
//            Row = (int)((Position) / GWidth); //was: (int)Conversion.Int((Position - 1) / GWidth);
//            Col = Position - Row * GWidth; //was: Position - 1 - Row * GWidth;
//            BL = (int)(Col / GridSize) + (int)(Row / GridSize) * GridSize;
//            //Remove from block
//            MakeLog(1, "", Position, Grid[Position]);
//            if (GotHint)
//                return;

//            for (x = 0; x <= GWmin1; x++)
//            {
//                Pos = Cell[BL, x, 1];
//                try
//                {
//                    if (Grid[Pos].Length > 1)
//                    {
//                        RemoveNum(Pos, Position);
//                        if (Grid[Pos].Length == 1)
//                        {
//                            MakeLog(1, "B", Pos, Grid[Position]);
//                            if (GotHint)
//                                return; // TODO: might not be correct. Was : Exit Sub

//                        }
//                    }
//                }
//                catch
//                {
//                    System.Windows.Forms.MessageBox.Show("Error...");
//                }
//            }
//            //Remove from rows
//            for (x = 0; x <= GWmin1; x++)
//            {
//                Pos = Cell[Row, x, 0];
//                if (Grid[Pos].Length > 1)
//                {
//                    RemoveNum(Pos, Position);
//                    if (Grid[Pos].Length == 1)
//                    {
//                        MakeLog(1, "R", Pos, Grid[Position]);
//                        if (GotHint)
//                            return;

//                    }
//                }
//            }
//            //Remove from columns
//            for (x = 0; x <= GWmin1; x++)
//            {
//                Pos = Cell[x, Col, 0];
//                if (Grid[Pos].Length > 1)
//                {
//                    RemoveNum(Pos, Position);
//                    if (Grid[Pos].Length == 1)
//                    {
//                        MakeLog(1, "R", Pos, Grid[Position]);
//                        if (GotHint)
//                            return; // TODO: might not be correct. Was : Exit Sub

//                    }
//                }
//            }
//            while (ExtSolved[0] > 0)
//            {
//                Pos = ExtSolved[ExtSolved[0]];
//                ExtSolved[0] -= 1;
//                SetGrid(Pos);
//            }
//        }

         
//        //store the methods used for later use by the level checker
//        private void AddUsedMeth()
//        {
//            string NewVal;
//            if (SolveMethod == 99)
//            {
//                NewVal = "Z";
//                return;
//            }
//            else
//            {
//                NewVal = "123456789ABCDEFG"[SolveMethod].ToString();
//            }
//            if (UsedMeth.EndsWith(NewVal))
//            {
//                UsedMeth += NewVal;
//            }
//        }

        
//        private string RightChar(string theString, int len)
//        {

//            if (theString.Length > 1)
//            {
//                return theString.Substring(theString.Length - 1, len);
//            }
//            else if (theString.Length == 1)
//            {
//                return theString;
//            }
//            else
//            {
//                return "";
//            }
//        }
//        private string LeftChar(string theString, int len)
//        {
//            return theString.Substring(0, len);
//        }
         
         


//        //create the log file
//        private void MakeLog(int SM, string RCB, int Position, params string[] Ext)
//        {

//            string Ext1 = "";
//            string Ext2 = "";
//            string Ext3 = "";
//            string Ext4 = "";
//            if (Ext.Length > 0) Ext1 = Ext[0];
//            if (Ext.Length > 1) Ext2 = Ext[1];
//            if (Ext.Length > 2) Ext3 = Ext[2];
//            if (Ext.Length > 3) Ext4 = Ext[3];

//            int Row;
//            int Col;
//            int x;
//            string RowL;
//            string BL;
//            string Num;
//            StringBuilder TXT = new StringBuilder();
//            if (LastMethod != SolveMethod & SolveMethod != 0)
//            {
//                if (LastMethod != 0)
//                    Print_Contents();
//                if (SM != 99)
//                {
//                    TXT.AppendLine("Solving using method " + SolveMethod);
//                }
//                else
//                {
//                    TXT.AppendLine("Solving using guessing");
//                }
//            }
//            Row = (int)((Position) / GWidth); //was: (int)Conversion.Int((Position - 1) / GWidth) + 1;
//            RowL = Convert.ToChar(64 + Row).ToString(); //offset?
//            Col = Position - (Row) * GWidth; //was: Position - (Row - 1) * GWidth;
//            BL = RowL + Col.ToString(); //was: RowL + Strings.Trim(Conversion.Str(Col));
//            Num = Grid[Position];
//            switch (SM)
//            {
//                case 0:
//                    TXT.AppendLine();
//                    if (IsSolved() == false || UsedUseGues == true)
//                    {
//                        TXT.Append("minimum possible solution is ").AppendLine(MinPossibilities.ToString());
//                    }
//                    else
//                    {
//                        TXT.Append("number of possible solutions is ").AppendLine(MinPossibilities.ToString());
//                    }

//                    Print_Contents();
//                    break;
//                case 1:
//                    switch (RCB.ToUpper())
//                    {
//                        case "":
//                            ToGo--;
//                            break;
//                        case "R":
//                            TPoints++;
//                            break;
//                        case "C":
//                            TPoints++;
//                            break;
//                        case "B":
//                            TPoints++;
//                            break;
//                    }
//                    if (HintOn == true && StartGrid[Position].Length == 0)
//                    {
//                        switch (RCB.ToUpper())
//                        {
//                            case "R":
//                            case "C":
//                            case "B":
//                                RC[0] = Row;
//                                RC[1] = Col;
//                                RV = Num;
//                                GotHint = true;
//                                break;
//                        }
//                    }

//                    break;
//                case 2:
//                    TPoints = TPoints + 30;
//                    if (HintOn == true && StartGrid[Position].Length == 0)
//                    {
//                        switch (RCB.ToUpper())
//                        {
//                            case "R":
//                            case "C":
//                            case "B":
//                                RC[0] = Row;
//                                RC[1] = Col;
//                                RV = Ext1;
//                                GotHint = true;
//                                break;
//                        }
//                    }

//                    break;
//                case 3:
//                    switch (RCB.ToUpper())
//                    {
//                        case "":
//                            break;
//                        case "R":
//                            TPoints += 100;
//                            break;
//                        case "C":
//                            TPoints += 100;
//                            break;
//                        case "B":
//                            break;
//                    }
//                    if (HintOn == true && StartGrid[Position].Length == 0)
//                    {
//                        switch (RCB.ToUpper())
//                        {
//                            case "":
//                                RC[0] = Row;
//                                RC[1] = Col;
//                                RV = Num;
//                                GotHint = true;
//                                break;
//                        }
//                    }

//                    break;
//                case 4:
//                    TPoints += 100;
//                    switch (RCB.ToUpper())
//                    {
//                        case "":
//                            break;
//                        case "R":
//                            TPoints += 100;
//                            break;
//                        case "C":
//                            TPoints += 100;
//                            break;
//                        case "B":
//                            break;
//                    }
//                    if (HintOn == true && StartGrid[Position].Length == 0)
//                    {
//                        switch (RCB.ToUpper())
//                        {
//                            case "":
//                                RC[0] = Row;
//                                RC[1] = Col;
//                                RC[2] = int.Parse(Num);
//                                GotHint = true;
//                                break;
//                        }
//                    }

//                    break;
//                case 5:
//                    TPoints = TPoints + 80;
//                    break;
//                case 6:
//                    TPoints = TPoints + 50;
//                    MinPossibilities = MinPossibilities * 2;
//                    break;
//                case 7:
//                    switch (RCB.ToUpper())
//                    {
//                        case "":
//                            break;
//                        case "R":
//                            TPoints += 50;
//                            break;
//                        case "C":
//                            TPoints += 50;
//                            break;
//                        case "B":
//                            TPoints += 50;
//                            break;
//                    }
//                    if (HintOn == true && StartGrid[Position].Length == 0)
//                    {
//                        switch (RCB.ToUpper())
//                        {
//                            case "":
//                                if (Num.Length == 1)
//                                {
//                                    RC[0] = Row;
//                                    RC[1] = Col;
//                                    RC[2] = int.Parse(Num);
//                                    GotHint = true;
//                                }

//                                break;
//                        }
//                    }

//                    break;
//                case 8:
//                    TPoints += 200;
//                    for (x = 2; x <= Ext3.Length; x++)
//                    {
//                    }

//                    if (HintOn == true)
//                    {
//                        RC[0] = Row;
//                        RC[1] = Col;
//                        RC[2] = int.Parse(Ext1);
//                        GotHint = true;
//                    }

//                    break;
//                case 99:
//                    break;
//            }
//            LastMethod = SolveMethod;
//        }


//        //show how the grid is at this moment
//        public void Print_Contents()
//        {
            
//        }

//        private string RepeatString(int numTimes, string charToRepeat)
//        {
//            return charToRepeat.PadLeft(numTimes, charToRepeat[0]);
//        }


        
//        //check if the puzzle is solved
//        private bool IsSolved()
//        {
//            int Z;
//            int x;
//            int y;
//            int[] T = new int[GWidth];

//            Z = 0;
//            for (x = 0; x < GNum; x++)
//            {
//                if (Grid[x].Length > 1)
//                    return false;

//                T[BString.IndexOf(Grid[x])] += 1; //was: T[Strings.InStr(BString, Grid(x))] = T(Strings.InStr(BString, Grid(x))) + 1;
//                if ((x) % GWidth == 0)
//                {
//                    Z = Z + 1;
//                    if (T[0] != 0)
//                        return false;

//                    for (y = 1; y <= GWidth; y++)
//                    {
//                        if (T[y] != Z)
//                            return false;

//                    }
//                }
//            }
//            return true;
//        }
//        private void RemoveNum(int From, int What)
//        {
//            int Pos = Grid[From].IndexOf(Grid[What].ToString());
//            if (Pos == -1) return;
//            Grid[From] = Grid[From].Remove(Pos, 1);

//            if (Grid[From].Length == 1)
//            {
//                ExtSolved[0] += 1;
//                ExtSolved[ExtSolved[0]] = From;
//            }

//            /*  //was:
//            Pos = Strings.InStr(Grid(From), Grid(What));
//            if (Pos == 0)
//                return; // TODO: might not be correct. Was : Exit Sub

//            Grid(From) = LeftChar(Grid(From), Pos - 1) + Strings.Mid(Grid(From), Pos + 1);
//            if (Strings.Len(Grid(From)) == 1)
//            {
//                ExtSolved(0) = ExtSolved(0) + 1;
//                ExtSolved(ExtSolved(0)) = From;
//            }
//            */
//        }
//        /*
//        //restore certain variables while guessing
//        private void CopyFromBackup()
//        {
//            int x;
//            for (x = 1; x <= GNum; x++)
//            {
//                Grid(x) = BackGrid(x);
//            }
//            TPoints = BuTPoints;
//            ToGo = BuToGo;
//            MinPossibilities = BUMinPos;
//        }
//        //make a backup of certain variables while guessing
//        private void CopyToBackup()
//        {
//            int x;
//            for (x = 1; x <= GNum; x++)
//            {
//                BackGrid(x) = Grid(x);
//            }
//            BuTPoints = TPoints;
//            BuToGo = ToGo;
//            BUMinPos = MinPossibilities;
//        }




//        //public bool prepHint()
//        //{
//        //    usedHint = true;
//        //    if (boardContainsErrors() == true)
//        //    {
//        //        Interaction.MsgBox(myLang.cantHint, MsgBoxStyle.OkOnly);
//        //        return false;
//        //    }
//        //    int x;
//        //    int y;
//        //    int linearX;
//        //    string RetVal;
//        //    Init_Solver();
//        //    for (x = 0; x <= 8; x++)
//        //    {
//        //        for (y = 0; y <= 8; y++)
//        //        {
//        //            linearX = (y * 9) + x + 1;
//        //            if (Strings.InStr(BString, (string)myGrid.Board(x, y)) > 0)
//        //            {
//        //                DefGrid(linearX, (string)myGrid.Board(x, y));
//        //            }
//        //        }
//        //    }
//        //    RetVal = Get_Hint();

//        //    if (RetVal != "")
//        //    {
//        //        hintX = (int)Conversion.Int(RetVal.Substring(0, 1));
//        //        hintY = (int)Conversion.Int(RetVal.Substring(1, 1));
//        //        return true;
//        //    }
//        //    else
//        //    {
//        //        return false;
//        //    }
//        //}
//        private bool boardContainsErrors()
//        {
//            int x;
//            int y;
//            for (x = 0; x <= 8; x++)
//            {
//                for (y = 0; y <= 8; y++)
//                {
//                    if (myGrid.Board(x, y) != myGrid.SolvedBoard(x, y) & myGrid.Board(x, y) > 0 & myGrid.SolvedBoard(x, y) > 0 & myGrid.Board(x, y) < 10)
//                    {
//                        Interaction.MsgBox(x + " " + y + ", is: " + myGrid.Board(x, y) + " should be: " + myGrid.SolvedBoard(x, y));
//                        return true;
//                    }
//                }
//            }
//            return false;
//        }

//        //try to get a hint from the program
//        public string Get_Hint()
//        {
//            string functionReturnValue = null;
//            bool BuUseGuess;
//            HintOn = true;
//            GotHint = false;
//            BuUseGuess = UseGuessing;
//            UseGuessing = false;
//            SolveSudoku();
//            if (GotHint == false)
//            {
//                Interaction.MsgBox(myLang.noHintsAvailable);
//                return "";
//            }
//            else
//            {
//                functionReturnValue = RC(1) - 1 + RC(0) - 1 + RV;
//            }
//            UseGuessing = BuUseGuess;
//            MakeLog(0, "", 1, "1");
//            //to reset is for solving
//            HintOn = false;
//            GotHint = false;
//            return functionReturnValue;
//        }


//        public void hintBlink()
//        {
//            int hintVal;
//            if (mySettings.nonSpoilerHints == true)
//            {
//                hintVal = 10;
//            }
//            else
//            {
//                hintVal = myGrid.SolvedBoard(hintX, hintY);
//            }

//            Grid tempGrid = new Grid(myGrid);
//            if (tempGrid.Board(hintX, hintY) == 0)
//            {
//                tempGrid.Board(hintX, hintY) = hintVal;
//                addToGridHistory(tempGrid);
//            }
//            else
//            {
//                gridHistory.RemoveAt(gridHistory.Count - 1);
//            }

//            blinkCount = blinkCount - 1;
//            redraw();
//        }

//        public void finalHintBlink()
//        {
//            finalizeTimers = false;
//            Grid tempGrid = new Grid(myGrid);
//            if (mySettings.nonSpoilerHints == true)
//            {
//                tempGrid.Board(hintX, hintY) = 0;
//            }
//            else
//            {
//                tempGrid.Board(hintX, hintY) = tempGrid.SolvedBoard(hintX, hintY);
//            }
//            if (myGrid.Board(hintX, hintY) != tempGrid.Board(hintX, hintY))
//                addToGridHistory(tempGrid);
//            redraw();
//        }


//        public void revealMistakes(bool removeFromGrid)
//        {
//            removeMistakesFromGrid = removeFromGrid;
//            if (!myGrid.boardIsSolved)
//                solveGrid();
//            int x;
//            int y;
//            for (x = 0; x <= 8; x++)
//            {
//                for (y = 0; y <= 8; y++)
//                {
//                    BoardPosIsMistake(x, y) = false;
//                    TempBoard(x, y) = myGrid.Board(x, y);
//                    if (myGrid.SolvedBoard(x, y) == 0 & myGrid.Board(x, y) != 0)
//                    {
//                        Interaction.MsgBox(myLang.cantRevealMistakes);
//                        return; // TODO: might not be correct. Was : Exit Sub
//                    }
//                    else
//                    {
//                        if (myGrid.Board(x, y) != myGrid.SolvedBoard(x, y) & myGrid.Board(x, y) != 0)
//                        {
//                            BoardPosIsMistake(x, y) = true;
//                            usedHint = true;
//                        }
//                    }
//                }
//            }

//            blinkCount = 10;
//            revealMistakesTimer.Enabled = true;
//        }

//        private void revealMistakesTimer_Tick(object sender, System.EventArgs e)
//        {
//            int x;
//            int y;

//            for (x = 0; x <= 8; x++)
//            {
//                for (y = 0; y <= 8; y++)
//                {
//                    if (BoardPosIsMistake(x, y))
//                    {
//                        if (myGrid.Board(x, y) == 0)
//                        {
//                            myGrid.Board(x, y) = TempBoard(x, y);
//                        }
//                        else
//                        {
//                            myGrid.Board(x, y) = 0;
//                        }
//                    }
//                }
//            }

//            if (blinkCount == 0 | finalizeTimers == true)
//            {
//                finalizeTimers = false;
//                for (x = 0; x <= 8; x++)
//                {
//                    for (y = 0; y <= 8; y++)
//                    {
//                        if (removeMistakesFromGrid)
//                        {
//                            if (BoardPosIsMistake(x, y))
//                                myGrid.Board(x, y) = 0;
//                        }
//                        else
//                        {
//                            if (BoardPosIsMistake(x, y))
//                                myGrid.Board(x, y) = TempBoard(x, y);
//                        }

//                    }
//                }
//                revealMistakesTimer.Enabled = false;
//            }

//            blinkCount = blinkCount - 1;
//            redraw();
//        }

//        */


//        #endregion
        
//    }
//}
