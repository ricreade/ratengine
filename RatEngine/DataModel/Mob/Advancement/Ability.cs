using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel;
using RatEngine.DataModel.Tagging;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Mob.Advancement
{
    /// <summary>
    /// This class defines a PlayerCharacter or NonPlayerCharacter game ability
    /// that they can use.  Abilities do not have to be magical - this class is any
    /// action a PC or NPC can use to manipulate other entities in game.  Abilities
    /// are also keywords and are referenced by the Keyword class.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Ability : GameElement
    {
        
        public Ability() 
        {
            
        }
        

        //private variable containing the level of the ability for the associated ladder.
        //this provides the ability to reference its own level instead of relying on the ladder
        //object.
        private int _level;

        //private variable containing the AbilityLadder reference. This is needed since Ability
        //will be instantiated for every AbilityLadder. This variable will distinguish which Ability
        //is contained in which ladder
        private AbilityLadder _ladder;

        

        public AbilityLadder Ladder
        {
            get { return _ladder; }
        }

        

        public int Level
        {
            get { return _level; }
        }

        


        /// <summary>
        /// Ability
        /// Constructor will take the AbilityLadder and fill the _ladder variable as will as parse
        /// the dataRow and fill the Construct Fields and the variables
        /// </summary>
        /// <param name="Row">[DataRow] The database record containing data supporting this class.</param>
        public Ability(AbilityLadder ladder)
        {
            
            if (ladder != null)
                //fill the _ladder property
                _ladder = ladder;
            else
                throw new NullReferenceException("The ability of an AbilityLadder cannot be null.");

        }
        
        
    }
}
