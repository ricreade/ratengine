using System;
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
    public class SqlDataConnection : DataConnection<SqlParameter, DataTable>
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
            _connection.Open();
        }

        public override DataTable SendReadRequest(string InstructionString, IList<SqlParameter> Parameters)
        {
            SqlDataAdapter adapter;
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand(InstructionString, _connection);

            if (Parameters != null)
            {
                foreach (SqlParameter param in Parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }
            
            using (adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(table);
            }

            return table;
        }

        public override int SendWriteRequest(string InstructionString, IList<SqlParameter> Parameters)
        {
            SqlCommand cmd = new SqlCommand(InstructionString, _connection);

            if (Parameters != null)
            {
                foreach (SqlParameter param in Parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            return cmd.ExecuteNonQuery();
        }
    }
}
