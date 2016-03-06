using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Questing
{
    /// <summary>
    /// This class provides context to the QuestGoal.  Possible types include
    /// 'retrieve an item', 'talk to an npc', or just about anything else.
    /// This class provide no additional information beyond the base class.
    /// </summary>
    class QuestGoalType : GameElement
    {
        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public override void LoadDataRow(System.Data.DataRow Row)
        {
            throw new NotImplementedException();
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
