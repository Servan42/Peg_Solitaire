using Boulier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boulier
{
    class Program
    {
        public static Random rand = new Random();
        static void Main(string[] args)
        {
            char inputMode;
            int gamecount = 0;
            int goal = 0;
            int startPosX = 0;
            int startPosY = 0;
            do
            {
                try
                {
                    // Ugly, unprotected conole menu.
                    Console.WriteLine("\n1: Quick Stat");
                    Console.WriteLine("2: Complete Stat");
                    Console.WriteLine("3: Occurence Stat");
                    Console.WriteLine("4: Replay Best");
                    Console.WriteLine("5: Replay Perfect");

                    inputMode = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    if (inputMode != '5') // Not Replay perfect
                    {
                        Console.Write("Game count: ");
                        gamecount = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        if (gamecount < 0) gamecount = 0;
                    }
                    else if (inputMode == '5') // Replay perfect
                    {
                        Console.Write("Goal? (1-25) ");
                        goal = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        if (goal < 1) goal = 1;
                    }

                    if (inputMode != '2') // Not Complete stats
                    {
                        // Prints the coordinates of the gameboard
                        Console.WriteLine("Coord: [x;y]\n");
                        for (int i = 0; i < 7; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                if ((i == 0 && j == 0)
                                || (i == 0 && j == 1)
                                || (i == 1 && j == 0)
                                || (i == 0 && j == 5)
                                || (i == 5 && j == 0)
                                || (i == 1 && j == 6)
                                || (i == 6 && j == 1)
                                || (i == 6 && j == 0)
                                || (i == 0 && j == 6)
                                || (i == 6 && j == 5)
                                || (i == 5 && j == 6)
                                || (i == 6 && j == 6))
                                {
                                    Console.Write("     ");
                                }
                                else
                                {
                                    Console.Write("[{0},{1}]", i, j);
                                }
                            }
                            Console.WriteLine("\n");
                        }
                        Console.Write("StartPosX: ");
                        startPosX = int.Parse(Console.ReadKey().KeyChar.ToString());
                        Console.WriteLine();
                        Console.Write("StartPosY: ");
                        startPosY = int.Parse(Console.ReadKey().KeyChar.ToString());
                        Console.WriteLine();
                    }

                    switch (inputMode)
                    {
                        case '1':
                            Console.WriteLine("Mode: QuickStat");
                            Statistics.QuickStat(startPosX, startPosY, gamecount);
                            break;
                        case '2':
                            Statistics.CompleteStatThread(gamecount);
                            //Statistics.CompleteStatThread2(gamecount);
                            //Statistics.CompleteStatThread(gamecount,0);
                            //Statistics.CompleteStatTasks(gamecount);
                            break;
                        case '3':
                            //Statistics.OccStat(startPosX, startPosY, gamecount);
                            Statistics.OccStatThread(startPosX, startPosY, gamecount, 0);
                            break;
                        case '4':
                            Replay.ReplayBest(startPosX, startPosY, gamecount);
                            break;
                        case '5':
                            if (goal < 3)
                            {
                                Console.WriteLine("Multithread? (y/n) -- /!\\ Ressource costly /!\\");
                                if (Console.ReadKey().KeyChar == 'y') Replay.ReplayPerfectThread(startPosX, startPosY, goal, 0);
                                else Replay.ReplayPerfect(startPosX, startPosY, goal);
                            }
                            else Replay.ReplayPerfect(startPosX, startPosY, goal);
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("\nQuit? (y/n)");
            } while (Console.ReadKey().KeyChar != 'y');

        }
    }
}
