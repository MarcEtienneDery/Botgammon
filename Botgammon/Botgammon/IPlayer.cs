using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGammon
{
    interface IPlayer
    {
        Move GetNextMove(Position pos);
    }
}
