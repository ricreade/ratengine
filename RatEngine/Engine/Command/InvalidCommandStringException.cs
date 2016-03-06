using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.Engine.Command
{
    public class InvalidCommandStringException : Exception
    {
        public InvalidCommandStringException()
        {

        }

        public InvalidCommandStringException(string message)
            : base(message)
        {

        }

        public InvalidCommandStringException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
