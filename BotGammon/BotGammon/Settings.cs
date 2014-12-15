using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    class Settings
    {
        // Gameplay Settings
        public const int TOTAL_GAMES = 200;

        // Artificial Intelligence Settings
            public const int DEPTH = 1;
            /* Possible Heuristics:
             * HeuristiqueSimple
             * HeuristiqueFranklin
             * */
            public const string HEURISTIC = "HeuristiqueFranklin";

            /* Possible Algorithms:
             * ExpectiMiniMaxSimple
             * ExpectiMiniMaxAlphaBeta
             * ExpectiMiniMaxIterSimple
             * ExpectiMiniMaxIterAlphaBeta
             * */
            public const string ALGORITHM = "ExpectiMiniMaxSimple";
            public const int TIME_TO_MOVE = 5; // miliseconds

        // Statistical Settings
        public const bool MESURE_MOVE_TIME = false;

    }
}
