using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataSource;

namespace RatEngine.DataModel.Mob
{
    /// <summary>
    /// BodySlot
    /// Represents a specific body slot for an item.  Most items are limited to one body
    /// slot, though some have more than one optional slot (left hand or right hand for 
    /// rings, for example) or no body slot at all (such as potions).
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class BodySlot : GameElement
    {
        /// <summary>
        /// Instantiates a new BodySlot object based on the specified unique Game ID.  If this value is
        /// provided, this object will be populated based on the data source.  If this is a new
        /// record, specify null for this value.
        /// </summary>
        /// <param name="GameID">The game id of this BodySlot object, or null if this is a new record.</param>
        public BodySlot(RatDataModelAdapter Adapter) : base(Adapter) { }

        public override RatDataModelAdapter DataAdapter
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override bool Delete(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        //public override void LoadDataRow(System.Data.DataRow Row)
        //{
        //    throw new NotImplementedException();
        //}

        public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        public override bool Save(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }
    }
}
