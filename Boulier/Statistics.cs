﻿using Boulier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boulier
{
    /// <summary>
    /// Static methods to do statistics
    /// </summary>
    class Statistics
    {


        public Statistics()
        {
        }

        /// <summary>
        /// Runs aGameCount games and prints the average remaining ball count and the minimum ball count on those tries.
        /// </summary>
        /// <param name="aX">X coordinates of the starting ball to remove.</param>
        /// <param name="aY">Y coordinates of the starting ball to remove.</param>
        /// <param name="aGameCount">Number of games to play.</param>
        public static void QuickStat(int aX, int aY, int aGameCount)
        {
            int sum = 0;
            int min = 37;
            int ballcount;
            int choicenumber = 0;
            Gameboard g;
            for (int i = 0; i < aGameCount; i++)
            {
                g = new Gameboard();
                g.StartGame(aX, aY);
                //Console.Write(g.BallCount() + " ");
                ballcount = g.BallCount();
                sum += ballcount;
                choicenumber += g.ChoiceNumber;
                if (ballcount < min) min = ballcount;
            }
            Console.WriteLine("QuickStat ({0},{1})  Average: {2}  Minimum: {3}  ChoiceNumber: {4}", aX, aY, (float)sum / aGameCount, min, (float)choicenumber / aGameCount);
        }

        /// <summary>
        /// Runs a QuickStat() for each positions. 
        /// </summary>
        /// <param name="aGameCount">Number of games to play.</param>
        public static void CompleteStat(int aGameCount)
        {
            //Console.WriteLine("Stats for (1,1) IMP");
            QuickStat(1, 1, aGameCount);
            //Console.WriteLine("\nStats for (2,0)");
            QuickStat(2, 0, aGameCount);
            //Console.WriteLine("\nStats for (2,1) IMP");
            QuickStat(2, 1, aGameCount);
            //Console.WriteLine("\nStats for (2,2) IMP");
            QuickStat(2, 2, aGameCount);
            //Console.WriteLine("\nStats for (3,0) IMP");
            QuickStat(3, 0, aGameCount);
            //Console.WriteLine("\nStats for (3,1)");
            QuickStat(3, 1, aGameCount);
            //Console.WriteLine("\nStats for (3,2)");
            QuickStat(3, 2, aGameCount);
            //Console.WriteLine("\nStats for (3,3) IMP");
            QuickStat(3, 3, aGameCount);
            //Console.WriteLine();
        }

        /// <summary>
        /// Runs a QuickStat() for each positions. Threaded.
        /// </summary>
        /// <param name="aGameCount">Number of games to play.</param>
        public static void CompleteStatThread(int aGameCount)
        {
            DateTime startTime = DateTime.Now;
            Thread t0 = new Thread(() => { QuickStat(1, 1, aGameCount); });
            Thread t1 = new Thread(() => { QuickStat(2, 0, aGameCount); });
            Thread t2 = new Thread(() => { QuickStat(2, 1, aGameCount); });
            Thread t3 = new Thread(() => { QuickStat(2, 2, aGameCount); });
            Thread t4 = new Thread(() => { QuickStat(3, 0, aGameCount); });
            Thread t5 = new Thread(() => { QuickStat(3, 1, aGameCount); });
            Thread t6 = new Thread(() => { QuickStat(3, 2, aGameCount); });
            Thread t7 = new Thread(() => { QuickStat(3, 3, aGameCount); });

            t0.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();
            t7.Start();

            t0.Join();
            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();
            t6.Join();
            t7.Join();

            Console.WriteLine("Execution Time: {0}", DateTime.Now - startTime);
        }

        /// <summary>
        /// Runs a QuickStat() for each positions. Threaded
        /// </summary>
        /// <param name="aGameCount">Number of games to play.</param>
        public static void CompleteStatThread2(int aGameCount)
        {
            DateTime startTime = DateTime.Now;
            List<Thread> threadList = new List<Thread>();
            threadList.Add(new Thread(() => { QuickStat(1, 1, aGameCount); }));
            threadList.Add(new Thread(() => { QuickStat(2, 0, aGameCount); }));
            threadList.Add(new Thread(() => { QuickStat(2, 1, aGameCount); }));
            threadList.Add(new Thread(() => { QuickStat(2, 2, aGameCount); }));
            threadList.Add(new Thread(() => { QuickStat(3, 0, aGameCount); }));
            threadList.Add(new Thread(() => { QuickStat(3, 1, aGameCount); }));
            threadList.Add(new Thread(() => { QuickStat(3, 2, aGameCount); }));
            threadList.Add(new Thread(() => { QuickStat(3, 3, aGameCount); }));

            foreach (Thread t in threadList) t.Start();
            foreach (Thread t in threadList) t.Join();

            Console.WriteLine("Execution Time: {0}", DateTime.Now - startTime);
        }

        /// <summary>
        /// Runs a QuickStat() for each positions. Threaded. Attempt to queue threads if CPU has less than 8 logical cores.
        /// </summary>
        /// <param name="aGameCount">Number of games to play.</param>
        /// <param name="aMaxThreadParallel">Max threads in parallel. Queue the others.</param>
        public static void CompleteStatThread(int aGameCount, int aMaxThreadParallel)
        {
            DateTime startTime = DateTime.Now;
            int nbRunning = 0;
            int idRunning = 0;
            List<Thread> threadList = new List<Thread>();

            if (aMaxThreadParallel == 0) aMaxThreadParallel = Environment.ProcessorCount;

            Thread t0 = new Thread(() => { QuickStat(1, 1, aGameCount); });
            Thread t1 = new Thread(() => { QuickStat(2, 0, aGameCount); });
            Thread t2 = new Thread(() => { QuickStat(2, 1, aGameCount); });
            Thread t3 = new Thread(() => { QuickStat(2, 2, aGameCount); });
            Thread t4 = new Thread(() => { QuickStat(3, 0, aGameCount); });
            Thread t5 = new Thread(() => { QuickStat(3, 1, aGameCount); });
            Thread t6 = new Thread(() => { QuickStat(3, 2, aGameCount); });
            Thread t7 = new Thread(() => { QuickStat(3, 3, aGameCount); });

            threadList.Add(t0);
            threadList.Add(t1);
            threadList.Add(t2);
            threadList.Add(t3);
            threadList.Add(t4);
            threadList.Add(t5);
            threadList.Add(t6);
            threadList.Add(t7);

            // This doesn't seem as efficient as I expected...
            do
            {
                nbRunning = 0;
                foreach (Thread t in threadList)
                {
                    if (t.IsAlive) nbRunning++;
                }
                if (idRunning <= 7 && nbRunning < aMaxThreadParallel)
                {
                    threadList[idRunning].Start();
                    idRunning++;
                }
            } while (idRunning != 8);

            foreach (Thread t in threadList)
            {
                t.Join();
            }

            Console.WriteLine("Execution Time: {0}", DateTime.Now - startTime);
        }

        /// <summary>
        /// Runs a QuickStat() for each positions. Threaded with .NET Tasks
        /// </summary>
        /// <param name="aGameCount">Number of games to play.</param>
        public static void CompleteStatTasks(int aGameCount)
        {
            DateTime startTime = DateTime.Now;
            Task t0 = Task.Run(() => { QuickStat(1, 1, aGameCount); });
            Task t1 = Task.Run(() => { QuickStat(2, 0, aGameCount); });
            Task t2 = Task.Run(() => { QuickStat(2, 1, aGameCount); });
            Task t3 = Task.Run(() => { QuickStat(2, 2, aGameCount); });
            Task t4 = Task.Run(() => { QuickStat(3, 0, aGameCount); });
            Task t5 = Task.Run(() => { QuickStat(3, 1, aGameCount); });
            Task t6 = Task.Run(() => { QuickStat(3, 2, aGameCount); });
            Task t7 = Task.Run(() => { QuickStat(3, 3, aGameCount); });

            t0.Wait();
            t1.Wait();
            t2.Wait();
            t3.Wait();
            t4.Wait();
            t5.Wait();
            t6.Wait();
            t7.Wait();

            Console.WriteLine("Execution Time: {0}", DateTime.Now - startTime);
        }

        /// <summary>
        /// Runs games and display the repartition of remaining ball counts.
        /// </summary>
        /// <param name="aX">Starting position X</param>
        /// <param name="aY">Strating position Y</param>
        /// <param name="aGameCount">Number of games to play</param>
        public static void OccStat(int aX, int aY, int aGameCount)
        {
            DateTime startTime = DateTime.Now;
            Console.WriteLine("Mode: OccStat\n");
            Dictionary<int, int> occList = new Dictionary<int, int>();
            StringBuilder s = new StringBuilder();
            int sum = 0;
            int min = 100;
            int ballcount;
            Gameboard g;

            // Generate the games and get the ball count at the end.
            for (int i = 0; i < aGameCount; i++)
            {
                g = new Gameboard();
                g.StartGame(aX, aY);
                //Console.Write(g.BallCount() + " ");
                ballcount = g.BallCount();
                sum += ballcount;
                if (ballcount < min) min = ballcount;
                if (occList.ContainsKey(ballcount))
                {
                    occList[ballcount] = occList[ballcount] + 1;
                }
                else
                {
                    occList.Add(ballcount, 1);
                }
            }
            
            // Add missing ball counts
            for (int i = 1; i < 25; i++)
            {
                if (!occList.ContainsKey(i)) occList.Add(i, 0);
            }

            // Display the results
            foreach (KeyValuePair<int, int> occ in occList.OrderBy(key => key.Key))
            {
                s.Clear();
                s.Append(occ.Key.ToString("D2")).Append(": ");
                for (int i = 0; i < occ.Value * 300 / aGameCount; i++)
                {
                    s.Append("*");
                }
                s.Append(" (").Append(Math.Round((float)occ.Value * 100 / aGameCount, 4)).Append("%)");
                Console.WriteLine(s.ToString());
            }

            Console.WriteLine("Execution Time: {0}", DateTime.Now - startTime);
            Console.WriteLine("\nMoyenne : {0}\nMinimum : {1}", (float)sum / aGameCount, min);
        }

        /// <summary>
        /// Runs games and display the repartition of remaining ball counts. Threaded.
        /// </summary>
        /// <param name="aX">Starting position X</param>
        /// <param name="aY">Strating position Y</param>
        /// <param name="aGameCount">Number of games to play</param>
        public static void OccStatThread(int aX, int aY, int aGameCount, int aThreadNumber)
        {
            DateTime startTime = DateTime.Now;
            Dictionary<int, int> occList = new Dictionary<int, int>();
            object lockOccList = new object();
            List <Thread> threadList = new List<Thread>();
            int threadNumber = aThreadNumber == 0 ? Environment.ProcessorCount : aThreadNumber;
            StringBuilder s = new StringBuilder();

            Console.WriteLine("Mode: OccStat ({0} threads)\n", threadNumber);

            aGameCount = (aGameCount / threadNumber) * threadNumber;

            // Thread code. Generate the games and get the ball count at the end.
            Action threadCode = () =>
            {
                Gameboard g;
                int sum = 0;
                int min = 100;
                int ballcount;
                for (int i = 0; i < aGameCount / threadNumber; i++)
                {
                    g = new Gameboard();
                    g.StartGame(aX, aY);
                    //Console.Write(g.BallCount() + " ");
                    ballcount = g.BallCount();
                    sum += ballcount;
                    if (ballcount < min) min = ballcount;
                    lock (lockOccList)
                    {
                        if (occList.ContainsKey(ballcount))
                        {
                            occList[ballcount] = occList[ballcount] + 1;
                        }
                        else
                        {
                            occList.Add(ballcount, 1);
                        }
                    }
                }
            };

            // Create the threads and run them.
            for (int i = 0; i < threadNumber; i++) threadList.Add(new Thread(new ThreadStart(threadCode)));
            foreach (Thread t in threadList) t.Start();
            foreach (Thread t in threadList) t.Join();

            // Add missing ball counts
            for (int i = 1; i < 25; i++)
            {
                if (!occList.ContainsKey(i)) occList.Add(i, 0);
            }

            // Display the results
            foreach (KeyValuePair<int, int> occ in occList.OrderBy(key => key.Key))
            {
                s.Clear();
                s.Append(occ.Key.ToString("D2")).Append(": ");
                for (int i = 0; i < occ.Value * 300 / aGameCount; i++)
                {
                    s.Append("*");
                }
                s.Append(" (").Append(Math.Round((float)occ.Value * 100 / aGameCount, 4)).Append("%)");
                Console.WriteLine(s.ToString());
            }

            Console.WriteLine("Execution Time: {0}", DateTime.Now - startTime);
            // Console.WriteLine("\nMoyenne : {0}\nMinimum : {1}", (float)sum / aGameCount, min);
        }
    }
}
