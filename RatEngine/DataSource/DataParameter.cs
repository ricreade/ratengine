using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataSource
{
    public class DataParameter
    {
        private string _fieldname;
        private object _value;

        public DataParameter(string FieldName, object Value)
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
