using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    public interface IRatDataModelAdapter
    {
        void Delete();

        IDataResultSet ResultSet { get; }

        void Retrieve();

        int ReturnValue { get; }

        void Save();      
    }
}
