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
            step = 0;
            diceUsed = 1;
            moveA = listeMovesIntermediaires[0];
            step += calcStep(listeMovesIntermediaires[0]);
            if (listeMovesIntermediaires.Count >= 2)
            {
                moveB = listeMovesIntermediaires[1];
                step += calcStep(listeMovesIntermediaires[1]);
                diceUsed++;
            }
            if (listeMovesIntermediaires.Count >= 3)
            {
                moveC = listeMovesIntermediaires[2];
                step += calcStep(listeMovesIntermediaires[2]);
                diceUsed++;
            }
            if (listeMovesIntermediaires.Count >= 4)
            {
                moveD = listeMovesIntermediaires[3];
                step += calcStep(listeMovesIntermediaires[3]);
                diceUsed++;
            }
        }



		//
		// retourne la string qui représente le move pour gnubg.
		//
        public string GetCmd()
        {
            string cmd = "move ";
            if (moveA != null)
            {
                cmd += moveA.Item1 + "/";
                if (moveA.Item2 <= 0) //TODO traiter le cas plus grand
                {
                    cmd += "off ";
                }
                else
                {
                    cmd += moveA.Item2 + " ";
                }
            }
            if (moveB != null)
            {
                cmd += moveB.Item1 + "/";
                if (moveB.Item2 <= 0)
                {
                    cmd += "off ";
                }
                else
                {
                    cmd += moveB.Item2 + " ";
                }
            }
            if (moveC != null)
            {
                cmd += moveC.Item1 + "/";
                if (moveC.Item2 <= 0)
                {
                    cmd += "off ";
                }
                else
                {
                    cmd += moveC.Item2 + " ";
                }
            }
            if (moveD != null)
            {
                cmd += moveD.Item1 + "/";
                if (moveD.Item2 <= 0)
                {
                    cmd += "off ";
                }
                else
                {
                    cmd += moveD.Item2 + " ";
                }
            }
            return cmd;
        }

        private int calcStep(Tuple<int, int> move)
        {
            int step = move.Item1;
            if (move.Item2 > 0)
            {
                step -= move.Item2;
            }
            return step;
        }

        public Tuple<int, int> MoveA
        {
            get { return moveA; }
        }

        public Tuple<int, int> MoveB
        {
            get { return moveB; }
        }

        public Tuple<int, int> MoveC
        {
            get { return moveC; }
        }

        public Tuple<int, int> MoveD
        {
            get { return moveD; }
        }

        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        public int DiceUsed
        {
            get { return diceUsed; }
            set { diceUsed = value; }
        }

        private readonly Tuple<int, int> moveA;
        private readonly Tuple<int, int> moveB;
        private readonly Tuple<int, int> moveC;
        private readonly Tuple<int, int> moveD;
        private int step;
        private int diceUsed;

        //public int CompareTo(object obj)
        //{
        //    if (obj == null) return 1;

        //    Move autreMove = obj as Move;
        //    if (autreMove != null)
        //    {
        //        if (moveA.Item1 == autreMove.moveA.Item1 && moveA.Item2 == autreMove.moveA.Item2 &&
        //            moveB.Item1 == autreMove.moveB.Item1 && moveB.Item2 == autreMove.moveB.Item2 &&
        //            moveC.Item1 == autreMove.moveC.Item1 && moveC.Item2 == autreMove.moveC.Item2 &&
        //            moveD.Item1 == autreMove.moveD.Item1 && moveD.Item2 == autreMove.moveD.Item2)
        //        {
        //            return 0;
        //        }
                
        //    }
        //    return -1;
        //}

        protected bool Equals(Move other)
        {
            return Equals(moveA, other.moveA) && Equals(moveB, other.moveB) && Equals(moveC, other.moveC) && Equals(moveD, other.moveD);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Move) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (moveA != null ? moveA.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (moveB != null ? moveB.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (moveC != null ? moveC.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (moveD != null ? moveD.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
