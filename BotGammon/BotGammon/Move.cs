using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class Move : IComparable
    {
        public Move(List<Tuple<int, int>> listeMovesIntermediaires)
        {
            moveA = listeMovesIntermediaires[0];
            if (listeMovesIntermediaires.Count >= 2)
                moveB = listeMovesIntermediaires[1];
            if (listeMovesIntermediaires.Count >= 3)
                moveC = listeMovesIntermediaires[2];
            if (listeMovesIntermediaires.Count >= 4)
                moveD = listeMovesIntermediaires[3];
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

        public Tuple<int, int> MoveA
        {
            get { return moveA; }
            set { moveA = value; }
        }

        public Tuple<int, int> MoveB
        {
            get { return moveB; }
            set { moveB = value; }
        }

        public Tuple<int, int> MoveC
        {
            get { return moveC; }
            set { moveC = value; }
        }

        public Tuple<int, int> MoveD
        {
            get { return moveD; }
            set { moveD = value; }
        }

        private Tuple<int, int> moveA;
        private Tuple<int, int> moveB;
        private Tuple<int, int> moveC;
        private Tuple<int, int> moveD;

        public int CompareTo(object obj)
        {
            // TODO si on veut faire du prunning.
            return 1;
        }
    }
}
