using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class ExpectiMiniMaxAlphaBeta : ExpectiMiniMax
    {
        private Heuristique heuristique;

        public ExpectiMiniMaxAlphaBeta()
        { 
            // TODO Call Factory instead
            heuristique = HeuristiqueFactory.Factory(Settings.HEURISTIC);
        }

        override public Move GetNextMove(Grille grille, int profondeur)
        {
            double valeurOptimal = Double.MinValue;
            Move moveOptimal = null;

            HashSet<Move> possibleMoves = grille.ListPossibleMoves();
            foreach (var possibleMove in possibleMoves)
            {
                Grille moveGrille = new Grille(grille);
                moveGrille.UpdateGrille(possibleMove);
                moveGrille.ReverseBoard();
                double valeurTest = Execute(moveGrille, profondeur - 1, valeurOptimal, Double.MaxValue);
                if (valeurTest > valeurOptimal)
                {
                    valeurOptimal = valeurTest;
                    moveOptimal = possibleMove;
                }
            }
            if (moveOptimal == null)
            {
                Console.WriteLine("WTF");
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

        // Stub method because we use header with alpha and beta vars
        override public double Execute(Grille grille, int profondeur)
        {
            return 0.0;
        }

        public double Execute(Grille grille, int profondeur, double alpha, double beta)
        {
            if (profondeur == 0) // on est au bout.
            {
                Grille grillePourEnnemi = new Grille(grille);
                grillePourEnnemi.ReverseBoard();
                double test = this.heuristique.Calculer(grille) - this.heuristique.Calculer(grillePourEnnemi);
                if (grille.player)
                {
                    return test;
                }
                else
                {
                    return -test;
                }

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
                        alpha = Math.Max(alpha, Execute(moveGrille, profondeur - 1, alpha, beta));
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
                        beta = Math.Min(beta, Execute(moveGrille, profondeur - 1, alpha, beta));
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
                foreach (var possDice in Player.possibleDiceRoll)
                {
                    Grille diceGrille = new Grille(grille);
                    diceGrille.dice = possDice.Item2;
                    value += possDice.Item1 * Execute(diceGrille, profondeur, alpha, beta);
                }
                return value;
            }
        }
    }
}
