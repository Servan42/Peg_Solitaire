using Peg_SolitaireTDD;
using Peg_SolitaireTDD.api;
using System;
using System.Data;

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
            //statisticsService.CompleteStatThread(20000);
            //statisticsService.CompleteStatThread2(20000);
            //statisticsService.CompleteStatTasks(20000);
            statisticsService.OccStatThread(3,3,100000,8);
            Console.WriteLine(DateTime.Now - starttime);
        }

        private static void PlayGames()
        {
            GameService gameService = new GameService((3,3));
            ReplayService replayService;
            List<ReplayStep> replaySteps;

            replaySteps = gameService.PlayFullGame();
            replaySteps = gameService.PlayPerfectGame();
            replaySteps = gameService.PlayGamesBestOfX(10000);

            replayService = new ReplayService((replaySteps[0].BallDestination.i, replaySteps[0].BallDestination.j));
            replayService.ReplayGame(replaySteps);
        }

        //private static void UseGeneticAlgorithToPlay()
        //{
        //    ReplayService replayService;
        //    List<ReplayStep> replaySteps;
        //    GeneticAlgorithmService geneticAlgorithmService = new();

        //    replaySteps = geneticAlgorithmService.Run();

        //    replayService = new ReplayService((replaySteps[0].BallDestination.i, replaySteps[0].BallDestination.j));
        //    replayService.ReplayGame(replaySteps);
        //}
    }
}