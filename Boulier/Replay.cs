using Boulier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Boulier
{
    /// <summary>
    /// Static functions that generate one or multiple games and replay it.
    /// </summary>
    class Replay
    {
        /// <summary>
        /// Generate a game and replay it. 
        /// </summary>
        /// <param name="aX">Specify the X coordinate of the starting position.</param>
        /// <param name="aY">Specify the Y coordinate of the starting position.</param>
        public static void ReplayOnce(int aX, int aY)
        {
            Console.WriteLine("Mode: ReplayOnce");
            Gameboard g;
            g = new Gameboard();
            List<Step> StepList;
            g.StartGame(aX, aY);
            StepList = g.StepList;
            g = new Gameboard();
            g.Replay(aX, aY, StepList);
        }

        /// <summary>
        /// Generate a specified number of games and replays the best.
        /// </summary>
        /// <param name="aX">Specify the X coordinate of the starting position.</param>
        /// <param name="aY">Specify the Y coordinate of the starting position.</param>
        /// <param name="aGameCount">NUmber of games to generate.</param>
        public static void ReplayBest(int aX, int aY, int aGameCount)
        {
            Console.WriteLine("Mode: ReplayBest");
            Gameboard g;
            List<Step> StepList = null;
            int min = 100;
            int ballcount;
            for (int i = 0; i < aGameCount; i++)
            {
                g = new Gameboard();
                g.StartGame(aX, aY);
                ballcount = g.BallCount();
                if (ballcount < min)
                {
                    min = ballcount;
                    StepList = g.StepList;
                }
            }

            Console.WriteLine("Remaining balls : {0}", min);
            g = new Gameboard();
            g.Replay(aX, aY, StepList);
        }

        /// <summary>
        /// Generate games until the goal (number of remaining balls to reach) is not reached.
        /// </summary>
        /// <param name="aX">Specify the X coordinate of the starting position.</param>
        /// <param name="aY">Specify the Y coordinate of the starting position.</param>
        /// <param name="goal">Number of remaining balls to reach</param>
        public static void ReplayPerfect(int aX, int aY, int goal)
        {
            Console.WriteLine("Mode: ReplayPerfect {0}", goal);
            Gameboard g = null;
            List<Step> StepList = null;
            int ballcount = 0;
            while (ballcount != goal && ballcount != 1)
            {
                g = new Gameboard();
                g.StartGame(aX, aY);
                ballcount = g.BallCount();
            }
            Console.WriteLine(ballcount);
            StepList = g.StepList;
            Console.WriteLine("Probability of this run: {0}%", g.Probability * 100.0);
            do
            {
                g = new Gameboard();
                g.Replay(aX, aY, StepList);
                Console.WriteLine("Replay? (y/n)");
            } while (Console.ReadKey().KeyChar == 'y');
        }

        /// <summary>
        /// Generate games through different threads until the goal (number of remaining balls to reach) is not reached.
        /// </summary>
        /// <param name="aX">Specify the X coordinate of the starting position.</param>
        /// <param name="aY">Specify the Y coordinate of the starting position.</param>
        /// <param name="goal">Number of remaining balls to reach</param>
        /// <param name="aThreadNumber">Number of threads to use. Set to 0 to have the maximum number your CPU can handle.</param>
        public static void ReplayPerfectThread(int aX, int aY, int goal, int aThreadNumber)
        {
            int threadNumber = aThreadNumber == 0 ? Environment.ProcessorCount : aThreadNumber;
            Console.WriteLine("Mode: ReplayPerfect {0} ({1} threads)", goal, threadNumber);
            bool found = false;
            object lockFound = new object();
            List<Step> StepList = null;
            object lockStepList = new object();
            List<Thread> threadList = new List<Thread>();
            Gameboard gr;

            // Thread code
            Action threadCode = () =>
            {
                bool localFound = false;
                Gameboard g = null;
                int ballcount = 0;
                while (!found)
                {
                    g = new Gameboard();
                    g.StartGame(aX, aY);
                    ballcount = g.BallCount();
                    if (ballcount == goal || ballcount == 1)
                    {
                        localFound = true;
                        // Stop all the threads
                        lock (lockFound) found = true;
                    }
                }
                if (localFound)
                {
                    Console.WriteLine("Probability of this run: {0}%", g.Probability * 100.0);
                    lock (lockStepList) StepList = g.StepList;
                }
                //if (localFound)
                //{
                //    Console.WriteLine("Found by thread {0}", Thread.CurrentThread.ManagedThreadId);
                //}
                //else
                //{
                //    Console.WriteLine("ballcount for thread {0}: {1}", Thread.CurrentThread.ManagedThreadId, ballcount);
                //}
            };

            // Create and start the threads
            for (int i = 0; i < threadNumber; i++) threadList.Add(new Thread(new ThreadStart(threadCode)));
            foreach (Thread t in threadList) t.Start();
            foreach (Thread t in threadList) t.Join();

            // Replay the best game
            do
            {
                gr = new Gameboard();
                gr.Replay(aX, aY, StepList);
                Console.WriteLine("Replay? (y/n)");
            } while (Console.ReadKey().KeyChar == 'y');
        }
    }
}
