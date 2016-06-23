using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Excelian.Maze.Generator
{
    public interface IRandomizer
    {
        T[] Shuffle<T> (IEnumerable<T> list);

        int GetRandomX(int max);

        int GetRandomY(int max);        
    }
}
