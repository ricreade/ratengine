using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    public abstract class DataConnection<ParameterType, RecordsetType>
    {
        protected string _connectionString;

        public DataConnection(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public abstract void CloseConnection();

        public abstract void OpenConnection();

        public abstract RecordsetType SendReadRequest(string InstructionString, IList<ParameterType> Parameters);

        public abstract int SendWriteRequest(string InstructionString, IList<ParameterType> Parameters);


    }
}
