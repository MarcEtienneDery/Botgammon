using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botgammon
{
    class Player : IPlayer
    {
        public Move GetNextMove(Position pos)
        {
            return new Move(1,2);
        }
    }
}
