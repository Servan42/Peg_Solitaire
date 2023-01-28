namespace Peg_SolitaireTDD.Model
{
    public class Case
    {
        public Case(bool isUsable, (int x, int y) posistion)
        {
            IsEmpty = false;
            IsUsable = isUsable;
            BallValidDestinations = new();
            Posistion = posistion;
        }

        public readonly (int x, int y) Posistion;

        public bool BallIsMovable { get => BallValidDestinations.Count > 0; }
        public List<(int x, int y, string orientation)> BallValidDestinations { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsUsable { get; set; }

        public override string ToString()
        {
            string output;
            if (!IsUsable) output = " ";
            else if(IsEmpty) output = ".";
            else output = "0";
            return output;
        }
    }
}