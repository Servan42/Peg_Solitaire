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
            StringBuilder sb= new StringBuilder();
            foreach (List<Case> ligne in CaseList)
            {
                ligne.ForEach(l => sb.Append(l.ToString()));
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}