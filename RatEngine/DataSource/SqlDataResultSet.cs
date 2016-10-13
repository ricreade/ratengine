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
        private int _returnvalue;

        public int RecordCount
        {
            get
            {
                if (_data != null)
                    return _data.Rows.Count;
                return 0;
            }
        }

        public int ReturnValue
        {
            get
            {
                return _returnvalue;
            }
        }

        public SqlDataResultSet(DataTable Data)
        {
            if (Data == null)
                throw new NullReferenceException("");

            _data = Data;
            _recordnumber = 0;
        }

        public SqlDataResultSet(int ReturnValue)
        {
            _returnvalue = ReturnValue;
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
            if (IsEmpty() || Position < 0 || Position >= RecordCount)
            {
                throw new ApplicationException("Requested record out of range.");
            }
            _recordnumber = Position;
        }
    }
}
