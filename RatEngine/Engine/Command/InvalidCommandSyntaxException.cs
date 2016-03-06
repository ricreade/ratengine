using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.Engine.Command
{
    public class InvalidCommandSyntaxException : Exception
    {
        public InvalidCommandSyntaxException()
        {

        }

        public InvalidCommandSyntaxException(string message)
            : base(message)
        {

        }

        public InvalidCommandSyntaxException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
