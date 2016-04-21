using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Inventory
{
    /// <summary>
    /// ItemType
    /// Represents a specific type of item defined in the game.  These are general
    /// categories for convenient reference.
    /// </summary>
    public class ItemType : GameElement
    {
        public ItemType(string GameID) : base(GameID) { }

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
