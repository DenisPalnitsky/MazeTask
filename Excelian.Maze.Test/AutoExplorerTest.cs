using Excelian.Maze.Generator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze.Test
{
    [TestFixture]
    public class AutoExplorerTest
    {
        public static readonly Maze MinimalMaze = Maze.Create(
          new MazeCellType[,] {            
            {  MazeCellType.Wall, MazeCellType.Start, MazeCellType.Wall },        // XXX                                  
            {  MazeCellType.Wall, MazeCellType.Space , MazeCellType.Wall},        // S.F
            {  MazeCellType.Wall, MazeCellType.Finish , MazeCellType.Wall}});     // XXX


        [Test]
        public void AutoExplore_when_called_for_minimalistic_maze_returns_route()
        {
            var explorer = new Explorer(MinimalMaze);

            var route = AutoExplorer.Explore(explorer).ToArray();

            Assert.AreEqual(new Cell(0, 1), route[0]);
            Assert.AreEqual(new Cell(1, 1), route[1]);
            Assert.AreEqual(new Cell(2, 1), route[2]);
        }

                        
        [Test(Description="Functional test just to let as know that something is broken")]        
        public void AutoExplore_when_called_for_randomly_generated_maze_finds_finish()
        {
            for (int i = 0; i < 10; i++)
            {
                MazeGenerator generator = new MazeGenerator();
                var maze = generator.Generate(10, 10, new Excelian.Maze.Generator.Randomizer());

                var explorer = new Explorer(maze);

                var route = AutoExplorer.Explore(explorer).ToArray();
                Assert.IsTrue(explorer.IsFinish);
            }
        }
        
        [Test]
        public void AutoExplore_when_called_for_big_maze_finds_finish()
        {
            MazeGenerator generator = new MazeGenerator();
            var maze = generator.Generate(10, 10, new MazeGeneratorTest.RandomizerMock());

            var explorer = new Explorer(maze);

            var route = AutoExplorer.Explore(explorer).ToArray();

            Assert.IsTrue(explorer.IsFinish);
        }


        [Test]
        public void AutoExplore_when_called_for_minimalistic_maze2_returns_route()
        {
            var maze = Maze.Create(
         new MazeCellType[,] {            
            {  MazeCellType.Wall, MazeCellType.Finish, MazeCellType.Wall },      // XXX                                  
            {  MazeCellType.Wall, MazeCellType.Space , MazeCellType.Wall},       // F.S
            {  MazeCellType.Wall, MazeCellType.Start , MazeCellType.Wall}});     // XXX

            var explorer = new Explorer(maze);

            var route = AutoExplorer.Explore(explorer).ToArray();

            Assert.IsTrue(explorer.IsFinish);
            Assert.AreEqual(new Cell(2, 1), route[0]);
            Assert.AreEqual(new Cell(1, 1), route[1]);
            Assert.AreEqual(new Cell(0, 1), route[2]);                        
        }


        [Test(Description="There was an issue in algorithm")]
        public void AutoExplore_when_called_for_maze_with_vertical_coridor()
        {
            var maze = Maze.Create(

            // ...
            // XSX
            // XFX
             new MazeCellType[,] {            
                {  MazeCellType.Space, MazeCellType.Wall,  MazeCellType.Wall },                                       
                {  MazeCellType.Space,MazeCellType.Start,  MazeCellType.Finish},    
                {  MazeCellType.Space, MazeCellType.Wall,  MazeCellType.Wall}});    
                      
            var explorer = new Explorer(maze);

            var route = AutoExplorer.Explore(explorer).ToArray();

            Assert.IsTrue(explorer.IsFinish);
        }        
    }
}
