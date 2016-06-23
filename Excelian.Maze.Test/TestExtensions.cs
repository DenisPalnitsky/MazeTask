using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze.Test
{
    public static class TestExtensions
    {
        // can't find the reason to move this code to Maze class 
        public static bool AreMazesEqual(this Maze a, Maze b)
        {
            if (a.Height == b.Height && a.Width == b.Width)
            {
                return a.AsEnumerable().SequenceEqual(b.AsEnumerable());
            }

            return false;
        }


        public static IEnumerable<MazeCellType> AsEnumerable( this Maze maze)
        {
            for (int x=0;x<maze.Width;x++)
            {
                for (int y=0;y<maze.Width;y++)
                {
                    yield return maze[x, y].CellType;
                }
            }
        }

    }
}
