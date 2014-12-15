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
        public static int TOTAL_GAMES = Properties.Settings.Default.totalGames;

        // Artificial Intelligence Settings
        public static int DEPTH = Properties.Settings.Default.depth;
        public static string HEURISTIC = Properties.Settings.Default.heuristique;
        public static string ALGORITHM = Properties.Settings.Default.algoritm;
        public static int TIME_TO_MOVE = Properties.Settings.Default.timeToMove; // miliseconds

        // Statistical Settings
        public static bool MESURE_MOVE_TIME = Properties.Settings.Default.mesureMoveTime;

    }
}
