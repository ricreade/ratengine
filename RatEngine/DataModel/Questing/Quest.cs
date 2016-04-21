using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Questing
{
    /// <summary>
    /// This class defines a Quest a player can undertake to gain experience and/or
    /// accomplish a story plotline.  Quests are created as service startup and remain
    /// available for all players for the duration of the application instance.
    /// </summary>
    class Quest : GameElement
    {
        // The minimum level a player has to have before undertaking this quest.  No
        // goals should become available until the player satisfies this requirement.
        private int _minlvl;

        // The amount of gold the player will receive for completing this quest.
        private int _goldaward;

        // The amount of experience the player will receive for completing this quest.
        private int _xpaward;

        // Whether this quest should be repeatable once they have completed it.  When a
        // player would initiate the first goal in a quest, the questmanager should 
        // check whether the player has already obtained the first goal.  In any case,
        // the player should never initiate the first goal a second time if the last
        // goal is still open.
        private bool _isallowrepeat;

        // If the quest is repeatable, the number of minutes in real time the player must
        // wait before trying to obtain the first goal again.
        private int _questcooldownminutes;

        // The quest goals associated with this quest.  The key value is the goal name.
        private ConcurrentDictionary<QuestGoal, string> _questgoals;

        /// <summary>
        /// Instantiates a new Quest object based on the specified unique Game ID.  
        /// If this value is provided, this object will be populated based on the data source.  
        /// If this is a new record, specify null for this value.
        /// </summary>
        /// <param name="GameID">The game id of this Quest object, or null if this 
        /// is a new record.</param>
        public Quest(string GameID) : base(GameID) { }

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
