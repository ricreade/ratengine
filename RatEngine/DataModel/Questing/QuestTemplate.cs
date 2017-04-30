using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Questing
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class QuestTemplate
    {

        public QuestTemplate() { }

        public QuestTemplate(string questName)
        {
            QuestName = questName;
        }

        [DataMember]
        public virtual string QuestName { get; protected set; }

        [DataMember]
        public virtual Guid QuestTemplateID { get; protected set; }

        [DataMember]
        public virtual List<QuestTaskTemplate> QuestTaskDefinitions { get; protected set; }

        public virtual void AddTaskDefinition(QuestTaskTemplate context)
        {
            if (context != null)
            {
                if (QuestTaskDefinitions == null)
                    QuestTaskDefinitions = new List<QuestTaskTemplate>();
                if (!HasTaskDefinition(context))
                    QuestTaskDefinitions.Add(context);
            }
        }

        public virtual Quest ConstructQuest()
        {
            Quest quest = new Quest(QuestName);
            foreach(QuestTaskTemplate template in QuestTaskDefinitions)
            {
                quest.AddQuestTask(template.ConstructTask(quest));
            }
            return quest;
        }

        public virtual bool HasTaskDefinition(QuestTaskTemplate context)
        {
            if (QuestTaskDefinitions == null)
                return false;

            return QuestTaskDefinitions.Find(
                taskDef =>
                    taskDef.TaskName.ToLower().Equals(
                        context.TaskName.ToLower())) != null;
        }

        public bool IsConforming(Quest quest)
        {
            bool conforms = true;
            foreach (QuestTaskTemplate context in QuestTaskDefinitions)
            {
                conforms &= context.IsConforming(quest.QuestTasks);
            }
            return conforms;
        }

        public virtual bool RemoveFlagDefinition(QuestTaskTemplate context)
        {
            if (QuestTaskDefinitions == null)
                return false;
            return QuestTaskDefinitions.Remove(context);
        }
    }
}
