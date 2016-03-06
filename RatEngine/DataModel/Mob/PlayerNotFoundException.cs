using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Mob
{
    /// <summary>
    /// Exception indicating an error condition where the expected player
    /// reference could not be found.
    /// </summary>
    class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException()
        {

        }

        public PlayerNotFoundException(string message)
            : base(message)
        {

        }

        public PlayerNotFoundException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
