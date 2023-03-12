using Peg_SolitaireTDD.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Peg_SolitaireTDD.api
{
    public class ReplayService
    {
        private GameService _gameService;

        public ReplayService((int i, int j) startingPosition)
        {
            this._gameService = new GameService(startingPosition);
        }

        public void ReplayGame(List<ReplayStep> replaySteps)
        {
            foreach (ReplayStep step in replaySteps)
            {
                _gameService.MoveBallReplay(step.BallInitialPosition, step.BallDestination);
                Console.WriteLine(step);
                //Console.WriteLine(_gameService.Gameboard.DetailedToString(step.BallDestination));
                _gameService.Gameboard.PrintColoredBoard(step.BallInitialPosition, step.BallDestination);
                Console.WriteLine();
            }
        }

        public int ComputeReplayScore(List<ReplayStep> replaySteps)
        {
            int remainingBalls = int.MaxValue;
            try
            {
                _gameService.InitGameBoard();
                remainingBalls = _gameService.NumberOfRemainingBalls();
                foreach (ReplayStep step in replaySteps)
                {
                    _gameService.MoveBallReplay(step.BallInitialPosition, step.BallDestination);
                    remainingBalls--;
                }
                return _gameService.NumberOfRemainingBalls();
            }
            catch
            {
                return remainingBalls;
            }
        }
    }
}
