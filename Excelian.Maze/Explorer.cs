using System;
using System.Collections.Generic;
namespace Excelian.Maze
{
    public enum MoveDirection 
    {
        Forward, 
        Rigth, 
        Backward, 
        Left 
    }

    public class Explorer
    {
        private readonly List<Cell> _movement = new List<Cell>();
        private readonly GridNavigator _navigator;        
        private readonly Maze _maze;
        private int _currentDirectionIndex = 1; // Right

        private static Direction[] ClockwiseOrderedDirections = 
            new[] { Direction.Up, Direction.Right, Direction.Down, Direction.Left };
                      
        public bool IsStart { get { return _maze[_navigator.CurrentPosition].CellType == MazeCellType.Start; } }
        
        public bool IsFinish { get { return _maze[_navigator.CurrentPosition].CellType == MazeCellType.Finish; } }        

        public Cell CurerentPosition { get { return _navigator.CurrentPosition; } }

        public IEnumerable<Cell> Movement { get { return _movement; } }

        /// <summary>
        /// Cell type or null in case if you are in front of border
        /// </summary>
        /// <returns></returns>
        public MazeCellType? ObjectInFront
        {
            get
            {
                if (_navigator.Neighbours.ContainsKey(CurrentDirection))
                    return _maze[_navigator.Neighbours[CurrentDirection]].CellType;

                return null;
            }
        }


        public IEnumerable<MoveDirection> AvailableMovementOptions
        {
            get
            {
                // there is better way but it requires more time to implement

                List<MoveDirection> directions = new List<MoveDirection>();

                // Let's look around
                if (ObjectInFront.HasValue && ObjectInFront.Value != MazeCellType.Wall)
                    directions.Add(MoveDirection.Forward);

                this.TurnRight();
                if (ObjectInFront.HasValue && ObjectInFront.Value != MazeCellType.Wall)
                    directions.Add(MoveDirection.Rigth);

                this.TurnRight();
                if (ObjectInFront.HasValue && ObjectInFront.Value != MazeCellType.Wall)
                    directions.Add( MoveDirection.Backward);

                this.TurnRight();
                if (ObjectInFront.HasValue && ObjectInFront.Value != MazeCellType.Wall)
                    directions.Add( MoveDirection.Left);

                // and back to initial position
                this.TurnRight();

                return directions;
                                
            }
        }

        public Direction CurrentDirection
        {
            get { return ClockwiseOrderedDirections[_currentDirectionIndex]; }            
        }
      
        public Explorer(Maze maze)
        {
            _maze = maze;
            _movement = new List<Cell>();
            _navigator = new GridNavigator(_maze.Width, _maze.Height);
            _navigator.SetCurrentPosition(_maze.Start.Coordinates.X, _maze.Start.Coordinates.Y);
            _movement.Add(_navigator.CurrentPosition);
        }        

        public void Move()
        {
            Move(ClockwiseOrderedDirections[_currentDirectionIndex]);
        }

        public void TurnLeft()
        {
            // move to next direction counterclockwise
             _currentDirectionIndex = _currentDirectionIndex - 1;

            if (_currentDirectionIndex < 0 )
                _currentDirectionIndex = ClockwiseOrderedDirections.Length-1;                        
        }

        public void TurnRight()
        {
            // move to next direction clockwise
             _currentDirectionIndex = _currentDirectionIndex + 1;

            if (_currentDirectionIndex >= ClockwiseOrderedDirections.Length )
                _currentDirectionIndex = 0;                       
        }

        public void TurnAround()
        {
            TurnRight();
            TurnRight();
        }

        private void Move(Direction direction)
        {
            var cell = _navigator.GetNeighbour(1, direction);
            if (cell == null)            
                throw new MazeExploringException("You are trying to move out of maze's bounds");

            if (_maze[cell.Value].CellType == MazeCellType.Wall)
                throw new MazeExploringException("You walked into a wall");

            _navigator.SetCurrentPosition(cell.Value.X, cell.Value.Y);
            _movement.Add(_navigator.CurrentPosition);
        }


    }
}
