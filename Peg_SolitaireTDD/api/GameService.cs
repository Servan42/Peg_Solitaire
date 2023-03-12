using Peg_SolitaireTDD.Model;
using Peg_SolitaireTDD.spi;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Peg_SolitaireTDD.api
{
    public class GameService
    {
        private const string MOVE_TOWARDS_I = "i";
        private const string MOVE_TOWARDS_MINUS_I = "-i";
        private const string MOVE_TOWARDS_J = "j";
        private const string MOVE_TOWARDS_MINUS_J = "-j";
        public (int i, int j) StartingPosition { get; private set; }

        public GameService((int i, int j) startingPosition)
        {
            StartingPosition = startingPosition;
            this.InitGameBoard();
        }

        public GameBoard Gameboard { get; private set; }

        /// <returns>Deleted ball Case object</returns>
        /// <exception cref="InvalidMoveException">When the destination given in parameter is not part of balls movable destinations</exception>
        public Case MoveBall(Case ball, (int i, int j, string orientation) destination, bool isReplay = false)
        {
            if (!isReplay &&
                (ball.BallValidDestinations.Count == 0
                || !ball.BallValidDestinations.Contains(destination))) throw new InvalidMoveException();

            Case deletedBallCase = null;
            ball.IsEmpty = true;
            ball.BallValidDestinations.Clear();

            Gameboard.CaseList[destination.i][destination.j].IsEmpty = false;

            //switch (destination.orientation)
            //{
            //    case MOVE_TOWARDS_I:
            //        deletedBallCase = Gameboard.CaseList[destination.i - 1][destination.j];
            //        break;
            //    case MOVE_TOWARDS_MINUS_I:
            //        deletedBallCase = Gameboard.CaseList[destination.i + 1][destination.j];
            //        break;
            //    case MOVE_TOWARDS_J:
            //        deletedBallCase = Gameboard.CaseList[destination.i][destination.j - 1];
            //        break;
            //    case MOVE_TOWARDS_MINUS_J:
            //        deletedBallCase = Gameboard.CaseList[destination.i][destination.j + 1];
            //        break;
            //}

            deletedBallCase = Gameboard.CaseList[(ball.Posistion.x + destination.i) / 2][(ball.Posistion.y + destination.j) / 2];

            deletedBallCase.IsEmpty = true;
            return deletedBallCase;
        }

        public Case MoveBall((int i, int j) ballToMovePosition, (int i, int j, string orientation) destination, bool isReplay = false)
        {
            return this.MoveBall(Gameboard.CaseList[ballToMovePosition.i][ballToMovePosition.j], destination, isReplay);
        }

        public int NumberOfRemainingBalls()
        {
            int nb = 0;
            Gameboard.CaseList.ForEach(ligne => nb += ligne.Count(c => c.IsUsable && !c.IsEmpty));
            return nb;
        }

        public int NumberOfRemainingPlaybleBalls()
        {
            int nb = 0;
            Gameboard.CaseList.ForEach(ligne => nb += ligne.Count(c => c.BallIsMovable));
            return nb;
        }

        /// <returns>The ball to play.</returns>
        public Case PickBallToPlay(List<Case> playableBallList, bool shouldComputeFirst = false)
        {
            if (shouldComputeFirst) playableBallList = this.ComputeValideMovesForEachBallAndReturnsPlayableBallList();
            //return playableBallList.First();
            return playableBallList[StaticRandom.Rand(0, playableBallList.Count)];
        }

        public (int i, int j, string orientation) PickValidBallDestinationFromCase(Case caseToPickFrom)
        {
            //return caseToPickFrom.BallValidDestinations.First();
            return caseToPickFrom.BallValidDestinations[StaticRandom.Rand(0, caseToPickFrom.BallValidDestinations.Count)];
        }

        /// <summary>
        /// Plays a full game of PegSolitaire
        /// </summary>
        /// <returns>The instruction list of the game, to replay it later</returns>
        public List<ReplayStep> PlayFullGame()
        {
            Case ballToPlay;
            (int i, int j, string orientation) ballDestination;
            List<ReplayStep> replaySteps = new List<ReplayStep>();
            List<Case> playableBallList = this.ComputeValideMovesForEachBallAndReturnsPlayableBallList();
            do
            {
                ballToPlay = this.PickBallToPlay(playableBallList);
                ballDestination = PickValidBallDestinationFromCase(ballToPlay);
                replaySteps.Add(new ReplayStep(ballToPlay.Posistion, ballDestination));
                this.MoveBall(ballToPlay, ballDestination);
                playableBallList = this.ComputeValideMovesForEachBallAndReturnsPlayableBallList();
            } while (playableBallList.Count > 0);
            return replaySteps;
        }

        /// <summary>
        /// Play gameCount games of PegSolitaire and returns the replay of the best one
        /// </summary>
        /// <returns>The instruction list of the game, to replay it later</returns>
        public List<ReplayStep> PlayGamesBestOfX(int gameCount)
        {
            int remainingBalls;
            int minBalls = int.MaxValue;
            List<ReplayStep> replaySteps;
            List<ReplayStep> bestReplay = null;
            Console.Clear();

            for(int i = 0; i < gameCount; i++)
            {
                this.InitGameBoard();
                replaySteps = this.PlayFullGame();
                remainingBalls = this.NumberOfRemainingBalls();
                if(minBalls > remainingBalls)
                {
                    minBalls = remainingBalls;
                    bestReplay = replaySteps;
                }
            }
            Console.WriteLine($"Remaining balls: {minBalls}");
            return bestReplay;
        }

        /// <summary>
        /// Play games of PegSolitaire until one of them wins (1 remaining ball)
        /// </summary>
        /// <returns>The instruction list of the game, to replay it later</returns>
        public List<ReplayStep> PlayPerfectGame()
        {
            int i = 0;
            int remainingBalls;
            List<ReplayStep> replaySteps;
            Console.Clear();
            do
            {
                this.InitGameBoard();
                replaySteps = this.PlayFullGame();
                remainingBalls = this.NumberOfRemainingBalls();
                Console.SetCursorPosition(0, 0);
                Console.Write($"Game n°{i++}\tRemaining balls: {remainingBalls}");
            } while (remainingBalls > 1);
            return replaySteps;
        }

        /// <returns>A list of valid balls to play.</returns>
        private List<Case> ComputeValideMovesForEachBallAndReturnsPlayableBallList()
        {
            List<Case> playableBallList = new();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Gameboard.CaseList[i][j].BallValidDestinations.Clear();
                    if (!Gameboard.CaseList[i][j].IsEmpty && Gameboard.CaseList[i][j].IsUsable)
                    {
                        if (this.IsMovableAlongI(i, j))
                        {
                            Gameboard.CaseList[i][j].BallValidDestinations.Add((i + 2, j, MOVE_TOWARDS_I));
                        }
                        if (this.IsMovableAlongMinusI(i, j))
                        {
                            Gameboard.CaseList[i][j].BallValidDestinations.Add((i - 2, j, MOVE_TOWARDS_MINUS_I));
                        }
                        if (this.IsMovableAlongJ(i, j))
                        {
                            Gameboard.CaseList[i][j].BallValidDestinations.Add((i, j + 2, MOVE_TOWARDS_J));
                        }
                        if (this.IsMovableAlongMinusJ(i, j))
                        {
                            Gameboard.CaseList[i][j].BallValidDestinations.Add((i, j - 2, MOVE_TOWARDS_MINUS_J));
                        }
                        if (Gameboard.CaseList[i][j].BallIsMovable) playableBallList.Add(Gameboard.CaseList[i][j]);
                    }
                }
            }
            return playableBallList;
        }

        private bool IsMovableAlongI(int i, int j)
        {
            return i + 2 < 7
                && Gameboard.CaseList[i + 2][j].IsUsable
                && Gameboard.CaseList[i + 2][j].IsEmpty
                && !Gameboard.CaseList[i + 1][j].IsEmpty;
        }
        private bool IsMovableAlongMinusI(int i, int j)
        {
            return i - 2 >= 0
                && Gameboard.CaseList[i - 2][j].IsUsable
                && Gameboard.CaseList[i - 2][j].IsEmpty
                && !Gameboard.CaseList[i - 1][j].IsEmpty;
        }
        private bool IsMovableAlongJ(int i, int j)
        {
            return j + 2 < 7
                && Gameboard.CaseList[i][j + 2].IsUsable
                && Gameboard.CaseList[i][j + 2].IsEmpty
                && !Gameboard.CaseList[i][j + 1].IsEmpty;
        }
        private bool IsMovableAlongMinusJ(int i, int j)
        {
            return j - 2 >= 0
                && Gameboard.CaseList[i][j - 2].IsUsable
                && Gameboard.CaseList[i][j - 2].IsEmpty
                && !Gameboard.CaseList[i][j - 1].IsEmpty;
        }

        private void InitGameBoard()
        {
            List<List<Case>> cases = new List<List<Case>>();
            for (int i = 0; i < 7; i++)
            {
                List<Case> ligne = new List<Case>();
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
                        ligne.Add(new Case(false, (i, j)));
                    }
                    else ligne.Add(new Case(true, (i, j)));
                }
                cases.Add(ligne);
            }
            InitStartingPosition(cases);

            Gameboard = new GameBoard(cases);
            this.ComputeValideMovesForEachBallAndReturnsPlayableBallList();
        }

        private void InitStartingPosition(List<List<Case>> cases)
        {
            cases[StartingPosition.i][StartingPosition.j].IsEmpty = true;
        }
    }

    public class InvalidMoveException : Exception { }
}