using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptingEngine
{
    /// <summary>
    /// I'm probably going to eliminate this.
    /// 
    /// Encapsulates a transaction to submit to the script engine.
    /// This might not be the way we ultimately represent a script request,
    /// but it'll serve as a stand-in for the time being.  This object should
    /// contain all the information required for the script engine to function,
    /// including the name of all scripts to be invoked in order and the
    /// required data references.  The transaction object should indicate
    /// the manner in which the transaction should be processed.  For example,
    /// if the entire transaction should be rolled back if any instruction fails.
    /// </summary>
    public abstract class ScriptTransaction
    {
        /// <summary>
        /// The script to execute.  A lot depends on how the scripts are organized.
        /// It might make more sense to roll this into the individual commands.
        /// </summary>
        protected string _scriptname;

        /// <summary>
        /// Instantiates the script transaction with the name of the script to
        /// execute.
        /// </summary>
        /// <param name="scriptName"></param>
        public ScriptTransaction(string scriptName)
        {
            _scriptname = scriptName;
        }
    }
}
