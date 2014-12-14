﻿using System;
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
            }
            else if (algorithm == "ExpectiMiniMaxAlphaBeta")
            {
                algo = new ExpectiMiniMaxAlphaBeta();
            }

            return algo;
        }
    }
}