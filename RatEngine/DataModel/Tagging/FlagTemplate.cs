using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// The data type of the data in a flag.
    /// </summary>
    public enum FlagDataType
    {
        String,

        Integer,

        Decimal,

        Boolean
    }

    /// <summary>
    /// Identifies the data type and acceptable values for a flag.
    /// </summary>
    public class FlagTemplate
    {
        private FlagDataType _datatype;
        private string _valuemask;

        public FlagDataType DataType
        {
            get { return _datatype; }
        }

        public string ValueMask
        {
            get { return _valuemask; }
        }

        public bool IsConforming(Flag Element)
        {
            throw new NotImplementedException();
        }
    }
}
