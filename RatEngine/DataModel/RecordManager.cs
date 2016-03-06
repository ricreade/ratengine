using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel
{
    /// <summary>
    /// Class to support all database communication for the application.
    /// </summary>
    public class RecordManager
    {
        /// <summary>
        /// CloseConnection
        /// Closes the specified sql connection.
        /// </summary>
        /// <param name="Connection">[SqlConnection] The connection to be closed.</param>
        /// <returns>[bool] True if the connection was successfully close.  Otherwise
        /// false.</returns>
        private bool CloseConnection(SqlConnection Connection)
        {
            try
            {
                Connection.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// OpenConnection
        /// Opens a connection to the database.  Returns the connection if successful,
        /// or null if not.
        /// </summary>
        /// <returns>[SqlConnection] The connection created, or null if the connection
        /// failed.</returns>
        private async Task<SqlConnection> OpenConnection()
        {
            SqlConnection c = null;
            try
            {
                c = new SqlConnection(Properties.Settings.Default.ConnString);
                await c.OpenAsync();
            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            return c;
        }

        /// <summary>
        /// SendReadRequest
        /// Invokes the specified SELECT stored procedure using the specified parameters at
        /// the database.  Returns a datatable representing the resulting query results.
        /// If an error occurs, throws standard SqlExceptions.
        /// </summary>
        /// <param name="StoredProcedure">[string] The name of the stored procedure to invoke.</param>
        /// <param name="Parameters">[IList<SqlParameter>] The list of parameters to include
        /// with the stored procedure call.</param>
        /// <returns></returns>
        public DataTable SendReadRequest(string StoredProcedure, IList<SqlParameter> Parameters)
        {
            // Try to open the connection.  The OpenConnection return operates on a Task to
            // avoid conflicts with similar requests.
            SqlConnection conn = OpenConnection().Result;
            DataTable dt = null;
            SqlCommand comm = null;
            SqlDataReader dr = null;

            // If the connection failed, don't bother trying to execute the command.  If the
            // connection worked, execute a reader asynchronously and wait for the result.
            // This might raise a SQLException.  If an exception occurs, rethrow it for the
            // calling method to handle.  Close the connection afterwards, no matter what
            // happens.
            if (conn != null)
            {
                try
                {
                    comm = new SqlCommand(StoredProcedure, conn);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddRange(Parameters.ToArray());
                    Task<SqlDataReader> t = comm.ExecuteReaderAsync();
                    t.Wait();
                    if (t.Exception != null)
                    {
                        throw new OperationFailedException("SendReadRequest failed", t.Exception);
                    }
                    dr = t.Result;
                    dt = new DataTable();
                    dt.Load(dr);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    CloseConnection(conn);
                }
            }
            else
            {
                throw new OperationFailedException("RecordManager.OpenConnection failed.");
            }

            return dt;
        }

        /// <summary>
        /// SendWriteRequest
        /// Invokes the specified INSERT, UPDATE, or DELETE stored procedure using the
        /// specified parameters at the database.  Returns an int indicating the number of
        /// records affected (UPDATE or DELETE) or the primary key value of the new record
        /// (INSERT).  If an error occurs, throws standard SqlExceptions.
        /// </summary>
        /// <param name="StoredProcedure">[string] The name of the stored procedure to invoke.</param>
        /// <param name="Parameters">[IList<SqlParameter>] The list of parameters to include
        /// with the stored procedure call.</param>
        /// <returns></returns>
        public int SendWriteRequest(string StoredProcedure, IList<SqlParameter> Parameters)
        {
            // Try to open the connection.  The OpenConnection return operates on a Task to
            // avoid conflicts with similar requests.
            SqlConnection conn = OpenConnection().Result;
            SqlCommand comm = null;
            int returnvalue = -1;
            Task<int> t = null;

            // Action queries will return an integer result.
            SqlParameter result = new SqlParameter("@result", SqlDbType.Int);
            result.Direction = ParameterDirection.ReturnValue;

            // If the connection failed, don't bother trying to execute the command.  If the
            // connection worked, execute the command asynchronously and wait for the result.
            // This might raise a SQLException.  If an exception occurs, rethrow it for the
            // calling method to handle.  Close the connection afterwards, no matter what
            // happens.
            if (conn != null)
            {
                try
                {
                    comm = new SqlCommand(StoredProcedure, conn);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddRange(Parameters.ToArray());
                    comm.Parameters.Add(result);
                    t = comm.ExecuteNonQueryAsync();
                    t.Wait();

                }
                catch (Exception ex)
                {
                    //throw;
                    return 0;
                }
                finally
                {
                    CloseConnection(conn);
                }
            }

            // Get the int return value from the return parameter.
            if (Int32.TryParse(result.Value.ToString(), out returnvalue))
                return returnvalue;
            else
                return 0;
        }
    }
}
