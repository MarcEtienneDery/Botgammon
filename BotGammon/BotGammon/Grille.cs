using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    internal class Grille
    {
        //
        // constructeur de grille à partir du parsing du fichier snowie.
        //
        public Grille(String snowie)
        {
            string[] parsing = snowie.Split(';');
            bar = Convert.ToInt32(parsing[12]);
            oppBar = Convert.ToInt32(parsing[37]);
            for (int i = 0; i < 24; i++)
            {
                board[i] = Convert.ToInt32(parsing[13 + i]);
            }
            for (int i = 0; i < 2; i++)
            {
                dice[i] = Convert.ToInt32(parsing[38 + i]);
            }
            if (dice[0] == dice[1]) // on a des double
            {
                dice[2] = dice[3] = dice[0];
            }
        }

        //
        // constructeur de grille à partir du parsing du fichier snowie.
        //
        public Grille(Grille grille)
        {
            grille.board.CopyTo(board,0);
            dice = new List<int>(grille.dice);
            bar = grille.bar;
            oppBar = grille.oppBar;
            player = grille.player;
        }

        //
        // si on est rendu à la dernière phase de jeu ou l'on doit vider nos pions.
        //
        public bool isFinalStage()
        {
            if (player)
            {
                if (bar > 0) // si le bar n'est pas vide
                {
                    return false;
                }
                for (int i = 5; i < 24; i++) // si on trouve des pion encore en jeu.
                {
                    if (board[i] > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //
        // TODO Effectue le move sur la grille et retourne la grille
        //
        public void UpdateGrille(Move move)
        {          
            //On transfert les checkers sur le board et on supprime le dé de la liste des dés
            if (move.MoveA != null)
            {
                applyMoveRemoveDice(move.MoveA);
            }
            if (move.MoveB != null)
            {
                applyMoveRemoveDice(move.MoveB);
            }
            if (move.MoveC != null)
            {
                applyMoveRemoveDice(move.MoveC);
            }
            if (move.MoveD != null)
            {
                applyMoveRemoveDice(move.MoveD);
            }  
            
        }

        //
        //Applique une move au board tout dependant si on est le joueur + ou - et enlève le dé utilisé de la liste de dé
        //
        public void applyMoveRemoveDice(Tuple<int, int> tuple)
        {
            //On enlève le dé de la liste des dés
            dice.Remove(Math.Abs(tuple.Item2 - tuple.Item1));

            if (player)
            {

                //Si notre move proviens du bar
                if (tuple.Item1 == 25)
                {
                    bar--;
                    //Si on s'apprette a manger un checker ennemi en sortant du bar
                    if (board[tuple.Item2 - 1] == -1)
                    {
                        board[tuple.Item2 - 1] = 1;
                        oppBar++;
                    }
                    else
                    {
                        board[tuple.Item2 - 1]++;
                    }

                }
                //Si on s'apprette a manger un checker ennemi
                else if (board[tuple.Item2 - 1] == -1)
                {
                    board[tuple.Item1 - 1]--;
                    board[tuple.Item2 - 1] = 1;
                    oppBar++;
                }
                //coup normal
                else
                {
                    board[tuple.Item1 - 1]--;
                    board[tuple.Item2 - 1]++;
                }

            }
            else
            {
                //Si notre move proviens du bar
                if (tuple.Item1 == 0)
                {
                    oppBar--;
                    //Si on s'apprette a manger un checker ennemi en sortant du bar
                    if (board[tuple.Item2 - 1] == 1)
                    {
                        board[tuple.Item2 - 1] = -1;
                        bar++;
                    }
                    else
                    {
                        board[tuple.Item2 - 1]--;
                    }

                }
                //Si on s'apprette a manger un checker ennemi
                else if (board[tuple.Item2 - 1] == 1)
                {
                    board[tuple.Item1 - 1]++;
                    board[tuple.Item2 - 1] = -1;
                    oppBar++;
                }
                //coup normal
                else
                {
                    board[tuple.Item1 - 1]++;
                    board[tuple.Item2 - 1]--;
                }
            }
        }

        //
        // transforme le tuple en move et call la function UpdateGrille
        //
        public void UpdateGrille(Tuple<int,int> tuple)
        {
            var listTuple = new List<Tuple<int, int>>();
            listTuple.Add(tuple);
            var move = new Move(listTuple);
            UpdateGrille(move);
        }

        //
        // Cette fonction va retourner la liste de toute les moves possible de cette grille.
        //
        public SortedSet<Move> ListPossibleMoves()
        {
            listPossibleMoves = new SortedSet<Move>();
            GetPossibleMoves(this, new List<Tuple<int, int>>());
            return listPossibleMoves;
        }

        //
        //  Cette fonction retourne la liste de tous les moves possible pour la grille.
        //
        private void GetPossibleMoves(Grille grille, List<Tuple<int,int>> listeMoves)
        {
            var moves = new SortedSet<Move>();
            bool foundPossibleMove = false;
            for (int i = 0; i < grille.dice.Count; i++)
            {
                var possibleMovesForDie = GetPossibleMovesForDie(grille, grille.dice[i]);
                if (possibleMovesForDie.Count != 0)
                {
                    foundPossibleMove = true;
                    foreach (var move in possibleMovesForDie)
                    {
                        Grille moveGrille = new Grille(grille);
                        moveGrille.UpdateGrille(move);
                        List<Tuple<int,int>> moveListe = new List<Tuple<int, int>>(listeMoves);
                        moveListe.Add(move);
                        GetPossibleMoves(moveGrille, moveListe);
                    }
                }
            }

            if (!foundPossibleMove)// on est dans une feuille, on ajoute le move a la liste.
            {
                listPossibleMoves.Add(new Move(listeMoves));
            }

        }

        //
        // Retourne la liste des moves possibles pour un dé
        //
        private SortedSet<Tuple<int, int>> GetPossibleMovesForDie(Grille grille, int die)
        {
            var moves = new SortedSet<Tuple<int, int>>();
            //si l'on doit vider le bar.
            if (grille.bar != 0)
            {
                if (grille.board[25 - die] >= 0)
                {
                    moves.Add(new Tuple<int, int>(25, 25 - die));
                }
            }
            else if (grille.isFinalStage())  // si on est rendu a vider la planche.
            {
                for (int i = 0; i < 5; i++)
                {
                    if (grille.board[i] > 0)
                    {
                        if (i - die < 0)
                        {
                            moves.Add(new Tuple<int, int>(i + 1, 0));
                        }
                        else if (grille.board[i - die] >= -1) 
                        {
                            moves.Add(new Tuple<int, int>(i + 1, i + 1 - die));
                        }
                    }
                }
            }
            else // on peut jouer n'importequoi
            {
                for (int i = 0; i < 24; i++)
                {
                    if (grille.board[i] > 0 && i - die >= 0 && grille.board[i - die] >= -1) //on peut jouer sur cette case.
                    {
                        moves.Add(new Tuple<int, int>(i + 1, i + 1 - die));
                    }
                }
            }
            return moves;
        }

        // TODO: checker pour l'ennemi aussi (dans l'autre direction?)
        public bool EnnemiEnAvantDuPoint(int point)
        {
            for (int i = 0; i < point; i++)
            {
                if (board[i] < 0)
                {
                    return true;
                }
            }
            return false;
        }

        // TODO: checker pour l'ennemi aussi
        public int GetNbPionsJoueurRentres()
        {
            int nbPionsJoueur = board.Where(t => t > 0).Sum();
            return 15 - nbPionsJoueur;
        }


        public int[] board = new int[24]; // représentation du board
        public List<int> dice = new List<int>(); // la valeur des dés joués
        public int bar; // le nombre de pion hors jeu
        public int oppBar; // le nombre de pion hors jeu de l'adversaire.
        public bool player; // si on est le premier joueur ( si faux, bar et oppBar sont inversé.)
        private SortedSet<Move> listPossibleMoves;

    }
}
