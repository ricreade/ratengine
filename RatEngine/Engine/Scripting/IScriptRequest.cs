using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.Engine.Command;
using RatEngine.Engine.Instruction;

namespace RatEngine.Engine.Scripting
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
        SystemInstruction Instruction { get; }

        GameCommand Command { get; }
        
    }
}
