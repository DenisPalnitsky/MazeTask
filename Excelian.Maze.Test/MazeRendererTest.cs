using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze.Test
{
    [TestFixture]
    class MazeRendererTest
    {
        [Test]
        public void Render_when_called_for_4X4_maze()
        {
            // Arrange
            Maze maze = MazeTest.MinimalMaze;

            StringBuilder expected = new StringBuilder();
            expected.AppendLine("XX");
            expected.AppendLine("SF");
            expected.AppendLine("XX");
            

            // Act
            var result = maze.Render();
            
            //Assert 
            Assert.AreEqual(expected.ToString(), result);            
        }

        // TODO: More tests to cover corner cases 
    }
}
