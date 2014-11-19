using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class Position
    {
        public Position(String snowie)
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

        private int[] board = new int[24]; // représentation du board
        private int[] dices = new int[2];  // la valeur des dés jouer
        private int bar;                   // le nombre de pion hors jeu
        private int oppBar;                // le nombre de pion hors jeu de l'adversaire.

    }
}
