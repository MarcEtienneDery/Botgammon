using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class ExpectiMiniMaxSimple : ExpectiMiniMax
    {
        override public Move GetNextMove(Grille grille, int profondeur)
        {
            // TODO code this lol
            return new Move(new List<Tuple<int, int>>());
        }
        override public double Execute(Grille grille, int profondeur)
        {
            return 0.0;
        }
    }
}
