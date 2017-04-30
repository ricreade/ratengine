using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.Engine.Scripting
{
    /// <summary>
    /// Represents an object created from a business script.
    /// </summary>
    public interface IScriptInstance
    {
        /// <summary>
        /// Passes a script request and its associated instructions to a specific
        /// script and returns a result.
        /// </summary>
        /// <param name="request">The request to pass to the script.</param>
        /// <returns>The result of the script operation.</returns>
        IScriptResult ProcessRequest(IScriptRequest request);
    }
}
