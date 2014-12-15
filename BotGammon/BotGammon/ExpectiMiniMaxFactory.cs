using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class ExpectiMiniMaxFactory
    {
        public static ExpectiMiniMax Factory(string algorithm)
        {
            // Default case
            ExpectiMiniMax algo = new ExpectiMiniMaxSimple();
            
            if (algorithm == "ExpectiMiniMaxSimple")
            {
                algo = new ExpectiMiniMaxSimple();
                Console.WriteLine("Using ExpectiMiniMax...");
            }
            else if (algorithm == "ExpectiMiniMaxAlphaBeta")
            {
                algo = new ExpectiMiniMaxAlphaBeta();
                Console.WriteLine("Using ExpectiMiniMax with alpha-beta pruning...");
            }
            else if (algorithm == "ExpectiMiniMaxIterSimple")
            {
                algo = new ExpectiMiniMaxIterSimple();
                Console.WriteLine("Using Iterative Deepening ExpectiMiniMax...");
            }
            else if (algorithm == "ExpectiMiniMaxIterAlphaBeta")
            {
                algo = new ExpectiMiniMaxIterAlphaBeta();
                Console.WriteLine("Using Iterative Deepening ExpectiMiniMax with alpha-beta pruning...");
            }
            return algo;
        }
    }
}
