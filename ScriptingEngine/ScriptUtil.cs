using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSScriptLibrary;

namespace ScriptingEngine
{
    /// <summary>
    /// Singleton class to provide a common interface to the scripting engine.
    /// All script transactions are processed through this class.  I am going
    /// to assume that this class will not be responsible for knowing which
    /// scripts are available (the CS Script engine takes care of loading),
    /// so I'm not going to try to provide an enumeration.  Instead, the
    /// caller will need to provide the name of the script to invoke.
    /// This class should retain no state whatsoever.  All submitted data is
    /// encapsulated in the transaction and all results as encapsulated in the
    /// result.  All operations should be completely agnostic.
    /// </summary>
    public static class ScriptUtil
    {
        /// <summary>
        /// Singleton constructor to initialize the script engine search.
        /// </summary>
        static ScriptUtil()
        {
            CSScript.GlobalSettings.AddSearchDir(@"scripts\");
        }

        /// <summary>
        /// Constructs a script request with the specified instruction and script name.
        /// This request will assume that the script name and class name are identical
        /// (except for case and file extension).
        /// </summary>
        /// <param name="instruction">The instruction for the script to process.</param>
        /// <param name="scriptName">The name of the script for the request to invoke.</param>
        /// <returns>A script request instance.</returns>
        public static IScriptRequest CreateRequest(string instruction, string scriptName)
        {
            return new ScriptRequestInst(instruction, scriptName, null);
        }

        /// <summary>
        /// Constructs a script request with the specified instruction, script name, and 
        /// class name.
        /// </summary>
        /// <param name="instruction">The instruction for the script to process.</param>
        /// <param name="scriptName">The name of the script for the request to invoke.</param>
        /// <param name="className">The name of the script class responsible for processing
        /// the request.</param>
        /// <returns>A script request instance.</returns>
        public static IScriptRequest CreateRequest(string instruction, string scriptName, string className)
        {
            return new ScriptRequestInst(instruction, scriptName, className);
        }

        /// <summary>
        /// Constructs a script result with the specified result type and message.
        /// </summary>
        /// <param name="result">The result to create.</param>
        /// <param name="message">The message to include with the result.</param>
        /// <returns>A script result instance.</returns>
        public static IScriptResult CreateResult(ScriptResult.ResultType result, string message)
        {
            return new ScriptResultInst(result, message);
        }

        /// <summary>
        /// Constructs an empty script transaction.
        /// </summary>
        /// <returns>A script transaction instance.</returns>
        public static IScriptTransaction CreateTransaction()
        {
            return new ScriptTransactionInst();
        }

        /// <summary>
        /// Constructs a script transaction instance with the specified rollback
        /// setting.
        /// </summary>
        /// <param name="rollbackOnFail">True to undo all requests if any request
        /// fails.</param>
        /// <returns>A script transaction instance.</returns>
        public static IScriptTransaction CreateTransaction(bool rollbackOnFail)
        {
            return new ScriptTransactionInst(rollbackOnFail);
        }

        /// <summary>
        /// Executes a script request.
        /// </summary>
        /// <param name="request">An script request instance with information
        /// on the results of the request execution.</param>
        /// <returns></returns>
        public static IScriptResult ExecuteRequest(IScriptRequest request){
            IScriptInstance script = GetScriptObject(
                request.ScriptName, request.ScriptClassName);
            return script.ProcessRequest(request);
        }

        /// <summary>
        /// Returns the IScriptInstance instantiated from the specified script.  
        /// This method assumes that the script name and class name are identical.
        /// The script name and class name do not need to have the same case.
        /// </summary>
        /// <param name="scriptName">The name of the script file to invoke.</param>
        /// <returns>The instantiated IScriptInstance.</returns>
        public static IScriptInstance GetScriptObject(string scriptName)
        {
            IScriptInstance inst;
            string className = scriptName.Split(new char[] {'.'})[0];
            
            inst = CSScript.Load(scriptName)
                .CreateInstance(className, true)
                .AlignToInterface<IScriptInstance>();

            return inst;
        }

        /// <summary>
        /// Returns the IScriptInstance instantiated from the specified
        /// class contained in the specified script.
        /// </summary>
        /// <param name="scriptName">The name of the script file to invoke.</param>
        /// <param name="className">The name of the class to instantiate.</param>
        /// <returns>The instantiated IScriptInstance.</returns>
        public static IScriptInstance GetScriptObject(string scriptName, string className)
        {
            IScriptInstance inst;

            inst = CSScript.Load(scriptName)
                .CreateInstance(className)
                .AlignToInterface<IScriptInstance>();

            return inst;
        }

        /// <summary>
        /// Submits a transaction to the script engine.  The transaction should
        /// contain all the commands (with script references) to execute.
        /// </summary>
        /// <param name="transaction">The transaction to execute.</param>
        /// <returns></returns>
        public static IScriptResult SubmitTransaction(ScriptTransaction transaction)
        {
            return null;        // so it'll compile.
        }

        /// <summary>
        /// Internal class representing an instance of a script result.  This is an
        /// instance class that allows the ScriptUtil to function as a script result
        /// factory.
        /// </summary>
        private class ScriptResultInst : ScriptResult
        {
            public ScriptResultInst(ResultType result, string message)
            {
                _result = result;
                _msg = message;
            }
        }

        /// <summary>
        /// Internal class representing an instance of a script request.  This is an
        /// instance class that allows the ScriptUtil to function as a script request
        /// factory.
        /// </summary>
        private class ScriptRequestInst : IScriptRequest
        {
            string _instr;
            string _script;
            string _class;

            public ScriptRequestInst(string instruction, string scriptName, string className)
            {
                _instr = instruction;
                _script = scriptName;
                _class = className;
            }

            public string Instruction
            {
                get { return _instr; }
            }

            public string ScriptName
            {
                get { return _script; }
            }

            public string ScriptClassName
            {
                get { return _class; }
            }
        }

        /// <summary>
        /// Internal class representing an instance of a script transaction.  This
        /// is an instance class that allows the ScriptUtil to function as a script
        /// transaction factory.
        /// </summary>
        private class ScriptTransactionInst : IScriptTransaction
        {
            private bool _rollback;
            private Queue<IScriptRequest> _requests;

            public ScriptTransactionInst()
            {
                _rollback = false;
                _requests = new Queue<IScriptRequest>();
            }

            public ScriptTransactionInst(bool rollbackOnFail)
            {
                _rollback = rollbackOnFail;
                _requests = new Queue<IScriptRequest>();
            }

            public bool RollbackOnFail
            {
                get { return _rollback; }
                set { _rollback = value; }
            }

            public Queue<IScriptRequest> Requests
            {
                get { return _requests; }
            }

            public bool AddRequest(IScriptRequest request)
            {
                try
                {
                    _requests.Enqueue(request);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            public IScriptResult ExecuteTransaction()
            {
                IScriptRequest request;
                IScriptResult result;
                try
                {
                    while (_requests.Count > 0)
                    {
                        request = _requests.Dequeue();
                        result = ExecuteRequest(request);
                        if (result.Result == ScriptResult.ResultType.Fail)
                            return result;
                    }
                }
                catch (Exception ex)
                {
                    return CreateResult(ScriptResult.ResultType.Fail, ex.Message);
                }
                return CreateResult(ScriptResult.ResultType.Success, "");
            }

            public IScriptRequest GetRequest()
            {
                if (_requests.Count > 0)
                    return _requests.Dequeue();
                return null;
            }

            public IScriptRequest GetRequest(bool remove)
            {
                if (remove)
                {
                    if (_requests.Count > 0)
                        return _requests.Dequeue();
                    return null;
                }
                else
                {
                    if (_requests.Count > 0)
                        return _requests.Peek();
                    return null;
                }
            }
        }
    }
}
