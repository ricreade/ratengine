using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptingEngine
{
    /// <summary>
    /// Interface for a single specific request to be submitted to a script.
    /// </summary>
    public interface IScriptRequest
    {
        /// <summary>
        /// Returns the string instruction that provides the foundation 
        /// for this request.
        /// </summary>
        string Instruction { get; }

        /// <summary>
        /// Returns the unique game ID representing the game entity that originated the request.
        /// </summary>
        string OriginatorGameID { get; }

        /// <summary>
        /// Returns the name of the script to invoke.
        /// </summary>
        string ScriptName { get; }

        /// <summary>
        /// Returns the name of the script class to instantiate.
        /// </summary>
        string ScriptClassName { get; }


    }
}
