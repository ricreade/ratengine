using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.Engine.Command
{
    public class InvalidTargetException : Exception
    {
        public InvalidTargetException()
        {

        }

        public InvalidTargetException(string message)
            : base(message)
        {

        }

        public InvalidTargetException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
