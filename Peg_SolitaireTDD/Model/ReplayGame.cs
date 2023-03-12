using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg_SolitaireTDD.Model
{
    public class ReplayGame
    {
        public List<ReplayStep> replaySteps = new List<ReplayStep>();
        public int Score { get; set; }
    }
}
