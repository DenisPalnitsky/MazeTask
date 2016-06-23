using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze
{ 
    /// <summary>
    /// Represents maze in text format
    /// </summary>
    public static class MazeStringRenderer
    {
        static readonly Dictionary<MazeCellType, string> CellRepresentationConatants =
            new Dictionary<MazeCellType, string>{             
                                            { MazeCellType.Finish, "F"},
                                            { MazeCellType.Space, "."},
                                            { MazeCellType.Start, "S"},                                            
                                            { MazeCellType.Wall, "X"},
                                        };


        public static string Render(this Maze maze)
        {            
            StringBuilder output = new StringBuilder();

            
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)    
                    output.Append(CellRepresentationConatants[ maze[x,y].CellType]) ;                
                output.Append(Environment.NewLine);
            }            
    
            return output.ToString();
        }
      
        public static string RenderWithRoute(this Maze maze, IEnumerable<Cell> route)
        {
            StringBuilder output = new StringBuilder();
            HashSet<Cell> cell = new HashSet<Cell>(route);


            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {                    
                    if (maze[x, y].CellType == MazeCellType.Space && route.Contains(new Cell(x,y) ))
                        output.Append("*");
                    else                     
                        output.Append(CellRepresentationConatants[maze[x, y].CellType]);
                }

                output.Append(Environment.NewLine);
            }

            return output.ToString();
        }
                
    }
}
