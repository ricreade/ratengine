using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Tagging
{
    /// <summary>
    /// The data type of the data in a flag.
    /// </summary>
    [DataContract]
    public enum FlagDataType
    {
        [EnumMember]
        String,

        [EnumMember]
        Integer,

        [EnumMember]
        Decimal,

        [EnumMember]
        Boolean
    }

    /// <summary>
    /// Identifies the data type and acceptable values for a flag.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class FlagTemplate
    {
        private FlagDataType _datatype;
        private string _valuemask;

        [DataMember]
        public FlagDataType DataType
        {
            get { return _datatype; }
        }

        [DataMember]
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
