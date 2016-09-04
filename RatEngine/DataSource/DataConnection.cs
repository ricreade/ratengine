using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    /// <summary>
    /// Abstract class to represent a data source connection.
    /// </summary>
    /// <typeparam name="ParameterType">The type to use to represent a parameter.</typeparam>
    /// <typeparam name="RecordsetType">The type to use to represent a record set.</typeparam>
    public abstract class DataConnection<ParameterType>
    {
        protected string _connectionString;

        public DataConnection(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        /// <summary>
        /// Closes the connection to the data source.
        /// </summary>
        public abstract void CloseConnection();

        /// <summary>
        /// Opens the connection to the data source.
        /// </summary>
        public abstract void OpenConnection();

        /// <summary>
        /// Sends a request to the data source to retrieve a recordset.
        /// </summary>
        /// <param name="InstructionString">The instruction string to send to the data source.</param>
        /// <param name="Parameters">The list of parameters to include with the data request.</param>
        /// <returns></returns>
        public abstract IDataResultSet SendReadRequest(string InstructionString, IList<ParameterType> Parameters);

        /// <summary>
        /// Sends a request to the data source to modify data records.
        /// </summary>
        /// <param name="InstructionString">The instruction string to send to the data source.</param>
        /// <param name="Parameters">The list of parameters to include with the modification request.</param>
        /// <returns></returns>
        public abstract int SendWriteRequest(string InstructionString, IList<ParameterType> Parameters);


    }
}
