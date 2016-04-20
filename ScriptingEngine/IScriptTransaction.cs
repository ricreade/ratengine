using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptingEngine
{
    /// <summary>
    /// Interface for a transaction to package script requests.
    /// This object should contain all the specific requests to pass to the
    /// script engine as well as the order in which they should be invoked.  
    /// The transaction object should indicate the manner in which the transaction 
    /// should be processed.  For example, if the entire transaction should be 
    /// rolled back if any instruction fails.  Requests are organized into a
    /// queue.
    /// </summary>
    public interface IScriptTransaction
    {
        /// <summary>
        /// Property to indicate whether the transaction should roll back
        /// all changes if any request fails.
        /// </summary>
        bool RollbackOnFail { get; set; }

        /// <summary>
        /// Returns all requests currently stored in the transaction.
        /// </summary>
        Queue<IScriptRequest> Requests { get; }

        /// <summary>
        /// Adds a request to the transaction.  The request is added to the end
        /// of the request queue.
        /// </summary>
        /// <param name="request">The request to add to the queue.</param>
        /// <returns>True if the request was successfully added.</returns>
        bool AddRequest(IScriptRequest request);

        /// <summary>
        /// Executes the transaction by invoking the requests it contains.
        /// </summary>
        /// <returns>The result of the transaction.</returns>
        IScriptResult ExecuteTransaction();

        /// <summary>
        /// Returns the request at the beginning of the queue.  The request is
        /// automatically removed from the queue.
        /// </summary>
        /// <returns>The request at the beginning of the queue.</returns>
        IScriptRequest GetRequest();

        /// <summary>
        /// Returns the request at the beginning of the queue.
        /// </summary>
        /// <param name="remove">True if the returned request should be removed
        /// from the queue.</param>
        /// <returns>The request at the beginning of the queue.</returns>
        IScriptRequest GetRequest(bool remove);
    }
}
