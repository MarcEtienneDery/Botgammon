using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    abstract class ExpectiMiniMax
    {
        abstract public double Execute(Grille grille, int profondeur);
        abstract public Move GetNextMove(Grille pos, int depth);
    }
}
