using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Excelian.Maze
{
    [Serializable]
    public class MazeExploringException : Exception
    {
        public MazeExploringException() { }
        public MazeExploringException(string message) : base(message) { }
        public MazeExploringException(string message, Exception inner) : base(message, inner) { }
        protected MazeExploringException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
