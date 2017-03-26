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
    /// This class defines an NPC in the game - any character controlled by the
    /// application rather than a user.  This class mirrors a lot of the same
    /// behavior as the PC except that it has no need to handle command strings
    /// because it initiates all application calls directly.  Objects created from
    /// this class should be run on a task so that their actions do not interrupt
    /// the application thread.  Because this class already derives from Combatant,
    /// it cannot derive from Task (sadly).  This class must therefore expose a
    /// method to start the NPC's actions.  NPCs are all created at system startup
    /// and remain active for the life of the application.  No additional NPCs are
    /// created while the service is running.  When an NPC dies, it should set an
    /// inactive flag on itself and drop whatever inventory it is carrying as a
    /// container.  When it respawns, it obtains new inventory and removes the flag.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class NonPlayerCharacter : Creature
    {
        // Indicates whether this NPC should attack PCs on sight.
        //private bool _ishostile;

        //// Indicates whether this NPC should ever leave its starting room.  This is
        //// primarily intended for non-hostile NPCs that players would expect to always
        //// find in the same place, such as shopkeepers.  A hostile plant would also
        //// be expected to stay put.  Most hostile NPCs are designed to be mobile.
        //private bool _ismobile;

        //// Indicates whether this NPC qualifies as a trainer.  This property might be
        //// better set as a flag.
        //private bool _istrainer;

        //// Indicates whether this NPC is associated with a specific quest goal.
        //// NPCs where this property is true should pay extra attention to any command
        //// string actions receied from a PC.
        //private bool _isgoalnpc;

        // If the preceding property is true, the goals with which this NPC is related.
        // The key value is the quest + goal name.
        private ConcurrentDictionary<QuestGoal, string> _questgoals;

        //public override RatDataModelAdapter DataAdapter
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        /// <summary>
        /// Instantiates a new NonPlayerCharacter object based on the specified unique Game ID.  
        /// If this value is provided, this object will be populated based on the data source.  
        /// If this is a new record, specify null for this value.
        /// </summary>
        /// <param name="GameID">The game id of this NonPlayerCharacter object, or null if this 
        /// is a new record.</param>
        public NonPlayerCharacter() { }

        //public override bool Delete()
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool Delete(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Kill(Creature Target)
        //{
        //    base.Kill(Target);
        //}

        //public override void LoadFromAdapter(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void LoadDataRow(System.Data.DataRow Row)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// ProcessTell
        /// Intended to handle cases where a user targets an NPC with the 'tell' command.
        /// Most NPCs should have a standard response to this in the database that they
        /// can repeat until the user stops talking.  Some NPCs, however, will have specific
        /// responses under specific circumstances.  If this NPC is the initiator of a quest
        /// then they will have a different response to kicking off that quest than they
        /// would at most other times.  This method must perform these checks to determine
        /// the most appropriate response.  The Source property is a Combatant rather than
        /// a PlayerCharacter to support cases where an NPC might kick off the conversation
        /// for some reason.
        /// </summary>
        /// <param name="MessageString">[string] The message the PC sent this NPC.</param>
        /// <param name="Source">[Combatant] The source of the request.</param>
        /// <returns>[string] This NPC's response.</returns>
        public string ProcessTell(string MessageString, Creature Source)
        {
            return "";
        }

        //public override bool Save()
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool Save(RatDataModelAdapter Adapter)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Start
        /// Because this class derives from Combatant, it cannot also derive from Task, and
        /// because this class needs to operate on a task while the game is running, it needs
        /// an asynchronous method to start up the object's behavior.  This method serves this
        /// purpose.  It must handle all NPC behaviors under all circumstances, which means it
        /// needs to consider a wide variety of environmental circumstances.  The Task return
        /// value encloses void because no return value to this method is required.
        /// </summary>
        /// <returns>[Task] The Task return value to issue when this task concludes.</returns>
        //public Task Start()
        //{
        //    return null;
        //}
    }
}
