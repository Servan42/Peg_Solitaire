using Boulier.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boulier.Model
{
    /// <summary>
    /// Represent the game board.
    /// </summary>
    public class Gameboard
    {
        public Case[,] Grid { get; private set; }
        public List<Step> StepList { get; private set; }
        /// <summary>
        /// Number of choices the algorithm did to complete the board.
        /// </summary>
        public int ChoiceNumber { get; private set; }
        /// <summary>
        /// Probability of that particular solution
        /// </summary>
        public double Probability { get; private set; }

        /// <summary>
        /// Creates and initiates the gameboard
        /// </summary>
        public Gameboard()
        {
            Grid = new Case[7, 7];
            StepList = new List<Step>();
            ChoiceNumber = 0;
            Probability = 1.0;
            init();
        }

        /// <summary>
        /// PLays a whole game until no other move is possible.
        /// Specify in parameters the ball to remove to start.
        /// </summary>
        /// <param name="x">Starting position x</param>
        /// <param name="y">Starting position y</param>
        public void StartGame(int x, int y)
        {
            Dictionary<(int x, int y), List<(int x, int y)>> boardStatus;
            Grid[x, y].Empty = true;
            boardStatus = GetBoardStatus();
            while (boardStatus.Count != 0)
            {
                PlayNextMove(boardStatus);
                boardStatus = GetBoardStatus();
            }
        }

        /// <summary>
        /// Counts the balls on the board
        /// </summary>
        /// <returns></returns>
        public int BallCount()
        {
            int counter = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Grid[i, j].Usable && !Grid[i, j].Empty)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        /// <summary>
        /// Uses the model to print a game stored in a list of steps given in parameters.
        /// </summary>
        /// <param name="x">Starting position x</param>
        /// <param name="y">Starting position y</param>
        /// <param name="aStepList">List of steps to replay</param>
        public void Replay(int x, int y, List<Step> aStepList)
        {
            Grid[x, y].Empty = true;
            Console.WriteLine("Clear mode? (y/n)");
            if (Console.ReadKey().KeyChar == 'y')
            {
                PrintBoardColorCursor(aStepList);
            }
            else
            {
                foreach (Step step in aStepList)
                {
                    Grid[step.StartPos.x, step.StartPos.y].Empty = true;
                    Grid[step.DelPos.x, step.DelPos.y].Empty = true;
                    Grid[step.EndPos.x, step.EndPos.y].Empty = false;
                    Console.WriteLine(step.ToString());
                    //Console.WriteLine(this.ToString());
                    PrintBoardColor(step);
                    Console.WriteLine();
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Creates the cases of the board and fills them with a ball.
        /// </summary>
        private void init()
        {
            for (int i = 0; i < 7; i++)
            {
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
                        Grid[i, j] = new Case(false);
                    }
                    else
                    {
                        Grid[i, j] = new Case(true);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a dictionnary containing for each ball (key) a list of valid destination where it can be moved (value).
        /// </summary>
        /// <returns></returns>
        private Dictionary<(int x, int y), List<(int x, int y)>> GetBoardStatus()
        {
            Dictionary<(int x, int y), List<(int x, int y)>> validBallAndDestinations = new Dictionary<(int x, int y), List<(int x, int y)>>();
            List<(int x, int y)> tempDestinationList;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Grid[i, j].Usable && !Grid[i, j].Empty)
                    {
                        tempDestinationList = validDestination(i, j);
                        if (tempDestinationList.Count != 0) validBallAndDestinations.Add((i, j), tempDestinationList);
                    }
                }
            }
            return validBallAndDestinations;
        }

        /// <summary>
        /// Given the coodinates of a ball, returns a list of valid desitnations where it can move.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private List<(int x, int y)> validDestination(int x, int y)
        {
            List<(int x, int y)> destinationList = new List<(int x, int y)>();
            // Test up
            if (y - 2 >= 0)
            {
                if ((Grid[x, y - 1].Usable && !Grid[x, y - 1].Empty) && (Grid[x, y - 2].Usable && Grid[x, y - 2].Empty)) destinationList.Add((x, y - 2));
            }
            // Test down
            if (y + 2 <= 6)
            {
                if ((Grid[x, y + 1].Usable && !Grid[x, y + 1].Empty) && (Grid[x, y + 2].Usable && Grid[x, y + 2].Empty)) destinationList.Add((x, y + 2));
            }
            // Test left
            if (x - 2 >= 0)
            {
                if ((Grid[x - 1, y].Usable && !Grid[x - 1, y].Empty) && (Grid[x - 2, y].Usable && Grid[x - 2, y].Empty)) destinationList.Add((x - 2, y));
            }
            // Test right
            if (x + 2 <= 6)
            {
                if ((Grid[x + 1, y].Usable && !Grid[x + 1, y].Empty) && (Grid[x + 2, y].Usable && Grid[x + 2, y].Empty)) destinationList.Add((x + 2, y));
            }
            return destinationList;
        }

        /// <summary>
        /// From a board status (dictionnary containing for each ball (key) a list of valid destination where it can be moved (value)), 
        /// choses the next ball to play and plays it.
        /// </summary>
        /// <param name="aBallAndDestinations"></param>
        private void PlayNextMove(Dictionary<(int x, int y), List<(int x, int y)>> aBallAndDestinations)
        {
            (int x, int y) startPos;
            (int x, int y) delPos;
            (int x, int y) endPos;

            startPos = new List<(int x, int y)>(aBallAndDestinations.Keys)[StaticRandom.Rand(0, aBallAndDestinations.Count)];
            endPos = aBallAndDestinations[startPos][StaticRandom.Rand(0, aBallAndDestinations[startPos].Count)];
            delPos = ((startPos.x + endPos.x) / 2, (startPos.y + endPos.y) / 2);

            if (aBallAndDestinations.Count > 1)
            {
                ChoiceNumber++;
                Probability *= (1.0 / (double)aBallAndDestinations.Count);
            }
            if (aBallAndDestinations[startPos].Count > 1)
            {
                ChoiceNumber++;
                Probability *= (1.0 / (double)aBallAndDestinations[startPos].Count);
            }

            StepList.Add(new Step(startPos, delPos, endPos));

            Grid[startPos.x, startPos.y].Empty = true;
            Grid[delPos.x, delPos.y].Empty = true;
            Grid[endPos.x, endPos.y].Empty = false;
        }

        /// <summary>
        /// Used in replay mode. Prints the gameboard, on a given step, with colors
        /// </summary>
        /// <param name="aStep">The step to print</param>
        public void PrintBoardColor(Step aStep)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == aStep.StartPos.x && j == aStep.StartPos.y)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    else if (i == aStep.DelPos.x && j == aStep.DelPos.y)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else if (i == aStep.EndPos.x && j == aStep.EndPos.y)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                    }
                    Console.Write(Grid[i, j].ToString());
                    Console.ResetColor();
                }
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Prints the different steps of a game, using console cursor repositioning.
        /// </summary>
        /// <param name="aStepList"></param>
        public void PrintBoardColorCursor(List<Step> aStepList)
        {
            Console.Clear();
            Console.WriteLine("\n\n" + this);
            Console.CursorVisible = false;
            Step oldStep = aStepList[0];
            foreach (Step step in aStepList)
            {
                Console.ReadKey();
                Console.ResetColor();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(step);
                Console.SetCursorPosition(oldStep.StartPos.y, oldStep.StartPos.x + 2);
                Console.Write('.');
                Console.SetCursorPosition(oldStep.DelPos.y, oldStep.DelPos.x + 2);
                Console.Write('.');
                Console.SetCursorPosition(oldStep.EndPos.y, oldStep.EndPos.x + 2);
                Console.Write('O');
                Console.SetCursorPosition(step.StartPos.y, step.StartPos.x + 2);
                ConsoleWriteColor(".",ConsoleColor.DarkRed);
                Console.SetCursorPosition(step.DelPos.y, step.DelPos.x + 2);
                ConsoleWriteColor(".", ConsoleColor.DarkGray);
                Console.SetCursorPosition(step.EndPos.y, step.EndPos.x + 2);
                ConsoleWriteColor("O", ConsoleColor.Red);
                oldStep = step;
            }
            Console.SetCursorPosition(0, 10);
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Wrties inline a specified string in a specified color.
        /// </summary>
        /// <param name="aOutput"></param>
        /// <param name="aColor"></param>
        private static void ConsoleWriteColor(string aOutput, ConsoleColor aColor)
        {
            Console.ForegroundColor = aColor;
            Console.Write(aOutput);
            Console.ResetColor();
        }

        override public string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    s.Append(Grid[i, j].ToString());
                }
                s.Append("\n");
            }
            return s.ToString();
        }
    }
;
}
