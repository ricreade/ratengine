using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataSource;

namespace RatEngine.DataModel.Mob.Advancement
{
    /// <summary>
    /// AbilityLadder
    /// Defines an object that represents a progression of Abilities that a PC or NPC
    /// can learn.  A PlayerCharacter can only learn abilities from a PC ladder progression.
    /// An NPC can have whatever ladders the game designers see fit.
    /// </summary>
    public class AbilityLadder : GameElement
    {
        // A collection of abilities for this ladder.  The key is the ability name.
        private ConcurrentDictionary<string, Ability> _abilities;

        // A collection of the levels for each ability in the _abilities collection.  The key is
        // the ability name.  This is intended as a parallel list.
        private ConcurrentDictionary<string, int> _levels;

        // Indicates whether this ladder is available to PlayerCharacters to learn.
        private bool _ispcladder;

        public bool IsPCLadder
        {
            get { return _ispcladder; }
        }

        /// <summary>
        /// AbilityLadder
        /// constructor will initialize components and accept DataRow of AbilityLadder to hydrate class  
        /// </summary>
        /// <params>DataRow abilityLadderRow</params>
        public AbilityLadder(RatDataModelAdapter Adapter) : base(Adapter)
        {

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
