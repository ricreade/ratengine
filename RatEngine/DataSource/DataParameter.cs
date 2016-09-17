using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    public class DataParameter
    {
        public enum DataParameterDirection
        {
            In,
            Out
        }

        private string _fieldname;
        private object _value;
        private DataParameterDirection _direction;

        public DataParameter(string FieldName, object Value):this(FieldName, Value, DataParameterDirection.In) { }

        public DataParameter(string FieldName, object Value, DataParameterDirection Direction)
        {
            _fieldname = FieldName;
            _value = Value;
        }

        public string FieldName
        {
            get { return _fieldname; }
        }

        public object Value
        {
            get { return _value; }
        }
    }
}
