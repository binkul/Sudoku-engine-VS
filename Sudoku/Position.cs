using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Sudoku_engine.Sudoku
{
    public class Position : IComparable<Position>, IEqualityComparer<Position>
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int x, int y)
        {
            Row = x;
            Column = y;
        }

        public override string ToString()
        {
            return "Position : [Row=" + Row + "; Column=" + Column + "]";
        }

        public int CompareTo(Position other)
        {
            if (Row == other.Row)
                return this.Column.CompareTo(other.Column);
            else
                return this.Row.CompareTo(other.Row);
        }

        public bool Equals(Position x, Position y)
        {
            return x.Row == y.Row && x.Column == y.Column;
        }

        public int GetHashCode(Position obj)
        {
            return HashCode.Combine(obj.Row, obj.Column);
        }
    }
}
