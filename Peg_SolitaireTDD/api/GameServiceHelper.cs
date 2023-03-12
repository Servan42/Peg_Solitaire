using Peg_SolitaireTDD.Model;
using Peg_SolitaireTDD.spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Peg_SolitaireTDD.api
{
    public static class GameServiceHelper
    {
        /// <summary>
        /// After 36 moves, there should be only one ball left on the board.
        /// </summary>
        public static readonly int MAX_MOVES_IN_A_GAME = 36;
        public static readonly string MOVE_TOWARDS_I = "i";
        public static readonly string MOVE_TOWARDS_MINUS_I = "-i";
        public static readonly string MOVE_TOWARDS_J = "j";
        public static readonly string MOVE_TOWARDS_MINUS_J = "-j";

        public static List<(int i, int j)> GetValidBoardPositionList()
        {
            List<(int i, int j)> validBoardPositionList = new();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (!((i == 0 && j == 0)
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
                        || (i == 6 && j == 6)))
                    {
                        validBoardPositionList.Add((i, j));
                    }
                }
            }
            return validBoardPositionList;
        }

        public static (int i, int j) GenerateRandomValidInitialPosition()
        {
            var validBoardPositionList = GetValidBoardPositionList();
            return validBoardPositionList[StaticRandom.Rand(0, validBoardPositionList.Count)];
        }

        public static (int i, int j, string orientation) GenerateRandomValidDestinationPositionFromInitialPosition((int i, int j) initialPosition)
        {
            var validBoardPositionList = GetValidBoardPositionList();

            Dictionary<string, (int i, int j, string orientation)> validOrientations = new()
            {
                { MOVE_TOWARDS_I, (initialPosition.i+2,initialPosition.j, MOVE_TOWARDS_I) },
                { MOVE_TOWARDS_J, (initialPosition.i,initialPosition.j+2, MOVE_TOWARDS_J) },
                { MOVE_TOWARDS_MINUS_I, (initialPosition.i-2,initialPosition.j, MOVE_TOWARDS_MINUS_I) },
                { MOVE_TOWARDS_MINUS_J, (initialPosition.i,initialPosition.j-2, MOVE_TOWARDS_MINUS_J) }
            };


            if (!validBoardPositionList.Contains((initialPosition.i + 2, initialPosition.j)))
            {
                validOrientations.Remove(MOVE_TOWARDS_I);
            }
            if (!validBoardPositionList.Contains((initialPosition.i - 2, initialPosition.j)))
            {
                validOrientations.Remove(MOVE_TOWARDS_MINUS_I);
            }
            if (!validBoardPositionList.Contains((initialPosition.i, initialPosition.j + 2)))
            {
                validOrientations.Remove(MOVE_TOWARDS_J);
            }
            if (!validBoardPositionList.Contains((initialPosition.i, initialPosition.j - 2)))
            {
                validOrientations.Remove(MOVE_TOWARDS_MINUS_J);
            }

            return validOrientations.ElementAt(StaticRandom.Rand(0, validOrientations.Count)).Value;
        }
    }
}
