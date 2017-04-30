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

        public Quest(string name)
        {
            Name = name;
        }

        [DataMember]
        public virtual List<QuestTask> QuestTasks { get; protected set; }

        [DataMember]
        public virtual QuestTemplate Template { get; set; }

        public virtual void AddQuestTask(QuestTask task)
        {
            if (task != null)
            {
                InitializeList(QuestTasks);
                QuestTasks.Add(task);
            }
        }

        public override IEnumerable<Task> ProcessCommand(GameCommand command)
        {
            List<Task> operations = new List<Task>();
            operations.AddRange(base.ProcessCommand(command));
            
            foreach(QuestTask task in QuestTasks)
            {
                operations.AddRange(task.ProcessCommand(command));
            }
            return operations;
        }

        public virtual bool RemoveQuestTask(QuestTask task)
        {
            InitializeList(QuestTasks);
            return QuestTasks.Remove(task);
        }
    }
}
