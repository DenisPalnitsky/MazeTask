using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze.Test
{
    class TestData
    {
        const string TestDataPath  = "TestData";

        public static string GetPathToFile(string filename)
        {
            return Path.Combine(TestDataPath, filename);
        }
    }
}
