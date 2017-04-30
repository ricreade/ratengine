using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Tagging;
using RatEngine.Engine.Command;

namespace RatEngine.DataModel.Questing
{
    /// <summary>
    /// Helper class that provides configuration rules and requirements to its
    /// associated quest task name.  
    /// </summary>
    /// <remarks>This class is used in the <see cref="QuestTemplate"/> class to
    /// define the tasks and task organization required to support the quest, 
    /// such as multiplicity, whether the task must be present in the 
    /// <see cref="Quest"/>, and other arbitrary settings as defined by the
    /// game.</remarks>
    [Serializable]
    [DataContract(IsReference = true)]
    public class QuestTaskTemplate
    {
        public QuestTaskTemplate() { }

        public QuestTaskTemplate(string taskName)
        {
            TaskName = taskName;
        }
        
        [DataMember]
        public virtual Guid QuestTaskTemplateID { get; protected set; }
        
        [DataMember]
        public virtual List<Flag> TaskFlags { get; protected set; }

        [DataMember]
        public virtual List<CommandListener> TaskListeners { get; protected set; }

        [DataMember]
        public virtual string TaskName { get; protected set; }

        public virtual QuestTask ConstructTask(Quest quest)
        {
            QuestTask task = new QuestTask(TaskName, quest);
            foreach (Flag flag in TaskFlags)
            {
                task.AddFlag(flag.Clone());
            }
            foreach (CommandListener listener in TaskListeners)
            {
                task.AddCommandListener(listener);
            }
            return task;
        }

        /// <summary>
        /// TODO: figure this out.
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public virtual bool IsConforming(List<QuestTask> tasks)
        {
            List<QuestTask> templateTasks = tasks.FindAll(
                task => task.Name.ToLower().Equals(TaskName.ToLower()));
            return false;
        }
    }
}
