using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze.Generator
{
    public class Randomizer : IRandomizer
    {
        readonly Random _random = new Random();

        public T[] Shuffle<T>(IEnumerable<T> list)
        {
            // shuffle 
            return list.OrderBy(r => _random.Next()).ToArray();            
        }


        public int GetRandomX(int max)
        {
            return _random.Next(max);
        }

        public int GetRandomY(int max)
        {
            return _random.Next(max);
        }
    }
}
