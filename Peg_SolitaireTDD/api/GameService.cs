﻿using Peg_SolitaireTDD.Model;
using System.Net.Http.Headers;

namespace Peg_SolitaireTDD.api
{
    public class GameService
    {
        private const string MOVE_TOWARDS_I = "i";
        private const string MOVE_TOWARDS_MINUS_I = "-i";
        private const string MOVE_TOWARDS_J = "j";
        private const string MOVE_TOWARDS_MINUS_J = "-j";

        public GameService()
        {
            this.InitGameBoard();
        }

        public GameBoard Gameboard { get; private set; }

        /// <returns>Deleted ball Case object</returns>
        /// <exception cref="InvalidMoveException">When the destination given in parameter is not part of balls movable destinations</exception>
        public Case MoveBall(Case ball, (int i, int j, string orientation) destination)
        {
            if(ball.BallValidDestinations.Count == 0 
                || !ball.BallValidDestinations.Contains(destination)) throw new InvalidMoveException();

            Case deletedBallCase = null;
            ball.IsEmpty = true;
            ball.BallValidDestinations.Clear();
            
            Gameboard.CaseList[destination.i][destination.j].IsEmpty = false;
            
            switch(destination.orientation)
            {
                case MOVE_TOWARDS_I:
                    deletedBallCase = Gameboard.CaseList[destination.i - 1][destination.j];
                    break;
                case MOVE_TOWARDS_MINUS_I:
                    deletedBallCase = Gameboard.CaseList[destination.i + 1][destination.j];
                    break;
                case MOVE_TOWARDS_J:
                    deletedBallCase = Gameboard.CaseList[destination.i][destination.j - 1];
                    break;
                case MOVE_TOWARDS_MINUS_J:
                    deletedBallCase = Gameboard.CaseList[destination.i][destination.j + 1];
                    break;
            }
            deletedBallCase.IsEmpty = true;
            return deletedBallCase;
        }

        public int NumberOfRemainingBalls()
        {
            int nb = 0;
            Gameboard.CaseList.ForEach(ligne => nb += ligne.Count(c => c.IsUsable && !c.IsEmpty));
            return nb;
        }

        public Case PickBallToPlay()
        {
            return this.ComputeValideMovesForEachBallAndReturnsPlayableBallList().First();
        }

        private List<Case> ComputeValideMovesForEachBallAndReturnsPlayableBallList()
        {
            List<Case> playableBallList = new();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (!Gameboard.CaseList[i][j].IsEmpty)
                    {
                        Gameboard.CaseList[i][j].BallValidDestinations.Clear();
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
            cases[3][3].IsEmpty = true;
        }
    }

    public class InvalidMoveException : Exception { }
}