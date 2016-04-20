using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptingEngine
{
    /// <summary>
    /// An abstract class to represent the result of a script transaction.
    /// I figure a simple boolean or string is unlikely to provide all
    /// the required feedback.  I am using an abstract class rather than
    /// an interface because an interface cannot contain an enumeration
    /// and the ResultType enumeration most logically belongs here.
    /// </summary>
    public abstract class ScriptResult : IScriptResult
    {
        protected ResultType _result;
        protected string _msg;

        /// <summary>
        /// Enumeration of results that may be returned from a request action.
        /// </summary>
        public enum ResultType
        {
            /// <summary>
            /// The request has not yet been processed.
            /// </summary>
            NoResult,

            /// <summary>
            /// The request was successfully processed.
            /// </summary>
            Success,

            /// <summary>
            /// The request failed.
            /// </summary>
            Fail
        }

        /// <summary>
        /// Returns the message associated with the result.
        /// </summary>
        public string Message
        {
            get { return _msg; }
        }

        /// <summary>
        /// Returns the result type enumeration value for this request.
        /// </summary>
        public ResultType Result
        {
            get { return _result; }
        }
    }
}
