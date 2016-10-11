using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RatEngine.DataModel.Inventory;
using RatEngine.DataModel.Mob;
using RatEngine.DataSource;

namespace RatEngine.DataModel.Questing
{
    /// <summary>
    /// This class defines a specific goal that to be completed to accomplish the
    /// quest.  Not all quest goals require major actions on the part of the player.
    /// For example, talking to a specific NPC is a perfectly acceptable goal.  Goals
    /// are created at startup along with all the quests.  As a player opens up a goal,
    /// they receive an object copy of the goal via a copy constructor so the goal can
    /// be marked complete specifically for that player.
    /// </summary>
    class QuestGoal : GameElement
    {
        // The quest associated with this goal.  The copy constructor should reference the
        // same quest object as the original goal object.
        private Quest _quest;

        // The sequential order of this goal for the quest.  Subsequent quests are
        // made available when previous ones are accomplished.  It is perfectly
        // acceptable to have two concurrent goals at the same sequence value.
        private int _seq;

        // The general category of goal.
        private QuestGoalType _type;

        // The NPC associated with this goal.  Not all goals require any association
        // with an NPC.  If an NPC is required, the tell command with the correct message
        // directed to that NPC typically accomplishes this goal.
        private NonPlayerCharacter _npc;

        // The item type associated with this goal.  Not all goals require any item
        // reference.  If an item is required, the player must acquire that item in
        // some way to accomplish this goal.
        private Inventory.Item _item;

        // If item acquisition is required to accomplish this goal, this is the number
        // of that item required.
        private int _itmqty;

        // Whether accomplishment of this goal is required to complete the quest.
        private bool _isrqd;

        // If this goal requires a tell command to an NPC, the message the player must provide
        // to satisfy the goal.
        private string _commandsyntax;

        // Whether this goal is complete for this player.
        private bool _iscomplete;

        // The specific NPC communication when requesting this goal, if this goal was
        // kicked off by an NPC.
        private string _npcrequest;

        // The specific NPC communication from the NPC when the player completes this goal,
        // If NPC communication is associated with this goal.
        private string _npccomplete;

        public override RatDataModelAdapter DataAdapter
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        // Copy Constructor to support a player-specific copy of the goal.
        public QuestGoal(QuestGoal Goal, RatDataModelAdapter Adapter) : base(Adapter)
        {

        }

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
