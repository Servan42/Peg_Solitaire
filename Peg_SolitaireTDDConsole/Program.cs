using Peg_SolitaireTDD;
using Peg_SolitaireTDD.api;
using System;

namespace Peg_SolitaireTDDConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunStats();
        }

        private static void RunStats()
        {
            StatisticsService statisticsService= new StatisticsService();
            DateTime starttime = DateTime.Now;
            //statisticsService.QuickStat(3, 3, 20000);
            //statisticsService.CompleteStat(20000);
            statisticsService.CompleteStatThread(20000);
            //statisticsService.CompleteStatThread2(20000);
            //statisticsService.CompleteStatTasks(20000);
            Console.WriteLine(DateTime.Now - starttime);
        }

        private static void PlayOneGameAndDisplayIt()
        {
            GameService gameService = new GameService((3,3));
            ReplayService replayService;
            List<ReplayStep> replaySteps;

            replaySteps = gameService.PlayFullGame();
            
            replayService = new ReplayService((replaySteps[0].BallDestination.i, replaySteps[0].BallDestination.j));
            replayService.ReplayGame(replaySteps);
        }

        private static void PlayPerfectGameAndDisplayIt()
        {
            GameService gameService = new GameService((3, 3));
            ReplayService replayService;
            List<ReplayStep> replaySteps;

            replaySteps = gameService.PlayPerfectGame();

            replayService = new ReplayService((replaySteps[0].BallDestination.i, replaySteps[0].BallDestination.j));
            replayService.ReplayGame(replaySteps);
        }
    }
}