using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Inventory;
using RatEngine.DataModel.Mob;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Questing
{
    /// <summary>
    /// Defines a specific task to be completed to accomplish a quest.
    /// </summary>
    /// <remarks>Use this class to define a specific action the player must perform to
    /// complete a stage of a quest or mission or whatever the game calls it.  The terms
    /// for completing a task are defined in the task's listeners and associated system
    /// instructions, which are arbitrarily determined in the game design.  As with terms
    /// and rewards, the sequence in which tasks are performed is entirely controlled by
    /// the system instructions, which allows the game to completely control sequencing.
    /// </remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class QuestTask : GameElement
    {
        
        public QuestTask() { }

        public QuestTask(Quest quest)
        {
            Quest = quest;
            IsComplete = false;
        }

        [DataMember]
        public virtual bool IsComplete { get; set; }
        
        [DataMember]
        public virtual Quest Quest { get; protected set; }

    }
}
