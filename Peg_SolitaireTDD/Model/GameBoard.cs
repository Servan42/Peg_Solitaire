using System.Text;

namespace Peg_SolitaireTDD.Model
{
    public class GameBoard
    {
        public GameBoard(List<List<Case>> caseList)
        {
            CaseList = caseList;
        }

        public List<List<Case>> CaseList { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (List<Case> ligne in CaseList)
            {
                ligne.ForEach(l => sb.Append(l.ToString()));
                sb.AppendLine();
            }
            return sb.ToString();
        }

        internal string DetailedToString((int i, int j, string orientation) ballDestination)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < CaseList.Count; i++)
            {
                for (int j = 0; j < CaseList[i].Count; j++)
                {
                    if (i == ballDestination.i && j == ballDestination.j)
                    {
                        sb.Append("O");
                    }
                    else if (ballDestination.orientation == "i" && i == ballDestination.i -1 && j == ballDestination.j
                        || ballDestination.orientation == "-i" && i == ballDestination.i + 1 && j == ballDestination.j
                        || ballDestination.orientation == "j" && i == ballDestination.i && j == ballDestination.j -1
                        || ballDestination.orientation == "-j" && i == ballDestination.i && j == ballDestination.j + 1)
                    {
                        sb.Append("x");
                    }
                    else
                    {
                        sb.Append(CaseList[i][j].ToString());
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        internal void PrintColoredBoard((int i, int j) ballInitialPosition, (int i, int j, string orientation) ballDestination)
        {
            for (int i = 0; i < CaseList.Count; i++)
            {
                for (int j = 0; j < CaseList[i].Count; j++)
                {
                    if (i == ballInitialPosition.i && j == ballInitialPosition.j)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(".");
                    }
                    else if (i == ballDestination.i && j == ballDestination.j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("O");
                    }
                    else if (ballDestination.orientation == "i" && i == ballDestination.i - 1 && j == ballDestination.j
                        || ballDestination.orientation == "-i" && i == ballDestination.i + 1 && j == ballDestination.j
                        || ballDestination.orientation == "j" && i == ballDestination.i && j == ballDestination.j - 1
                        || ballDestination.orientation == "-j" && i == ballDestination.i && j == ballDestination.j + 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("x");
                    }
                    else
                    {
                        Console.Write(CaseList[i][j].ToString());
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}