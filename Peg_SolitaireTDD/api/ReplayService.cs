using Peg_SolitaireTDD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
                _gameService.MoveBall(step.BallInitialPosition, step.BallDestination, true);
                Console.WriteLine(step);
                //Console.WriteLine(_gameService.Gameboard.DetailedToString(step.BallDestination));
                _gameService.Gameboard.PrintColoredBoard(step.BallInitialPosition ,step.BallDestination);
                Console.WriteLine();
            }
        }
    }
}
