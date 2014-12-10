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
        // constructeur de grille à partir du parsing du fichier snowie.
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
            // On a des doubles!
            isDouble = dice[0] == dice[1];
        }

        public bool IsBlocked(int point)
        {
            return board[point] >= 2;
        }

        // Effectue le move sur la grille et retourne la grille
        public Grille UpdateGrille(Grille oldGrille, Move move)
        {
            Grille newGrille;
            return newGrille;
        }

        // Retourne la liste des moves possibles pour un dé
        public SortedSet<Move> GetPossibleMovesForDie(Grille grille, int die)
        {
            var moves = new SortedSet<Move>();

            //    if bar: on doit jouer le bar
            //    si pas bar, on itere sur toute les points de notre joueurs, et on check si on peut jouer.

            return moves;
        }

        //
        // TODO cette fonction retourne la liste de tous les moves possible pour la grille.
        //
        public SortedSet<Move> GetPossibleMoves(Grille grille, int[] des, List<Move> listeMoves)
        {
            var moves = new SortedSet<Move>();
            bool foundPossibleMove = false;
            for (int i = 0; i < des.Length; i++)
            {
                var possibleMovesForDie = GetPossibleMovesForDie(grille, des[i]);
                if (possibleMovesForDie.Count != 0)
                {
                    foundPossibleMove = true;
                }
            }

//            var movesIntermediaires = new List<Tuple<int, int>>();
//            
//            // Si on a au moins un checker de mangé
//            if (bar != 0)
//            {
//                List<int> dicesLeft = dice;
//                if (isDouble)
//                {
//                    // On ne peut rien jouer
//                    if (IsBlocked(25 - dice[0]))
//                    {
//                        return null;
//                    }
//                    var listeAMettreDansMoves = new List<int>();
//                    // On vide le bar tant qu'il reste des dés
//                    while (bar != 0 || dicesLeft.Count != 0)
//                    {
//                        var singleMove = new Tuple<int, int>(25, 25 - dice[0]);
//                        movesIntermediaires.Add(singleMove);
//                        dicesLeft.RemoveAt(0);
//                        
//                        foreach (var move in movesIntermediaires)
//                        {
//                            listeAMettreDansMoves.Add(move.Item1);
//                            listeAMettreDansMoves.Add(move.Item2);
//                        }
//                    }
//                    // S'il reste des checkers sur le bar ou si on a fini de jouer, on retourne la liste
//                    if (bar != 0 || (bar == 0 && dicesLeft.Count == 0))
//                    {
//                        Move move = null;
//                        // Façon cancéreuse d'envoyer le move :(
//                        switch (dicesLeft.Count)
//                        {
//                            case 0:
//                                move = new Move(listeAMettreDansMoves[0], listeAMettreDansMoves[1], listeAMettreDansMoves[2],
//                                    listeAMettreDansMoves[3], listeAMettreDansMoves[4], listeAMettreDansMoves[5],
//                                    listeAMettreDansMoves[6], listeAMettreDansMoves[7]);
//                                break;
//                            case 1:
//                                move = new Move(listeAMettreDansMoves[0], listeAMettreDansMoves[1], listeAMettreDansMoves[2],
//                                    listeAMettreDansMoves[3], listeAMettreDansMoves[4], listeAMettreDansMoves[5]);
//                                break;
//                            case 2:
//                                move = new Move(listeAMettreDansMoves[0], listeAMettreDansMoves[1], listeAMettreDansMoves[2],
//                                    listeAMettreDansMoves[3]);
//                                break;
//                            case 3:
//                                move = new Move(listeAMettreDansMoves[0], listeAMettreDansMoves[1]);
//                                break;
//                        }
//                        moves.Add(move);
//                        return moves;
//                    }
//                    // Il reste 1 dé à jouer
//                    // TODO: Mettre ça plus générique peu importe le nombre de dés restants
//                    if (dicesLeft.Count == 1)
//                    {
//                        // Pour tous les points du board
//                        foreach (var point in board)
//                        {
//                            var movesIntermediairesTemp = movesIntermediaires;
//                            // Si le point ne contient pas de checker joueur on skip
//                            if (board[point] <= 0) continue;
//                            int newPoint = point - dicesLeft[0];
//
//                            // Si le move restant est infaisable ou dépasse le board on skip
//                            if ((IsBlocked(newPoint)) || (newPoint <= 0)) continue;
//                            movesIntermediairesTemp.Add(new Tuple<int, int>(point, newPoint));
//                            listeAMettreDansMoves.Add(point);
//                            listeAMettreDansMoves.Add(newPoint);
//
//                            Move move = new Move(listeAMettreDansMoves[0], listeAMettreDansMoves[1],
//                                listeAMettreDansMoves[2], listeAMettreDansMoves[3],
//                                listeAMettreDansMoves[4], listeAMettreDansMoves[5],
//                                listeAMettreDansMoves[6], listeAMettreDansMoves[7]);
//                            moves.Add(move);
//
//                            listeAMettreDansMoves.RemoveAt(6);
//                            listeAMettreDansMoves.RemoveAt(7);
//                        }
//                    }
//                    // Il reste 2 ou 3 dés à jouer (va remplacer le if d'en haut
//                    else
//                    {
//                        
//                    }
//                }
//                else
//                {
//                    
//                }
//            }




            return moves;
        }

        //
        // TODO cette fonction effectue le move sur la grille.
        //
        public void DoMove(Move move)
        {

        }

        public int[] board = new int[24]; // représentation du board
        public List<int> dice = new List<int>(); // la valeur des dés joués
        public bool isDouble;
        public int bar; // le nombre de pion hors jeu
        public int oppBar; // le nombre de pion hors jeu de l'adversaire.
        public bool player; // si on est le premier joueur ( si faux, bar et oppBar sont inversé.)

    }
}
