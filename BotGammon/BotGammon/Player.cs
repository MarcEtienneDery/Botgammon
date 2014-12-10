using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		//TODO fonction de base du minimax qui va s'occuper de la profondeur itérative.
		//
        public Move GetNextMove(Grille grille, int profondeur)
        {
            double valeurOptimal = Double.MinValue;
		    Move moveOptimal = null;

            SortedSet<Move> possibleMoves = grille.ListPossibleMoves();
            foreach (var possibleMove in possibleMoves)
            {
                Grille moveGrille = new Grille(grille);
                moveGrille.UpdateGrille(possibleMove);
                moveGrille.player = !moveGrille.player;
                double valeurTest = ExpectiMinimax(moveGrille, profondeur - 1, valeurOptimal, Double.MaxValue);
                if (valeurTest > valeurOptimal)
                {
                    valeurOptimal = valeurTest;
                    moveOptimal = possibleMove;
                }
            }
            return moveOptimal;
   
        }

		//
		//TODO: le ExpectiMinimax
		//
        public double ExpectiMinimax(Grille grille, int profondeur, double alpha, double beta)
        {
            if (profondeur == 0) // on est au bout.
            {
                return Heuristique(grille);
            }

            if (grille.dice.Count > 0) // un joueur peut jouer.
            {
                if (grille.player) // on joue
                {
                    SortedSet<Move> possibleMoves = grille.ListPossibleMoves();
                    foreach (var possibleMove in possibleMoves)
                    {
                        Grille moveGrille = new Grille(grille);
                        moveGrille.UpdateGrille(possibleMove);
                        moveGrille.player = !moveGrille.player;
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
                    SortedSet<Move> possibleMoves = grille.ListPossibleMoves();
                    foreach (var possibleMove in possibleMoves)
                    {
                        Grille moveGrille = new Grille(grille);
                        moveGrille.UpdateGrille(possibleMove);
                        moveGrille.player = !moveGrille.player;
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
        private double Heuristique(Grille grille)
		{

		    return 0;

		}

        private readonly List<Tuple<double, List<int>>> possibleDiceRoll = new List<Tuple<double, List<int>>>();

    }
}
