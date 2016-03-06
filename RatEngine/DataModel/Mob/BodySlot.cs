using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Mob
{
    /// <summary>
    /// BodySlot
    /// Represents a specific body slot for an item.  Most items are limited to one body
    /// slot, though some have more than one optional slot (left hand or right hand for 
    /// rings, for example) or no body slot at all (such as potions).
    /// </summary>
    public class BodySlot : GameElement
    {
        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override void LoadDataRow(System.Data.DataRow Row)
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
