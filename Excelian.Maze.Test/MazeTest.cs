using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze.Test
{
    [TestFixture]
    public class MazeTest
    {
        public static readonly Maze MinimalMaze =  Maze.Create(
            new MazeCellType[,] {
            {MazeCellType.Wall, MazeCellType.Start , MazeCellType.Wall},  // row 0
            {MazeCellType.Wall, MazeCellType.Finish , MazeCellType.Wall}}); // row 1
        

        [Test]
        public void Width_returns_maze_width()  
        {
            Maze maze = MinimalMaze;
            Assert.AreEqual(3, maze.Height);
        }

        [Test]
        public void Height_returns_maze_weight()
        {
            Maze maze = MinimalMaze;
            Assert.AreEqual(2, maze.Width);
        }
        
              
    }
}
