using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace SuperDoku
{
    /// <summary>Represents the position of the SelectionBar in relation to the Board.</summary>
    public enum SelectionBarPositions { Top = 1, Bottom, Left, Right }

    public static class Settings
    {        
        static Settings()
        {
            
        }

        /// <summary>Settings that change throughout the game (for example, when the user switches PencilMark modes)</summary>
        public static class Current
        {
            /// <summary>If true, tapping the Board toggles a PencilMark instead of changing the Square's value.</summary>
            public static bool PencilMarkMode { get; set; }
            
            static Current()
            {
                PencilMarkMode = false;
            }
        }
        /// <summary>Settings pertaining to the graphical Skin used in the game. Change the Skin via SetSkin().</summary>
        public static class Skin
        {
            /// <summary>Gets the filename of the Skin. Change via that SetSkin method.</summary>
            public static string FileName { get; private set; }
            /// <summary>Gets the Skin bitmap</summary>
            static public Bitmap Bitmap { get; private set; }
            /// <summary> Gets the pixel length of the Squares in the skin</summary>
            static public int SquareLength { get; private set; }
            /// <summary> Gets the pixel length of the short edge of a PencilMark in the skin</summary>
            static public int PencilMarkShortLength { get; private set; }
            /// <summary> Gets the pixel length of the long edge of a PencilMark in the skin</summary>
            static public int PencilMarkLongLength { get; private set; }
            /// <summary> Gets the pixel length of an unscaled Board created from the Squares in the skin. Because some squares overlap, this is slightly less than SkinSquareLength * 9.</summary>
            static public int BoardLength { get; private set; }
            /// <summary>Gets the number of pixels between each Square in the skin</summary>
            static public int GutterLength { get; private set; }
            /// <summary>Gets the number of pixels of the border on each Square in the skin</summary>
            static public int OverlapLength { get; private set; }
            /// <summary>Gets the Rectangle containing the Background in the skin</summary>
            static public Rectangle BackgroundRect { get; private set; }
            /// <summary>A list of Board lengths (i.e. widths/heights) that cause little distortion when resized from the BoardLength, i.e. half, 2x, etc.</summary>
            static public List<int> IdealBoardLengths { get; private set; }

            static Skin()
            {
                FileName = "LightEmUp(FKB)VGA.png";//"OS X.2VGA.PNG";

                //mSkin = new Bitmap(@"D:\My Documents\Visual Studio 2005\Projects\SuperDoku\SuperDoku\Skin Files\VGA\OS X.2VGA.PNG");
                //mSkin = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("OS X.2VGA.PNG"));
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                Stream myStream = myAssembly.GetManifestResourceStream("SuperDoku.Resources.Images." + FileName);
                Bitmap = new Bitmap(myStream);

                SquareLength = 52;
                PencilMarkLongLength = 18;
                PencilMarkShortLength = 16;
                BoardLength = 456;
                GutterLength = 2;
                OverlapLength = 2;
                BackgroundRect = new Rectangle(596, 110, 214, 214);
                IdealBoardLengths = new List<int>();
                IdealBoardLengths.Add(BoardLength);
                IdealBoardLengths.Add((int)(BoardLength / 2));
                IdealBoardLengths.Add(BoardLength * 2);
            }

            public static void SetSkin(string fileName, Bitmap bitmap)
            {
                FileName = fileName;
                Bitmap = bitmap;
            }
        }
        public static class Layout
        {
            /// <summary>If true, SuperDoku will choose the best layout for the screen.</summary>
            public static bool ChooseBestSettings { get; set; }
            /// <summary>The position of the SelectionBar relative to the Board. This property is ignored if ChooseBestLayoutSettings is set. In either case, the Field's SelectionBarPosition property reflects the current SelectionBar's position.</summary>
            public static SelectionBarPositions SelectionBarPosition { get; set; }
            /// <summary>The minimum margin used to space the Board and SelectionBar apart from each other and from the sides of the Field, expressed as a percentage of the length of a Square.</summary>
            public static double MarginPercent { get; set; }

            static Layout()
            {
                ChooseBestSettings = true;
                SelectionBarPosition = SelectionBarPositions.Bottom;
                MarginPercent = .173;
            }
        }
        public static class Navigation
        {
            /// <summary>If true, a Selection Rectangle on the Board determines where the Selected Number will be placed when the user presses the Submit key. The Selection Rectangle can be controlled by the Navigation Keys. This allows for tap-free operation, which is necessary for Smartphones.</summary>
            public static bool SmartphoneMode { get; set; }

            public static InputSetting BoardSelectionUp { get; set; }
            public static InputSetting BoardSelectionDown { get; set; }
            public static InputSetting BoardSelectionLeft { get; set; }
            public static InputSetting BoardSelectionRight { get; set; }
            /// <summary>Only applies when the SelectionSquare is Vertical.</summary>
            public static InputSetting SelectionBarUp { get; set; }
            /// <summary>Only applies when the SelectionSquare is Vertical.</summary>
            public static InputSetting SelectionBarDown { get; set; }
            /// <summary>Only applies when the SelectionSquare is Horizontal.</summary>
            public static InputSetting SelectionBarLeft { get; set; }
            /// <summary>Only applies when the SelectionSquare is Horizontal.</summary>
            public static InputSetting SelectionBarRight { get; set; }
            public static InputSetting Enter { get; set; }
         
            static Navigation()
            {
                SmartphoneMode = true;
                BoardSelectionUp = new InputSetting(System.Windows.Forms.Keys.Up);
                BoardSelectionDown = new InputSetting(System.Windows.Forms.Keys.Down);
                BoardSelectionLeft = new InputSetting(System.Windows.Forms.Keys.Left);
                BoardSelectionRight = new InputSetting(System.Windows.Forms.Keys.Right);
                SelectionBarUp = new InputSetting(System.Windows.Forms.Keys.W);
                SelectionBarDown = new InputSetting(System.Windows.Forms.Keys.S);
                SelectionBarLeft = new InputSetting(System.Windows.Forms.Keys.A);
                SelectionBarRight = new InputSetting(System.Windows.Forms.Keys.D);
                Enter = new InputSetting(System.Windows.Forms.Keys.Space);
            }

            public class InputSetting
            {
                public System.Windows.Forms.Keys Key { get; set; }
                public InputSetting(System.Windows.Forms.Keys key) { Key = key; }
                public bool Equals(InputSetting obj)
                {
                    return Key.Equals(obj.Key);
                }
            }
        }

        



        public static class Style
        {
            /// <summary>This style is used for all Squares whose Value matches that of the SelectionBar's SelectedSquare.</summary>
            public static StyleSetting Selected { get; set; }
            /// <summary>This style is used for all PencilMarks whose Value matches that of the SelectionBar's SelectedSquare.</summary>
            public static StyleSetting SelectedPencilMark { get; set; }
            /// <summary>This style is used for all Squares of which there are exactly 9 on the Board, whether they are in the right places or not.</summary>
            public static StyleSetting Completed { get; set; }
            /// <summary>This style is used for the Squares on the SelectionBar of which there are exactly 9 on the Board, whether they are in the right places or not.</summary>
            public static StyleSetting CompletedOnSelectionBar { get; set; }
            /// <summary>This style is used for all Squares in a complted Row or Column, whether or not the values are correct.</summary>
            public static StyleSetting CompletedRowOrColumn { get; set; }
            /// <summary>This style is used for all Squares in a completed Block, whether or not the values are correct.</summary>
            public static StyleSetting CompletedBlock { get; set; }
            /// <summary>This style is used for all permanent Squares that were part of the original game.</summary>
            public static StyleSetting Given { get; set; }

            /// <summary>This style is used for all Squares that don't fit any other category.</summary>
            public static Styles EverythingElse { get; set; }
            /// <summary>This style is used for all PencilMarks that don't fit any other category.</summary>
            public static Styles EverythingElsePencilMarks { get; set; }
            /// <summary>This style is used for all Squares on the SelectionBar that don't fit any other category.</summary>
            public static Styles EverythingElseOnSelectionBar { get; set; }

            /// <summary>Specifies whether or not multiple taps on the SelectionBar activates / deactivates the Selected style settings.</summary>
            public static bool ToggleSelectedStyleOnRepeatedTaps { get; set; }

            static Style()
            {
                Selected = new StyleSetting(Styles.Bold, true);
                SelectedPencilMark = new StyleSetting(Styles.Bold, true);
                Completed = new StyleSetting(Styles.Dim, true);
                CompletedOnSelectionBar = new StyleSetting(Styles.Dim, true);
                CompletedRowOrColumn = new StyleSetting(Styles.Dim, false);
                CompletedBlock = new StyleSetting(Styles.Dim, false);
                Given = new StyleSetting(Styles.Regular, false);

                EverythingElse = Styles.Regular;
                EverythingElseOnSelectionBar = Styles.Regular;

                ToggleSelectedStyleOnRepeatedTaps = true;
            }

            /// <summary>Represents the Style of visual elements (Squares or PencilMarks). If not Active, the Style will be ignored.</summary>
            public class StyleSetting
            {
                /// <summary>The style of this StyleSetting.</summary>
                public Styles Style { get; set; }
                /// <summary>Gets or sets a value indicating whether or not to apply this StyleSetting to the elements on the screen.</summary>
                public bool Active { get; set; }

                public StyleSetting(Styles style, bool active)
                {
                    Style = style;
                    Active = active;
                }
            }

        }
    }
}
