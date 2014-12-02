using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class Grille
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
                dices[i] = Convert.ToInt32(parsing[38 + i]);
            }
        }

		//
		// TODO cette fonction retourne la liste de tous les moves possible pour la grille.
		//
		public SortedSet<Move> GetPossiblesMoves(){
			SortedSet<Move> moves = new SortedSet<Move> ();
			return moves;
		}

		//
		// TODO cette fonction effectue le move sur la grille.
		//
		public void DoMove(Move move){

		}

        public int[] board = new int[24]; // représentation du board
		public int[] dices = new int[2];  // la valeur des dés joués
		public int bar;                   // le nombre de pion hors jeu
		public int oppBar;                // le nombre de pion hors jeu de l'adversaire.
		public bool player;				  // si on est le premier joueur ( si faux, bar et oppBar sont inversé.)

    }
}
