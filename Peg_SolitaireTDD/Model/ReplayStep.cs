namespace Peg_SolitaireTDD
{
    public class ReplayStep
    {
        public ReplayStep((int i, int j) ballInitialPosition, (int i, int j, string orientation) ballDestination)
        {
            BallInitialPosition = ballInitialPosition;
            BallDestination = ballDestination;
        }

        public (int i, int j) BallInitialPosition { get; set; }
        public (int i, int j, string orientation) BallDestination { get; set; }

        public override string ToString()
        {
            return $"InitialPos: [{BallInitialPosition.i},{BallInitialPosition.j}] | Destination: [{BallDestination.i},{BallDestination.j}] (towards {BallDestination.orientation})";
        }
    }
}