using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class HeuristiqueSimple : Heuristique
    {
        public override double Calculer(Grille grille)
        {
            double valeurHeuristique = 0;
            double coefficient = 1.0;

            // parcours toute le board
            for (int i = 0; i < 24; i++)
            {
                if (grille.board[i] == 1) // si on a un pion non protegé
                {
                    valeurHeuristique -= 1 * coefficient;
                }
                else if (grille.board[i] >= 2) // si on a un pion protegé
                {
                    valeurHeuristique += 1 * coefficient * grille.board[i];
                }
                coefficient -= 0.01;
            }

            valeurHeuristique += grille.oppBar*1.3;

            // On check pour quel joueur on est.
            if (grille.player)
            {
                return valeurHeuristique;
            }
            return -valeurHeuristique;
        }
    }
}
