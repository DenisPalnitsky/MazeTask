using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze
{
    public struct Cell
    {
        public readonly int X;
        public readonly int Y;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return String.Format("Cell ({0}, {1})", X, Y);
        }
    }
}
