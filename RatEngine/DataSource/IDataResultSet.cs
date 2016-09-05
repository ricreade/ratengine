using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    public interface IDataResultSet
    {
        T GetValue<T>(string FieldName);

        bool HasField(string FieldName);

        bool IsEmpty();

        void MoveToRecord(int Position);

        int RecordCount { get; }

        int ReturnValue { get; }
    }
}
