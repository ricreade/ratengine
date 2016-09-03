using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatEngine.DataModel.Inventory;
using RatEngine.DataModel.Mob.Advancement;
using RatEngine.DataModel.World;

namespace RatEngine.DataModel.Mob
{
    /// <summary>
    /// Combatant
    /// Describes a game object that is capable of engaging in combat and moving in the game
    /// world.  PlayerCharacter and NonPlayerCharacter derive from this class.
    /// </summary>
    public abstract class Creature : Inventoried
    {
        // The Combatant's current character level.
        protected int _lvl;

        // The current Room location in the game map.
        protected Room _location;

        // The maximum possible hit points.
        protected int _hpmax;

        // The current total hit points.
        protected int _hpcurr;

        // The maximum possible magic points.
        protected int _mpmax;

        // The current total magic points.
        protected int _mpcurr;

        // The Combatant's strength score.
        protected int _str;

        // The Combatant's dexterity score.
        protected int _dex;

        // The Combatant's intelligence score.
        protected int _int;

        // A simple attack move that all Combatant's know to support the "Kill" command.
        //private Ability _simpleattack;

        // The levels the Combatant has in the various ability ladders.  The key is the ladder name.
        protected ConcurrentDictionary<LadderLevel, string> _ladders;

        /// <summary>
        /// The base constructor for this abstract class, which carries forward the requirement to
        /// specify the combatant object's game ID.
        /// </summary>
        /// <param name="GameID">The game id of this combatant object, or null if this is a new record.</param>
        public Creature(string GameID) : base(GameID) { }

        public int CurrentHP
        {
            get { return _hpcurr; }
            set { _hpcurr = value; }
        }

        public int CurrentMP
        {
            get { return _mpcurr; }
            set { _mpcurr = value; }
        }

        public int Dexterity
        {
            get { return _dex; }
            set { _dex = value; }
        }

        public int Intelligence
        {
            get { return _int; }
            set { _int = value; }
        }

        public int Level
        {
            get { return _lvl; }
            set { _lvl = value; }
        }

        public Room Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public int MaximumHP
        {
            get { return _hpmax; }
            set { _hpmax = value; }
        }

        public int MaximumMP
        {
            get { return _mpmax; }
            set { _mpmax = value; }
        }

        public int Strength
        {
            get { return _str; }
            set { _str = value; }
        }

        // The various stat types provided in this class to support stat modifications.
        public enum StatType { HPMaximum, HPCurrent, MPMaximum, MPCurrent, Strength, Dexterity, Intelligence };

        /// <summary>
        /// AdjustStat
        /// This method alters the specified stat by the specified value.  Use a negative value to
        /// reduce a stat and a positive value to increase a stat.  Changes to Strength, Dexterity,
        /// Intelligence, HP Maximum, and MPMaximum should only be done for permanent effects.  This
        /// method should perform checks for any stat changes that would cause the Combatant to die
        /// and take appropriate action.
        /// </summary>
        /// <param name="Type">[StatType] The stat to be modifed by this method.</param>
        /// <param name="Value">[int] The magnitude of change to make to the stat.</param>
        public void AdjustStat(StatType Type, int Value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// CalculateDamage
        /// This method calculates the damage this Combatant deals with the specified ability.
        /// This method is used by the TryDefend method to determine the damage to apply if the
        /// defense fails.
        /// </summary>
        /// <param name="Ability">[Ability] The Ability dealing the damage.</param>
        /// <returns>[int] The amount of damage dealt.  This value is positive for damage points
        /// and negative for healing points.</returns>
        public int CalculateDamage(Advancement.Ability Ability)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetLadder
        /// Returns the Combatant's ladder level specified by the keyword string.  This method should
        /// perform a keyword search on the collection rather than attempting to use the keyword as
        /// given and attempt to return the one best LadderLevel.  If no match was found, this method
        /// should return null;
        /// </summary>
        /// <param name="LadderKeyword">[string] The keyword string the method will use to find a
        /// match.</param>
        /// <returns>[LadderLevel] The LadderLevel object associated with the keyword.  If no match
        /// was found, this method should return null instead.</returns>
        public LadderLevel GetLadder(string LadderKeyword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kill
        /// This method causes the combatant to repeatedly attack the target with the default Ability.
        /// To avoid slowing down the server, this method should be run on a task as it might take
        /// some time to finish.  If possible, this method should check to see whether it is running
        /// on a task and summarily abort if it is not.  This method is virtual to allow derived
        /// classes to provide their own implementation.
        /// </summary>
        /// <param name="Target">[Combatant] The Combatant targetted with the Kill command.</param>
        public virtual void Kill(Creature Target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Move
        /// Causes the Combatant to move through the specified transition from the current room to the
        /// room targeted by the Transition.  Any flag checks to determine whether the move is possible 
        /// should be done here.  If the Combatant is successfully added to the new room, this method
        /// attempts to remove this Combatant from the old room.  If that remove is successful, the method
        /// sets the Combatant location to the new room.  If the remove is not successful, this method
        /// removes the Combatant from the new room.
        /// </summary>
        /// <param name="To">[Transition] The transition through which the combatant is moving to
        /// reach the next Room.</param>
        public void Move(Transition To)
        {
            if (To.RoomTo.AddCombatant(this))
            {
                if (Location.RemoveCombatant(this))
                {
                    lock (Location)
                    {
                        Location = To.RoomTo;
                    }
                }
                else
                    To.RoomTo.RemoveCombatant(this);
            }
        }

        /// <summary>
        /// TryDefend
        /// This method determines whether this Combatant was able to ward off the use of the specified
        /// Ability by the specified Combatant.  If the defense was successful, the method returns false.
        /// Otherwise, it returns true and applies any consequences (or benefits) of the Ability to this
        /// Combatant.
        /// </summary>
        /// <param name="Ability">[Ability] The Ability against which this Combatent is defending.</param>
        /// <param name="Source">[Combatant] The Combatant who used the ability against this
        /// Combatant.</param>
        /// <returns>[bool] True if the attack was prevented, otherwise false.</returns>
        public bool TryDefend(Advancement.Ability Ability, Creature Source)
        {
            throw new NotImplementedException();
        }
    }
}
