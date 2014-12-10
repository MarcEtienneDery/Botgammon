using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class Move
    {
        public Move(List<Tuple<int, int>> listeMovesIntermediaires)
        {

            moveA = listeMovesIntermediaires[0];
            if (listeMovesIntermediaires[1] == null)
                moveB = listeMovesIntermediaires[1];
            if (listeMovesIntermediaires[2] == null)
                moveA = listeMovesIntermediaires[2];
            if (listeMovesIntermediaires[3] == null)
                moveB = listeMovesIntermediaires[3];
        }

		//
		// retourne la string qui représente le move pour gnubg.
		//
        public string GetCmd()
        {
            string cmd = "move ";
            if (moveA != null)
            {
                cmd += moveA.Item1 + "/" + moveA.Item2 + " ";
            }
            if (moveB != null)
            {
                cmd += moveB.Item1 + "/" + moveB.Item2 + " ";
            }
            if (moveC != null)
            {
                cmd += moveC.Item1 + "/" + moveC.Item2 + " ";
            }
            if (moveD != null)
            {
                cmd += moveD.Item1 + "/" + moveD.Item2 + " ";
            }
            return cmd;
        }

        private Tuple<int, int> moveA;
        private Tuple<int, int> moveB;
        private Tuple<int, int> moveC;
        private Tuple<int, int> moveD;
    }
}
