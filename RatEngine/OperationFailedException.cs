using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException()
        {

        }

        public OperationFailedException(string message)
            : base(message)
        {

        }

        public OperationFailedException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
