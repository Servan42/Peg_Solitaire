using Peg_SolitaireTDD;
using Peg_SolitaireTDD.api;
using System;

namespace Peg_SolitaireTDDConsole // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayPerfectGameAndDisplayIt();
        }

        private static void PlayOneGameAndDisplayIt()
        {
            GameService gameService = new GameService();
            ReplayService replayService = new ReplayService();

            List<ReplayStep> replaySteps;

            replaySteps = gameService.PlayFullGame();
            replayService.ReplayGame(replaySteps);
        }

        private static void PlayPerfectGameAndDisplayIt()
        {
            GameService gameService = new GameService();
            ReplayService replayService = new ReplayService();

            List<ReplayStep> replaySteps;

            replaySteps = gameService.PlayPerfectGame();
            replayService.ReplayGame(replaySteps);
        }
    }
}