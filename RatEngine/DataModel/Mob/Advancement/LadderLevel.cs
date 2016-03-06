using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatEngine.DataModel.Mob.Advancement
{
    /// <summary>
    /// LadderLevel
    /// This class represents a PC's or NPC's current number of levels with a particular
    /// ladder.
    /// </summary>
    public class LadderLevel
    {
        // The AbilityLadder at issue.
        private AbilityLadder _ladder;

        // The current level of the ladder for this particular PC/NPC.
        private int _level;
    }
}
