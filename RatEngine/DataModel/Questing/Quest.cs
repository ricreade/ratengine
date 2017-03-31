using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataSource;
using RatEngine.Engine.Command;

namespace RatEngine.DataModel.Questing
{
    /// <summary>
    /// Defines a game quest or set of tasks.
    /// </summary>
    /// <remarks>Use the <see cref="Quest"/> structure to define any logical grouping of tasks
    /// defined by the game.  A quest must have at least one associated <see cref="QuestTask"/>
    /// because the individual tasks define the specific action(s) the player must perform to
    /// complete the quest.  The terms and rewards for a quest are determined by the game design
    /// as configured by the flags and system instructions associated with the quest and its
    /// tasks.  Quests and quest tasks both respond with command listeners for all state changes.
    /// The <see cref="GameElement"/> IsListening property is particularly important for these 
    /// classes as it effectively marks the quest or task open or closed.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class Quest : GameElement
    {
        
        public Quest() { }

        [DataMember]
        public virtual List<QuestTask> QuestTasks { get; protected set; }

        public virtual void AddQuestGoal(QuestTask goal)
        {
            if (goal != null)
            {
                InitializeList(QuestTasks);
                QuestTasks.Add(goal);
            }
        }

        public override void ProcessCommand(GameCommand command)
        {
            base.ProcessCommand(command);
            foreach(QuestTask task in QuestTasks)
            {
                task.ProcessCommand(command);
            }
        }

        public virtual bool RemoveQuestGoal(QuestTask goal)
        {
            InitializeList(QuestTasks);
            return QuestTasks.Remove(goal);
        }
    }
}
