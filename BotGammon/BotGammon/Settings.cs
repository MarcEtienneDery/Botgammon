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
        public const int TOTAL_GAMES = 10;

        // Artificial Intelligence Settings
        public const int DEPTH = 1;
        public const string HEURISTIC = "HeuristiqueSimple";
        public const string ALGORITHM = "ExpectiMiniMaxSimple";
        public const int TIME_TO_MOVE = 500; // miliseconds

        // Statistical Settings
        public const bool MESURE_MOVE_TIME = false;

    }
}
