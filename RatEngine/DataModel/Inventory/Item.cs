using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Mob;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Inventory
{
    /// <summary>
    /// Item
    /// This class represents an item (magical or not) in the game.  The item can pretty
    /// much be anything, from flaming swords, to potions, to a pair of dice.  Anything a
    /// player (or monster) can wear, wield, drink, pick up, drop, or otherwise carry around
    /// is an Item.  Some items can also be containers for other items, which is why this
    /// class derives from Inventoried.  The IsContainer property determines whether an
    /// Item's inventory is available.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Item : Inventoried
    {
        
        /// <summary>
        /// Instantiates a new Item object based on the specified unique Game ID.  If this value is
        /// provided, this object will be populated based on the data source.  If this is a new
        /// record, specify null for this value.
        /// </summary>
        /// <param name="GameID">The game id of this Item object, or null if this is a new record.</param>
        public Item() { }

        [DataMember]
        public virtual bool IsEquipped { get; set; }

        [DataMember]
        public virtual bool IsContainer { get; protected set; }

        /// <summary>
        /// AddItem
        /// This method adds an Item to this Item's inventory, if it is a container.  This method
        /// verifies that this Item is a container and that the Item to be stored is not also
        /// a container.  This method then simply calls the Inventoried classes's AddItem_Base
        /// method.
        /// </summary>
        /// <param name="NewItem">[Item] The Item to be added to this Item's inventory.</param>
        public override void AddItem(Item item)
        {
            if (IsContainer)
                base.AddItem(item);
        }

        public override bool RemoveItem(Item item)
        {
            if (IsContainer)
                return base.RemoveItem(item);
            return false;
        }
    }
}
