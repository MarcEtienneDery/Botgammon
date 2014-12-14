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
        public const int TOTAL_GAMES = 100;

        // Artificial Intelligence Settings
        public const int DEPTH = 1;
        public const string HEURISTIC = "Default";
        public const string ALGORITHM = "ExpectiMiniMaxAlphaBeta";

        // Statistical Settings
        public const bool MESURE_MOVE_TIME = false;

    }
}
