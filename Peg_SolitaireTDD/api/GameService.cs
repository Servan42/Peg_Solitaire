using Peg_SolitaireTDD.Model;

namespace Peg_SolitaireTDD.api
{
    public class GameService
    {
        public GameService()
        {
            this.InitGameBoard();
        }

        public GameBoard Gameboard { get; private set; }

        public void MoveBall(Case ball, (int x, int y) value)
        {
            throw new NotImplementedException();
        }

        public int NumberOfRemainingBalls()
        {
            int nb = 0;
            Gameboard.CaseList.ForEach(ligne => nb += ligne.Count(c => c.IsUsable && !c.IsEmpty));
            return nb;
        }

        public Case PickBallToPlay()
        {
            this.ComputeValideMovesForEachBall();
            throw new NotImplementedException();
        }

        private void ComputeValideMovesForEachBall()
        {
            throw new NotImplementedException();
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
                        ligne.Add(new Case(false));
                    }
                    else ligne.Add(new Case(true));
                }
                cases.Add(ligne);
            }
            InitStartingPosition(cases);

            Gameboard = new GameBoard(cases);
        }

        private void InitStartingPosition(List<List<Case>> cases)
        {
            cases[3][3].IsEmpty = true;
        }
    }
}