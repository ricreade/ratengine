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
    /// Helper class that identifies the data type and acceptable values for 
    /// a <see cref="Flag"/> instance.
    /// </summary>
    /// <remarks>Every flag must have an associated template to tell the 
    /// application how to interpret its data as the <see cref="Flag"/> 
    /// instance does not otherwise provide a way to read its value.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class FlagTemplate
    {
        protected FlagTemplate() { }

        public FlagTemplate(FlagDataType dataType, string valueMask)
        {
            DataType = dataType;
            ValueMask = valueMask;
        }

        [DataMember]
        public virtual Guid FlagTemplateID { get; protected set; }

        [DataMember]
        public virtual FlagDataType DataType { get; protected set; }

        [DataMember]
        public virtual string ValueMask { get; protected set; }

        public virtual bool IsConforming(Flag flag)
        {
            //switch (DataType)
            //{
            //    case FlagDataType.Boolean:
            //        return flag.Data.Length == 1;

            //    case FlagDataType.Decimal:
            //        return Convert.ToDecimal()
            //}
            return false;
        }
    }
}
