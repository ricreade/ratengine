using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataSource;

namespace RatEngine.DataModel.Questing
{
    /// <summary>
    /// This class provides context to the QuestGoal.  Possible types include
    /// 'retrieve an item', 'talk to an npc', or just about anything else.
    /// This class provide no additional information beyond the base class.
    /// </summary>
    class QuestGoalType : GameElement
    {
        /// <summary>
        /// Instantiates a new QuestGoalType object based on the specified unique Game ID.  
        /// If this value is provided, this object will be populated based on the data source.  
        /// If this is a new record, specify null for this value.
        /// </summary>
        /// <param name="GameID">The game id of this QuestGoalType object, or null if this 
        /// is a new record.</param>
        public QuestGoalType(RatDataModelAdapter Adapter) : base(Adapter) { }

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
