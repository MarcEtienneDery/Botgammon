﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BotGammon
{
    class ExpectiMiniMaxSimple : ExpectiMiniMax
    {
        private Heuristique heuristique;

        public ExpectiMiniMaxSimple()
        { 
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
                double valeurTest = Execute(moveGrille, profondeur - 1);
                if (valeurTest > valeurOptimal)
                {
                    valeurOptimal = valeurTest;
                    moveOptimal = possibleMove;
                }
            }
            return moveOptimal;
        }
        override public double Execute(Grille grille, int profondeur)
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
                    double value = double.MinValue;
                    foreach (var possibleMove in possibleMoves)
                    {
                        Grille moveGrille = new Grille(grille);
                        moveGrille.UpdateGrille(possibleMove);
                        moveGrille.ReverseBoard();
                        value = Math.Max(value, Execute(moveGrille, profondeur - 1));
                    }
                    return value;
                }
                else //l'adversaire joue.
                {
                    HashSet<Move> possibleMoves = grille.ListPossibleMoves();
                    double value = double.MaxValue;
                    foreach (var possibleMove in possibleMoves)
                    {
                        Grille moveGrille = new Grille(grille);
                        moveGrille.UpdateGrille(possibleMove);
                        moveGrille.ReverseBoard();
                        value = Math.Max(value, Execute(moveGrille, profondeur - 1));
                    }
                    return value;
                }
            }
            else // on est dans notre cas random.
            {
                double value = 0;
                double[] values = new double[Player.possibleDiceRoll.Count + 1];
                Thread[] threads = new Thread[Player.possibleDiceRoll.Count + 1];
                int i = 0;

                for (i = 0; i < Player.possibleDiceRoll.Count; i++)
                {
                    Grille diceGrille = new Grille(grille);
                    diceGrille.dice = Player.possibleDiceRoll[i].Item2;
                    int num = i;
                    threads[i] = new Thread(delegate()
                    {
                        values[num] = Player.possibleDiceRoll[num].Item1 * Execute(diceGrille, profondeur);
                    });
                    threads[i].Start();
                }

                for (int j = 0; j < Player.possibleDiceRoll.Count; j++)
                {
                    threads[j].Join();
                    value += values[j];
                }
                return value;
            }
        }
    }
}
