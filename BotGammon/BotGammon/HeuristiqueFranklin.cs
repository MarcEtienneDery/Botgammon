using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class HeuristiqueFranklin : Heuristique
    {
        override public double Calculer(Grille grille)
        {
            double valeurHeuristique = 0;

            // Aucune menace: http://i.imgur.com/ktuiqfY.jpg
            // Menace ennemie: http://i.imgur.com/oBM4sIj.jpg
            bool menaceEnnemie = false;
            // TODO: à vérifier pour l'ennemi (dans l'autre direction?)
            for (int i = 0; i < grille.board.Length; i++)
            {
                int nbPionsJoueur = 0;
                if (grille.board[i] < 0)
                {
                    menaceEnnemie = true;
                    break;
                }
                if (grille.board[i] > 0)
                {
                    nbPionsJoueur += grille.board[i];
                    if (nbPionsJoueur == 15)
                    {
                        break;
                    }
                }
            }

            // S'il n'y a aucune menace ennemie, c'est free-for-all
            if (menaceEnnemie)
            {
                int nbPairsColles = 0;
                double multiplicateurRecompense = 0;
                int nbGroupesPairs = 0;
                for (int i = 0; i < grille.board.Length; i++)
                {
                    // On pénalise tous les checkers non protégés
                    if (grille.board[i] == 1)
                    {
                        bool ennemiEnAvant = grille.EnnemiEnAvantDuPoint(i);

                        if (ennemiEnAvant)
                        {
                            // Plus pénalisant si on est proche de la fin
                            valeurHeuristique -= 2000 / (double)(i + 1);
                        }
                        // Moins grave s'il y a aucun checker ennemi en avant, mais quand même risqué
                        else
                        {
                            valeurHeuristique -= 200 / (double)(i + 1);
                        }
                    }

                    // On récompense les pairs (bloquent le point)
                    if (grille.board[i] >= 2)
                    {
                        if (i >= 1 && grille.board[i - 1] < 2)
                        {
                            nbPairsColles = 0;
                            nbGroupesPairs++;
                        }

                        bool ennemiEnAvant = grille.EnnemiEnAvantDuPoint(i);
                        nbPairsColles++;
                        if (ennemiEnAvant)
                        {
                            multiplicateurRecompense += 2 + (0.1 * i);
                        }
                        // Se trouve dans les 6 derniers points
                        if (i <= 5)
                        {
                            multiplicateurRecompense += 2 + (0.1 * i);
                        }
                    }
                }
                double valeurHeuristiquePairs = 0;
                switch (nbPairsColles)
                {
                    case 0:
                        break;
                    case 1:
                        valeurHeuristiquePairs += (5 * multiplicateurRecompense);
                        break;
                    case 2:
                        valeurHeuristiquePairs += (15 * multiplicateurRecompense);
                        break;
                    case 3:
                        valeurHeuristiquePairs += (40 * multiplicateurRecompense);
                        break;
                    case 4:
                        valeurHeuristiquePairs += (70 * multiplicateurRecompense);
                        break;
                    case 5:
                        valeurHeuristiquePairs += (110 * multiplicateurRecompense);
                        break;
                    case 6:
                        valeurHeuristiquePairs += (200 * multiplicateurRecompense);
                        break;
                    default:
                        valeurHeuristiquePairs += (200 * multiplicateurRecompense);
                        break;
                }
                valeurHeuristiquePairs *= nbGroupesPairs;
                valeurHeuristique += valeurHeuristiquePairs;
            }

            // Plus on peut manger de checkers, mieux c'est
            // TODO: Pondérer en fonction de la position du checker mangé (HOW!?)
            valeurHeuristique += 1000 * grille.bar;

            // Plus on peut bear-off (rentrer) de checkers, mieux c'est
            valeurHeuristique += grille.GetNbPionsJoueurRentres() * 10000;

            if (grille.player)
            {
                return valeurHeuristique;
            }
            return -valeurHeuristique;
        }
    }
}
