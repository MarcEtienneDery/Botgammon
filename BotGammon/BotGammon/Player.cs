using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotGammon
{
    class Player
    {
        public Player()
        {
            for (int i = 1; i <= 6; i++)
            {
                for (int j = i; j <= 6; j++)
                {
                    List<int> dices = new List<int>();
                    dices.Add(i);
                    dices.Add(j);
                    if (i == j)
                    {
                        dices.Add(i);
                        dices.Add(j);
                        possibleDiceRoll.Add(new Tuple<double, List<int>>(1.0 / 36.0, dices));
                    }
                    else
                    {
                        possibleDiceRoll.Add(new Tuple<double, List<int>>(2.0 / 36.0, dices));
                    }
                }
            }

            // Load the right algorithm with settings
            minimax = ExpectiMiniMaxFactory.Factory(Settings.ALGORITHM);
        }

        public static readonly List<Tuple<double, List<int>>> possibleDiceRoll = new List<Tuple<double, List<int>>>();
        public ExpectiMiniMax minimax;

    }
}
