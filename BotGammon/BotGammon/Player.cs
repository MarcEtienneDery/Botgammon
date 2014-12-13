using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotGammon
{
    class Player : IPlayer
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
                        possibleDiceRoll.Add(new Tuple<double, List<int>>(1/36, dices));
                    }
                    else
                    {
                        possibleDiceRoll.Add(new Tuple<double, List<int>>(2 / 36, dices));
                    }
                }
            }
        }

		//
		//fonction de base du minimax qui va s'occuper de la profondeur itérative.
		//
        public Move GetNextMove(Grille grille, int profondeur)
        {
            double valeurOptimal = Double.MinValue;
		    Move moveOptimal = null;

            HashSet<Move> possibleMoves = grille.ListPossibleMoves();
            foreach (var possibleMove in possibleMoves)
            {
                Grille moveGrille = new Grille(grille);
                moveGrille.UpdateGrille(possibleMove);
                moveGrille.ReverseBoard();
                double valeurTest = ExpectiMinimax(moveGrille, profondeur - 1, valeurOptimal, Double.MaxValue);
                if (valeurTest > valeurOptimal)
                {
                    valeurOptimal = valeurTest;
                    moveOptimal = possibleMove;
                }
            }
            return moveOptimal;
            //Random random = new Random();
            //int num = random.Next(possibleMoves.Count);
            //foreach (var move in possibleMoves)
            //{
            //    if (num <= 0)
            //    {
            //        return move;
            //    }
            //    else
            //    {
            //        num --;
            //    }
            //}
            //return possibleMoves.Max;
        }

		//
		//TODO: le ExpectiMinimax
		//
        public double ExpectiMinimax(Grille grille, int profondeur, double alpha, double beta)
        {
            if (profondeur == 0) // on est au bout.
            {
                Grille grillePourEnnemi = new Grille(grille);
                grillePourEnnemi.ReverseBoard();
                return Heuristique(grille) - Heuristique(grillePourEnnemi);
            }

            if (grille.dice.Count > 0) // un joueur peut jouer.
            {
                if (grille.player) // on joue
                {
                    HashSet<Move> possibleMoves = grille.ListPossibleMoves();
                    foreach (var possibleMove in possibleMoves)
                    {
                        Grille moveGrille = new Grille(grille);
                        moveGrille.UpdateGrille(possibleMove);
                        moveGrille.ReverseBoard();
                        alpha = Math.Max(alpha, ExpectiMinimax(moveGrille, profondeur - 1, alpha, beta));
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                    return alpha;
                }
                else //l'adversaire joue.
                {
                    HashSet<Move> possibleMoves = grille.ListPossibleMoves();
                    foreach (var possibleMove in possibleMoves)
                    {
                        Grille moveGrille = new Grille(grille);
                        moveGrille.UpdateGrille(possibleMove);
                        moveGrille.ReverseBoard();
                        beta = Math.Min(beta, ExpectiMinimax(moveGrille, profondeur - 1, alpha, beta));
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                    return beta;
                }
            }
            else // on est dans notre cas random.
            {
                double value = 0;
                foreach (var possDice in possibleDiceRoll)
                {
                    Grille diceGrille = new Grille(grille);
                    diceGrille.dice = possDice.Item2;
                    value += possDice.Item1 * ExpectiMinimax(diceGrille, profondeur, alpha, beta);
                }
                return value;
            }
        }

		//
		// TODO faire un fonction qui va calculer un heuristique pour la grille.
		//
        private static double Heuristique(Grille grille)
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
                            valeurHeuristique -= 2000 / (double)(i+1);
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
                            nbGroupesPairs ++;
                        }

                        bool ennemiEnAvant = grille.EnnemiEnAvantDuPoint(i);
                        nbPairsColles++;
                        if (ennemiEnAvant)
                        {
                            multiplicateurRecompense += 2 + (0.1*i);
                        }
                        // Se trouve dans les 6 derniers points
                        if (i <= 5)
                        {
                            multiplicateurRecompense += 2 + (0.1*i);
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
            valeurHeuristique += 1000*grille.bar;

            // Plus on peut bear-off (rentrer) de checkers, mieux c'est
            valeurHeuristique += grille.GetNbPionsJoueurRentres() * 10000;

            if (grille.player)
            {
                return valeurHeuristique;
            }
            return -valeurHeuristique;
        }

        private readonly List<Tuple<double, List<int>>> possibleDiceRoll = new List<Tuple<double, List<int>>>();

    }
}
