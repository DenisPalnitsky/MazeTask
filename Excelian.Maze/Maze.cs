using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Excelian.Maze
{
    public enum MazeCellType : byte
    {        
        Wall,
        Space,
        Start, 
        Finish
    }

    public class MazeCell
    {
        public MazeCellType CellType { get; private set; }

        public Cell Coordinates { get; private set; }

        public MazeCell(int x, int y, MazeCellType type)
        {
            Coordinates = new Cell(x, y);            
            CellType = type;
        }        
    }

    public class Maze 
    {
        private readonly MazeCellType[,] _cells;

        public int Height 
        { 
            get { return _cells.GetLength(1); }
        }

        public int Width
        {
            get { return _cells.GetLength(0); }
        }

        public MazeCell Start { get; private set; }
        
        public MazeCell Finish { get; private set; }

        private Maze(MazeCellType[,] cells)
        {
            _cells = cells;
        }

        public static Maze Create(MazeCellType[,] cells)
        {
            Maze maze = new Maze(cells);

            for (int x = 0; x < maze.Width; x++)
            {
                for (int y = 0; y < maze.Height; y++)
                {
                    switch (cells[x, y])
                    {
                        case MazeCellType.Finish:
                            maze.Finish = new MazeCell(x, y, MazeCellType.Finish);
                            break;

                        case MazeCellType.Start:
                            maze.Start = new MazeCell(x, y, MazeCellType.Start);
                            break;

                    }
                }
            }

            if (maze.Finish == null)
                throw new ArgumentException("Maze doesn't contain Finish cell");

            if (maze.Start == null)
                throw new ArgumentException("Maze doesn't contain Start cell");

            return maze;
        }

        public MazeStats GetStatistics ()
        {
            return MazeStats.Gather(this);
        }

        // support iteration but prevent editing 
        public MazeCell this[int x, int y]
        {
            get
            {
                return new MazeCell(x, y, _cells[x, y]);
            }
        }

        public MazeCell this[Cell cell]
        {
            get
            {
                return new MazeCell(cell.X, cell.Y, _cells[cell.X, cell.Y]);
            }
        }
       
    }
}
