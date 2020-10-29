using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Sudoku_engine.AppData
{
    public static class Data
    {
        public const int MinValue = 1;
        public const int MaxValue = 9;
        public const int Section = 3;
        public const int Empty = 0;

        public static Color SingleColor = Color.Red;
        public static Color NormalColor = Color.Black;
        public static Color UniqueColor = Color.Blue;
        public static Color BackTrackColor = Color.Green;

    }
}
