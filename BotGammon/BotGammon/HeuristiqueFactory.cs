using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class HeuristiqueFactory
    {
        public static Heuristique Factory(string heuristique)
        {
            Heuristique h = new HeuristiqueFranklin();
            
            // Possibilite d'ajout d'heuristiques ici

            if (heuristique == "HeuristiqueFranklin")
            {
                h = new HeuristiqueFranklin();
                Console.WriteLine("Using Frank's Evaluation Function...");
            }
            else if (heuristique == "HeuristiqueSimple")
            {
                h = new HeuristiqueSimple();
                Console.WriteLine("Using Simpler Evaluation Function...");
            }

            return h;
        }
    }
}
