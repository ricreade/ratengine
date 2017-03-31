using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Inventory;
using RatEngine.DataModel.Mob.Advancement;
using RatEngine.DataModel.World;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Mob
{
    /// <summary>
    /// Combatant
    /// Describes a game object that is capable of engaging in combat and moving in the game
    /// world.  PlayerCharacter and NonPlayerCharacter derive from this class.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class Creature : Inventoried
    {
        

        // The levels the Combatant has in the various ability ladders.  The key is the ladder name.
        protected ConcurrentDictionary<LadderLevel, string> _ladders;

        /// <summary>
        /// The base constructor for this abstract class, which carries forward the requirement to
        /// specify the combatant object's game ID.
        /// </summary>
        /// <param name="GameID">The game id of this combatant object, or null if this is a new record.</param>
        public Creature() { }

        [DataMember]
        public virtual Room Location { get; set; }

        

        /// <summary>
        /// GetLadder
        /// Returns the Combatant's ladder level specified by the keyword string.  This method should
        /// perform a keyword search on the collection rather than attempting to use the keyword as
        /// given and attempt to return the one best LadderLevel.  If no match was found, this method
        /// should return null;
        /// </summary>
        /// <param name="LadderKeyword">[string] The keyword string the method will use to find a
        /// match.</param>
        /// <returns>[LadderLevel] The LadderLevel object associated with the keyword.  If no match
        /// was found, this method should return null instead.</returns>
        public LadderLevel GetLadder(string LadderKeyword)
        {
            throw new NotImplementedException();
        }

        
    }
}
