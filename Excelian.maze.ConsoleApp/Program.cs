using Excelian.Maze;
using Excelian.Maze.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.maze.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MazeGenerator generator = new MazeGenerator();
            var maze = generator.Generate(20, 20, new Excelian.Maze.Generator.Randomizer());

            Console.WriteLine("Generated maze:");
            Console.WriteLine(maze.Render());
            var explorer = new Explorer(maze);

            Console.WriteLine("Route:");

            var route = AutoExplorer.Explore(explorer);

            Console.WriteLine(maze.RenderWithRoute(route)); 

            Console.ReadKey();

        }
    }
}
