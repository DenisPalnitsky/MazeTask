using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze.Test
{
    [TestFixture]
    public class ExplorerTest
    {                 

        public static readonly Maze MinimalMaze =  Maze.Create(
            new MazeCellType[,] {            
            {  MazeCellType.Wall, MazeCellType.Start, MazeCellType.Wall },        // XXX                                  
            {  MazeCellType.Wall, MazeCellType.Space , MazeCellType.Wall},        // S.F
            {  MazeCellType.Wall, MazeCellType.Finish , MazeCellType.Wall}});     // XXX


        [Test]
        public void Move_when_called_sets_new_current_position()
        {
            var explore = new Explorer(MinimalMaze);

            // turn in proper direction 
            while (!explore.ObjectInFront.HasValue || explore.ObjectInFront.Value != MazeCellType.Space)
                explore.TurnRight();
            
            explore.Move();

            Assert.AreEqual(new Cell(1,1) , explore.CurerentPosition);
        }


        [Test]
        public void ExploreMaze_when_called_sets_current_position_to_start()
        {
            var explore = new Explorer(MinimalMaze);
            
            Assert.AreEqual(new Cell(0, 1) , explore.CurerentPosition);
            Assert.IsTrue(explore.IsStart);
        }

        [Test]
        public void Move_throws_exception_for_illegal_move()
        {
            var explorer = new Explorer(MinimalMaze);            

            explorer.TurnLeft();
            
            Assert.Throws<MazeExploringException>(()=> explorer.Move());
        }

       
        [Test]
        public void ObjectInFront_returns_type_of_objects_around ()
        {
            var explorer = new Explorer(MinimalMaze);

            Assert.AreEqual(MazeCellType.Space, explorer.ObjectInFront);

            explorer.TurnLeft();
            Assert.AreEqual(MazeCellType.Wall, explorer.ObjectInFront);

            explorer.TurnLeft();
            Assert.IsFalse( explorer.ObjectInFront.HasValue);

            explorer.TurnLeft();
            Assert.AreEqual(MazeCellType.Wall, explorer.ObjectInFront);

        }

        [Test]
        public void AvailableDirections_returns_what_expected()
        {
            var explorer = new Explorer(MinimalMaze);

            Assert.AreEqual(1, explorer.AvailableMovementOptions.Count());
            Assert.AreEqual(MoveDirection.Forward, explorer.AvailableMovementOptions.First());
        }

        [Test]
        public void AvailableDirections_returns_two_available_options()
        {
            var explorer = new Explorer(MinimalMaze);
            explorer.Move();

            Assert.That(explorer.AvailableMovementOptions.Contains(MoveDirection.Forward) &&
                explorer.AvailableMovementOptions.Contains(MoveDirection.Backward));            
        }



        [Test]
        public void Movement_returns_passed_route()
        {
            var explorer = new Explorer(MinimalMaze);
            explorer.Move();
            explorer.Move();

            var route = explorer.Movement.ToArray();

            Assert.AreEqual(new Cell(0, 1), route[0]);
            Assert.AreEqual(new Cell(1, 1), route[1]);
            Assert.AreEqual(new Cell(2, 1), route[2]);
        }

        [Test]
        public void Turn_when_called_turns_explorer()
        {
            var explorer = new Explorer(MinimalMaze);

            Assert.AreEqual(Direction.Right, explorer.CurrentDirection, "Check default direction");
            
            explorer.TurnRight();
            Assert.AreEqual(Direction.Down, explorer.CurrentDirection);

            explorer.TurnLeft();
            Assert.AreEqual(Direction.Right, explorer.CurrentDirection);

            explorer.TurnAround();
            Assert.AreEqual(Direction.Left, explorer.CurrentDirection);            
        }

        [Test]
        public void GetAvailableOptions_does_not_change_position() {

            var explorer = new Explorer(MinimalMaze);
            var direction = explorer.CurrentDirection;

            var options = explorer.AvailableMovementOptions.ToArray();

            Assert.AreEqual(direction, explorer.CurrentDirection);


        }


    }
}
