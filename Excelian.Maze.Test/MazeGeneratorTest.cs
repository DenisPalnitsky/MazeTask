using Excelian.Maze.Generator;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Excelian.Maze.Test
{
    [TestFixture]
    public class MazeGeneratorTest
    {
        public class RandomizerMock : IRandomizer
        {
            public T[] Shuffle<T>(IEnumerable<T> list)
            {
                return list.ToArray();
            }

            public int GetRandomX(int max)
            {
                return 1;
            }

            public int GetRandomY(int max)
            {
                return 0; 
            }
        }

        [Test]
        public void Generate_when_called_generates_maze()
        {
            // Arrange
            MazeGenerator generator = new MazeGenerator();
            
            // Act         
            Maze maze = generator.Generate(3, 3, new RandomizerMock());

            System.Diagnostics.Debug.Write(maze.Render());

            // Assert 
            Assert.AreEqual(9, maze.AsEnumerable().Count() );
            Assert.IsTrue(maze.AsEnumerable().Contains(MazeCellType.Start));
            Assert.IsTrue(maze.AsEnumerable().Contains(MazeCellType.Finish));            
        }

        [Test]
        public void GenerateBigMaze()
        {
            // Arrange
            MazeGenerator generator = new MazeGenerator();       

            System.Diagnostics.Debug.WriteLine("Final 10X10:");
            System.Diagnostics.Debug.Write(generator.Generate(10, 10, new RandomizerMock()).Render());

            //System.Diagnostics.Debug.Write(generator.Generate(3, 3).Render());            

        }

        [Test]
        public void GeenrateMinimalSufficientMaze ()
        {
            // Arrange
            MazeGenerator generator = new MazeGenerator();

            // Act                                                
            System.Diagnostics.Debug.WriteLine("Final:");
            System.Diagnostics.Debug.Write(generator.Generate(3, 3, new RandomizerMock()).Render());            
        }

    }
}
