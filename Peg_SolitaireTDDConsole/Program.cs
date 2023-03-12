using Peg_SolitaireTDD;
using Peg_SolitaireTDD.api;
using System;

namespace Peg_SolitaireTDDConsole // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayOneGameAndDisplayIt();
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