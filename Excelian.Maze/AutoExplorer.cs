using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze
{

    // User story 3
    public class AutoExplorer
    {        
        /// <summary>
        /// Automaticaly finds exit and returns route.
        /// It doesn't find shortest route just finds finish. 
        /// </summary>        
        public static IEnumerable<Cell> Explore(Explorer explorer)
        {
            //// choose direction to go first
            switch (explorer.AvailableMovementOptions.First())
            {
                case MoveDirection.Rigth:
                    explorer.TurnRight();
                    break;

                case MoveDirection.Left:
                    explorer.TurnLeft();
                    break;

                case MoveDirection.Backward:
                    explorer.TurnAround();
                    break;
            }


            ExploreRecursive(explorer);

            return explorer.Movement;
        }

        private static void ExploreRecursive(Explorer explorer)
        {                     
            for (int i = 0; i < 4; i++)
            {
                if (explorer.IsFinish)
                    return;       

                // in case of 0 we just go forward
                switch (i)
                {
                    case 1:
                        explorer.TurnLeft();
                        break;

                    case 2:
                        explorer.TurnAround(); 
                        break;

                    case 3:
                        explorer.TurnRight(); // we move back and restore direction
                        explorer.Move();
                        explorer.TurnAround();                        
                        return; // we are going back                        
                }

                if (explorer.ObjectInFront.HasValue && explorer.ObjectInFront.Value != MazeCellType.Wall)
                {
                    explorer.Move();
                    System.Diagnostics.Debug.WriteLine("{0} : {1}", explorer.CurerentPosition , explorer.CurrentDirection);

                    ExploreRecursive(explorer);
                }
                                  
            }
        }
    }
}
