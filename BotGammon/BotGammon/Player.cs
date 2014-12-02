using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class Player : IPlayer
    {
		//
		//TODO fonction de base du minimax qui va s'occuper de la profondeur itérative.
		//
		public Move GetNextMove(Grille pos, int depth)
        {


            return new Move(1,2);
        }

		//
		//TODO: le ExpectMinimax, dom, tu expliquera ta variable action! :)
		//
        public Move ExpectMinimax(Grille pos, int profondeur, int action)
        {

			// TODO pour commencer, on fait une fonction qui retourne un move random
			// donc grille.getPossiblesMoves() et on prend un random.

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

		//
		// TODO faire un fonction qui va calculer un heuristique pour la grille.
		//
		private int heuristique(Grille grille){

		}



    }
}
