using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze
{
    public class GridNavigator
    {     
        static readonly Dictionary<Direction, int> xShift = new Dictionary<Direction, int> 
                    {   { Direction.Up, 0 }, 
                        { Direction.Down, 0 }, 
                        { Direction.Left, -1 }, 
                        { Direction.Right, 1 } };


        static readonly Dictionary<Direction, int> yShift = new Dictionary<Direction, int> 
                    {   { Direction.Up, -1 }, 
                        { Direction.Down, 1 }, 
                        { Direction.Left, 0 }, 
                        { Direction.Right, 0 } };

        Cell _curentPosition;


        public Cell CurrentPosition { get { return _curentPosition; } }
                

        readonly int _width;
        readonly int _height;

        public GridNavigator(int width, int height)
        {
            _curentPosition = new Cell(0, 0);

            _width = width;
            _height = height;
        }

        
        public void SetCurrentPosition( int x, int y)
        {
            if (!IsCellInBounds(x, y))
                throw new ArgumentOutOfRangeException("Cell is outside of grid bounds");

            _curentPosition = new Cell(x, y);
        }

        public bool IsCellInBounds(int x, int y)
        {
            if (x >= 0 && x < _width && y >= 0 && y < _height)            
                return true;

            return false;
        }

        public Dictionary<Direction, Cell> Neighbours 
        { 
            get 
            {
                Dictionary<Direction, Cell> result = new Dictionary<Direction,Cell>();
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    var neighbour = GetNeighbour(1, direction);
                    if (neighbour.HasValue)
                        result.Add(direction, neighbour.Value);
                }
                
                return result;
            }         
        }        

        /// <summary>
        /// Gets cell that is in distance of <paramref name="numberOfSteps"/> cells in certain directions
        /// To get closest neighbour set <paramref name="numberOfSteps"/> to 1
        /// </summary>
        /// <param name="numberOfSteps"></param>
        /// <param name="direction"></param>
        /// <returns>Cell, or null if out of bounds</returns>
        public Cell? GetNeighbour(int numberOfSteps, Direction direction)
        {
            var nextX = _curentPosition.X + xShift[direction]*numberOfSteps;
            var nextY = _curentPosition.Y + yShift[direction]*numberOfSteps;

            if (IsCellInBounds(nextX, nextY))
                return new Cell(nextX, nextY);

            return null;
        }
       
    }
}
