using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze.Generator
{
    // RecursiveBacktracking
    public class MazeGenerator
    {       
       MazeCellType [,] _cells;
       IRandomizer _randomizer;
       bool isFinishFound = false;
              

        public Maze Generate(int width, int height, IRandomizer randomizer)
        {
            if (width < 3 || height < 3)
                throw new ArgumentException("Minimal size of generated maze is 3x3");

            _randomizer = randomizer;
                                
            _cells = new MazeCellType[width, height];
            
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                    _cells[x, y] = MazeCellType.Wall;
            }

            int startX = _randomizer.GetRandomX(width - 2)+1;
            int startY = _randomizer.GetRandomY(height - 2)+1;

            _cells[startX, startY] = MazeCellType.Start;
                        
            CarvePassages(startX, startY);

            return Maze.Create(_cells);
        }

        // We need local copy of GridNavigator for each call of recursive func        
        private GridNavigator GetGridNavigator(int x, int y)
        {
            var result = new GridNavigator(_cells.GetLength(0), _cells.GetLength(1));
            result.SetCurrentPosition(x, y);
            return result;
        }

        public void CarvePassages(int currentX, int currentY)
        {            
            Direction[] directions = _randomizer.Shuffle(
                    new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right });

            GridNavigator gridNavigator = GetGridNavigator(currentX, currentY);
                       
            foreach (var direction in directions)
            {                
                var nextCell = gridNavigator.GetNeighbour(2, direction);

                if (!nextCell.HasValue || isOnEdge(nextCell.Value) )
                    continue;

                if (_cells[nextCell.Value.X, nextCell.Value.Y] != MazeCellType.Wall) 
                    continue;
                
                // remove wall between cells
                var cellBetween =  gridNavigator.Neighbours[direction];
                _cells[cellBetween.X, cellBetween.Y] = MazeCellType.Space;
                
                _cells[nextCell.Value.X, nextCell.Value.Y] = MazeCellType.Space;

                CarvePassages(nextCell.Value.X, nextCell.Value.Y);
            }

            if (!isFinishFound)
            {
                var wallOnEdge =  gridNavigator.Neighbours.FirstOrDefault(c => isOnEdge(c.Value));
                if (!wallOnEdge.Equals(default(KeyValuePair<Direction, Cell>)))
                    _cells[wallOnEdge.Value.X, wallOnEdge.Value.Y] = MazeCellType.Finish;
                else 
                    _cells[currentX, currentY] = MazeCellType.Finish;
                isFinishFound = true;
            }
        }    

        private bool isOnEdge(Cell cell)
        {
            return cell.X == 0 || cell.Y == 0 || cell.X == _cells.GetLength(0)-1 || cell.Y == _cells.GetLength(1)-1 ;
        }
    }
}
