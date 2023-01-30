using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg_SolitaireTDD.spi
{
    public class StaticRandom
    {
        static int seed = Environment.TickCount;

        static readonly ThreadLocal<Random> random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        //static readonly ThreadLocal<Random> random =
        //new ThreadLocal<Random>(() => new Random(0));

        public static int Rand()
        {
            return random.Value.Next();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aDeb">Included</param>
        /// <param name="aFin">Not Included</param>
        /// <returns></returns>
        public static int Rand(int aDeb, int aFin)
        {
            return random.Value.Next(aDeb, aFin);
        }
    }
}
