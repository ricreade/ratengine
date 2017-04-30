using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Questing;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Mob
{
    /// <summary>
    /// Represents a character controlled by the game.
    /// </summary>
    /// <remarks>Use this class to represent a game character for the players to
    /// encounter.  This class is essentially a concrete implementation of the 
    /// Creature abstract class.  All interaction with the character is controlled
    /// through its listeners and associated system instructions.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class NonPlayerCharacter : Creature
    {
        
        /// <summary>
        /// Instantiates a new NonPlayerCharacter object based on the specified unique Game ID.  
        /// If this value is provided, this object will be populated based on the data source.  
        /// If this is a new record, specify null for this value.
        /// </summary>
        /// <param name="GameID">The game id of this NonPlayerCharacter object, or null if this 
        /// is a new record.</param>
        public NonPlayerCharacter() { }
        
    }
}
