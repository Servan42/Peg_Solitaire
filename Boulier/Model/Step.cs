using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boulier.Model
{
    /// <summary>
    /// Used to describe a move in the game. 
    /// </summary>
    public class Step
    {
        /// <summary>
        /// Coordinates of the ball to move.
        /// </summary>
        public (int x, int y) StartPos { get; private set; }
        /// <summary>
        /// Coordinates of the ball being deleted.
        /// </summary>
        public (int x, int y) DelPos { get; private set; }
        /// <summary>
        /// Arrival coordinates of the ball being moved.
        /// </summary>
        public (int x, int y) EndPos { get; private set; }

        /// <summary>
        /// Constructor of step. 
        /// </summary>
        /// <param name="aStartPos">Coordinates of the ball to move.</param>
        /// <param name="aDelPos">Coordinates of the ball being deleted.</param>
        /// <param name="aEndPos">Arrival coordinates of the ball being moved.</param>
        public Step((int x, int y) aStartPos, (int x, int y) aDelPos, (int x, int y) aEndPos)
        {
            StartPos = aStartPos;
            DelPos = aDelPos;
            EndPos = aEndPos;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("S:").Append(StartPos.x).Append(",").Append(StartPos.y).Append(" | ");
            s.Append("D:").Append(DelPos.x).Append(",").Append(DelPos.y).Append(" | ");
            s.Append("E:").Append(EndPos.x).Append(",").Append(EndPos.y);
            return s.ToString();
        }
    }
}
