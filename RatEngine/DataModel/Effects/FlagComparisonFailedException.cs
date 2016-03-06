using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Effects
{
    class FlagComparisonFailedException : Exception
    {
        public FlagComparisonFailedException()
        {

        }

        public FlagComparisonFailedException(string message)
        {

        }

        public FlagComparisonFailedException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
