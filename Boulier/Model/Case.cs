using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boulier.Model
{
    /// <summary>
    /// Represent a case of the gameboard. May contain a ball. 
    /// </summary>
    public class Case
    {
        /// <summary>
        /// True if the case does not contain a ball.
        /// </summary>
        public bool Empty { get; set; }
        /// <summary>
        /// True if the case is part of the gameboard, and can be used as such.
        /// </summary>
        public bool Usable { get; private set; }

        /// <summary>
        /// Case constructor.
        /// </summary>
        /// <param name="aUsable">True if the case is usable (part of the gameboard).</param>
        public Case(bool aUsable)
        {
            Usable = aUsable;
            Empty = false;
        }

        public override string ToString()
        {
            if (Usable)
            {
                if (Empty) return ".";
                else return "O";
            }
            else return " ";
        }
    }
}
