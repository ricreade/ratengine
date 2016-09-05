﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    /// <summary>
    /// Represents a connection to a SQL data source.
    /// </summary>
    public class SqlDataConnection : DataConnection<SqlParameter>
    {
        private SqlConnection _connection;
        private SqlCredential _credentials;
        public SqlDataConnection(string ConnectionString) : this(ConnectionString, null) { }

        public SqlDataConnection(string ConnectionString, SqlCredential Credentials) : base(ConnectionString)
        {
            _credentials = Credentials;
            using (_connection = new SqlConnection(ConnectionString, Credentials))
            {
                try
                {
                    OpenConnection();
                }
                catch (InvalidOperationException ex)
                {
                    throw new ApplicationException("The connection string is invalid or the connection is already open.", ex);
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException("The connection failed.", ex);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        public override void CloseConnection()
        {
            _connection.Close();
        }

        public override void OpenConnection()
        {
            try
            {
                _connection.Open();
            }
            catch (InvalidOperationException ex)
            {
                // TODO: Log event.
                throw;
            }
            catch (SqlException ex)
            {
                // TODO: Log event.
                throw;
            }
        }

        /// <summary>
        /// Invokes the specified SELECT stored procedure at the database using the specified parameters.
        /// Returns a DataTable of the retrieved records.
        /// </summary>
        /// <param name="InstructionString">The name of the stored procedure to invoke.</param>
        /// <param name="Parameters">A list of parameters associated with the request.</param>
        /// <returns>A DataTable containing the results of the request.</returns>
        public override IDataResultSet SendReadRequest(string InstructionString, IList<SqlParameter> Parameters)
        {
            SqlDataReader reader = null;
            DataTable table = null;
            SqlCommand cmd = null;

            try
            {
                OpenConnection();
            }
            catch (Exception ex)
            {
                // TODO: Log event.
                CloseConnection();
                return null;
            }

            try
            {
                cmd = new SqlCommand(InstructionString, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (Parameters != null)
                {
                    cmd.Parameters.AddRange(Parameters.ToArray());
                }
                
                reader = cmd.ExecuteReader();
                table = new DataTable();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                // TODO: Log event.
                throw;
            }
            finally
            {
                CloseConnection();
            }
            
            return new SqlDataResultSet(table);
        }

        /// <summary>
        /// Invokes the specified INSERT, UPDATE, or DELETE stored procedure at the database using the 
        /// specified parameters.  Returns an integer indicating the number of records affected (UPDATE or
        /// DELETE) or the primary key value of the new record (INSERT).
        /// </summary>
        /// <param name="InstructionString">The name of the stored procedure to invoke.</param>
        /// <param name="Parameters">A list of parameters associated with the request.</param>
        /// <returns></returns>
        public override IDataResultSet SendWriteRequest(string InstructionString, IList<SqlParameter> Parameters)
        {
            SqlCommand cmd = null;
            int returnVal;
            SqlParameter result = null;

            try
            {
                OpenConnection();
            }
            catch (Exception ex)
            {
                // TODO: Log event.
                CloseConnection();
                return new SqlDataResultSet(0);
            }

            try
            {
                result = new SqlParameter("@result", SqlDbType.Int);
                result.Direction = ParameterDirection.ReturnValue;

                cmd = new SqlCommand(InstructionString, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                
                if (Parameters != null)
                    cmd.Parameters.AddRange(Parameters.ToArray());

                cmd.Parameters.Add(result);

            }
            catch (Exception ex)
            {
                // TODO: Log event.
                return new SqlDataResultSet(0);
            }
            finally
            {
                CloseConnection();
            }

            if (int.TryParse(result.Value.ToString(), out returnVal))
            {
                return new SqlDataResultSet(returnVal);
            }
            return new SqlDataResultSet(0);
        }
    }
}
