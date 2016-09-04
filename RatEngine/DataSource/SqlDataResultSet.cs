using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    class SqlDataResultSet : IDataResultSet
    {
        private DataTable _data;
        private int _recordnumber;

        public SqlDataResultSet(DataTable Data)
        {
            if (Data == null)
                throw new NullReferenceException("");

            _data = Data;
            _recordnumber = 0;
        }

        public T GetValue<T>(string FieldName)
        {
            if (_data.Columns.Contains(FieldName))
            {
                return (T)_data.Rows[_recordnumber][FieldName];
            }
            throw new KeyNotFoundException("");
        }

        public bool HasField(string FieldName)
        {
            return _data.Columns.Contains(FieldName);
        }

        public bool IsEmpty()
        {
            return _data.Rows.Count == 0;
        }

        public void MoveToRecord(int Position)
        {
            throw new NotImplementedException();
        }

        public int RecordCount()
        {
            return _data.Rows.Count;
        }
    }
}
