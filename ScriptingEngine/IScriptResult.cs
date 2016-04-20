using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptingEngine
{
    /// <summary>
    /// I am planning to eliminate this interface as an abstract class
    /// more effectively fulfills this role.
    /// 
    /// An interface to represent the result of a script transaction.
    /// I figure a simple boolean or string is unlikely to provide all
    /// the required feedback.
    /// </summary>
    public interface IScriptResult
    {
        /// <summary>
        /// Returns the message associated with the result.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Returns the result type enumeration value for this request.
        /// </summary>
        ScriptResult.ResultType Result { get; }
    }
}
