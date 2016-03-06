﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatEngine.DataModel.Effects;

namespace RatEngine.DataModel.Inventory
{
    /// <summary>
    /// Inventoried
    /// This abstract class defines an object that can have an inventory.  By definition,
    /// if something can have an inventory, then it can also be affected by Abilities.
    /// This supports magic items (as Item derives from this class).  As an odd consequence,
    /// it is technically possible to have a long series of items within items. We will have
    /// to use game logic to prevent that.
    /// </summary>
    public abstract class Inventoried : Effectable
    {
        // Class Constructor
        public Inventoried()
        {
            _inv = new ConcurrentDictionary<string, Item>();
        }

        // The inventory collection contained by the derived object.  There is no direct
        // accessor to this list provided by this class.  Any derived class must provide its own
        // This is because derived classes might want to impose restrictions on what items
        // are stored here.  For example, a Combatant object might not care whether the stored
        // item is a container, but an Item object would not want to allow a container to be
        // stored inside it.
        protected ConcurrentDictionary<string, Item> _inv;

        public IEnumerable<Item> Inventory
        {
            get { return _inv.Select(item => item.Value); }
        }

        /// <summary>
        /// AddItem
        /// This method provides a means to add an item object to the dictionary collection.
        /// This method is protected because it is not automatically exposed.  The derived class
        /// must provide a method to expose this functionality.  This is to provide the derived
        /// class with an opportunity to restrict what Items may be stored or to limit the
        /// circumstances under which storage occurs.
        /// </summary>
        /// <param name="NewItem">[Item] The Item object to be stored.</param>
        protected void AddItem_Base(Item NewItem)
        {

        }

        /// <summary>
        /// GetItem
        /// Method to obtain a reference to the specified item.  The argument for this method really
        /// is a keyword.  This method must support partial matches and include logic to try to
        /// return the one best result.
        /// </summary>
        /// <param name="ItemKeyword">[string] A keyword string used to identify the item by name.</param>
        /// <returns>[Item] The item returned.  If no match was found, this method returns null.</returns>
        public Item GetItem(string ItemKeyword)
        {
            return null;
        }

        /// <summary>
        /// RemoveItem
        /// Method to remove an item from the dictionary.  This method can be used in conjunction
        /// with GetItem to obtain the correct Item reference.
        /// </summary>
        /// <param name="OldItem">[Item] The Item object to remove.</param>
        /// <returns>[Item] A reference to the removed Item.  If no item was removed, this method
        /// returns null.</returns>
        public Item RemoveItem(Item OldItem)
        {
            return null;
        }
    }
}
