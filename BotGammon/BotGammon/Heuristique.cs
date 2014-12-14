using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    abstract class Heuristique
    {
        abstract public double Calculer(Grille grille);
    }
}
