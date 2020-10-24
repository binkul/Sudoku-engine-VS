using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Sudoku_engine.Element
{
    class Position : IComparable<Position>
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString()
        {
            return "Position : [X=" + X + "; Y=" + Y + "]";
        }

        public int CompareTo(Position other)
        {
            if (X == other.X)
                return this.Y.CompareTo(other.Y);
            else
                return this.X.CompareTo(other.X);
        }
    }
}
