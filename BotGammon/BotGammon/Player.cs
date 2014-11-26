using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class Player : IPlayer
    {
        public Move GetNextMove(Position pos)
        {


            return new Move(1,2);
        }

        public Move ExpectMinimax(Position pos, int profondeur, int action)
        {

            if (profondeur == 0)
            {
                //Need ajout fonction heuristique
                return null;
            }

            if (action == 1)
            {

                
            }
            else if (action == 2)
            {
                
            }
            else if (action == 3)
            {
                for (int i = 1; i < 7; i++)
                {
                    for (int j = i; j < 7; j++)
                    {

                    }
                }
            }
            return null;

        }

        public int[] ProbabilityDice()
        {
            var listeDice = new List<Tuple<int, int>>();



            return null;
        }



    }
}
