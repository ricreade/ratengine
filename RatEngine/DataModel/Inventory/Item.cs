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
        // The general classification for this item.
        protected ItemType _type;

        // If this Item can be wielded as a weapon, the average damage it deals.
        protected double _avgdmg;

        // If this Item can be worn as armor, the average defense bonus it provides.
        protected double _avgdef;

        // The degree of variance that the average damage and defense values can have.
        // A weapon that deals the same damage every time has a variance of 0.  A weapon
        // that can deal between 50% and 150% of its average has variance of 0.5.
        protected double _variance;

        // The body slot that this item occupies, if any.  This property might require some
        // rethink to support items that have more than one optional body slot (or example,
        // rings can be worn on either hand, swords can be swung by either hand, etc).
        protected BodySlot _slot;

        // The minimum strength value required to wield, wear, or use this item.
        protected int _minstr;

        // The minimum dexterity value required to wield, wear, or use this item.
        protected int _mindex;

        // The minimum intelligence value required to wield, wear, or use this item.
        protected int _minint;

        // If this Item is a weapon, whether it must be wielded two-handed (i.e. without a shield)
        protected bool _istwohand;

        // If this Item can be used as an offensive weapon (which might include some potions),
        // the average likelihood that it will hit.
        protected double _accur;

        // If this item can be equipped, whether this has been done.
        protected bool _isequipped;

        // Whether this item is a container and there can make use of its inventory.
        protected bool _iscontainer;

        /// <summary>
        /// Instantiates a new Item object based on the specified unique Game ID.  If this value is
        /// provided, this object will be populated based on the data source.  If this is a new
        /// record, specify null for this value.
        /// </summary>
        /// <param name="GameID">The game id of this Item object, or null if this is a new record.</param>
        public Item(RatDataModelAdapter Adapter) : base(Adapter) { }

        [DataMember]
        public bool IsContainer
        {
            get { return _iscontainer; }
        }

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

        /// <summary>
        /// AddItem
        /// This method adds an Item to this Item's inventory, if it is a container.  This method
        /// verifies that this Item is a container and that the Item to be stored is not also
        /// a container.  This method then simply calls the Inventoried classes's AddItem_Base
        /// method.
        /// </summary>
        /// <param name="NewItem">[Item] The Item to be added to this Item's inventory.</param>
        public override void AddItem(Item NewItem)
        {
            if (IsContainer)
                base.AddItem(NewItem);
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override bool Delete(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        {
            throw new NotImplementedException();
        }

        //public override void LoadDataRow(System.Data.DataRow Row)
        //{
        //    throw new NotImplementedException();
        //}

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
