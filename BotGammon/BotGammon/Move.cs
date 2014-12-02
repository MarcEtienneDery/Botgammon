using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class Move
    {
        public Move(int a, int newA)
        {
            moveA = new Tuple<int, int>(a, newA);
        }

        public Move(int a, int newA, int b, int newB)
        {
            moveA = new Tuple<int, int>(a, newA);
            moveB = new Tuple<int, int>(b, newB);
        }

        public Move(int a, int newA, int b, int newB, int c, int newC)
        {
            moveA = new Tuple<int, int>(a, newA);
            moveB = new Tuple<int, int>(b, newB);
            moveC = new Tuple<int, int>(c, newC);
        }

        public Move(int a, int newA, int b, int newB, int c, int newC, int d, int newD)
        {
            moveA = new Tuple<int, int>(a, newA);
            moveB = new Tuple<int, int>(b, newB);
            moveC = new Tuple<int, int>(c, newC);
            moveD = new Tuple<int, int>(d, newD);
        }

		//
		// retourne la string qui représente le move pour gnubg.
		//
        public string getCmd()
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
